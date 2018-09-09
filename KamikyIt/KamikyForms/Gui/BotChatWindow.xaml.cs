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
using KamikyForms.Core;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для BotChatWindow.xaml
    /// </summary>
    public partial class BotChatWindow : Canvas
    {
        public Bot.SmartBot sm;
        public Core.Theme theme;
        public string name;
        public SmartBotWindow win;
        public BotChatWindow()
        {
            InitializeComponent();
        }

        public void wireData(SmartBotWindow win, Bot.SmartBot sm, string name)
        {
            this.sm = sm;
            this.name = name;
            this.win = win;
            theme = sm.themes.FirstOrDefault(o => o.Name == name);
            themeName.Content = name;
            datagrid.ItemsSource = theme.messages;
            datagrid.Items.Refresh();
        }


        private void Datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedItems.Count == 1)
            {
                //убираем у других chatWindowSelection
                win.clearSelections(name);
                Theme.ThemeItem item = (Theme.ThemeItem)datagrid.SelectedItems[0];
                string message = item.message;
                win.setMessage(message);


            }

        }
    }
}
