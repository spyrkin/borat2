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

namespace Chat.Gui
{
	/// <summary>
	/// Логика взаимодействия для SimpleMessageBox.xaml
	/// </summary>
	public partial class SimpleMessageBox : Window
	{

		public string msg;
		public SimpleMessageBox()
		{
			InitializeComponent();
		}

		private void onSubmit(object sender, RoutedEventArgs e)
		{
			msg = textblock.Text;
			DialogResult = true;

		}
	}
}
