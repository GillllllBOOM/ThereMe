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