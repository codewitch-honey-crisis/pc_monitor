#if __has_include(<Arduino.h>)
#include <Arduino.h>
#endif
#define LCD_IMPLEMENTATION
#include "lcd_init.h"
#include "lcd_panel.hpp"
#include <gfx.hpp>
#include <uix.hpp>
#include <driver/gpio.h>
#include <driver/spi_master.h>
#include <esp_lcd_panel_io.h>
#include <esp_lcd_panel_ops.h>
#include <esp_lcd_panel_vendor.h>
#ifdef ARDUINO
namespace arduino {} // shut the compiler up
using namespace arduino;
#else
namespace esp_idf {}
using namespace esp_idf;
#endif
using namespace gfx;
using namespace uix;

static screen_t* lcd_screen_handle=nullptr;
#ifdef LCD_DMA
// tell UIX the DMA transfer is complete
static bool lcd_flush_ready(esp_lcd_panel_io_handle_t panel_io,
                            esp_lcd_panel_io_event_data_t *edata,
                            void *user_ctx) {
    if(lcd_screen_handle!=nullptr) {
        lcd_screen_handle->flush_complete();
    }
    return true;
}
#endif
// tell the lcd panel api to transfer data via DMA
void lcd_on_flush(const gfx::rect16 &bounds, const void *bmp, void *state) {
    int x1 = bounds.x1, y1 = bounds.y1, x2 = bounds.x2, y2 = bounds.y2;
    lcd_panel_draw_bitmap(x1, y1, x2, y2, (void *)bmp);
#ifndef LCD_DMA
    if(lcd_screen_handle!=nullptr) {
        lcd_screen_handle->flush_complete();
    }
#endif
}

// initialize the screen using the esp panel API
void lcd_init() {
#ifdef LCD_DMA
   lcd_panel_init(32*1024,lcd_flush_ready);
#else
    lcd_panel_init();
#endif
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
    }
    if(value!=nullptr) {
        value->invalidate();        
        value->on_flush_callback(lcd_on_flush);
    }
    lcd_screen_handle = value;
}
void lcd_update() {
    if(lcd_screen_handle!=nullptr) {
        lcd_screen_handle->update();
    }
}
