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
using KamikyForms.Bot;
using MahApps.Metro.Controls;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для SmartBotWindow.xaml
    /// </summary>
    public partial class SmartBotWindow : MetroWindow
    {
        public Chat.Gui.PersonChat personChat;
        public SmartBot sm;

        public SmartBotWindow(Chat.Gui.PersonChat personChat)
        {
            this.personChat = personChat;
            sm = personChat.sm;
            InitializeComponent();
            //if (personChat.Person == null)
            //{
            //    return;
            //}
            //сообщения
            //pName.Content = personChat.Person.name;
            //datagrid.ItemsSource = personChat.chatMessages;
            //datagrid.Items.Refresh();


            bc1.wireData(sm, "общее");
            bc2.wireData(sm, "путешествия");
            bc3.wireData(sm, "детство");
            bc4.wireData(sm, "спорт");
            bc5.wireData(sm, "увлечения");
            bc6.wireData(sm, "кино");
            bc7.wireData(sm, "учеба");
            bc8.wireData(sm, "отношения");



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
