#include <RGBLedManager.h>
#include <unity.h>

RGBLedManager manager;
struct RGBColor {
    uint8_t red;
    uint8_t green;
    uint8_t blue;
  };
void setUp(void) {
    manager.Setup();
}

void tearDown(void) {
    // clean stuff up here
}

void test_led_red(void) {
    RGBLedManager::RGBColor rgb = manager.ConvertToRGB("FF0000");
    TEST_ASSERT_EQUAL(255, rgb.red);
    TEST_ASSERT_EQUAL(0, rgb.green);
    TEST_ASSERT_EQUAL(0, rgb.blue);
}

void test_led_green(void) {
    RGBLedManager::RGBColor rgb = manager.ConvertToRGB("00FF00");
    TEST_ASSERT_EQUAL(0, rgb.red);
    TEST_ASSERT_EQUAL(255, rgb.green);
    TEST_ASSERT_EQUAL(0, rgb.blue);
}

void test_led_blue(void) {
    RGBLedManager::RGBColor rgb = manager.ConvertToRGB("0000FF");
    TEST_ASSERT_EQUAL(0, rgb.red);
    TEST_ASSERT_EQUAL(0, rgb.green);
    TEST_ASSERT_EQUAL(255, rgb.blue);
}

void test_manuel(void) {
    RGBLedManager::RGBColor rgb = manager.ConvertToRGB("0099cc");
    manager.SetRGB(rgb);
    rgb = manager.ConvertToRGB("ffcc00"); // bleu
    delay(10000);
    manager.SetRGB(rgb); // orange
    rgb = manager.ConvertToRGB("cc0000");
    delay(10000);
    manager.SetRGB(rgb); // rouge
    delay(10000);
    TEST_ASSERT_EQUAL(0, 0);
}

int RUN_UNITY_TESTS() {
    UNITY_BEGIN();
    RUN_TEST(test_led_red);
    RUN_TEST(test_led_green);
    RUN_TEST(test_led_blue);
    RUN_TEST(test_manuel);
    UNITY_END();

    return 0;
}

#ifdef ARDUINO

#include <Arduino.h>
void setup() {
    // NOTE!!! Wait for >2 secs
    // if board doesn't support software reset via Serial.DTR/RTS
    delay(2000);

    RUN_UNITY_TESTS();
}

void loop() {
    digitalWrite(13, HIGH);
    delay(100);
    digitalWrite(13, LOW);
    delay(500);
}

#else

int main(int argc, char **argv) {
    RUN_UNITY_TESTS();
    return 0;
}

#endif