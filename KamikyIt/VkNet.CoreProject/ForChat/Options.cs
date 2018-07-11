using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkNet.Examples.ForChat
{

    //ака локал сейв
    public class Options
    {
        public bool debug;
        public Hero hero;


        //загрузка init data
        public void load()
        {
            List<string> data = FileParser.getInits();
            foreach (string s  in data)
            {
                string[] tokens = s.Split(new[] { ": " }, StringSplitOptions.None);
                if (s.Contains("debug"))
                {
                    debug = tokens[1] == "1" ? true : false;
                }
                if (s.Contains("user"))
                {
                    hero = tokens[1] == "1" ? Hero.getInstance(1) : Hero.getInstance(2);
                }
            }
        }

        //сохранение init data
        public void save(bool debug, Hero hero)
        {
            FileParser.saveInits(debug, hero);
        }
    }
}
