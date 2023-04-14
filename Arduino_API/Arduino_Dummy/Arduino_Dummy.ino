#include "FuelInfo.h"
#include "Dummies.h"
#include "GPS.h"

FuelInfo fuel;
Gps gps;


void setup() {

	Serial.begin(9600);

	while (1) {
		Serial.print(fuel.getFuelInfo());
		Serial.print(",");
		Serial.print(gps.getLat(), 6);
		Serial.print(",");
		Serial.println(gps.getLon(), 6);
		delay(1000);
	}

}

void loop() {


}
