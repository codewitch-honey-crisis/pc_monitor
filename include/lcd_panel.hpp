#pragma once
#include <stddef.h>
#include <stdint.h>
#include "lcd_config.h"
#ifdef LCD_DMA
constexpr const size_t lcd_transfer_buffer_size = 32*1024;
#else
constexpr const size_t lcd_transfer_buffer_size = 64*1024;
#endif

extern uint8_t* lcd_transfer_buffer1;
extern uint8_t* lcd_transfer_buffer2;
void lcd_init();
void lcd_update();

