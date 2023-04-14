#include "Arduino.h"

#ifndef  DUMMIES_H
#define  DUMMIES_H
class Dummies {
private:
	double totalFuel = 70;
public:

	double generateLatValue();
	double generateLonValue();
	double infoFuel();
};
#endif
