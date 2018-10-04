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
            //Write();
        }

        public void change()
        {

            foreach (KeyValuePair<string, PersonChat> kvp in cw.personWindows)
            {
                PersonChat chat = kvp.Value;
                if (chat.Person != null)
                {
                    changeStat(chat, "status");
                    changeStat(chat, "follower");
                    changeStat(chat, "startup");
                }
            }

        }

        private void changeStat(PersonChat ch, string _type)
        {
            List<StatisticItem> mlist = info[_type];
            int increment = ch._goodTalk == true ? 1 : 0;
            string mess = "";
            if (_type == "status")
            {
                mess = ch.Person.Relation;
            }
            if (_type == "follower")
            {
                mess = ch.Person.followers.ToString();
            }
            if (_type == "startup")
            {
                mess = ch.startUpFraze;
            }
            else
            {
                return;
            }
            StatisticItem st = mlist.FirstOrDefault(o => o.m == mess);
            if (st == null)
            {
                StatisticItem st_new = new StatisticItem();
                st_new.m = mess;
                st_new.current = increment;
                st_new.all = 1;
                st_new.percent = increment * 100 / 1;
                info[_type].Add(st_new);
            }
            else
            {
                st.current = st.current + increment;
                st.all = st.all + 1;
                st.percent = st.current * 100 / st.all;
            }

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
            change();
            foreach (KeyValuePair<string, List<StatisticItem>> kvp in info)
            {
                string key = kvp.Key;
                List<StatisticItem> value = kvp.Value;
                value = value.OrderByDescending(o => o.percent).ToList();
                string data = "";
                foreach (StatisticItem st in value)
                {
                    string per = st.percent.ToString("##.00");
                    string s = st.m + "@" + st.current + "@" + st.all + "@" + per;
                    data = data + s + "\n";
                }
                FileParser.saveStat(key, data);
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
