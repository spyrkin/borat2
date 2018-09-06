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
using Chat.Gui;
using MahApps.Metro.Controls;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для SmartBotWindow.xaml
    /// </summary>
    public partial class SmartBotWindow : MetroWindow
    {
        public Chat.Gui.PersonChat personChat;
   
        public SmartBotWindow(Chat.Gui.PersonChat personChat)
        {
            this.personChat = personChat;
            InitializeComponent();
            if (personChat.Person == null)
            {
                return;
            }
            pName.Content = personChat.Person.name;
            datagrid.ItemsSource = personChat.chatMessages;
            datagrid.Items.Refresh();
        }


        //отправка сообщений через бота
        private void onSubmit(object sender, RoutedEventArgs e)
        {

        }
        #region события гриды чата персоны
        private void dataGridView1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void dataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {
        }
        #endregion

        private void openTooltip(object sender, ToolTipEventArgs e)
        {
        }
    }
}
