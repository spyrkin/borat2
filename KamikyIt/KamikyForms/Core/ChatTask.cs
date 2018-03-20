using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core
{
    public class ChatTask
    {

        public bool isStopped = false;
        public TaskEnum type;
        public string message { get; set; }
        public long vkId;
        public string personChatId { get; set; }
        public string personName { get; set; }
        public DateTime timeExpared;

        public bool isMessage
        {
            get { return type == TaskEnum.MESSAGE; }
        }


    //количество секунд до запуска
        public int sekExpared
	    {
            get
            {
                DateTime localDate = DateTime.Now;
                TimeSpan rez = timeExpared - localDate;
                return rez.Hours*60*60 + rez.Minutes*60 + rez.Seconds;

            }
        }

        //время когда произойдет запуск
        public string timeExparedToString
		{
			get
			{
				return timeExpared.ToString("HH:mm:ss");
			}
		}
		//t.type = Chat.Core.TaskEnum.MESSAGE;
		//t.message = "Привет";
		//t.vkId = 10;
		//t.timeExpared = localDate.AddSeconds(dsek);
	}
}