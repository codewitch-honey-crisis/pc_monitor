#include "lcd_panel.hpp"
#include <stddef.h>
#include <stdint.h>
#include <lvgl.h>
#include "ui.hpp"
// our font for the UI. 
#define OPENSANS_REGULAR_IMPLEMENTATION
#include <assets/OpenSans_Regular.hpp>
#define CHAIN_SLASH_IMPLEMENTATION
#include <assets/chain_slash.hpp>

uint8_t* lcd_transfer_buffer1=nullptr;
uint8_t* lcd_transfer_buffer2=nullptr;

const lv_font_t* ui_text_font=nullptr;
static void ui_init_font() {
    lv_tiny_ttf_init();
    ui_text_font = lv_tiny_ttf_create_data(OpenSans_Regular_data,sizeof(OpenSans_Regular_data),40);
    if(ui_text_font==nullptr) {
        puts("Font initialization failure");
    }
}
void ui_init() {
    ui_init_font();
    static lv_style_t style;
    lv_style_init(&style);
    lv_style_set_text_font(&style,ui_text_font);
    lv_obj_t *label = lv_label_create( lv_screen_active() );
    lv_obj_add_style(label, &style, 0); 
    lv_label_set_text( label, "Hello from LVGL" );
    lv_obj_align( label, LV_ALIGN_CENTER, 0, 0 );

}