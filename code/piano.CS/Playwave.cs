using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace hands_viewer.cs
{
    class Playwave
    {
        [DllImport("winmm.dll")]
        public static extern uint mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            uint uReturnLength,
            uint hWndCallback);

        private List<KeyValuePair<int, int>> playhistory = new List<KeyValuePair<int, int>>();

        public void savehistory(int i, int t)
        {
            KeyValuePair<int, int> temp = new KeyValuePair<int, int>(i, t);
            playhistory.Add(temp);
            return;
        }

        public List<KeyValuePair<int, int>> gethistory()
        {
            return playhistory;
        }

        public void play(int i)
        {
            switch (i)
            {
                case 1:
                    {
                        mciSendString("stop 15c3.wav", null, 0, 0);
                        mciSendString("play 15c3.wav", null, 0, 0);
                        break;
                    }
                case 2:
                    {
                        mciSendString("stop 17d3.wav", null, 0, 0);
                        mciSendString("play 17d3.wav", null, 0, 0);
                        break;
                    }
                case 3:
                    {
                        mciSendString("stop 19E3.wav", null, 0, 0);
                        mciSendString("play 19E3.wav", null, 0, 0);
                        break;
                    }
                case 4:
                    {
                        mciSendString("stop 20F3.wav", null, 0, 0);
                        mciSendString("play 20F3.wav", null, 0, 0);
                        break;
                    }
                case 5:
                    {
                        mciSendString("stop 22G3.wav", null, 0, 0);
                        mciSendString("play 22G3.wav", null, 0, 0);
                        break;
                    }
                case 6:
                    {
                        mciSendString("stop 24A3.wav", null, 0, 0);
                        mciSendString("play 24A3.wav", null, 0, 0);
                        break;
                    }
                case 7:
                    {
                        mciSendString("stop 26B3.wav", null, 0, 0);
                        mciSendString("play 26B3.wav", null, 0, 0);
                        break;
                    }
                case 8:
                    {
                        mciSendString("stop 27C4.wav", null, 0, 0);
                        mciSendString("play 27C4.wav", null, 0, 0);
                        break;
                    }
                case 9:
                    {
                        mciSendString("stop 29D4.wav", null, 0, 0);
                        mciSendString("play 29D4.wav", null, 0, 0);
                        break;
                    }
                case 10:
                    {
                        mciSendString("stop 31E4.wav", null, 0, 0);
                        mciSendString("play 31E4.wav", null, 0, 0);
                        break;
                    }
                case 11:
                    {
                        mciSendString("stop 32F4.wav", null, 0, 0);
                        mciSendString("play 32F4.wav", null, 0, 0);
                        break;
                    }
                case 12:
                    {
                        mciSendString("stop 34G4.wav", null, 0, 0);
                        mciSendString("play 34G4.wav", null, 0, 0);
                        break;
                    }
                case 13:
                    {
                        mciSendString("stop 36A4.wav", null, 0, 0);
                        mciSendString("play 36A4.wav", null, 0, 0);
                        break;
                    }
                case 14:
                    {
                        mciSendString("stop 38B4.wav", null, 0, 0);
                        mciSendString("play 38B4.wav", null, 0, 0);
                        break;
                    }
                case 15:
                    {
                        mciSendString("stop 39C5.wav", null, 0, 0);
                        mciSendString("play 39C5.wav", null, 0, 0);
                        break;
                    }
                case 16:
                    {
                        mciSendString("stop 41D5.wav", null, 0, 0);
                        mciSendString("play 41D5.wav", null, 0, 0);
                        break;
                    }
                case 17:
                    {
                        mciSendString("stop 43E5.wav", null, 0, 0);
                        mciSendString("play 43E5.wav", null, 0, 0);
                        break;
                    }
                case 18:
                    {
                        mciSendString("stop 44F5.wav", null, 0, 0);
                        mciSendString("play 44F5.wav", null, 0, 0);
                        break;
                    }
                case 19:
                    {
                        mciSendString("stop 46G5.wav", null, 0, 0);
                        mciSendString("play 46G5.wav", null, 0, 0);
                        break;
                    }
                case 20:
                    {
                        mciSendString("stop 48A5.wav", null, 0, 0);
                        mciSendString("play 48A5.wav", null, 0, 0);
                        break;
                    }
                case 21:
                    {
                        mciSendString("stop 50B5.wav", null, 0, 0);
                        mciSendString("play 50B5.wav", null, 0, 0);
                        break;
                    }
                case 22:
                    {
                        mciSendString("stop 51C6.wav", null, 0, 0);
                        mciSendString("play 51C6.wav", null, 0, 0);
                        break;
                    }

                //Drum sound
                case 100:
                    {
                        mciSendString("stop open.wav", null, 0, 0);
                        mciSendString("play open.wav", null, 0, 0);
                        break;
                    }
                case 101:
                    {
                        mciSendString("stop closed.wav", null, 0, 0);
                        mciSendString("play closed.wav", null, 0, 0);
                        break;
                    }
                case 102:
                    {
                        mciSendString("stop tom2.wav", null, 0, 0);
                        mciSendString("play tom2.wav", null, 0, 0);
                        break;
                    }
                case 103:
                    {
                        mciSendString("stop tom1.wav", null, 0, 0);
                        mciSendString("play tom1.wav", null, 0, 0);
                        break;
                    }
                case 104:
                    {
                        mciSendString("stop tom3.wav", null, 0, 0);
                        mciSendString("play tom3.wav", null, 0, 0);
                        break;
                    }
                case 105:
                    {
                        mciSendString("stop snare.wav", null, 0, 0);
                        mciSendString("play snare.wav", null, 0, 0);
                        break;
                    }
                default:
                    break;
            }
            return;
        }

        public void store(string filename)
        {
            StreamWriter finmusic = File.CreateText(filename);
            List<KeyValuePair<int, int>> history = new List<KeyValuePair<int, int>>();
            history = this.gethistory();
            for (int i = 0; i < history.Count(); i++)
            {
                finmusic.WriteLine(history[i].Key);
                finmusic.WriteLine(history[i].Value);
            }
            finmusic.Flush();
            finmusic.Close();
        }
    }
}
