using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Chat.Gui;
using ConfigurationsProject;
using KamikyForms.Annotations;
using VkNet.Examples.Core;
using VkNet.Examples.DataBaseBehaviour;

namespace KamikyForms
{
	public class LoginFormViewModel : INotifyPropertyChanged
	{
		private string _loginSelected;

		public string Login { get; set; }
		public string Password { get; set; }

		public string Error
		{
			get { return _error; }
			set
			{
				_error = value;
				OnPropertyChanged("Error");
			}
		}

		public string LoginSelected
		{
			get { return _loginSelected; }
			set
			{
				_loginSelected = value;
				Login = _loginSelected;
				Password = GetPasswordForLogin(Login);

				OnPropertyChanged("Login");
				OnPropertyChanged("Password");
			}
		}

		private string GetPasswordForLogin(string login)
		{
			var pass = _loginPassPairs.FirstOrDefault(x => x.Key == login);

			if (string.IsNullOrEmpty(pass.Key))
				return "";

			return pass.Value;
		}

		public ObservableCollection<string> LoginsList { get; set; }
		private List<KeyValuePair<string, string>> _loginPassPairs;
		private string _error;
		private LoginWindow _loginWindow;

		public ICommand LoginCommand { get; set; }
		public ICommand CreateCommand { get; set; }
		public ICommand ChangePassCommand { get; set; }

		public LoginFormViewModel()
		{
			LoginsList = new ObservableCollection<string>();

			UpdateLoginPassList();

			Login = LoginsList.FirstOrDefault();

			Password = GetPasswordForLogin(Login);
			
			LoginCommand = new RelayCommand(LoginExecute, LoginCanExecute);

			CreateCommand = new RelayCommand(CreateExecute, LoginCanExecute);

			ChangePassCommand = new RelayCommand(ChangePassExecute, LoginCanExecute);
		}

		private void UpdateLoginPassList()
		{
			//_loginPassPairs = ConfigurationManager.ReadLoginPassFile(ConfigurationManager.LoginPasswordFile);
			_loginPassPairs = BotContextWrapper.GetAllLoginsPasswords();

			LoginsList.Clear();
			
			_loginPassPairs.ForEach(x => LoginsList.Add(x.Key));

			OnPropertyChanged("LoginsList");
		}

		private void ChangePassExecute(object obj)
		{
			if (MessageBox.Show("Вы уверены, что хотите изменить Логин и Пароль на : " + Login + " : " + Password, "Изменение пароля", MessageBoxButton.YesNoCancel) ==
			    MessageBoxResult.Yes)
			{
				var error = "";

				BotContextWrapper.ChangeLoginPassword(Login, Password, out error);
				//if (ConfigurationManager.ChangePass(Login, Password, out error))
				//	UpdateLoginPassList();

				Error = error;

				UpdateLoginPassList();
			}
		}

		private void CreateExecute(object obj)
		{
			var error = "";

			BotContextWrapper.CreateLoginPassword(Login, Password, out error);

			//if (ConfigurationManager.AddNewLoginPassword(Login, Password, out error))
			//	UpdateLoginPassList();

			Error = error;

			UpdateLoginPassList();
		}

		private bool LoginCanExecute(object obj)
		{
			return 
				!string.IsNullOrEmpty(Login) && !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrEmpty(Password) &&
				!string.IsNullOrWhiteSpace(Password);
		}

		private void LoginExecute(object obj)
		{
			var error = "";
			if (VkApiInstrument.Login(Login, Password, out error) == false)
			{
				Error = error;
				return;
			}

			var chatWindow = new ChatWindow();

			chatWindow.Show();

			//var searchFilterWindow = new SearchFilterWindow();

			//searchFilterWindow.Show();

			_loginWindow.Close();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void SetWindow(LoginWindow loginWindow)
		{
			this._loginWindow = loginWindow;
		}
	}
}