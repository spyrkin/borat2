using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationsProject
{
	public static class ConfigurationManager
	{
		public static string LoginPasswordFile { get; private set; }

		static ConfigurationManager()
		{
			LoginPasswordFile = @"D:\Programms\Magnetto\Configurations\LoginPasswords.txt";
		}

		public static List<KeyValuePair<string, string>> ReadLoginPassFile(string filename)
		{
			var result = new List<KeyValuePair<string, string>>();

			if (File.Exists(filename) == false)
				return result;

			var lines = File.ReadAllLines(filename);

			foreach (var line in lines)
			{
				var parts = line.Split('\t');

				if (parts.Length != 2)
					continue;

				var login = parts[0];

				var pass = parts[1];

				result.Add(new KeyValuePair<string, string>(login, pass));
			}

			return result;
		}

		public static bool AddNewLoginPassword(string login, string pass, out string error)
		{
			if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(login))
			{
				error = "Пустой логин нельзя добавлять";

				return false;
			}

			if (string.IsNullOrWhiteSpace(pass) || string.IsNullOrEmpty(pass))
			{
				error = "Пустой пароль нельзя добавлять";

				return false;
			}

			var pairs = ReadLoginPassFile(LoginPasswordFile);
			
			if (pairs.Any(x => x.Key == login))
			{
				error = "Такой логин уже существует : " + login;

				return false;
			}

			pairs.Add(new KeyValuePair<string, string>(login, pass));

			RewritePairsToFile(LoginPasswordFile, pairs, out error);

			return true;
		}

		private static void RewritePairsToFile(string filename, List<KeyValuePair<string, string>> pairs, out string error)
		{
			try
			{
				File.Delete(filename);

				var lines = pairs.Select(x => string.Format("{0}\t{1}", x.Key, x.Value)).ToArray();

				File.Create(filename).Close();

				File.WriteAllLines(filename, lines);

				error = "";
			}
			catch (Exception e)
			{
				error = e.Message;
			}
		}

		public static bool ChangePass(string login, string pass, out string error)
		{
			if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(login))
			{
				error = "Пустой логин нельзя добавлять";
				return false;
			}

			if (string.IsNullOrWhiteSpace(pass) || string.IsNullOrEmpty(pass))
			{
				error = "Пустой пароль нельзя добавлять";
				return false;
			}

			var pairs = ReadLoginPassFile(LoginPasswordFile);

			var selectedPair = pairs.FirstOrDefault(x => x.Key == login);

			if (string.IsNullOrEmpty(selectedPair.Key))
			{
				error = "Пользователь с таким логином не найден : " + login;
				return false;
			}

			var index = pairs.IndexOf(selectedPair);
			pairs.RemoveAt(index);

			pairs.Insert(index, new KeyValuePair<string, string>(login, pass));

			RewritePairsToFile(LoginPasswordFile, pairs, out error);

			return true;
		}
	}
}
