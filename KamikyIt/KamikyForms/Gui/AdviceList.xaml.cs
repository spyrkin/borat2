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
using MahApps.Metro.Controls;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для AdviceList.xaml
    /// </summary>
    public partial class AdviceList : MetroWindow
    {

        public List<String> advices = new List<string>();
        public string ruName;
        public string message;
        public AdviceList(List<String> advices, string ruName)
        {
            InitializeComponent();
            this.advices = advices;
            this.ruName = ruName;
            
        }

        public class MM
        {
            public string message { get; set; }
        }


        private void feeldatagrid()
        {
            List<MM> items = new List<MM>();
            foreach (string mes in advices)
            {
                items.Add(new MM() { message = mes });
            }
            datagrid.ItemsSource = items;
            datagrid.Items.Refresh();
        }

        private void AdviceList_OnLoaded(object sender, RoutedEventArgs e)
        {
            phraz.Content = ruName;
            feeldatagrid();
        }

        private void Datagrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagrid.SelectedItems.Count == 0)
            {
                return;
            }
            message = (datagrid.SelectedItems[0] as MM).message;
            DialogResult = true;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox bl = sender as TextBox;
            message = bl.Text;
            DialogResult = true;

        }
    }
}
