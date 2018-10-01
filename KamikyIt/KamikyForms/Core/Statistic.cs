using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Gui;

namespace KamikyForms.Core
{
    public class Statistic
    {
        public ChatWindow cw;
        public Dictionary<string, List<StatisticItem>> info = new Dictionary<string, List<StatisticItem>>();

        public Statistic(ChatWindow chatWindow)
        {
            this.cw = chatWindow;
        }


        //записываем данные
        public void Write()
        {
            if (cw.debug)
            {
                return;
            }


        }

        public class StatisticItem
        {
            public string m;
            public int current;
            public int all;
            public double percent;

        }
    }
}
