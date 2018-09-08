using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для BotChatWindow.xaml
    /// </summary>
    public partial class BotChatWindow : Canvas
    {
        private Core.Theme theme;
        private string name;
        public BotChatWindow()
        {
            InitializeComponent();
        }

        public void wireData(Bot.SmartBot sm, string name)
        {
            this.name = name;
            theme = sm.themes.FirstOrDefault(o => o.Name == name);
            themeName.Content = name;
            datagrid.ItemsSource = theme.messages;
            datagrid.Items.Refresh();
        }


        private void Datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
