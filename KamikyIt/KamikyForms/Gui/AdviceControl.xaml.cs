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
            AdviceList alist = new AdviceList(advices, ruName);
            var res = alist.ShowDialog();
            if (res == true)
            {
                textblock.Text = alist.message;
            }
        }

        public void wire(object acTag, TextBox textblock)
        {
            tag = acTag.ToString();
            this.textblock = textblock;
        }
    }
}
