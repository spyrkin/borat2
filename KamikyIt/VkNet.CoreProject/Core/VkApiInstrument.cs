using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiWrapper.Core;
using VkNet.Enums.Filters;
using VkNet.Examples.ForChat;

namespace VkNet.Examples.Core
{
	public static class VkApiInstrument
	{
		static VkApiInstrument()
		{
			OnAuthorization += SetVkApiInstrumentProfile;
			OnAuthorization += SetSearchInstrumentProfile;
			OnAuthorization += SetChatCoreInstance;
		}

		private static void SetChatCoreInstance(object sender, KeyValuePair<string, string> e)
		{
			ChatCoreHelper.SetVkApiInstance(api);
		}

		private static void SetVkApiInstrumentProfile(object sender, KeyValuePair<string, string> e)
		{
			api = new VkNet.VkApi();

<<<<<<< HEAD
			api.Authorize(new ApiAuthParams
			{
				ApplicationId = 6394527,
				Login = e.Key,
				Password = e.Value,
				//Settings = Settings.All,
				TwoFactorAuthorization = () =>
				{
					Console.WriteLine("Enter Code:");
					return Console.ReadLine();
				}
			});
=======

            //6884639
            var apiparam = new ApiAuthParams
		    {
		        ApplicationId = 6394527,
		        Login = e.Key,
		        Password = e.Value,
		        Settings = Settings.All,
		        TwoFactorAuthorization = () =>
		        {
		            Console.WriteLine("Enter Code:");
		            return Console.ReadLine();
		        }
		    };

		    try
		    {
		        api.Authorize(apiparam);
		    }
            catch(System.Exception ex)
		    {
		        
		    }
>>>>>>> 9a11317e83b5ad98e66dd9424f23e32c29966dfe
		}

		private static VkApi api;

		private static void SetSearchInstrumentProfile(object sender, KeyValuePair<string, string> e)
		{
			SearchInstrument.SetAuthorization(api);
		}

		public static bool Login( string login, string password, out string error)
		{
			try
			{
				OnAuthorization(null, new KeyValuePair<string, string>(login, password));
				error = "";
				return true;
			}
			catch (System.Exception e)
			{
				error = e.Message;
				return false;
			}
		}

		private static EventHandler<KeyValuePair<string, string>> OnAuthorization;
	}
}
