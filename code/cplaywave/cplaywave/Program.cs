using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace cplaywave
{
    class Program
    {
        static void Main(string[] args)
        {
            Playwave temp = new Playwave();
            Gettime tim = new Gettime();

            temp.play(5); 
            DateTime DateTime1 = DateTime.Now;
            Thread.Sleep(300);
           
            temp.play(6);
            DateTime DateTime2 = DateTime.Now;
            double diff = tim.ExecTimeDiff(DateTime1, DateTime2);
            temp.savehistory(5, (int)diff);
            Console.Write(diff);
            temp.savehistory(6, 1000);
            Thread.Sleep(1000);

            
            //temp.play(6); 
            //Thread.Sleep(300);
            //temp.savehistory(6, 300);
            //temp.play(7); 
            //Thread.Sleep(300);
            //temp.savehistory(7, 300);
            //temp.play(7);
            //Thread.Sleep(300);
            //temp.savehistory(7, 300);
            //temp.play(6); 
            //Thread.Sleep(300);
            //temp.savehistory(6, 300);
            //temp.play(5);
            //Thread.Sleep(300);
            //temp.savehistory(5, 300);
            //temp.play(4);
            //Thread.Sleep(800);
            //temp.savehistory(4, 300);

            
            //temp.play(102);
            //Thread.Sleep(300);
            //temp.play(102);
            //Thread.Sleep(300);
            //temp.play(101);
            //Thread.Sleep(300);
            //temp.play(102);
            //Thread.Sleep(300);
            //temp.play(101);
            //Thread.Sleep(300);
            //temp.play(103);
            //Thread.Sleep(300);
            //temp.play(102);
            //Thread.Sleep(300);
            //temp.play(100);

            List<KeyValuePair<int, int>> history = temp.gethistory();
            for (int i = 0; i < history.Count; i++)
            {
                temp.play(history[i].Key);
                Thread.Sleep(history[i].Value);
            }
                Console.Read();
        }
    }
}
