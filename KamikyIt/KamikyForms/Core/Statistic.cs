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
    }
}
