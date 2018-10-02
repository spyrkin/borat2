using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Gui;
using VkNet.Examples.ForChat;

namespace KamikyForms.Core
{
    public class Statistic
    {
        public ChatWindow cw;
        public Dictionary<string, List<StatisticItem>> info = new Dictionary<string, List<StatisticItem>>();

        public Statistic(ChatWindow chatWindow)
        {
            this.cw = chatWindow;
            load();
        }

        //грузим данные
        private void load()
        {
            load("status");
            load("follower");
            load("startup");

        }

        private void load(string name)
        {
            List<string> mlist = FileParser.LoadStatData(name);
            info.Add(name, new List<StatisticItem>());
            var siList = info[name];
            foreach (String s in mlist)
            {
                StatisticItem si = new StatisticItem();
                si.data = s;
                string[] words = s.Split(new string[] { "@" }, StringSplitOptions.None);
                si.m = words[0];
                si.current = Convert.ToInt32(words[1]);
                si.all = Convert.ToInt32(words[2]);
                si.percent = Convert.ToDouble(words[3].Replace(".",","));
                siList.Add(si);
            }
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
            public string data;
            public string m;
            public int current;
            public int all;
            public double percent;

        }
    }
}
