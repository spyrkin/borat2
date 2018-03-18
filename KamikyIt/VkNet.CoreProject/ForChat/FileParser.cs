using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkNet.Examples.ForChat
{
    public static class FileParser
    {
        public static List<String> getBans()
        {
            List<String> bans = new List<string>();
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

        public static string getLogPath()
        {

            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Log\\";


        }


        public static List<String> getAnswer()
        {
            List<String> bans = new List<string>();
            string path = getAnswerFilePath();
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
            return root_path + "Data\\banlist.txt";

        }





        public static string getAnswerFilePath()
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Data\\answer_databse.txt";

        }

        public static void setBanList(List<String> domains)
        {
            string path = getBanFilePath();
            foreach (String s in domains)
            {
                File.AppendAllText(path, s + "\r\n");
            }
        }
    }
}
