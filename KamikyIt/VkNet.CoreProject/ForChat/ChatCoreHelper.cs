using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model.RequestParams;

namespace VkNet.Examples.ForChat
{
	public static class ChatCoreHelper
	{
		public static VkApi api { get; set; }

		public static void SetVkApiInstance(VkApi instance)
		{
			api = instance;	
		}

	    public static long WriteMessage(long userId, string message)
	    {
	        var messageInfo = new MessagesSendParams()
	        {
	            Message = message,
	            UserId = userId,
	        };

	        try
	        {
	            long res = api.Messages.Send(messageInfo);
	            return res;
	        }
	        catch (System.Exception e)
	        {
	            object temp = null;

	            temp = e.GetType().GetProperty("ErrorCode").GetValue(temp);

	            return (long)temp;

	        }
	    }

        public static KeyValuePair<long, string> GetCurrentUserInfo()
		{
			return new KeyValuePair<long, string>(api.UserId.Value, api.Users.Get(new List<long>() {api.UserId.Value}).FirstOrDefault().FirstName);
		}

		/// <summary>
		/// Список. Сообщение|Id_Отправителя|Время
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static List<string[]> GetMessagesFromUser(long userId)
		{
			var history = new MessagesGetHistoryParams();
			history.UserId = userId;
			history.Reversed = false;

			history.Count = 20;

			var messages = api.Messages.GetHistory(history).Messages;

			if (messages == null || !messages.Any())
			{
				return new List<string[]>();
			}

			return api.Messages.GetHistory(history).Messages.Select(x => new string[]{x.Body, x.FromId.ToString(), x.Date.ToString()}).ToList();
		}

	}
}
