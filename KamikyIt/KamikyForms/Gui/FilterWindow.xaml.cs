using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ApiWrapper.Core;
using MahApps.Metro.Controls;


namespace KamikyForms.Gui
{
    /// <summary>
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : MetroWindow
    {

        public SearchFilter filter;
        public List<PersonModel> persons = new List<PersonModel>();
        public List<PersonModel> choosenpersons = new List<PersonModel>();


        public FilterWindow()
        {
            InitializeComponent();
        }

        private void onTest(object sender, RoutedEventArgs e)
        {
            //var filter = new SearchFilter()
            //{
            //    AgeInterval = new IntInterval<int>(10, 30),

            //    CityId = 157,
            //    CountryId = 1,
            //    Sex = SexEnum.Woman,
            //    IsOnline = true,
            //    HasPhoto = true,
            //    FamilyState = FamilyState.ActiveSearch,
            //};

            var peoples = SearchInstrument.getPersons(filter);
            MessageBox.Show(peoples[0].ToString());

            return;

        }

        private void FilterWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //инициализация начальных опций
            cgender.IsChecked = true;
            cminage.IsChecked = true;
            cmaxage.IsChecked = true;
            ccoountry.IsChecked = true;
            ccity.IsChecked = true;
            cfamily.IsChecked = true;
            conline.IsChecked = true;
            cfoto.IsChecked = true;
            cfriendmin.IsChecked = true;
            cfriendmax.IsChecked = true;
            csubsmin.IsChecked = true;
            csubsmax.IsChecked = true;
            cfriendmin.IsChecked = true;
            cfriendmax.IsChecked = true;
            cpostmin.IsChecked = true;
            cpostmax.IsChecked = true;
            ssort.IsChecked = true;
            coffcet.IsChecked = true;

            gender.SelectedIndex = 0;
            country.SelectedIndex = 0;
            city.SelectedIndex = 0;
            family.SelectedIndex = 0;
            online.SelectedIndex = 0;
            foto.SelectedIndex = 0;
            sort.SelectedIndex = 0;
            Random r = new Random();
            double rand = r.NextDouble();
            minage.Text = "20";
            maxage.Text = "24";
            if (rand >= 0.5)
            {
                minage.Text = "25";
                maxage.Text = "30";
            }
            friendmin.Text = "50";
            friendmax.Text = "500";
            subsmin.Text = "0";
            subsmax.Text = "1000";
            postmin.Text = "1";
            postmax.Text = "1000";
            offcet.Text = "0";

        }




        public void getPostMin()
        {
            if (cpostmin.IsChecked == false) return;
            filter.PostMin = Convert.ToInt32(postmin.Text);
        }

        public void getPostMax()
        {
            if (cpostmax.IsChecked == false) return;
            filter.PostMax = Convert.ToInt32(postmax.Text);
        }


        public void getSubsMin()
        {
            if (csubsmin.IsChecked == false) return;
            filter.SubsMin = Convert.ToInt32(subsmin.Text);
        }

        public void getSubsMax()
        {
            if (csubsmax.IsChecked == false) return;
            filter.SubsMax = Convert.ToInt32(subsmax.Text);
        }


        public void getFriendMin()
        {
            if (cfriendmin.IsChecked == false) return;
            filter.FriendMin = Convert.ToInt32(friendmin.Text);
        }

        public void getFriendMax()
        {
            if (cfriendmax.IsChecked == false) return;
            filter.FriendMax = Convert.ToInt32(friendmax.Text);
        }

        public void getMinAge()
        {
            if (cminage.IsChecked == false) return;
            filter.MinAge = Convert.ToInt32(minage.Text);

        }


        public void getMaxAge()
        {
            if (cmaxage.IsChecked == false) return;
            filter.MaxAge = Convert.ToInt32(maxage.Text);

        }

        public void getCountry()
        {
            if (ccoountry.IsChecked == false) return;
            if (country.SelectedIndex == 0)
            {
                filter.CountryId = 1;
            }
        }

        public void getCity()
        {
            if (ccity.IsChecked == false) return;
            if (city.SelectedIndex == 0)
            {
                filter.CityId = 157;
            }
        }

