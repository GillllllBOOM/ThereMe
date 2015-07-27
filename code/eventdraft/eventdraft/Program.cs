using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventdraft
{
    class Program
    {
        static void Main(string[] args)
        {
            // 实例化一个事件发送器
            XYManager monitor = new XYManager();
            // 实例化一个事件接收器
            XYReceiver eventReceiver = new XYReceiver(monitor);
            // 运行
            monitor.SimulateXY(10.3, 12.4);

            // 实例化一个事件发送器
            ClickManager monitor2 = new ClickManager();
            // 实例化一个事件接收器
            ClickReceiver eventReceiver2 = new ClickReceiver(monitor2);
            // 运行
            monitor2.SimulateClick("1.wav");
        }
    }
}
