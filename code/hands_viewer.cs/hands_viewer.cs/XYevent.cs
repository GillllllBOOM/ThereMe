using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hands_viewer.cs
{
    class XYEventArgs : EventArgs
    {
        private double x;
        private double y;
        public XYEventArgs(double x, double y)
            : base()
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }
    }

    //提供事件的类
    class XYManager
    {
        //事件所用的委托(链表)
        public delegate void XYEventHandler(object sender, XYEventArgs e);
        // 将创建的委托和特定事件关联
        public event XYEventHandler NewXY;
        //通知已订阅事件的对象
        protected virtual void OnXY(XYEventArgs e)
        {
            XYEventHandler temp = NewXY; //MulticastDelegate一个委托链表
            //通知所有已订阅事件的对象
            if (temp != null)
                temp(this, e); //通过事件委托逐一回调客户端的方法
        }
        //提供一个方法，引发事件
        public void SimulateXY(double x, double y)
        {
            XYEventArgs e = new XYEventArgs(x, y);
            OnXY(e);
        }
    }

    class XYReceiver
    {
        public XYReceiver(XYManager monitor)
        {
            // 产生一个委托实例并添加到KeyInputMonitor产生的事件列表中
            monitor.NewXY += new XYManager.XYEventHandler(this.OnXYDown);
        }

        private void OnXYDown(object sender, XYEventArgs e)
        {
            // 真正的事件处理函数
            //Console.WriteLine(e.X);
            //Console.WriteLine(e.Y);
        }
    }
}

