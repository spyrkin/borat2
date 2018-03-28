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
using Chat.Core;
using Chat.Gui;
using VkNet.Examples.ForChat;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для AdviceControl.xaml
    /// </summary>
    public partial class AdviceControl : Canvas
    {
        public List<String> advices = new List<string>();
        public string tag;
        public string ruName;
        public TextBox textblock;
        public List<PersonChat> resevers;


        public AdviceControl()
        {
            InitializeComponent();
        }


        public void setResourcec()
        {
            double margin = (Width - mainBlock.Width) / 2;
            Canvas.SetLeft(mainBlock, margin);
            if (tag == "hobby")
            {
                ruName = "УВЛЕЧЕНИЯ";
            }
            if (tag == "quastins")
            {
                ruName = "ВОПРОСЫ";
            }
            adviceName.Content = ruName;
            loadResources();

        }

        private void loadResources()
        {
            advices = FileParser.getAdvise(tag);
        }


        //нажатие на кнопку

        private void onChose(object sender, RoutedEventArgs e)
        {
            List<string> adv = filterAdvices();
            AdviceList alist = new AdviceList(adv, ruName);
            var res = alist.ShowDialog();
            if (res == true)
            {
                textblock.Text = alist.message;
            }
        }


        //выбираем только те фразы что не говорили
        private List<string> filterAdvices()
        {
            //собираем все сказанное
            List<string> sayed = new List<string>();
            foreach (PersonChat pc in resevers)
            {
                foreach (ChatMessage ch in pc.chatMessages)
                {
                    if (ch.isBot == false)
                    {
                        continue;
                    }
                    sayed.Add(ch.message);
                }
            }


            List<string> result = advices.Where(o => !sayed.Contains(o)).ToList();
            return result;


        }

        public void wire(object acTag, TextBox textblock, List<PersonChat> reseverse)
        {
            tag = acTag.ToString();
            this.textblock = textblock;
            this.resevers = reseverse;
        }
    }
}
