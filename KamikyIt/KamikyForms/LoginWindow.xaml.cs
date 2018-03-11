using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApiWrapper.Core;
using MahApps.Metro.Controls;

namespace KamikyForms
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class LoginWindow : MetroWindow
	{
		public LoginWindow()
		{
			InitializeComponent();

			(DataContext as LoginFormViewModel)?.SetWindow(this);
		}
	}
}
