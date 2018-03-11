using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core
{
    public class ConsoleMessage
    {
        public DateTime time;

        public string TimeString
        {
            get { return TimeToString(time); }
          
        }
        public string message { get; set; }
        public String TimeToString(DateTime time)
        {
			return String.Format("{0}:{1}", time.Hour, time.Minute);
        }
    }
}
