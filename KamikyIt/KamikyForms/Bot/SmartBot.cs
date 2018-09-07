using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Chat.Core;
using Chat.Gui;
using KamikyForms.Core;
using VkNet.Examples.ForChat;

namespace KamikyForms.Bot
{

    // умный бот
    public class SmartBot
    {
        public PersonChat pc;
        private List<ChatMessage> messages;
        public List<Theme> themes = new List<Theme>();

        public SmartBot(PersonChat pc)
        {
            this.pc = pc;
            setThemes();
        }

        private void setThemes()
        {
            setTheme("детство");
            setTheme("друзья");
            setTheme("кино");
            setTheme("путешествия");
            setTheme("спорт");
            setTheme("увлечения");
            setTheme("отношения");

        }

        private void setTheme(string themeName)
        {
            //собираем сообщения
            List<string> mlist = FileParser.getTheme(themeName);
            Theme theme = new Theme();
            theme.Name = themeName;
            int i = 0;
            foreach (String s in mlist)
            {
                Theme.ThemeItem ti = new Theme.ThemeItem();
                ti.message = s;
                ti.n = i;
                i++;
                theme.messages.Add(ti);
            }
            themes.Add(theme);
        }

        public void findAnswer()
        {

            ChatMessage lastNotAnswered = pc.lastNotAnsweredMessage;
            if (lastNotAnswered == null)
            {
                return;
            }

            //пришел ответ от няши!!!!
            //MessageBox.Show("лолка");
        }


    }
}
