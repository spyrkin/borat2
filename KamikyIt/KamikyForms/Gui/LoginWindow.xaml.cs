using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using KamikyForms.Bot;
using MahApps.Metro.Controls;
using VkNet.Examples.ForChat;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public Hero hero;
        public bool debug;
        public Options options;
        public bool loading = true;
        public LoginWindow()
        {
            InitializeComponent();
            options = new Options();
            options.load();
            hero = options.hero;
            debug = options.debug;
            checkBox1.IsChecked = options.debug;
            combo.SelectedIndex = options.hero.id == 1 ? 0 : 1;
            string version = getVersion();
            verText.Content = version;
            loading = false;

            //for (int i = 0; i < 10; i++)
            //{
            //    Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            //    Thread.Sleep(1);

            //    double r = rand.NextDouble();
            //    Console.WriteLine(r);
            //}
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
                var chatWindow = new ChatWindow(debug, hero);
                chatWindow.Show();
                this.Close();
                chatWindow.Show();
            }
            else
            {
                errorText.Text = res;
            }
        }

        private void onChangeUser(object sender, SelectionChangedEventArgs e)
        {

            if (combo.SelectedIndex == 0)
            {
                hero = Hero.getInstance(1);
            }

            if (combo.SelectedIndex == 1)
            {
                hero = Hero.getInstance(2);
            }
            Login.Text = hero.login;
            Pass.Password = hero.pass;
            if (loading)
            {
                return;
            }
            options.save(debug, hero);


        }

        //change debug mode
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            if (loading)
            {
                return;
            }
            debug = !debug;
            options.save(debug, hero);

            //  checkBox1.IsChecked = debug;
        }
    }
}
