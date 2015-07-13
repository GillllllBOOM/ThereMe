#include <Windows.h>
#include <stdio.h>
#include <mmsystem.h>
#pragma comment(lib, "winmm.lib")

using namespace std;

void help(){
	printf("input 1 for record audio\n2 for play it\n");
}

int main(){
	help();
	int order;
	scanf_s("%d", &order);
	mciSendString(L"set wave bitpersample 8", 0, 0, 0);	
	mciSendString(L"set wave samplespersec 11025", 0, 0, 0);	//11025HZ
	mciSendString(L"set wave channels 2", 0, 0, 0);	//2����
	mciSendString(L"set wave format tag pcm", 0, 0, 0);	//��ʽ

	switch (order){
	case 1:{
		mciSendString(L"close movie", 0, 0, 0);
		mciSendString(L"open new type WAVEAudio alias movie", 0, 0, 0);
		mciSendString(L"record movie", 0, 0, 0);
		system("pause");
		mciSendString(L"stop movie", 0, 0, 0);	//ֹͣ
		mciSendString(L"save movie C:/Users/liu/Desktop/123.wav", 0, 0, 0);	//����
		mciSendString(L"close movie", 0, 0, 0);
		break;
	}
	case 2:{
		mciSendString(L"close movie", 0, 0, 0);
		mciSendString(L"play C:/Users/liu/Desktop/123.wav", 0, 0, 0);
		system("pause");
		break;
	}
	default:
	{
		help();
		break;
	}
	}

	return 0;
}