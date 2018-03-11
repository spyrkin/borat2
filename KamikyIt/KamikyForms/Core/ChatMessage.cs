using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core
{
    public class ChatMessage
    {
        public string message { get; set; }
        public string personChatId { get; set; }
        public DateTime time { get; set; }
        public bool isVirtual { get; set; } = false;
        public long vkId { get; set; }
        public bool isBot { get; set; }
        public string personName { get; set; }


        public string timeToString
        {
            get
            {
                return time.ToString("HH:mm:ss");
            }
        }
    }
}
