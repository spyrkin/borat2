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

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для AdviceControl.xaml
    /// </summary>
    public partial class AdviceControl : Canvas
    {
        public AdviceControl()
        {
            InitializeComponent();
            setResourcec();
        }

        private void setResourcec()
        {
            double margin = (Width - mainBlock.Width) / 2;
            Canvas.SetLeft(mainBlock, margin);
            String t = Name;
            if (t == "hobby")
            {
                adviceName.Content = "УВЛЕЧЕНИЯ";
            }
        }


        //нажатие на кнопку

        private void onChose(object sender, RoutedEventArgs e)
        {
        }
    }
}
