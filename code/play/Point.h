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
	bool isContain();
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

bool checkPoint::isContain(){
	bool checkx = false, checkz = false;
	if (p.getX() < 1 && p.getX() > 0){
		checkx = true;
	}

	if (p.getZ() < 1 && p.getZ() > 0){
		checkz = true;
	}

	return (checkz && checkz);
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