using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamikyForms.Core
{
    public class ErrorMessage
    {

        public DateTime time;

        public string TimeString
        {
            get { return TimeToString(time); }
        }

        public string reason { get; set; }

        public string message { get; set; }

        public String TimeToString(DateTime time)
        {
            return String.Format("{0}:{1}", time.Hour, time.Minute);
        }
    }
}
