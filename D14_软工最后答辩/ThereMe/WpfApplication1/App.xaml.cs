using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        
		[STAThread]
		public static void Main()
        {
            
            Application app = new Application();
            MainWindow mw = new MainWindow();
			//UI.Multiplayer multi = new UI.Multiplayer();
            app.Run(mw);

            
        }

        //~App()
        //{
        //    session.Dispose();
        //}


    
    }
}
