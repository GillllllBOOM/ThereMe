#include <iostream>
#include <vector>
#include <windows.h>
using namespace std;

struct Coordinate{
	int handside;
	int finger;
	float x, y, z;
};

class Handsdata{
private:
	vector<pair<Coordinate, DWORD> > handsdata;
public:
	void savedata(pair<Coordinate, DWORD> p);
	vector<pair<int, int> > click();
};