#pragma once
#include <stdint.h>
// the packet to receive
typedef struct screen_packet {
    // the command id. this is followed by a size
    constexpr static const int command = 1;
    char format[16];
    float value_max;
    uint8_t icon[16*16*2];
    uint16_t colors[2];
    bool hsv_color;
    float value;
} screen_packet_t;