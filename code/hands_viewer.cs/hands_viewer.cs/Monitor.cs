using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace hands_viewer.cs
{
    class Monitor
    {
        public volatile bool stop = false;

        //public enum mode
        //{
        //    piano = 0,
        //    drum = 1
        //}

        public void Start(int mode)
        {
            HandsRecognition gr = new HandsRecognition(this);
            gr.SimplePipeline(mode);
            stop = false;
        }

        public void Stop()
        {
            Console.WriteLine("Stop monitor\n");
            stop = true;
        }
    }
}