        public void getPhoto()
        {
            if (cfoto.IsChecked == false) return;
            if (foto.SelectedIndex == 0)
            {
                filter.HasPhoto = true;
            }
            else
            {
                filter.HasPhoto = false;
            }
        }

        public void getOnline()
        {
            if (conline.IsChecked == false) return;
            if (online.SelectedIndex == 0)
            {
                filter.IsOnline = true;
            }
            else
            {
                filter.IsOnline = false;

            }
        }

        public void getFamily()
        {
            if (cfamily.IsChecked == false) return;
            filter.FamilyState = (FamilyState)ConvertBack(family);
        }

        public void getSex()
        {
            if (cgender.IsChecked == false) return;
            if (gender.SelectedIndex == 0)
            {
                filter.Sex = SexEnum.Woman;
            }
            if (gender.SelectedIndex == 1)
            {
                filter.Sex = SexEnum.Man;
            }
            if (gender.SelectedIndex == 2)
            {
                filter.Sex = SexEnum.Any;
            }
        }

        public void getSort()
        {
            if (ssort.IsChecked == false) return;
            if (sort.SelectedIndex == 0)
            {
                filter.profileSort = ProfileSort.date;
            }
            if (gender.SelectedIndex == 1)
            {
                filter.profileSort = ProfileSort.popular;

            }

        }

        public void getOffcet()
        {
            if (coffcet.IsChecked == false) return;
            filter.Offcet = Convert.ToInt32(offcet.Text);

        }

        private void oncheckChanged(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            String tag = ch.Tag.ToString();
            if (tag == "gender")
            {
                gender.IsEnabled = ch.IsChecked == true;
            }
            if (tag == "minage")
            {
                minage.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "maxage")
            {
                maxage.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "country")
            {
                country.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "city")
            {
                city.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "family")
            {
                family.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "online")
            {
                online.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "friendmin")
            {
                friendmin.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "friendmax")
            {
                friendmax.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "subsmin")
            {
                subsmin.IsEnabled = ch.IsChecked == true;
            }


            if (tag == "subsmax")
            {
                subsmax.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "postmin")
            {
                postmin.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "postmax")
            {
                postmax.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "sort")
            {
                sort.IsEnabled = ch.IsChecked == true;
            }

            if (tag == "offcet")
            {
                offcet.IsEnabled = ch.IsChecked == true;
            }

        }


        public object ConvertBack(object value)
        {
            var str = (value as ComboBox).SelectionBoxItem.ToString();

            switch (str)
            {
                case "не женат (не замужем)":
                    return FamilyState.NotMarry;
                case "встречается<":
                    return FamilyState.Dating;
                case "помолвлен(-а)":
                    return FamilyState.Betrothed;
                case "женат (замужем)":
                    return FamilyState.Marry;
                case "всё сложно<":
                    return FamilyState.AllHardShit;
                case "в активном поиске<":
                    return FamilyState.ActiveSearch;
                case "влюблен(-а)":
                    return FamilyState.Loved;
                case "в гражданском браке<":
                    return FamilyState.CivilMarry;
                default:
                    throw new Exception("Невозможно распарсить");
            }
        }

        private void onSearch(object sender, RoutedEventArgs e)
        {
            filter = new SearchFilter();
            getSex();
            getMinAge();
            getMaxAge();
            getCountry();
            getCity();
            getFamily();
            getOnline();
            getFriendMin();
            getFriendMax();
            getSubsMin();
            getSubsMax();
            getPostMin();
            getPostMax();
            getOffcet();
            getSort();
            List<PersonModel> peoples = SearchInstrument.getPersons(filter);
            persons = peoples;
            UpdateUi();

        }

        private void onSelect(object sender, RoutedEventArgs e)
        {
            PreparePersonsForm form = new PreparePersonsForm(persons);
            var res = form.ShowDialog();
            if (res == true)
            {
                choosenpersons = form.pl2;
                UpdateUi();
            }
        }

        public void UpdateUi()
        {
            finded.Content = "Найдено : " + persons.Count;
            chosen.Content = "Выбрано : " + choosenpersons.Count;
        }

        private void onApply(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
