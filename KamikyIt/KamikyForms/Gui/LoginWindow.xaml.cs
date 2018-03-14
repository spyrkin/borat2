using System;
using System.Collections.Generic;
using System.IO;
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
using Chat.Gui;
using MahApps.Metro.Controls;
using VkNet.Examples.ForChat;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            string version = getVersion();
            verText.Content = version;
            //List<String> bans = FileParser.getAnswer();

        }

        public string getVersion()
        {
            string version = "";
            DirectoryInfo dir = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            foreach (var item in dir.GetFiles())
            {
                if (item.Extension == ".exe")
                {
                    version = "build: " + item.LastWriteTime.ToString("yyyy-MM-dd");
                    return version;
                }
            }
            return version;

        }

        private void onLogin(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string pass = Pass.Password;
            string res = LoginCoreHelper.Login(login, pass);
            if (String.IsNullOrEmpty(res))
            {
                var chatWindow = new ChatWindow();
                chatWindow.Show();
                this.Close();
                chatWindow.Show();
            }
            else
            {
                errorText.Text = res;
            }
        }
    }
}
