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
        public OpenPhrase()
        {
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
            if (!st.Any(o => o.message == startMessage))
            {
                FileParser.WriteStartUps(startMessage);
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
    }
}
