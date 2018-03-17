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

namespace KamikyForms.Bot
{
    /// <summary>
    /// Логика взаимодействия для BotVariantsWindow.xaml
    /// </summary>
    public partial class BotVariantsWindow : MetroWindow
    {

        public string message;
        public List<string> messages;
        public Chat.Gui.PersonChat personChat;
        public BotVariantsWindow(string message, List<string> mm, Chat.Gui.PersonChat personChat)
        {
            InitializeComponent();
            this.message = message;
            this.messages = mm;
            this.personChat = personChat;
        }

        private void _OnLoaded(object sender, RoutedEventArgs e)
        {
            phraz.Content = "Варианты на фразу:  " + message.ToUpper();
            phraz_col.Content = "Количество: " + messages.Count;
            List<MM> items = new List<MM>();
            foreach (string mes in messages)
            {
                items.Add(new MM(){ message  = mes});
            }
            datagrid.ItemsSource = items;
            datagrid.Items.Refresh();
        }

        public class MM
        {
            public string message { get; set; } 
        }


        private void Datagrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedItems.Count != 1)
            {
                return;
            }
            String message = (datagrid.SelectedItems[0] as MM).message;
            personChat.writeMyMessage(message);
            Close();
        }
    }
}
