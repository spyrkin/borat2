using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkNet.Examples.ForChat
{
    public static class BanList
    {
        public static List<String> get()
        {
            List < String > bans = new List<string>();
            string path = getBanFilePath();
            StreamReader fs = new StreamReader(path);
            string s = "";
            while (s != null)
            {
                s = fs.ReadLine();
                if (!String.IsNullOrEmpty(s))
                {
                    bans.Add(s);
                }
            }
            fs.Close();
            return bans;
        }

        public static string getBanFilePath()
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "banlist.txt";

        }

        public static void setBanList(List<String> domains)
        {
            string path = getBanFilePath();
            foreach (String s in domains)
            {
                File.AppendAllText(path, s +"\r\n");
            }
        }
    }
}
