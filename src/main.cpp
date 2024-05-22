#if __has_include(<Arduino.h>)
#include <Arduino.h>
#endif
#include "lcd_config.h"
#include "ui.hpp"
#include "serial.hpp"
#include "lcd_panel.hpp"
#ifdef M5STACK_CORE2
#include <m5core2_power.hpp>
#endif
#ifdef ARDUINO
namespace arduino {} // shut compiler up
using namespace arduino;
#else
using namespace esp_idf;
static uint32_t millis() {
    return ((uint32_t)pdTICKS_TO_MS(xTaskGetTickCount()));
}
void loop();
#endif
#ifdef M5STACK_CORE2
static m5core2_power power;
#endif
// the screen/control definitions
screen_t main_screen({LCD_WIDTH, LCD_HEIGHT},
                     lcd_transfer_buffer_size,
                     lcd_transfer_buffer1,
                     lcd_transfer_buffer2);
screen_t disconnected_screen({LCD_WIDTH, LCD_HEIGHT},
                     lcd_transfer_buffer_size,
                     lcd_transfer_buffer1,
                     lcd_transfer_buffer2);
bool connected = false;
#ifdef ARDUINO
void setup() {
    Serial.begin(115200);
#else
extern "C" void app_main() {
#endif
#ifdef M5STACK_CORE2
    power.initialize();
    power.lcd_voltage(3);
#endif
    lcd_init();
    ui_init();
    lcd_active_screen(&disconnected_screen);
   
#ifndef ARDUINO
    int count=0;
    while(1) {
        if(count++==10) {
            vTaskDelay(5);
            count = 0;
        }
        loop();
    }
#endif
}
data::simple_vector<screen_packet_t> incoming_packets;
void loop() {
    static uint32_t timeout_ts = 0;
    // timeout for disconnection detection (1 second)
    if(timeout_ts!=0 && millis()>timeout_ts+1000) {
        timeout_ts = 0;
        ui_clear_graphs();
        lcd_active_screen(&disconnected_screen);
        connected = false;
    }
    int cmd = serial_getch();
    int count;
    bool refresh = false;
    screen_packet_t packet;
    screen_entry_t* entry;
    if(cmd!=-1) {
        switch(cmd) {
            case 1:
                count = serial_getch();
                if(count==-1) {
                    break;
                }
                if(connected ==false || incoming_packets.size()!=count) {
                    refresh = true;
                }
                incoming_packets.clear(true);
                incoming_packets.reserve(count);
                entry = ui_screen_entries;
                for(int i = 0;i<count;++i) {
                    if(sizeof(screen_packet_t)!=serial_read_bytes((uint8_t*)&packet,sizeof(packet))) {
                        while(serial_getch()!=-1);
                        goto done;
                    }
                    if(!refresh && entry!=nullptr) {
                        if(0!=strcmp(entry->value_format,packet.format)) {
                            refresh = true;
                        }
                        entry=entry->next;
                    }
                    incoming_packets.push_back(packet);
                }
                if(refresh) {
                    ui_status_screen_build(incoming_packets.begin(),incoming_packets.size());
                } else {
                    ui_status_screen_update(incoming_packets.begin(),incoming_packets.size());
                }
                if(!connected) {
                    lcd_active_screen(&main_screen);
                }
                timeout_ts = millis();
                connected = true;
                break;
            default:
                while(serial_getch()!=-1);
                break;
        }
    }
done:
    lcd_update();
}
