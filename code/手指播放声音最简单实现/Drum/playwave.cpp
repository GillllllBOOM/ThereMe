#include <windows.h>
#include <mmsystem.h>
#include "playwave.h"

using namespace std;
#pragma comment(lib,"winmm.lib") 

PlayWave::PlayWave(){

}

void PlayWave::play(int i){
	switch (i){
		/*
	case 1:{
		mciSendString(TEXT("stop ah.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play ah.wav"), NULL, 0, NULL);
		Sleep(i);
		break;
	}
	case 2:{
		mciSendString(TEXT("stop speech.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play speech.wav"), NULL, 0, NULL);
		Sleep(i);
		break;
	}
	case 100:{
		mciSendString(TEXT("stop bass.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play bass.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 101:{
		mciSendString(TEXT("stop closed.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play closed.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 102:{
		mciSendString(TEXT("stop crash.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play crash.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 103:{
		mciSendString(TEXT("stop open.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play open.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 104:{
		mciSendString(TEXT("stop rim.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play rim.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 105:{
		mciSendString(TEXT("stop snare.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play snare.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 200:{
		mciSendString(TEXT("stop 0A1.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 0A1.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 201:{
		mciSendString(TEXT("stop 1Bb1.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 1Bb1.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 202:{
		mciSendString(TEXT("stop 2B1.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 2B1.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 203:{
		mciSendString(TEXT("stop 3C2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 3C2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 204:{
		mciSendString(TEXT("stop 4Db2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 4Db2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 205:{
		mciSendString(TEXT("stop 5D2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 5D2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 206:{
		mciSendString(TEXT("stop 6Eb2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 6Eb2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 207:{
		mciSendString(TEXT("stop 7E2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 7E2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 208:{
		mciSendString(TEXT("stop 8F2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 8F2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 209:{
		mciSendString(TEXT("stop 9F#2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 9F#2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 210:{
		mciSendString(TEXT("stop 10G2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 10G2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 211:{
		mciSendString(TEXT("stop 11G#2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 11G#2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 212:{
		mciSendString(TEXT("stop 12a2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 12a2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 213:{
		mciSendString(TEXT("stop 13#a2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 13#a2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 214:{
		mciSendString(TEXT("stop 14b2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 14b2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 215:{
		mciSendString(TEXT("stop 15c3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 15c3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 216:{
		mciSendString(TEXT("stop 16Db3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 16Db3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 217:{
		mciSendString(TEXT("stop 17d3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 17d3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 218:{
		mciSendString(TEXT("stop 18Eb3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 18Eb3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 219:{
		mciSendString(TEXT("stop 19E3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 19E3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 220:{
		mciSendString(TEXT("stop 20F3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 20F3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 221:{
		mciSendString(TEXT("stop 21F#3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 21F#3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 222:{
		mciSendString(TEXT("stop 22G3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 22G3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 223:{
		mciSendString(TEXT("stop 23G#3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 23G#3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 224:{
		mciSendString(TEXT("stop 24A3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 24A3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 225:{
		mciSendString(TEXT("stop 25Bb3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 25Bb3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 226:{
		mciSendString(TEXT("stop 26B3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 26B3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 227:{
		
		mciSendString(TEXT("stop 27C4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 27C4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 228:{
		
		mciSendString(TEXT("stop 28Db4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 28Db4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 229:{
		
		mciSendString(TEXT("stop 29D4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 29D4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 230:{
		
		mciSendString(TEXT("stop 30Eb4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 30Eb4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 231:{
		
		mciSendString(TEXT("stop 31E4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 31E4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 232:{
		
		mciSendString(TEXT("stop 32F4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 32F4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 233:{
		
		mciSendString(TEXT("stop 33F#4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 33F#4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 234:{
		
		mciSendString(TEXT("stop 34G4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 34G4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 235:{
		
		mciSendString(TEXT("stop 35G#4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 35G#4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 236:{
		mciSendString(TEXT("stop 36A4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 36A4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 237:{
		mciSendString(TEXT("stop 37Bb4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 37Bb4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 238:{
		mciSendString(TEXT("stop 38B4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 38B4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 239:{
		mciSendString(TEXT("stop 39C5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 39C5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 240:{
		mciSendString(TEXT("stop 40Db5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 40Db5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 241:{
		mciSendString(TEXT("stop 41D5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 41D5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 242:{
		mciSendString(TEXT("stop 42Eb5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 42Eb5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 243:{
		mciSendString(TEXT("stop 43E5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 43E5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 244:{
		mciSendString(TEXT("stop 44F5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 44F5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 245:{
		mciSendString(TEXT("stop 45F#5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 45F#5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 246:{
		mciSendString(TEXT("stop 46G5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 46G5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 247:{
		mciSendString(TEXT("stop 47G#5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 47G#5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 248:{
		mciSendString(TEXT("stop 48A5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 48A5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 249:{
		mciSendString(TEXT("stop 49Bb5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 49Bb5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 250:{
		mciSendString(TEXT("stop 50B5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 50B5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 251:{
		mciSendString(TEXT("stop 51C6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 51C6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 252:{
		mciSendString(TEXT("stop 52Db6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 52Db6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 253:{
		mciSendString(TEXT("stop 53D6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 53D6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 254:{
		mciSendString(TEXT("stop 54Eb6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 54Eb6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 255:{
		mciSendString(TEXT("stop 55E6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 55E6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 256:{
		mciSendString(TEXT("stop 56F6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 56F6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 257:{
		mciSendString(TEXT("stop 57F#6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 57F#6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 258:{
		mciSendString(TEXT("stop 58G6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 58G6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 259:{
		mciSendString(TEXT("stop 59G#6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 59G#6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 260:{
		mciSendString(TEXT("stop 60A6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 60A6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 261:{
		mciSendString(TEXT("stop 61Bb6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 61Bb6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 262:{
		mciSendString(TEXT("stop 62B6.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 62B6.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 263:{
		mciSendString(TEXT("stop 63C7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 63C7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 264:{
		mciSendString(TEXT("stop 64Db7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 64Db7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 265:{
		mciSendString(TEXT("stop 65D7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 65D7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 266:{
		mciSendString(TEXT("stop 66Eb7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 66Eb7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 267:{
		mciSendString(TEXT("stop 67E7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 67E7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 268:{
		mciSendString(TEXT("stop 68F7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 68F7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 269:{
		mciSendString(TEXT("stop 69F#7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 69F#7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 270:{
		mciSendString(TEXT("stop 70G7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 70G7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 271:{
		mciSendString(TEXT("stop 71G#7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 71G#7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 272:{
		mciSendString(TEXT("stop 72A7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 72A7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 273:{
		mciSendString(TEXT("stop 73Bb7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 73Bb7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 274:{
		mciSendString(TEXT("stop 74B7.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 74B7.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 275:{
		mciSendString(TEXT("stop 75C8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 75C8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 276:{
		mciSendString(TEXT("stop 76Db8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 76Db8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 277:{
		mciSendString(TEXT("stop 77D8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 77D8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 278:{
		mciSendString(TEXT("stop 78Eb8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 78Eb8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 279:{
		mciSendString(TEXT("stop 79E8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 79E8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 280:{
		mciSendString(TEXT("stop 80F8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 80F8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 281:{
		mciSendString(TEXT("stop 81F#8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 81F#8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 282:{
		mciSendString(TEXT("stop 82G8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 82G8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 283:{
		mciSendString(TEXT("stop 83G#8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 83G#8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 284:{
		mciSendString(TEXT("stop 84A8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 84A8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 285:{
		mciSendString(TEXT("stop 85Bb8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 85Bb8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 286:{
		mciSendString(TEXT("stop 86B8.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 86B8.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 287:{
		mciSendString(TEXT("stop 87C9.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 87C9.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	*/
	case 0:{
		mciSendString(TEXT("stop 39C5.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 39C5.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 1:{
		mciSendString(TEXT("stop 38B4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 38B4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 2:{
		mciSendString(TEXT("stop 36A4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 36A4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 3:{
		mciSendString(TEXT("stop 34G4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 34G4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 4:{
		mciSendString(TEXT("stop 32F4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 32F4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 5:{

		mciSendString(TEXT("stop 31E4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 31E4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 6:{

		mciSendString(TEXT("stop 29D4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 29D4.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 7:{

		mciSendString(TEXT("stop 27C4.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 27C4.wav"), NULL, 0, NULL);
		////Sleep(l);
		break;
	}
	case 8:{
		mciSendString(TEXT("stop 26B3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 26B3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 9:{
		mciSendString(TEXT("stop 24A3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 24A3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 10:{
		mciSendString(TEXT("stop 22G3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 22G3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 11:{
		mciSendString(TEXT("stop 20F3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 20F3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 12:{
		mciSendString(TEXT("stop 19E3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 19E3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 13:{
		mciSendString(TEXT("stop 17d3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 17d3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 14:{
		mciSendString(TEXT("stop 15c3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play 15c3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}

	//Drum sound
	case 100:{
		mciSendString(TEXT("stop open.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play open.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 101:{
		mciSendString(TEXT("stop closed.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play closed.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 102:{
		mciSendString(TEXT("stop tom2.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play tom2.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 103:{
		mciSendString(TEXT("stop tom1.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play tom1.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 104:{
		mciSendString(TEXT("stop tom3.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play tom3.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	case 105:{
		mciSendString(TEXT("stop snare.wav"), NULL, 0, NULL);
		mciSendString(TEXT("play snare.wav"), NULL, 0, NULL);
		//Sleep(l);
		break;
	}
	default:
		break;
	}
	return;
}

void PlayWave::savehistory(int i, double l){
	pair<int, double> temp = make_pair<int&, double&>(i, l);
	playhistory.push_back(temp);
	return;
}
string PlayWave::getwave(int i){
	switch (i){
	case 100:
		return "bass.wav";
	case 101:
		return "closed.wav";
	case 102:
		return "crash.wav";
	case 103:
		return "open.wav";
	case 104:
		return "rim.wav";
	case 105:
		return "snare.wav";
	/**/
	}
}

vector<pair<int, double>> PlayWave::gethistory(){
	return playhistory;
}
