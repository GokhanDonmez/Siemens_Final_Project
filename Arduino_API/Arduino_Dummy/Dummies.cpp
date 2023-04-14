#include "Dummies.h"



double Dummies::generateLatValue() {
	return random(370420, 370430) / 10000.00;
}

double Dummies::generateLonValue() {
	return random(353011, 353100) / 10000.00;
}

double Dummies::infoFuel() {
	totalFuel -= random(0, 100) / 1000.0;
	return totalFuel;

}
