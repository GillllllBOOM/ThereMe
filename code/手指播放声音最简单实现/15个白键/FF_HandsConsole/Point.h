#include <math.h>

#define rightCymbalx 5
#define leftCymbalx 35
#define Cymbalz 19
#define midTomdrumx 15
#define leftTomdrumx 25
#define Tomdrumz 29
#define leftSnaredrumx 34
#define Snaredrumz 40
#define rightTomdrumx 6
#define areasmall 25
#define areabig 36

class Point{
private:
	double x;
	double y;
	double z;

public:
	Point(){
		x = y = z = 0;
	};
	Point(double tx, double ty, double tz){
		x = tx; y = ty; z = tz;
	};
	~Point(){};

	double getX();
	double getY();
	double getZ();

	bool operator == (const Point &tp) const;
	Point& operator = (const Point &tp);
};

//------------------------------------------------------------------------------

class checkPoint{
private:
	Point p;
	bool press;

public:
	checkPoint(){ press = false; };
	checkPoint(Point tp){
		p = tp; press = false;
	};
	~checkPoint() {};
	int isContain();
	void isClick();
	bool getPress();
	checkPoint& operator () (const Point &tp){
		*this = tp; return *this;
	}
};


//------------------------------------------------------------------------------

class checkDrum{//:	public checkPoint{
private:
	Point p;
	bool press;

public:
	checkDrum(){};// : checkPoint(){};
	//checkDrum(Point tp) :checkPoint(tp){};
	checkDrum(Point tp){
		p = tp; press = false;
	};
	~checkDrum() {};
	int isContain();
	void isClick();
	bool getPress();
	checkDrum& operator () (const Point &tp){
		*this = tp; return *this;
	}
};


//------------------------------------------------------------------------------

double Point::getX(){
	return x;
}

double Point::getY(){
	return y;
}

double Point::getZ(){
	return z;
}

bool Point::operator == (const Point &tp) const{
	if (this->x == tp.x && this->y == tp.y&&this->z == tp.z){
		return true;
	}
	return false;
}

Point& Point::operator = (const Point &tp){
	if (*this == tp)
		return *this;

	this->x = tp.x;
	this->y = tp.y;
	this->z = tp.z;

	return *this;
}

//------------------------------------------------------------------------------

int checkPoint::isContain(){
	if (p.getX() >= 0.18 || p.getX() <= -0.12 || p.getZ() <= 0.15 || p.getZ() >= 0.30)
		return 0;

	double temp = p.getX() * 100 + 12;
	int whiteX = temp / 2;

	return whiteX;
}

void checkPoint::isClick(){
	if (p.getY() < 0)
		press = true;
	else
		press = false;
}

bool checkPoint::getPress(){
	return press;
}

//------------------------------------------------------------------------------

int checkDrum::isContain(){
	if (p.getX() >= 0.20 || p.getX() <= -0.20 || p.getZ() <= 0.14 || p.getZ() >= 0.46)
		return 999;

	double tempX = p.getX() * 100 + 20;
	double tempZ = p.getZ() * 100;
	int drumNum = 0;

	if ((pow(tempX - rightCymbalx, 2) + pow(tempZ - Cymbalz, 2)) < areasmall)
		drumNum = 100;
	else if ((pow(tempX - leftCymbalx, 2) + pow(tempZ - Cymbalz, 2)) < areasmall)
		drumNum = 101;
	else if ((pow(tempX - midTomdrumx, 2) + pow(tempZ - Tomdrumz, 2)) < areasmall)
		drumNum = 102;
	else if ((pow(tempX - leftTomdrumx, 2) + pow(tempZ - Tomdrumz, 2)) < areasmall)
		drumNum = 103;
	else if ((pow(tempX - rightTomdrumx, 2) + pow(tempZ - Snaredrumz, 2)) < areabig)
		drumNum = 104;
	else if ((pow(tempX - leftSnaredrumx, 2) + pow(tempZ - Snaredrumz, 2)) < areabig)
		drumNum = 105;

	return drumNum;
}

void checkDrum::isClick(){
	if (p.getY() < 0)
		press = true;
	else
		press = false;
}

bool checkDrum::getPress(){
	return press;
}