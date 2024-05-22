#pragma once
#include "lcd_config.h"
#include "interface.hpp"
#include "lcd_panel.hpp"
#include "circular_buffer.hpp"
using surface_t = screen_t::control_surface_type;
using label_t = uix::label<surface_t>;
using canvas_t = uix::canvas<surface_t>;
using color_t = gfx::color<typename screen_t::pixel_type>;
using color32_t = gfx::color<gfx::rgba_pixel<32>>;

using graph_buffer_t = circular_buffer<uint8_t,100>;
#ifdef LCD_DMA
constexpr const size_t lcd_transfer_buffer_size = 32*1024;
#else
constexpr const size_t lcd_transfer_buffer_size = 64*1024;
#endif
// define these in a CPP somewhere:
// using lcd_panel.cpp
#ifdef LCD_DMA
// use two 32KB buffers (DMA)
extern uint8_t lcd_transfer_buffer1[];
extern uint8_t lcd_transfer_buffer2[];
#else
extern uint8_t lcd_transfer_buffer1[];
#define lcd_transfer_buffer2 nullptr
#endif

extern screen_t main_screen;
extern screen_t disconnected_screen;
typedef struct screen_entry {
    screen_entry(uix::invalidation_tracker& parent);
    screen_entry(screen_entry&& rhs);
    screen_entry& operator=(screen_entry&& rhs);
    label_t value;
    char value_data[32];
    char value_format[16];
    canvas_t icon;
    uint8_t icon_data[16*16*2];
    canvas_t graph;
    graph_buffer_t graph_data;
    uint16_t colors[2];
    bool hsv_color;
    float value_real;
    float value_max;
    screen_entry* next;
private:
    void do_move(screen_entry& rhs);
} screen_entry_t;

extern screen_entry_t* ui_screen_entries;
void ui_init();
bool ui_status_screen_build(const screen_packet_t* packets, size_t packets_count);
bool ui_status_screen_update(const screen_packet_t* packets, size_t packets_count);
void ui_clear_graphs();