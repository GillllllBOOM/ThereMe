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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void jump(object sender, System.Windows.RoutedEventArgs e)
        {
			InRoom_Piano room = new InRoom_Piano();
			room.Show();
			this.Close();
        }

        private void jumpsetup(object sender, System.Windows.RoutedEventArgs e)
        {
			
        }

        private void jumponline(object sender, System.Windows.RoutedEventArgs e)
        {
			UI.Multiplayer multi = new UI.Multiplayer();
			multi.Show();
			this.Close();
		}

        private void log_button(object sender, System.Windows.RoutedEventArgs e)
        {
        	Login lg = new Login();
			lg.Show();
			this.Close();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}
    }
}
