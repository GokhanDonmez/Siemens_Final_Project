#include "Dummies.h"
#include "GPS.h"
extern Dummies dum;
double Gps::getLat() {
	return dum.generateLatValue();
}
double Gps::getLon() {
	return dum.generateLonValue();
}
