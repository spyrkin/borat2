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
using KamikyForms.Bot;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для SmartBot2Window.xaml
    /// </summary>
    public partial class SmartBot2Window : Canvas
    {
        public SmartBot sm;
        public List<BotChatWindow> chats = new List<BotChatWindow>();

        public SmartBot2Window()
        {
            InitializeComponent();
            sm = new SmartBot(null);
            loaded();
        }

        public void loaded()
        {
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

        public void setMessage(string message, int i, Core.Theme theme)
        {
            //textblock.Text = message;
            //pNumber.Text = i.ToString();
            //currentI = i;
            //currentTheme = theme;

        }

        private void onSubmit(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
