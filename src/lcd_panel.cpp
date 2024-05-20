#if __has_include(<Arduino.h>)
#include <Arduino.h>
#endif
#include <lcd_panel.hpp>
#include <gfx.hpp>
#include <uix.hpp>
#include <ft6336.hpp>
#include <driver/gpio.h>
#include <driver/spi_master.h>
#include <esp_lcd_panel_ili9342.h>
#include <esp_lcd_panel_io.h>
#include <esp_lcd_panel_ops.h>
#include <esp_lcd_panel_vendor.h>
#ifdef ARDUINO
using namespace arduino;
#else
using namespace esp_idf;
#endif
using namespace gfx;
using namespace uix;

extern ft6336<320, 280> touch;

static esp_lcd_panel_handle_t lcd_handle;
static screen_t* lcd_screen_handle=nullptr;
#ifndef USE_SINGLE_BUFFER
// use two 32KB buffers (DMA)
uint8_t lcd_transfer_buffer1[32 * 1024];
uint8_t lcd_transfer_buffer2[32 * 1024];
#else
uint8_t lcd_transfer_buffer1[64 * 1024];
uint8_t* const lcd_transfer_buffer2 = nullptr;
#endif

// tell UIX the DMA transfer is complete
static bool lcd_flush_ready(esp_lcd_panel_io_handle_t panel_io,
                            esp_lcd_panel_io_event_data_t *edata,
                            void *user_ctx) {
    if(lcd_screen_handle!=nullptr) {
        lcd_screen_handle->flush_complete();
    }
    return true;
}

// for the touch panel
static void lcd_on_touch(gfx::point16 *out_locations, size_t *in_out_locations_size, void *state) {
    static uint32_t touch_ts = 0;
    if (millis() > touch_ts + 50) {
        touch_ts = millis();
        touch.update();
    }
    // UIX supports multiple touch points. so does the FT6336 so we potentially have
    // two values
    *in_out_locations_size = 0;
    uint16_t x, y;
    if (touch.xy(&x, &y)) {
        // printf("xy: (%d,%d)\n",x,y);
        out_locations[0] = gfx::point16(x, y);
        ++*in_out_locations_size;
        if (touch.xy2(&x, &y)) {
            // printf("xy2: (%d,%d)\n",x,y);
            out_locations[1] = gfx::point16(x, y);
            ++*in_out_locations_size;
        }
    }
}

// tell the lcd panel api to transfer data via DMA
void lcd_on_flush(const gfx::rect16 &bounds, const void *bmp, void *state) {
    int x1 = bounds.x1, y1 = bounds.y1, x2 = bounds.x2 + 1, y2 = bounds.y2 + 1;
    esp_lcd_panel_draw_bitmap(lcd_handle, x1, y1, x2, y2, (void *)bmp);
}

// initialize the screen using the esp panel API
void lcd_panel_init() {
    spi_bus_config_t buscfg;
    memset(&buscfg, 0, sizeof(buscfg));
    buscfg.sclk_io_num = 18;
    buscfg.mosi_io_num = 23;
    buscfg.miso_io_num = -1;
    buscfg.quadwp_io_num = -1;
    buscfg.quadhd_io_num = -1;
    buscfg.max_transfer_sz = sizeof(lcd_transfer_buffer1) + 8;

    // Initialize the SPI bus on VSPI (SPI3)
    spi_bus_initialize(SPI3_HOST, &buscfg, SPI_DMA_CH_AUTO);

    esp_lcd_panel_io_handle_t io_handle = NULL;
    esp_lcd_panel_io_spi_config_t io_config;
    memset(&io_config, 0, sizeof(io_config));
    io_config.dc_gpio_num = 15, io_config.cs_gpio_num = 5, io_config.pclk_hz = 40 * 1000 * 1000,
    io_config.lcd_cmd_bits = 8, io_config.lcd_param_bits = 8, io_config.spi_mode = 0,
    io_config.trans_queue_depth = 10, io_config.on_color_trans_done = lcd_flush_ready;
    // Attach the LCD to the SPI bus
    esp_lcd_new_panel_io_spi((esp_lcd_spi_bus_handle_t)SPI3_HOST, &io_config, &io_handle);

    lcd_handle = NULL;
    esp_lcd_panel_dev_config_t panel_config;
    memset(&panel_config, 0, sizeof(panel_config));
    panel_config.reset_gpio_num = -1;
#if ESP_IDF_VERSION >= ESP_IDF_VERSION_VAL(5, 0, 0)
    panel_config.rgb_endian = LCD_RGB_ENDIAN_BGR;
#else
    panel_config.color_space = ESP_LCD_COLOR_SPACE_BGR;
#endif
    panel_config.bits_per_pixel = 16;

    // Initialize the LCD configuration
    if(ESP_OK!=esp_lcd_new_panel_ili9342(io_handle, &panel_config, &lcd_handle)) {
        printf("Error initializing LCD panel.\n");
        while(1);
    }

    // Reset the display
    esp_lcd_panel_reset(lcd_handle);

    // Initialize LCD panel
    esp_lcd_panel_init(lcd_handle);
    //  Swap x and y axis (Different LCD screens may need different options)
    esp_lcd_panel_swap_xy(lcd_handle, false);
    esp_lcd_panel_set_gap(lcd_handle, 0, 0);
    esp_lcd_panel_mirror(lcd_handle, false, false);
    esp_lcd_panel_invert_color(lcd_handle, true);
    // Turn on the screen
#if ESP_IDF_VERSION >= ESP_IDF_VERSION_VAL(5, 0, 0)
    esp_lcd_panel_disp_on_off(lcd_handle, false);
#else
    esp_lcd_panel_disp_off(lcd_handle, false);
#endif
    touch.initialize();
}

screen_t *lcd_active_screen() {
    return lcd_screen_handle;
}
void lcd_active_screen(screen_t* value) {
    if(lcd_screen_handle!=nullptr) {
        while(lcd_screen_handle->flushing()) {
            vTaskDelay(5);
        }
        lcd_screen_handle->on_flush_callback(nullptr);
        lcd_screen_handle->on_touch_callback(nullptr);
    }
    if(value!=nullptr) {
        value->invalidate();        
        value->on_flush_callback(lcd_on_flush);
        value->on_touch_callback(lcd_on_touch);
    }
    lcd_screen_handle = value;
}
void lcd_update() {
    if(lcd_screen_handle!=nullptr) {
        lcd_screen_handle->update();
    }
}
