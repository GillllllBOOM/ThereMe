#include <iostream>
#include <vector>
#include "Point.h"
using namespace std;

int main(){
	cout << "input x,y,z" << endl;
	double ix, iy, iz;
	vector<Point>ps;
	while (cin >> ix >> iy >> iz){
		Point p(ix, iy, iz);
		ps.push_back(p);
		checkPoint cp(p);

		cp.isClick();
		if (cp.isContain() && cp.getPress())
			cout << "play sound" << endl;

		checkPoint tcp;
		if (ps.size() > 1){
			tcp(ps[ps.size() - 2]);
			tcp.isClick();
		}

		if (!cp.getPress() && tcp.getPress())
			ps.clear();
	}

	system("pasue");
	return 0;
}