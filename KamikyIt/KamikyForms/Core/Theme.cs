using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamikyForms.Core
{

    //Тема беседы
    public class Theme
    {
        public string Name;
        public List<ThemeItem> messages = new List<ThemeItem>();
        public int curIndex;
        public List<int> expired = new List<int>();                 //сообщение которые уже написаны
 
        public class ThemeItem
        {
            public int n { get; set; }
            public string message { get; set; }
            public bool isExpired { get; set; }
        }
    }
}
