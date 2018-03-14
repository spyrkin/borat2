using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiWrapper.Core;
using JetBrains.Annotations;

namespace VkNet.Examples.DataBaseBehaviour
{
	internal class BotContext : DbContext
	{
		public BotContext() : base()
		{		
		}

		public DbSet<BotLoginPassword> LoginPasswords { get; set; }
		
		public DbSet<SearchFilterInfo> SearchFilterInfos { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("Bot");

			modelBuilder.Entity<BotLoginPassword>().ToTable("LoginPasswords");
			modelBuilder.Entity<SearchFilterInfo>().ToTable("SearchFilterInfos");

			base.OnModelCreating(modelBuilder);
		}

		#region EF костыль для DLL
		static BotContext()
		{
			// ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
			// As it is installed in the GAC, Copy Local does not work. It is required for probing.
			// Fixed "Provider not loaded" error
			var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}

		#endregion
	}



	public class BotLoginPassword
	{
		[Key]
		public int Id { get; set; }

		public string Login { get; set; }
		public string Password { get; set; }
	}

	public class SearchFilterInfo
	{
		[Key]
		public int Id { get; set; }

		public string FilterName { get; set; }

		// Количество записей, которые выдать
		public int? Count { get; set; }

		//Пол
		public SexEnum? Sex { get; set; }

		// Семейное положение
		public FamilyState? FamilyState { get; set; }


		// Возраст
		public int? YearMin { get; set; }
		public int? YearMax { get; set; }


		// Страна
		public int? CountryId { get; set; }

		// Город
		public int? CityId { get; set; }

		// Количество друзей
		public int? FriendsMin { get; set; }
		public int? FriendsMax { get; set; }

		// Количество пиздолизов.
		public int? SubscribersMin { get; set; }
		public int? SubscribersMax { get; set; }

		// Дата регистрации
		//public DateTime RegistrationDate { getBans; set; }

		// Сейчас онлайн
		public bool? IsOnline { get; set; }

		public int? PhotosMin { get; set; }
		public int? PhotosMax { get; set; }

		// С фото
		public bool? HasPhoto { get; set; }

		public override string ToString()
		{
			return FilterName;
		}
	}

	public static class BotContextWrapper
	{
		public static void CreateLoginPassword(string login, string password, out string error)
		{
			try
			{
				using (var ctx = new BotContext())
				{
					var existEntity = ctx.LoginPasswords.FirstOrDefault(x => x.Login == login);

					if (existEntity != null)
					{
						error = "Логин : " + login + " уже существует.";
						return;
					}

					var entity = new BotLoginPassword()
					{
						Login = login,
						Password = password,
					};

					ctx.LoginPasswords.Add(entity);

					ctx.SaveChanges();
				}

				error = "";
			}
			catch (System.Exception e)
			{
				error = e.Message;
			}
			

		}

		public static void ChangeLoginPassword(string login, string password, out string error)
		{
			try
			{
				using (var ctx = new BotContext())
				{
					var entity = ctx.LoginPasswords.FirstOrDefault(x => x.Login == login);

					if (entity == null)
					{
						error = "Логин : " + login + " отсутствует.";
						return;
					}

					entity.Password = password;

					ctx.SaveChanges();
				}

				error = "";
			}
			catch (System.Exception e)
			{
				error = e.Message;
			}

		}

		public static List<KeyValuePair<string, string>> GetAllLoginsPasswords()
		{
			using (var ctx = new BotContext())
			{
				var entities = ctx.LoginPasswords;

				if (entities.Any())
					return entities.ToList().Select(x => new KeyValuePair<string, string>(x.Login, x.Password)).ToList();

				return new List<KeyValuePair<string, string>>();
			}
		}

	}

	public static class SearchFiltersContextWrapper
	{
		public static void CreateNewFilter(SearchFilterInfo createSearchFilterInfo, out string error)
		{
			try
			{
				using (var ctx = new BotContext())
				{
					var all = ctx.SearchFilterInfos;

					if (all.Any())
					{
						var existing = ctx.SearchFilterInfos.ToList();

						if (existing.Any(x => x.FilterName == createSearchFilterInfo.FilterName))
						{
							error = "Фильтр с таким именем уже существует";
							return;
						}
					}

					ctx.SearchFilterInfos.Add(createSearchFilterInfo);

					ctx.SaveChanges();
				}

				error = "";
			}
			catch (System.Exception e)
			{
				error = e.Message;
			}
		}

		public static List<SearchFilterInfo> GetAllSearchFilters(out string error)
		{
			try
			{
				using (var ctx = new BotContext())
				{
					error = "";
					return ctx.SearchFilterInfos.ToList();
				}
			}
			catch (System.Exception e)
			{
				error = e.Message;
				return null;
			}
		}
	}
}
