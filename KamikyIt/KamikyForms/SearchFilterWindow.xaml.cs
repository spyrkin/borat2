using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using KamikyForms.Annotations;
using MahApps.Metro.Controls;
using VkNet.Examples.DataBaseBehaviour;

namespace KamikyForms
{
	/// <summary>
	/// Логика взаимодействия для SearchFilterWindow.xaml
	/// </summary>
	public partial class SearchFilterWindow : MetroWindow
	{
		public SearchFilterWindow()
		{
			InitializeComponent();
		}
	}

	public class SearchFilterViewModel : INotifyPropertyChanged
	{
		public ICommand SaveAsNewFilterCommand { get; set; }

		public ObservableCollection<SearchFilterInfo> AllFilters { get; set; }

		#region Fields

		public SearchFilterInfo SelectedFilter { get; set; }

		// Название данного фильтра.
		public string FilterName { get; set; }

		// Количество записей, которые выдать
		public int Count { get; set; }


		public bool SexHasValue { get; set; }
		//Пол
		public SexEnum Sex { get; set; }

		public bool FamilyStateHasValue { get; set; }
		// Семейное положение
		public FamilyState FamilyState { get; set; }


		public bool AgeIntervalHasValue { get; set; }
		// Возраст
		public IntInterval<int> AgeInterval { get; set; }

		public bool CountryHasValue { get; set; }
		// Страна
		public string Country { get; set; }
		public int CountryId { get; set; }


		public bool CityHasValue { get; set; }
		// Город
		public string City { get; set; }
		public int CityId { get; set; }

		public bool FriendsIntervalHasValue { get; set; }
		// Количество друзей
		public IntInterval<int> FriendsInterval { get; set; }

		public bool SubscribersIntervalHasValue { get; set; }
		// Количество пиздолизов.
		public IntInterval<int> SubscribersInterval { get; set; }

		// Дата регистрации
		public DateTime RegistrationDate { get; set; }

		public bool PhotosCountHasValue { get; set; }
		public IntInterval<int> PhotosCount { get; set; }

		// Сейчас онлайн
		public bool IsOnline { get; set; }

		// С фото
		public bool HasPhoto { get; set; }

		#endregion


		public SearchFilterViewModel()
		{
			AllFilters = new ObservableCollection<SearchFilterInfo>();

			UpdateSearchFiltersList();

			SaveAsNewFilterCommand = new RelayCommand(SaveAsNewFilterExecute);

			AgeInterval = new IntInterval<int>(0, 0);
			FriendsInterval = new IntInterval<int>(0, 0);
			SubscribersInterval = new IntInterval<int>(0, 0);
			PhotosCount = new IntInterval<int>(0, 0);
		}

		private void SaveAsNewFilterExecute(object obj)
		{
			var error = "";
			SearchFiltersContextWrapper.CreateNewFilter(ConverToSearchFilterInfo(), out error);

			if (error != "")
				MessageBox.Show(error);
			else
				UpdateSearchFiltersList();
		}

		private void UpdateSearchFiltersList()
		{
			this.AllFilters.Clear();

			var error = "";

			var dbFilters = SearchFiltersContextWrapper.GetAllSearchFilters(out error);

			dbFilters.ForEach(x => AllFilters.Add(x));

			OnPropertyChanged("AllFilters");
		}

		private SearchFilterInfo ConverToSearchFilterInfo()
		{
			var info = new SearchFilterInfo();

			info.FilterName = FilterName;

			if (SexHasValue)
				info.Sex = Sex;

			if (FamilyStateHasValue)
				info.FamilyState = FamilyState;

			if (AgeIntervalHasValue)
			{
				info.YearMin = AgeInterval.MinYear;
				info.YearMax = AgeInterval.MaxYear;
			}

			if (FriendsIntervalHasValue)
			{
				info.FriendsMin = AgeInterval.MinYear;
				info.FriendsMax = AgeInterval.MaxYear;
			}

			if (SubscribersIntervalHasValue)
			{
				info.SubscribersMin = SubscribersInterval.MinYear;
				info.SubscribersMax = SubscribersInterval.MaxYear;
			}

			info.HasPhoto = HasPhoto;
			info.IsOnline = IsOnline;

			info.Count = Count;

			return info;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}

	public class StringToSexEnumConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SexEnum res;

			if (Enum.TryParse(value.ToString(), out res) == false)
				throw new Exception("Ошибка конвертации : " + value.ToString());

			return res;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Enum.Parse(targetType, (value as ComboBoxItem).Content.ToString());
		}
	}

	public class StringToFamilyStateEnumConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			FamilyState res;

			if (Enum.TryParse(value.ToString(), out res) == false)
				throw new Exception("Ошибка конвертации : " + value.ToString());

			return res;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var str = (value as ComboBoxItem).Content.ToString();

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
	}
}
