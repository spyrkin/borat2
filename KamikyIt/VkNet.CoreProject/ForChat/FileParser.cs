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


        public static List<String> getStartUps()
        {
            List<String> startups = new List<string>();
            string path = getStartUpFilePath();
            StreamReader fs = new StreamReader(path);
            string s = "";
            while (s != null)
            {
                s = fs.ReadLine();
                if (!String.IsNullOrEmpty(s))
                {
                    startups.Add(s);
                }
            }
            fs.Close();
            return startups;
        }


        public static List<String> getInits()
        {
            List<String> data = new List<string>();
            string path = getInitFilePath();
            StreamReader fs = new StreamReader(path);
            string s = "";
            while (s != null)
            {
                s = fs.ReadLine();
                if (!String.IsNullOrEmpty(s))
                {
                    data.Add(s);
                }
            }
            fs.Close();
            return data;
        }







        public static string getLogPath()
        {

            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Log\\";


        }

        public static List<String> getAdvise(string filename)
        {
            List<String> advices = new List<string>();
            string path = getAdviseFilePath(filename);
            StreamReader fs = new StreamReader(path);
            string s = "";
            while (s != null)
            {
                
                s = fs.ReadLine();
                if (String.IsNullOrEmpty(s))
                {
                    break;
                }
                advices.Add(s);
            }
            fs.Close();
            return advices;
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

        public static string getAdviseFilePath(String name)
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Data\\"+name+".txt";

        }

        public static string getBanFilePath()
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Data\\" + Hero.instance.log_name;
        }


        public static string getStartUpFilePath()
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Data\\startup.txt";
        }


        public static string getInitFilePath()
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);
            return root_path + "Data\\init.txt";
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

        public static void WriteStartUps(string message)
        {
            string path = getStartUpFilePath();
            File.AppendAllText(path, "\r\n"+ message);
        }

        public static void saveInits(bool debug, Hero hero)
        {
            string path = getInitFilePath();
           // string data = "debug: " + Convert.ToInt32(debug) + "\r\n" + "user: " + hero.id;
            StreamWriter sw = new StreamWriter(path);

            //Write a line of text
            sw.WriteLine("debug: " + Convert.ToInt32(debug));
            sw.WriteLine("user: " + hero.id);
            sw.Close();
        }
    }
}
