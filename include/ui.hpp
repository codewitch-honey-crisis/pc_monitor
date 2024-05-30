#pragma once
#include "lcd_config.h"
#include "interface.hpp"
#include "lcd_panel.hpp"
#include "circular_buffer.hpp"
#include "ui_common.hpp"
using graph_buffer_t = circular_buffer<uint8_t,100>;

void ui_init();