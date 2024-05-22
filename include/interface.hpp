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
    inline screen_packet() {}
    inline screen_packet(const screen_packet& rhs) {
        do_copy(rhs);
    }
    inline screen_packet& operator=(const screen_packet& rhs) {
        do_copy(rhs);
        return *this;
    }
    inline screen_packet(screen_packet&& rhs) {
        do_copy(rhs);
    }
    inline screen_packet& operator=(screen_packet&& rhs) {
        do_copy(rhs);
        return *this;
    }
    private:
    inline void do_copy(const screen_packet& rhs) {
        memcpy(format,rhs.format,sizeof(format));
        value_max = rhs.value_max;
        memcpy(icon,rhs.icon,sizeof(icon));
        memcpy(colors,rhs.colors,sizeof(colors));
        hsv_color = rhs.hsv_color;
        value = rhs.value;
    }
} screen_packet_t;