using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Chat.Core;
using Chat.Gui;
using VkNet.Examples.ForChat;

namespace KamikyForms.Core
{
    public class Logger
    {

        public DispatcherTimer timerExecute;
        public int UPDATELOGGING= 1000 * 2;  //запись всех сообщений в лог
        public ChatWindow ch;
        public string log_file_name;
        public int last_count = 0;
        public void wire(ChatWindow ch)
        {
            this.ch = ch;
        }

        public void init()
        {
            timerExecute = new DispatcherTimer();
            timerExecute.Interval = System.TimeSpan.FromMilliseconds(UPDATELOGGING);
            timerExecute.Tick += timerExecute_Tick;
            timerExecute.Start();

            DateTime date = DateTime.Now;
            log_file_name = date.ToString("MM-dd-yyyy") + ".txt";

        }

        private void timerExecute_Tick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                writeLog();

            });
        }


        public void writeLog()
        {
            string log_path = FileParser.getLogPath() + log_file_name;
            ///File f = null;

            List<ChatMessage> messagesAll = new List<ChatMessage>();
            foreach (KeyValuePair<string, PersonChat> kvp in ch.personWindows)
            {
                PersonChat pc = kvp.Value;
                List<ChatMessage> messages = pc.chatMessages;
                foreach (ChatMessage mess in messages)
                {
                    if (mess.isVirtual) continue;
                    messagesAll.Add(mess);
                }
            }
            messagesAll = messagesAll.OrderBy(o => o.time).ToList();
            int delta = messagesAll.Count - last_count;
            if (delta == 0)
            {
                return;
            }
            FileStream fs = new FileStream(log_path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            foreach (ChatMessage m in messagesAll)
            {
               sw.WriteLine("[" +m.timeToString+"]" + "   " + m.personName);
               sw.WriteLine(m.message);
               sw.WriteLine("  ");
            }
            last_count = messagesAll.Count;

            ch.addConsoleMsg("Log: +"+delta+" messages");
            sw.Close();

        }

    }
}
