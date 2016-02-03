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
	/// RoomWindow.xaml 的交互逻辑
	/// </summary>
	public partial class RoomWindow : Window
	{
		int flag=0;
		public RoomWindow()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}



		private void cb_Load(object sender, System.Windows.RoutedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
						if(myCB.SelectedIndex==0)
			{
				PianoImg.Visibility=System.Windows.Visibility.Hidden;
				DrumImg.Visibility=System.Windows.Visibility.Visible;
			}
			else if(myCB.SelectedIndex==1)
			{
				PianoImg.Visibility=System.Windows.Visibility.Visible;
				DrumImg.Visibility=System.Windows.Visibility.Hidden;
				
			}

		}


		private void CB_Change(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
			if(myCB.SelectedIndex==0&&flag!=0)
			{
				PianoImg.Visibility=System.Windows.Visibility.Hidden;
				DrumImg.Visibility=System.Windows.Visibility.Visible;
			}
			else if(myCB.SelectedIndex==1)
			{
				PianoImg.Visibility=System.Windows.Visibility.Visible;
				DrumImg.Visibility=System.Windows.Visibility.Hidden;
				
			}
			flag++;
		}

        private void returnMain(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }




	}
}