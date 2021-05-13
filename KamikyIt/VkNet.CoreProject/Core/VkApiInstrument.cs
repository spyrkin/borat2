using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiWrapper.Core;
using VkNet.Enums.Filters;
using VkNet.Examples.ForChat;
using VkNet.Model;

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
            //6884639
            api.Authorize(new ApiAuthParams
			{

                ApplicationId = 6884639,
                Login = "spyrkin@gmail.com",
                Password = "crescent919",
                // Settings = Settings.Friends,
                TwoFactorAuthorization = () =>
                {
                    Console.WriteLine("Enter Code:");
                    return Console.ReadLine();
                }
            });


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
