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

namespace Chat.Gui
{
    /// <summary>
    /// Логика взаимодействия для OpenPhrase.xaml
    /// </summary>
    public partial class OpenPhrase : Window
    {
        public String startMessage = "Привет, давай знакомиться =)";

        public OpenPhrase()
        {
            InitializeComponent();
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
            Close();
        }
    }
}
