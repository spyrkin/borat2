using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chat.Gui;
using KamikyForms.Core;
using VkNet.Examples.ForChat;

namespace KamikyForms.Bot
{
    public class Bot
    {

        Dictionary<string, List<string>> answers = new Dictionary<string, List<string>>();
        List<string> lines = new List<string>();
        public bool loaded = false;
        public ChatWindow cw;

        public void wire(ChatWindow cw)
        {
            this.cw = cw;
            LoadDataTask();
        }

        public void LoadDataTask()
        {
            Task.Factory.StartNew(() =>
            {
                loadAction();
                
                cw.addConsoleMsg("Bot loaded: "+ answers.Keys.Count);

            });
        }


        public void loadAction()
        {
            lines = FileParser.getAnswer();
            foreach (string s in lines)
            {
                string[] words = s.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 2) continue;
                string w1 = BotHelper.prepareString(words[0]);
               
                string w2 = words[1];
                if (answers.ContainsKey(w1))
                {
                    answers[w1].Add(w2);
                }
                else
                {
                    answers.Add(w1, new List<string>() { w2 });
                }
            }
        }


        public List<String> getMessages(String message)
        {
            if (answers.ContainsKey(BotHelper.prepareString(message)))
            {
                return answers[message];
            }
            else
            {
                return new List<string>();
            }
        }


    }
}
