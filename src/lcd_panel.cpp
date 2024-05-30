#if __has_include(<Arduino.h>)
#include <Arduino.h>
#endif
#define LCD_IMPLEMENTATION
#include "lcd_init.h"
#include "lcd_panel.hpp"
#include <driver/gpio.h>
#include <driver/spi_master.h>
#include <esp_lcd_panel_io.h>
#include <esp_lcd_panel_ops.h>
#include <esp_lcd_panel_vendor.h>

#include <lvgl.h>

#ifdef ARDUINO
namespace arduino {} // shut the compiler up
using namespace arduino;
#else
namespace esp_idf {}
using namespace esp_idf;
#endif
static lv_display_t* lcd_display = nullptr;

#ifdef LCD_DMA
// tell UIX the DMA transfer is complete
static bool lcd_flush_ready(esp_lcd_panel_io_handle_t panel_io,
                            esp_lcd_panel_io_event_data_t *edata,
                            void *user_ctx) {
    if(lcd_display!=nullptr) {
        lv_disp_flush_ready(lcd_display);
    }
    return true;
}
#endif
void lcd_flush_display(lv_display_t * disp, const lv_area_t * area, uint8_t * color_p)
{
    int32_t x, y;
    /*It's a very slow but simple implementation.
     *`set_pixel` needs to be written by you to a set pixel on the screen*/
    lcd_panel_draw_bitmap(area->x1,area->y1,area->x2,area->y2,color_p);
#ifndef LCD_DMA
    lv_display_flush_ready(disp);         /* Indicate you are ready with the flushing*/
#endif
}
static uint32_t ui_ticks() {
    return millis();
}
// initialize the screen using the esp panel API
void lcd_init() {
#ifdef LCD_DMA
    lcd_transfer_buffer1 = (uint8_t*)heap_caps_malloc(lcd_transfer_buffer_size,MALLOC_CAP_DMA);
    if(lcd_transfer_buffer1==nullptr) {
        puts("Out of memory");
        while(1);
    }
    lcd_transfer_buffer2 = (uint8_t*)heap_caps_malloc(lcd_transfer_buffer_size,MALLOC_CAP_DMA);
    if(lcd_transfer_buffer2==nullptr) {
        free(lcd_transfer_buffer1);
        puts("Out of memory");
        while(1);
    }
    lcd_panel_init(lcd_transfer_buffer_size,lcd_flush_ready);
#else
    lcd_transfer_buffer2 = nullptr;
    lcd_transfer_buffer1 = (uint8_t*)heap_caps_malloc(lcd_transfer_buffer_size,MALLOC_CAP_DEFAULT);
    if(lcd_transfer_buffer1==nullptr) {
        lcd_transfer_buffer1 = (uint8_t*)heap_caps_malloc(lcd_transfer_buffer_size,MALLOC_CAP_SPIRAM);
        if(lcd_transfer_buffer1==nullptr) {
            puts("Out of memory");
            while(1);
        }
    }
    lcd_panel_init();
    lv_init();
    lv_tick_set_cb(ui_ticks);
    lcd_display = lv_display_create(LCD_WIDTH, LCD_HEIGHT);
    lv_display_set_buffers(lcd_display,lcd_transfer_buffer1,lcd_transfer_buffer2,lcd_transfer_buffer_size,LV_DISPLAY_RENDER_MODE_PARTIAL);
    lv_display_set_flush_cb(lcd_display, lcd_flush_display);
    /*Change the active screen's background color*/
    lv_obj_set_style_bg_color(lv_screen_active(), lv_color_hex(0x003a57), LV_PART_MAIN);
#endif
}

void lcd_update() {
    lv_timer_handler();
}
