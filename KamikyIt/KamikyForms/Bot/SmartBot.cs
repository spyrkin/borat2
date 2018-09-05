using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Chat.Core;
using Chat.Gui;

namespace KamikyForms.Bot
{

    // умный бот
    public class SmartBot
    {
        public PersonChat pc;
        private List<ChatMessage> messages;

        public SmartBot(PersonChat pc)
        {
            this.pc = pc;
        }
        
        public void findAnswer()
        {

           messages = pc.chatMessages;
           if (messages.Count < 2)
           {
              return;
           }
           ChatMessage mess = messages.Last();
           if (mess.isBot == true)
           {
              return;
           } 
           //пришел ответ от няши!!!!
           MessageBox.Show("лолка");
        }


    }
}
