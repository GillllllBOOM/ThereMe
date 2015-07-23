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
            temp.play(5);
            Thread.Sleep(300);
            temp.play(5);
            Thread.Sleep(300);
            temp.play(6); 
            Thread.Sleep(300);
            temp.play(7); 
            Thread.Sleep(300);
            temp.play(7);
            Thread.Sleep(300);
            temp.play(6); 
            Thread.Sleep(300);
            temp.play(5);
            Thread.Sleep(300);
            temp.play(4);
            Thread.Sleep(800);

            temp.play(5);
            Thread.Sleep(300);
            temp.play(5);
            Thread.Sleep(300);
            temp.play(6);
            Thread.Sleep(300);
            temp.play(7);
            Thread.Sleep(300);
            temp.play(7);
            Thread.Sleep(300);
            temp.play(6);
            Thread.Sleep(300);
            temp.play(5);
            Thread.Sleep(300);
            temp.play(4);

            Thread.Sleep(800);
            temp.play(102);
            Thread.Sleep(300);
            temp.play(102);
            Thread.Sleep(300);
            temp.play(101);
            Thread.Sleep(300);
            temp.play(102);
            Thread.Sleep(300);
            temp.play(101);
            Thread.Sleep(300);
            temp.play(103);
            Thread.Sleep(300);
            temp.play(102);
            Thread.Sleep(300);
            temp.play(100);

            Console.Read();
        }
    }
}
