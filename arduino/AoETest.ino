// Joystick 1 (Player 1) 
const int p1UpPin = 12;    // Player 1 UP (D12)
const int p1DownPin = 4;   // Player 1 DOWN (D4)
const int p1RightPin = A0; // Player 1 RIGHT (A0) - unused
const int p1LeftPin = A3;  // Player 1 LEFT (A3) - unused

// Joystick 2 (Player 2) 
const int p2UpPin = 8;     // Player 2 UP (D8)
const int p2DownPin = 2;   // Player 2 DOWN (D2)
const int p2RightPin = A1; // Player 2 RIGHT (A1) - unused
const int p2LeftPin = A5;  // Player 2 LEFT (A5) - unused

// Joystick 3 (Menu/Selection) 
const int menuVrxPin = A2; // Joystick 3 VRx
const int menuVryPin = A4; // Joystick 3 VRy
const int menuSwPin = 7;   // Joystick 3 button (SW)

void setup() {
  // Set joystick 1 pins
  pinMode(p1UpPin, INPUT_PULLUP);
  pinMode(p1DownPin, INPUT_PULLUP);
  pinMode(p1RightPin, INPUT_PULLUP);
  pinMode(p1LeftPin, INPUT_PULLUP); 

  // Set joystick 2 pins
  pinMode(p2UpPin, INPUT_PULLUP);
  pinMode(p2DownPin, INPUT_PULLUP);
  pinMode(p2RightPin, INPUT_PULLUP); 
  pinMode(p2LeftPin, INPUT_PULLUP);  

  // Set joystick 3 pins
  pinMode(menuSwPin, INPUT_PULLUP);  // Button as digital input

  pinMode(menuVryPin, INPUT);
  pinMode(menuVrxPin, INPUT);


  Serial.begin(115200); // Start Serial communication
}

void loop() {
  // Read Player 1 UP and DOWN
  int p1UpState = digitalRead(p1UpPin);
  int p1DownState = digitalRead(p1DownPin);

  // Read Player 2 UP and DOWN
  int p2UpState = digitalRead(p2UpPin);
  int p2DownState = digitalRead(p2DownPin);

  // Read Menu Joystick VRy (vertical) and SW (button)
  int menuVryValue = analogRead(menuVryPin); 
  int menuSwState = digitalRead(menuSwPin);  
  int menuVrxValue = analogRead(menuVrxPin); 

  // Print the readings
  Serial.print(p1UpState);
  Serial.print(",");
  Serial.print(p1DownState);
  
  Serial.print(",");
  Serial.print(p2UpState);
  Serial.print(",");
  Serial.print(p2DownState);

  Serial.print(",");
  Serial.print(menuVryValue);
  Serial.print(",");
  Serial.println(menuSwState);

  delay(25);
}
