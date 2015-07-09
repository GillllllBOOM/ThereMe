#include "playwave.h"
#include <windows.h>
#include <mmsystem.h>
#include <time.h>
#include <stdio.h>  

int main(){
	PlayWave draft;
	
	int huanle[100] = { 231, 231, 232, 234,
		234, 232, 231, 229,
		227, 227, 229, 231,
		231, 229, 229,
		231, 231, 232, 234,
		234, 232, 231, 229,
		227, 227, 229, 231,
		229, 227, 227,
		229, 229, 231, 227,
		229, 231, 232, 231, 227,
		229, 231, 232, 231, 227,
		227, 229, 222, 231,
		231, 232, 234,
		234, 232, 231, 229,
		227, 227, 229, 231,
		229, 227, 227 };

	for (int i = 0; i < 100; i++){
		draft.play(huanle[i]);
		time_t start, end;
		time(&start);
		int j = 0;
		while (j < 700){
			cout << j << endl;
			j++;
		}
		time(&end);
		draft.savehistory(huanle[i], difftime(start, end));
	}

	vector<pair<int, double> > history = draft.gethistory();
	for (int i = 0; i < history.size(); i++){
		draft.play(history[i].first);
		Sleep(history[i].second);
	}

	while (true);
}