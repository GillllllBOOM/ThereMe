using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventdraft
{
    class ClickEventArgs : EventArgs
    {
        string song;
        public ClickEventArgs(string s)
            : base()
        {
            this.song = s;
        }

        public string Song
        {
            get
            {
                return song;
            }
        }
    }

    //提供事件的类
    class ClickManager
    {
        //事件所用的委托(链表)
        public delegate void ClickEventHandler(object sender, ClickEventArgs e);
        // 将创建的委托和特定事件关联
        public event ClickEventHandler NewClick;
        //通知已订阅事件的对象
        protected virtual void OnClick(ClickEventArgs e)
        {
            ClickEventHandler temp = NewClick; //MulticastDelegate一个委托链表
            //通知所有已订阅事件的对象
            if (temp != null)
                temp(this, e); //通过事件委托逐一回调客户端的方法

        }
        //提供一个方法，引发事件
        public void SimulateClick(string s)
        {
            ClickEventArgs e = new ClickEventArgs(s);
            OnClick(e);
        }
    }

    class ClickReceiver
    {
        public ClickReceiver(ClickManager monitor)
        {
            // 产生一个委托实例并添加到KeyInputMonitor产生的事件列表中
            monitor.NewClick += new ClickManager.ClickEventHandler(this.OnClickDown);
        }

        private void OnClickDown(object sender, ClickEventArgs e)
        {
            // 真正的事件处理函数
            Console.WriteLine(e.Song);
        }
    }
}
