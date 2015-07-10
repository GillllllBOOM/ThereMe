#include <iostream>
#include <string.h>
#include <time.h>
#include <vector>
#include <utility>

using namespace std;
class PlayWave{
private:
	vector<pair<int, double> > playhistory;
public:
	PlayWave();
	void play(int i);
	void savehistory(int i, double l);
	string getwave(int i);	//辅助函数，获取wav与编号对应关系
	vector<pair<int, double>> gethistory();

};