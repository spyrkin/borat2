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

        public class ThemeItem
        {
            public int n;
            public string message;
        }
    }
}
