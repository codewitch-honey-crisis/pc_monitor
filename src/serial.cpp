#if __has_include(<Arduino.h>)
#include <Arduino.h>
#else
#include <stdio.h>
#include <stdint.h>
#endif
#include "serial.hpp"
int serial_getch() {
#ifdef ARDUINO
    return Serial.read();
#else
    return getchar();
#endif
}
size_t serial_read_bytes(uint8_t* buf, size_t len) {
#ifdef ARDUINO
    return Serial.readBytes((char*)buf,len);
#else
    return fread(buf,1,len,stdin);
#endif
}