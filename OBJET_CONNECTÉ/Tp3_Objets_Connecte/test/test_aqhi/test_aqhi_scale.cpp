#include <AqhiScale.h>
#include <unity.h>
AqhiScale aqhi;

void setUp(void) {
    aqhi.OfflineSetup();
}

void tearDown(void) {
    // clean stuff up here
}

void test_aqhiscale_label1(void) {
    
    TEST_ASSERT_EQUAL_STRING("1) Faible risque", aqhi.GetLabel(0).c_str());
}

void test_aqhiscale_label2(void) {
    TEST_ASSERT_EQUAL_STRING("5) Risque modéré", aqhi.GetLabel(45).c_str());
}

void test_aqhiscale_label3(void) {
    TEST_ASSERT_EQUAL_STRING("+ de 10) Risque très élevé", aqhi.GetLabel(101).c_str());
}

void test_aqhiscale_getColor1(void) {
    TEST_ASSERT_EQUAL_STRING("ff0000", aqhi.GetColor(77).c_str());
}

void test_aqhiscale_getColor2(void) {
    
    TEST_ASSERT_EQUAL_STRING("006699", aqhi.GetColor(22).c_str());
}

void RUN_UNITY_TESTS() {
    UNITY_BEGIN();
    RUN_TEST(test_aqhiscale_label1);
    RUN_TEST(test_aqhiscale_label2);
    RUN_TEST(test_aqhiscale_label3);
    RUN_TEST(test_aqhiscale_getColor1);
    RUN_TEST(test_aqhiscale_getColor2);
    UNITY_END();
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