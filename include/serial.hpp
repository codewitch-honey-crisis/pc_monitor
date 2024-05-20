#pragma once
#include <stdint.h>
#include <stddef.h>

int serial_getch();
size_t serial_read_bytes(uint8_t* buf, size_t len);
