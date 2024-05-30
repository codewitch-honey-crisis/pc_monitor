#if __has_include(<Arduino.h>)
#include <Arduino.h>
#endif
#include "lcd_config.h"
#include "ui.hpp"
#include "serial.hpp"
#include "lcd_panel.hpp"
#ifdef ARDUINO
#include "esp_task_wdt.h"

extern bool loopTaskWDTEnabled;
xTaskHandle loopTask2Handle;
extern TaskHandle_t loopTaskHandle;
#define LOOPTASK_STACK_SIZE (64 * 1024)
namespace arduino {} // shut compiler up
using namespace arduino;
#else
using namespace esp_idf;
static uint32_t millis() {
    return ((uint32_t)pdTICKS_TO_MS(xTaskGetTickCount()));
}
void loop();
#endif
// the screen/control definitions
bool connected = false;
#ifdef ARDUINO
void setup() {
    Serial.begin(115200);
#else
extern "C" void app_main() {
#endif
    lcd_init();
    ui_init();
   
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
void loop() {
    static uint32_t timeout_ts = 0;
    // timeout for disconnection detection (1 second)
    if(timeout_ts!=0 && millis()>timeout_ts+1000) {
        timeout_ts = 0;
        connected = false;
    }
    
    lcd_update();
}
