#include <WiFi.h>
#include <WiFiClient.h>
#include "esp_camera.h"

const char* ssid = "ABAB";
const char* password = "T8C5P3_IP9_C7!";
const char* serverAddress = "192.168.0.33"; // Adresse IP du serveur
//T8C5P3_IP9_C7!
const int serverPort = 5960;

#define PWDN_GPIO_NUM     32
#define RESET_GPIO_NUM    -1
#define XCLK_GPIO_NUM      0
#define SIOD_GPIO_NUM     26
#define SIOC_GPIO_NUM     27

#define Y9_GPIO_NUM       35
#define Y8_GPIO_NUM       34
#define Y7_GPIO_NUM       39
#define Y6_GPIO_NUM       36
#define Y5_GPIO_NUM       21
#define Y4_GPIO_NUM       19
#define Y3_GPIO_NUM       18
#define Y2_GPIO_NUM        5
#define VSYNC_GPIO_NUM    25
#define HREF_GPIO_NUM     23
#define PCLK_GPIO_NUM     22

// 4 for flash led or 33 for normal led
#define LED_GPIO_NUM       4
//#define CAMERA_MODEL_WROVER_KIT // Has PSRAM
//#define CAMERA_MODEL_ESP_EYE // Has PSRAM
//#define CAMERA_MODEL_ESP32S3_EYE // Has PSRAM
//#define CAMERA_MODEL_M5STACK_PSRAM // Has PSRAM
//#define CAMERA_MODEL_M5STACK_V2_PSRAM // M5Camera version B Has PSRAM
//#define CAMERA_MODEL_M5STACK_WIDE // Has PSRAM
//#define CAMERA_MODEL_M5STACK_ESP32CAM // No PSRAM
//#define CAMERA_MODEL_M5STACK_UNITCAM // No PSRAM
#define CAMERA_MODEL_AI_THINKER // Has PSRAM
//#define CAMERA_MODEL_TTGO_T_JOURNAL // No PSRAM
//#define CAMERA_MODEL_XIAO_ESP32S3 // Has PSRAM
// ** Espressif Internal Boards **
//#define CAMERA_MODEL_ESP32_CAM_BOARD
//#define CAMERA_MODEL_ESP32S2_CAM_BOARD
//#define CAMERA_MODEL_ESP32S3_CAM_LCD
//#define CAMERA_MODEL_DFRobot_FireBeetle2_ESP32S3 // Has PSRAM
//#define CAMERA_MODEL_DFRobot_Romeo_ESP32S3 // Has PSRAM



