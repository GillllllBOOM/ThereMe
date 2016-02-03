using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
	/// <summary>
	/// InRoom.xaml 的交互逻辑
	/// </summary>
    
	public partial class InRoom : Window
	{
        private HandsRecognition first = null;
        private bool sflag = false;
        private Playwave playwave = null;
		public InRoom()
		{
			this.InitializeComponent();
            
            first = new HandsRecognition(1);
            playwave = new Playwave();
            first.HandClick += first_HandClick;
            first.RHandMove += first_RHandMove;
            first.LHandMove += first_LHandMove;
			// 在此点之下插入创建对象所需的代码。
		}

         ~InRoom()
        {
            if (sflag)
            {
                first.Stop();
                sflag = false;
            }
        }
         void first_LHandMove(double x, double y)
         {

         }
         void first_RHandMove(double x, double y)
         {
            
         }
         void first_HandClick(int song, int duration)
         {
             //处理手按下键
             playwave.play(song);
         }
		private void JumpToPiano(object sender, System.Windows.RoutedEventArgs e)
		{
            if (sflag)
            {
                first.Stop();
                sflag = false;
            }
            first.Dispose();
			InRoom_Piano room = new InRoom_Piano();
			room.Show();
			this.Close();
		}

		private void JumpToMain(object sender, System.Windows.RoutedEventArgs e)
		{
            if (sflag)
            {
                first.Stop();
                sflag = false;
            }
            first.Dispose();
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}

        private void Test(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!sflag)
            {
                first.Start();
                sflag = true;
            }
            else
            {
                first.Stop();
                sflag = false;
            }
            
        }

	}
}