//www.elegoo.com

#include <NewPing.h>
#include <Servo.h>

int Echo = A4;
int Trig = A5;

#define ENB 5
#define IN1 7
#define IN2 8
#define IN3 9
#define IN4 11
#define ENA 6

int highSpeed=200;
int lowSpeed=100;

unsigned char carSpeed = 100;
char getstr;

int MAX_DISTANCE = 400;
int Angle_Min = 45;
int Angle_Max = 135;
int Current_Angle = 90;
int defualtStep = 15;
int Step = defualtStep;

bool sweeping;


NewPing sonar(Trig, Echo, MAX_DISTANCE); // NewPing setup of pins and maximum distance.
Servo myservo;  // create servo object to control a servo

void sweep()
{
  if (!sweeping)
  {
    sweeping = true;
  }

  if (Current_Angle > Angle_Max && Step > 0)
  {
    Step = -defualtStep;
  }
  else if (Current_Angle < Angle_Min && Step < 0)
  {
    Step = defualtStep;
  }
  Current_Angle += Step;

  //delay(50);
  myservo.write(Current_Angle);

  int dis = sonar.ping_cm();
  if (dis == 0)
  {
    dis = MAX_DISTANCE;

  }

  String dist = "#" + (String)dis;

  String ang = "/" + (String)Current_Angle;
  Serial.println(ang);
  delay(50);
  Serial.println(dist);


}

void forward() {
  analogWrite(ENA, carSpeed);
  analogWrite(ENB, carSpeed);
  digitalWrite(IN1, HIGH);
  digitalWrite(IN2, LOW);
  digitalWrite(IN3, LOW);
  digitalWrite(IN4, HIGH);

}

void back() {
  analogWrite(ENA, carSpeed);
  analogWrite(ENB, carSpeed);
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, HIGH);
  digitalWrite(IN3, HIGH);
  digitalWrite(IN4, LOW);

}

void left() {
  analogWrite(ENA, highSpeed);
  analogWrite(ENB, highSpeed);
  digitalWrite(IN1, LOW);
  digitalWrite(IN2, HIGH);
  digitalWrite(IN3, LOW);
  digitalWrite(IN4, HIGH);

}

void right() {
  analogWrite(ENA, highSpeed);
  analogWrite(ENB, highSpeed);
  digitalWrite(IN1, HIGH);
  digitalWrite(IN2, LOW);
  digitalWrite(IN3, HIGH);
  digitalWrite(IN4, LOW);

}

void stop() {
  digitalWrite(ENA, LOW);
  digitalWrite(ENB, LOW);

}

void setup()
{
  Serial.begin(9600);
  Serial.setTimeout(100);

  myservo.attach(3);
  pinMode(Echo, INPUT);
  pinMode(Trig, OUTPUT);
  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT);
  pinMode(ENA, OUTPUT);
  pinMode(ENB, OUTPUT);
  stop();
}

void Listen()
{

  getstr = Serial.read();
  switch (getstr)
  {
    case 'f': forward();  break;
    case 'b': back();     break;
    case 'l': left();     break;
    case 'r': right();    break;
    case 's': stop();     break;
    default:      break;
  }

}

void loop()
{

 noInterrupts();
    while (Serial.available() > 0)
    {
     getstr = Serial.read();
    }
  
  switch (getstr)
  {
    case 'f': forward();  break;
    case 'b': back();     break;
    case 'l': left();     break;
    case 'r': right();    break;
    case 's': stop();     break;
    default:      break;
  }

  interrupts();
  sweep();

  //  while (Serial.available() > 0)
  //  {
  //    Serial.read();
  //
  //  }

}
