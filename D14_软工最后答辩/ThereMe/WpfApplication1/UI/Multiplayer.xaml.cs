using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace WpfApplication1.UI
{
	/// <summary>
	/// Multiplayer.xaml 的交互逻辑
	/// </summary>
	public partial class Multiplayer : Window
	{
		private Playwave playwave;
		private Library.Client client;
		private HandsRecognition first = null;
		private bool sflag = false;

		public Multiplayer()
		{
			InitializeComponent();
			playwave = new Playwave();
			client = new Library.Client();
			client.ClientConnectedEvent += client_ClientConnectedEvent;
			client.ClientDisconnectedEvent += client_ClientDisconnectedEvent;
			client.ClientKeyPlayedEvent += client_ClientKeyPlayedEvent;
			//client_ClientConnectedEvent("aaaa");
		}

		private void StopRecognition()
		{
			if (sflag)
			{
				first.Stop();
				sflag = false;
			}
		}

		void client_ClientKeyPlayedEvent(int Key)
		{
			playwave.play(Key);
		}

		void client_ClientDisconnectedEvent()
		{
			this.Dispatcher.Invoke(new Action(() => {
				ConnectName.Content = "No user";
				ConnectName.Foreground = new SolidColorBrush(Colors.Black);
				textName.IsEnabled = true;
				textIP.IsEnabled = true;
				buttonConnect.IsEnabled = true;
				buttonStart.IsEnabled = false;
				StopRecognition();
			}));
		}

		void client_ClientConnectedEvent(string UserName)
		{
			this.Dispatcher.Invoke(new Action(() => {
				ConnectName.Content = UserName;
				ConnectName.Foreground = new SolidColorBrush(Colors.Blue);
				textName.IsEnabled = false;
				textIP.IsEnabled = false;
				buttonConnect.IsEnabled = false;
				buttonStart.IsEnabled = true;
			}));
		}

		private void JumpToMain(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}

		/*private void Foo()
		{
			Thread.Sleep(1500);

			for (int i = 0; i < 5; ++i)
			{
				Set_Point1(4 / 7, 4 % 7);
				Thread.Sleep(100);
			}

			int[] BirthSong = new int[] { 5, 5, 6, 5, 8, 7, 5, 5, 6, 5, 9, 8, 5, 5, 12, 10, 8, 7, 6, 11, 11, 10, 8, 9, 8 };
			double[] Duration = new double[] { 0.35, 0.35, 0.7, 0.7, 0.6, 1.2, 0.4, 0.35, 0.5, 0.6, 0.6, 1, 0.3, 0.3, 0.6, 0.7, 0.6, 0.6, 0.5, 0.3, 0.3, 0.7, 0.6, 0.6, 2 };
			int length = BirthSong.Length;

			for (int i = 0; i < length; ++i)
			{
				PlayKey(BirthSong[i], Duration[i] * 1000);
			}
		}

		private void PlayKey(int key, double duration)
		{
			playwave.play(key);
			key--;
			//Set_Point1(key / 7, key % 7);
			Thread.Sleep((int)duration);
		}

		public void Set_Point1(double x, double y)
		{ 
			this.Dispatcher.Invoke(new Action(() => {
				Pointer1.SetValue(Canvas.LeftProperty, 220.0 + y * 133.0);
			}));
			this.Dispatcher.Invoke(new Action(() =>
			{
				Pointer1.SetValue(Canvas.TopProperty, 190.0 + x * 260.0);
			}));
		}*/

		private void Connect_Click(object sender, RoutedEventArgs e)
		{
			if (textName.Text == "")
			{
				MessageBox.Show("用户名不能为空");
				return;
			}

			//System.Net.IPAddress ip;
			/*if(System.Net.IPAddress.TryParse(textIP.Text, out ip))
			{
				MessageBox.Show("请输入合法IP");
				return;
			}*/

			client.Codec = "A-Law";
			client.Name = textName.Text;
			client.IP = textIP.Text;

			client.Connect();
		}

		private void buttonStart_Click(object sender, RoutedEventArgs e)
		{
			first = new HandsRecognition(0);

			first.RHandMove += first_RHandMove;
			first.LHandMove += first_LHandMove;
			first.HandClick += first_HandClick;

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

		void first_HandClick(int song, int duration)
		{
			//处理手按下键
			playwave.play(song);
			Set_Keypressed(song);
			client.SendKey(song);
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
				this.Dispatcher.Invoke(new Action(() =>
				{
					Pointer1.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
					Pointer1.SetValue(Canvas.TopProperty, (90d + 771d / 6));
				}));
			}
			else if (y > -0.04)
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					Pointer1.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
					Pointer1.SetValue(Canvas.TopProperty, (90d + 771d / 2));
				}));
			}
			else
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					Pointer1.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
					Pointer1.SetValue(Canvas.TopProperty, (90d + 771d * 5 / 6));
				}));
			}
		}

		public void Set_Point2(double x, double y)
		{
			if (x < -0.10 || x > 0.11 || y < -0.12 || y > 0.12) { }
			else if (y > 0.04)
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					Pointer2.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
					Pointer2.SetValue(Canvas.TopProperty, (90d + 771d / 6));
				}));
			}
			else if (y > -0.04)
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					Pointer2.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
					Pointer2.SetValue(Canvas.TopProperty, (90d + 771d / 2));
				}));
			}
			else
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					Pointer2.SetValue(Canvas.LeftProperty, (170d + 900d * (0.11 - x) / 0.21));
					Pointer2.SetValue(Canvas.TopProperty, (90d + 771d * 5 / 6));
				}));
			}
		}

		private void Set_Keypressed(int x)
		{
			this.Dispatcher.Invoke(new Action(() =>
			{
				key_pressed.Visibility = System.Windows.Visibility.Visible;
			}));
			if (x <= 0)
			{

			}

			else if (x < 8)
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					key_pressed.SetValue(Canvas.LeftProperty, (170d + (x - 1) % 7 * 900d / 7));
					key_pressed.SetValue(Canvas.TopProperty, 604d);
				}));
			}
			else if (x < 15)
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					key_pressed.SetValue(Canvas.LeftProperty, (170d + (x - 1) % 7 * 900d / 7));
					key_pressed.SetValue(Canvas.TopProperty, 347d);
				}));
			}
			else if (x < 22)
			{
				this.Dispatcher.Invoke(new Action(() =>
				{
					key_pressed.SetValue(Canvas.LeftProperty, (170d + (x - 1) % 7 * 900d / 7));
					key_pressed.SetValue(Canvas.TopProperty, 90d);
				}));
			}

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int[] BirthSong = new int[] { 5, 5, 6, 5, 8, 7, 5, 5, 6, 5, 9, 8, 5, 5, 12, 10, 8, 7, 6, 11, 11, 10, 8, 9, 8 };
			double[] Duration = new double[] { 0.35, 0.35, 0.7, 0.7, 0.6, 1.2, 0.4, 0.35, 0.5, 0.6, 0.6, 1, 0.3, 0.3, 0.6, 0.7, 0.6, 0.6, 0.5, 0.3, 0.3, 0.7, 0.6, 0.6, 2 };
			int length = BirthSong.Length;

			for (int i = 0; i < length; ++i)
			{
				client.SendKey(BirthSong[i]);
				Thread.Sleep((int)(Duration[i] * 500));
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			StopRecognition();
			client.Exit();
		}
	}
}
