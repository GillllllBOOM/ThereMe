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
	/// OnlineInRoom.xaml 的交互逻辑
	/// </summary>
	public partial class OnlineInRoom : Window
	{
		public OnlineInRoom()
		{
			this.InitializeComponent();
			
			// 在此点之下插入创建对象所需的代码。
		}

		private void ReadyOrNot(object sender, System.Windows.RoutedEventArgs e)
		{
			if(Ready_Label.Visibility.Equals("Hidden"))
				Ready_Label.Visibility=System.Windows.Visibility.Visible;
			else
				Ready_Label.Visibility = System.Windows.Visibility.Hidden;
		}

		private void Exit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
			InRoom room = new InRoom();
			room.Show();
			this.Close();
		}
	}
}