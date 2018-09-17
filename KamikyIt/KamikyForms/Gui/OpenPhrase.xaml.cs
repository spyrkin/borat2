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
using MahApps.Metro.Controls;
using VkNet.Examples.ForChat;

namespace Chat.Gui
{
    /// <summary>
    /// Логика взаимодействия для OpenPhrase.xaml
    /// </summary>
    public partial class OpenPhrase : Window
    {
        public String startMessage = "Привет, давай знакомиться =)";
        public List<StartUps> st = new List<StartUps>();
        public bool debug;
        public OpenPhrase(bool debug)
        {
            this.debug = debug;
            InitializeComponent();
            List<String> startups = FileParser.getStartUps();
            foreach (String s in startups)
            {
                StartUps f = new StartUps();
                f.message = s;
                st.Add(f);
            }
            st = st.OrderBy(o => o.message).ToList();
            datagrid.ItemsSource = st;
            datagrid.Items.Refresh();
            textblock.Text = startMessage;
        }

        private void onSubmit(object sender, RoutedEventArgs e)
        {
            startMessage = textblock.Text;

            if (String.IsNullOrEmpty(startMessage))
            {
                Close();
                return;
            }
            if (!debug)
            {
                if (!st.Any(o => o.message == startMessage))
                {
                    FileParser.WriteStartUps(startMessage);
                }
            }
            Close();
        }

        public class StartUps
        {
            public string message
            {
                get;
                set;
            }
        }


        //выбрали из списка startUps
        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox bl = sender as TextBox;
            string message = bl.Text;
            textblock.Text = message;

        }


        //what
        private void Datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void emodziClick(object sender, RoutedEventArgs e)
        {
            string em = (sender as Button).Content.ToString();
            textblock.Text = textblock.Text + em;
        }

        private void mainForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {

                if (e.Key == Key.N)
                {
                    textblock.Text = textblock.Text + "$name";
                }
                if (e.Key == Key.F)
                {
                    textblock.Text = textblock.Text + "$fullname";

                }
                if (e.Key == Key.P)
                {
                    textblock.Text = textblock.Text + "Хорошо, $name. Если вдруг с парнем не сложится, напиши мне потом на эту страницу ))";

                }
            }
        }
    }
}
