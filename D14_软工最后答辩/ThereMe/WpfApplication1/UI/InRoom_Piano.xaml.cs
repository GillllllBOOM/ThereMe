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
	/// InRoom_Piano.xaml 的交互逻辑
	/// </summary>
	public partial class InRoom_Piano : Window
	{
        private HandsRecognition first = null;
        private bool sflag = false;
        private bool rflag = false;
        private Playwave playwave = null;
		public InRoom_Piano()
		{
			this.InitializeComponent();
			first = new HandsRecognition(0);
            playwave = new Playwave();
			first.RHandMove += first_RHandMove;
            first.LHandMove += first_LHandMove;
			first.HandClick += first_HandClick;
		}

        ~InRoom_Piano()
        {
            if (sflag)
            {
                first.Stop();
                sflag = false;
            }
            
        }

		void first_HandClick(int song, int duration)
		{
			//处理手按下键
            playwave.play(song);
            Set_Keypressed(song);
		}

		void first_RHandMove(double x, double y)
		{
			//处理手移动
            Set_Point1(x, y);
            
		}

        void first_LHandMove(double x, double y)
        {
            //处理手移动
            Set_Point2(x, y);

        }

		public void Set_Point1(double x, double y)
		{
            if (x < -0.10 || x > 0.11 || y < -0.12 || y > 0.12) { }
            else if (y > 0.04)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    Pointer1.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
                    Pointer1.SetValue(Canvas.TopProperty, (90d + 771d / 6));
                }));
            }
            else if (y > -0.04)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    Pointer1.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
                    Pointer1.SetValue(Canvas.TopProperty, (90d + 771d/2));
                }));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() => {
                    Pointer1.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
                    Pointer1.SetValue(Canvas.TopProperty, (90d + 771d*5/6));
                }));
            }
		}

        public void Set_Point2(double x, double y)
        {
            if (x < -0.10 || x > 0.11 || y < -0.12 || y > 0.12) { }
            else if (y > 0.04)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    Pointer2.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
                    Pointer2.SetValue(Canvas.TopProperty, (90d + 771d / 6));
                }));
            }
            else if (y > -0.04)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    Pointer2.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
                    Pointer2.SetValue(Canvas.TopProperty, (90d + 771d / 2));
                }));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() => {
                    Pointer2.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
                    Pointer2.SetValue(Canvas.TopProperty, (90d + 771d * 5 / 6));
                }));
            }
        }

		private void Set_Keypressed(int x)
		{
            this.Dispatcher.Invoke(new Action(() => {
                key_pressed.Visibility = System.Windows.Visibility.Visible;
            }));
            if(x <= 0)
            {
			
            }
			
            else if(x<8)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    key_pressed.SetValue(Canvas.LeftProperty,(170d+(x-1)%7*900d/7));
                    key_pressed.SetValue(Canvas.TopProperty,604d);
                }));
            }
            else if(x<15)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    key_pressed.SetValue(Canvas.LeftProperty, (170d + (x - 1) % 7 * 900d / 7));
                    key_pressed.SetValue(Canvas.TopProperty,347d);
                }));
            }
            else if(x<22)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    key_pressed.SetValue(Canvas.LeftProperty, (170d + (x - 1) % 7 * 900d / 7));
				    key_pressed.SetValue(Canvas.TopProperty,90d);
                }));
			}
			
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!sflag)
            {
            }
            else
            {
                first.Stop();
                sflag = false;
            }
            first.Dispose();
			InRoom room = new InRoom();
			room.Show();
			this.Close();
		}

		private void JumpToMain(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!sflag)
            {
            }
            else
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
			//key_pressed.Visibility = System.Windows.Visibility.Hidden;
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

		private void change_pos(object sender, System.Windows.RoutedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
			key_pressed.SetValue(Canvas.LeftProperty,170d);
		}
	}
}
