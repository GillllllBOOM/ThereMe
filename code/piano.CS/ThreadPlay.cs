using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace hands_viewer.cs
{
    class ThreadPlay
    {
        public int play(string foo)
        {
            try
            {
                StreamReader sr = new StreamReader(foo, Encoding.Default);
                String line;
                List<int> wave = new List<int>();
                while ((line = sr.ReadLine()) != null)
                {
                    wave.Add(int.Parse(line));
                }
                if (wave.Count % 2 != 0)
                    return -2;
                Thread t = new Thread(new ParameterizedThreadStart(cplay));
                t.Start(wave);

            }
            catch
            {
                Console.WriteLine("音频不存在");
                return -1;
            }
            return 0;
        }

        public static void cplay(object t)
        {
            List<int> temp = t as List<int>;
            Playwave p = new Playwave();
            for (int i = 0; i < temp.Count; i += 2)
            {
                p.play(temp[i]);
                Thread.Sleep(temp[i + 1]);
            }
        }
    }
}