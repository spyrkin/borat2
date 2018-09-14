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
using Theme = KamikyForms.Core.Theme;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для SmartBotWindow.xaml
    /// </summary>
    public partial class SmartBotWindow : MetroWindow
    {
        public Chat.Gui.PersonChat personChat;
        public SmartBot sm;
        public List<BotChatWindow> chats = new List<BotChatWindow>();
        public Core.Theme currentTheme;
        public int currentI;

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


            bc1.wireData(this, sm, "общее");
            bc2.wireData(this, sm, "путешествия");
            bc3.wireData(this, sm, "детство");
            bc4.wireData(this, sm, "спорт");
            bc5.wireData(this, sm, "увлечения");
            bc6.wireData(this, sm, "кино");
            bc7.wireData(this, sm, "учеба");
            bc8.wireData(this, sm, "отношения");
            chats.Add(bc1);
            chats.Add(bc2);
            chats.Add(bc3);
            chats.Add(bc4);
            chats.Add(bc5);
            chats.Add(bc6);
            chats.Add(bc7);
            chats.Add(bc8);




        }


        //отправка сообщений через бота
        private void onSubmit(object sender, RoutedEventArgs e)
        {
            string msg = textblock.Text;
            if (String.IsNullOrEmpty(msg))
            {
                Close();
                return;
            }
            string pText = pNumber.Text;
            if (!String.IsNullOrEmpty(pText))
            {
                int k = 0;
                foreach (Theme.ThemeItem ti in currentTheme.messages)
                {
                    if (k == currentI)
                    {
                        ti.isExpired = true;
                        break;
                    }
                    k++;
                }
                currentTheme.expired.Add(currentI);
                personChat.sm.currentTheme = currentTheme;
            }
            personChat.writeMyMessage(msg);
            Close();
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

        public void clearSelections(string name)
        {
            foreach (BotChatWindow c in chats)
            {
                if (c.name == name)
                {
                    continue;
                }
                c.datagrid.SelectedIndex = -1;
            }
        }

        public void setMessage(string message, int i,  Core.Theme theme)
        {
            textblock.Text = message;
            pNumber.Text = i.ToString();
            currentI = i;
            currentTheme = theme;

        }
    }
}