void setup() 
{
 
  camera_config_t config;
  config.ledc_channel = LEDC_CHANNEL_0;
  config.ledc_timer = LEDC_TIMER_0;
  config.pin_d0 = Y2_GPIO_NUM;
  config.pin_d1 = Y3_GPIO_NUM;
  config.pin_d2 = Y4_GPIO_NUM;
  config.pin_d3 = Y5_GPIO_NUM;
  config.pin_d4 = Y6_GPIO_NUM;
  config.pin_d5 = Y7_GPIO_NUM;
  config.pin_d6 = Y8_GPIO_NUM;
  config.pin_d7 = Y9_GPIO_NUM;
  config.pin_xclk = XCLK_GPIO_NUM;
  config.pin_pclk = PCLK_GPIO_NUM;
  config.pin_vsync = VSYNC_GPIO_NUM;
  config.pin_href = HREF_GPIO_NUM;
  config.pin_sccb_sda = SIOD_GPIO_NUM;
  config.pin_sccb_scl = SIOC_GPIO_NUM;
  config.pin_pwdn = PWDN_GPIO_NUM;
  config.pin_reset = RESET_GPIO_NUM;
  config.xclk_freq_hz = 20000000;
  config.frame_size = FRAMESIZE_HD;//FRAMESIZE_UXGA;
  config.pixel_format = PIXFORMAT_JPEG; // for streaming
  //config.pixel_format = PIXFORMAT_RGB565; // for face detection/recognition
  config.grab_mode = CAMERA_GRAB_WHEN_EMPTY;
  config.fb_location = CAMERA_FB_IN_PSRAM;

  config.jpeg_quality = 4;
  config.fb_count = 1;
  config.grab_mode = CAMERA_GRAB_LATEST;

#if defined(CAMERA_MODEL_ESP_EYE)
  pinMode(13, INPUT_PULLUP);
  pinMode(14, INPUT_PULLUP);
#endif
  // Init Camera
    esp_err_t err = esp_camera_init(&config);

if (err != ESP_OK) {
    Serial.printf("Camera init failed with error 0x%x", err);
    ESP.restart();
  }
 
  sensor_t * s = esp_camera_sensor_get();  

  s->set_brightness(s, 0);     // -2 to 2
  s->set_contrast(s, 0);       // -2 to 2
  s->set_saturation(s, 0);     // -2 to 2
  s->set_special_effect(s, 0); // 0 to 6 (0 - No Effect, 1 - Negative, 2 - Grayscale, 3 - Red Tint, 4 - Green Tint, 5 - Blue Tint, 6 - Sepia)
 // s->set_whitebal(s, 1);       // 0 = disable , 1 = enable
  s->set_awb_gain(s, 1);       // 0 = disable , 1 = enable
  s->set_wb_mode(s, 0);        // 0 to 4 - if awb_gain enabled (0 - Auto, 1 - Sunny, 2 - Cloudy, 3 - Office, 4 - Home)
 // s->set_exposure_ctrl(s, 1);  // 0 = disable , 1 = enable
 // s->set_aec2(s, 0);           // 0 = disable , 1 = enable
  s->set_ae_level(s, 0);       // -2 to 2
 // s->set_aec_value(s, 300);    // 0 to 1200
//  s->set_gain_ctrl(s, 1);      // 0 = disable , 1 = enable
//  s->set_agc_gain(s, 0);       // 0 to 30
  s->set_gainceiling(s, (gainceiling_t)0);  // 0 to 6
  s->set_bpc(s, 0);            // 0 = disable , 1 = enable
  s->set_wpc(s, 1);            // 0 = disable , 1 = enable
  s->set_raw_gma(s, 1);        // 0 = disable , 1 = enable
  s->set_lenc(s, 1);           // 0 = disable , 1 = enable
  s->set_hmirror(s, 0);        // 0 = disable , 1 = enable
  s->set_vflip(s, 0);          // 0 = disable , 1 = enable
  s->set_dcw(s, 1);            // 0 = disable , 1 = enable
  s->set_colorbar(s, 0);       // 0 = disable , 1 = enable

  Serial.begin(115200);

  //Se connecter au wifi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connexion au WiFi en cours...");
  }
  Serial.println("Connecté au WiFi");
  Serial.println("Adresse IP de l'ESP32-CAM : ");
  Serial.println(WiFi.localIP());

  WiFiClient tcpClient;
  camera_fb_t * fb = esp_camera_fb_get();
  esp_camera_fb_return(fb);
  fb = esp_camera_fb_get();

  if (tcpClient.connect(serverAddress, serverPort)) 
  {
    Serial.println("Connected !");

    char buffer_return[4];
    
    buffer_return[3] = (fb->len >> 24) & 0xFF;
    buffer_return[2] = (fb->len >> 16) & 0xFF;
    buffer_return[1] = (fb->len >> 8) & 0xFF;
    buffer_return[0] = (fb->len >> 0) & 0xFF;
    size_t sent = tcpClient.write(buffer_return, 4);
    tcpClient.flush();
    Serial.println(sent);
    //sent = 0;
    delay(50);

    if(sent)
    {
      sent = 0;
      int total = fb->len;
      Serial.println(total);
      int total_to_sent = 0;

      while(total > 0)
      {
        if(total >= 1460)
        {
          
          sent = tcpClient.write(&fb->buf[total_to_sent], 1460);//udpClient.write(fb->buf, 1460);
          Serial.println(sent);
          Serial.println(total_to_sent);
          total -= sent;
          total_to_sent += sent;
          //tcpClient.flush();
        }
        else
        {
          sent = tcpClient.write(&fb->buf[total_to_sent], total);//udpClient.write(fb->buf, total);
          Serial.println(total_to_sent);
          Serial.println(sent);
          total -= sent;
          total_to_sent += sent;
          //tcpClient.flush();
        }
      }
    }
    //tcpClient.flush();
    esp_camera_fb_return(fb);
    fb = NULL;
 //   tcpClient.read();
   // tcpClient.flush();
   tcpClient.abort();
    //tcpClient.stop();
  }


  //A convertir en TCP début
  //A convertir en TCP fin

  //ssSerial.println(total_to_sent);
  //Serial.println(sent);
}

void loop() 
{

}