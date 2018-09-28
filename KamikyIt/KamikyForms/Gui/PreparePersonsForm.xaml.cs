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
using ApiWrapper.Core;
using MahApps.Metro.Controls;

namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для PreparePersonsForm.xaml
    /// </summary>
    public partial class PreparePersonsForm : MetroWindow
    {
        List<ApiWrapper.Core.PersonModel> people = new List<PersonModel>();
        public List<ApiWrapper.Core.PersonModel> pl1 = new List<PersonModel>();
        public List<ApiWrapper.Core.PersonModel> pl2 = new List<PersonModel>();
        public List<ApiWrapper.Core.PersonModel> allp = new List<PersonModel>();

        public ApiWrapper.Core.PersonModel ovPerson = null;

        public readonly int MAX_COUNT = 20;

        public PreparePersonsForm(List<ApiWrapper.Core.PersonModel> peoples)
        {
            InitializeComponent();
            this.people = peoples;
            foreach (PersonModel pl in people)
            {
                pl1.Add(pl);
                allp.Add(pl);
            }
            refreshDg();
        }


        public void refreshDg()
        {

            dataGridView1.ItemsSource = pl1;
            dataGridView1.Items.Refresh();
            s1.Content = "Все: " + pl1.Count;
            s2.Content = "Выбрано: " + pl2.Count;

        }




        //private void dataGridView1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //    if (pl2.Count == MAX_COUNT)
        //    {
        //        MessageBox.Show("Больше не влезет ... совсем не влезет ");
        //        return;
        //    }
        //    pl1 = RemovePlayer(pl1, ovPlayer);
        //    pl2 = AddPlayer(pl2, ovPlayer);

        //    refreshDg();
        //}


        public List<PersonModel> RemovePlayer(List<PersonModel> p, PersonModel pl)
        {
            if (pl == null) return p;
            p = p.Where(o => o.id != pl.id).ToList();
            return p;
        }


        public List<PersonModel> AddPlayer(List<PersonModel> p, PersonModel pl)
        {
            if (pl == null) return p;
            PersonModel s = allp.First(o => o.id == pl.id);
            p.Add(s);
            return p;
        }


        private void clear_OnClick(object sender, RoutedEventArgs e)
        {
            dataGridView1.SelectedItems.Clear();
            dataGridView1.Items.Refresh();
            s2.Content = "Выбрано: " + 0;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (dataGridView1.SelectedItems.Count > MAX_COUNT)
            {
                MessageBox.Show("Нельзя выбрать больше: " + MAX_COUNT);
                return;
            }
            foreach (PersonModel p in dataGridView1.SelectedItems)
            {
                pl2.Add(p);
            }
            DialogResult = true;
        }

        private void DataGridView1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int chosen = dataGridView1.SelectedItems.Count;
            s2.Content = "Выбрано: " + chosen;
            if (chosen == 20)
            {
                foreach (PersonModel p in dataGridView1.SelectedItems)
                {
                    pl2.Add(p);
                }
                DialogResult = true;
            }
        }


        //открытие VK
        private void openVk(object sender, MouseButtonEventArgs e)
        {
            if (ovPerson == null)
            {
                return;
            }
            string profilePage = "https://vk.com/" + ovPerson.Domain;
            System.Diagnostics.Process.Start(profilePage);
        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem lv = sender as ListViewItem;
            ovPerson = lv.Content as PersonModel;
        }


        private void PreparePersonsForm_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                if (ovPerson == null)
                {
                    return;
                }
                //проверяем что есть
                bool del = false;
                foreach (var ps in dataGridView1.SelectedItems)
                {
                    PersonModel pm = ps as PersonModel;
                    if (pm.id == ovPerson.id)
                    {
                        del = true;
                    }
                }
                if (del)
                {
                    dataGridView1.SelectedItems.Remove(ovPerson);

                }
                else
                {
                    dataGridView1.SelectedItems.Add(ovPerson);
                }
                dataGridView1.Items.Refresh();
            }
        }
    }
}
