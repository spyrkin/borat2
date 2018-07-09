using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ApiWrapper.Core;
using Chat.Core;
using MahApps.Metro.Controls;
using System.Threading;
using System.Windows.Documents;
using KamikyForms.Bot;
using KamikyForms.Core;
using KamikyForms.Gui;
using MahApps.Metro.IconPacks;
using VkNet.Examples.ForChat;

namespace Chat.Gui
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : MetroWindow
    {
        public List<ConsoleMessage> consoleMsg = new List<ConsoleMessage>();
        public Dictionary<string, PersonChat> personWindows = new Dictionary<string, PersonChat>();
        private DispatcherTimer timerSync;
        public List<Chat.Core.ChatTask> tasks = new List<Chat.Core.ChatTask>();
        public List<PersonModel> Persons = new List<PersonModel>();
        public KeyValuePair<long, string> CurrentUser;
        public TaskExecuter te = new TaskExecuter();
        public DateTime playedTime;
        public StageEnum stage;
        public bool debug = true;
        public Bot bot;
        public Logger log = new Logger();

        public ChatWindow()
        {
            InitializeComponent();
            stage = StageEnum.INIT;
        }



        private void timerSync_Tick(object sender, EventArgs e)
        {
            UpdateUI();

        }

        public void UpdateUI()
        {
            DateTime localDate = DateTime.Now;
            timer.Content = localDate.ToString("HH:mm:ss");
            button_play.IsEnabled = stage == StageEnum.CHOSEN;
            button_search.IsEnabled = stage == StageEnum.LOADED;
            button_update_all.IsEnabled = stage == StageEnum.LAUCHED;
            int not_answered = 0;
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                ChatMessage lastNotAnswered = pc.lastNotAnsweredMessage;
                if (lastNotAnswered == null)
                {
                    pc.Background = Brushes.DarkGray;
                }
                else
                {
                    not_answered++;
                    pc.Background = Brushes.Green;
                }
            }
            if (not_answered == 0)
            {
                PageAnswered.Kind = PackIconModernKind.Page0;
            }
            if (not_answered == 1)
            {
                PageAnswered.Kind = PackIconModernKind.Page1;
            }
            if (not_answered == 2)
            {
                PageAnswered.Kind = PackIconModernKind.Page2;
            }
            if (not_answered == 3)
            {
                PageAnswered.Kind = PackIconModernKind.Page3;
            }
            if (not_answered == 4)
            {
                PageAnswered.Kind = PackIconModernKind.Page4;
            }
            if (not_answered == 5)
            {
                PageAnswered.Kind = PackIconModernKind.Page5;
            }
            if (not_answered == 6)
            {
                PageAnswered.Kind = PackIconModernKind.Page6;
            }
            if (not_answered == 7)
            {
                PageAnswered.Kind = PackIconModernKind.Page7;
            }
            if (not_answered == 8)
            {
                PageAnswered.Kind = PackIconModernKind.Page8;
            }
            if (not_answered == 9)
            {
                PageAnswered.Kind = PackIconModernKind.Page9;
            }
            if (not_answered >= 10)
            {
                PageAnswered.Kind = PackIconModernKind.PageNew;
            }

        }

        private void FillPersons()
        {
            var i = 1;
            foreach (var person in Persons)
            {
                try
                {
                    var persWindow = personWindows["person" + i++];
                    persWindow.personId = person.id;
                    persWindow.profileName.Content = person.name;
                    persWindow.profileAge.Content = person.birthDate == null ? "xxx" : person.birthDate.ToString();
                    persWindow.profileImage.Source = new BitmapImage(new Uri(person.photoUrlMax.ToString()));
                    persWindow.profileFollowers.Content = "П: " + person.followers;
                    persWindow.profileInterests.Content = "И: " + person.interests;
                    persWindow.profileCicates.Content = "Ц: " + person.Status;

                }
                catch (Exception e)
                {
                    addConsoleMsg(e.Message, true);
                }
            }

            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                pc.wire(this);
            }

        }

        private void resizeItems()
        {

            //собираема
            List<PersonChat> allChats =
                GetVisualChilds<PersonChat>(mainCanvas as DependencyObject);
            int z = 1;
            foreach (PersonChat ch in allChats)
            {
                ch.personChatId = "person" + z;
                ch.profileChatNumber.Content = ch.personChatId;
                personWindows.Add(ch.Name, ch);
                z++;
            }

            int i = 0;
            int j = 0;
            int startX = -5;
            int startY = 0;

            int xSumm = 0;
            int ySumm = 0;
            int hh = 0;
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                hh = pc.person_width + pc.person_width_margin;

                xSumm = startX + i * (pc.person_width + pc.person_width_margin);
                ySumm = startY + j * (pc.person_height + pc.person_height_margin);
                pc.left = xSumm;
                pc.top = ySumm;
                pc.normalize();
                i++;

                if (i % 7 == 0)
                {
                    j++;
                    i = 0;
                }
                pc.wire(this);
            }
            Canvas.SetLeft(console, xSumm + hh);
            Canvas.SetTop(console, ySumm);
            console.Width = hh - 5;
        }


        public static List<T> GetVisualChilds<T>(DependencyObject parent) where T : DependencyObject
        {
            List<T> childs = new List<T>();
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = VisualTreeHelper.GetChild(parent, i);
                if (v is T)
                    childs.Add(v as T);
                childs.AddRange(GetVisualChilds<T>(v));
            }
            return childs;
        }

        public void addConsoleMsg(string msg, bool isError=false)
        {
            DateTime localDate = DateTime.Now;
            ConsoleMessage message = new ConsoleMessage();
            message.time = localDate;
            message.message = msg;
            message.isError = isError;
            consoleMsg.Add(message);
            Render.DoAction(() =>
            {
                console.ItemsSource = consoleMsg;
                console.Items.Refresh();
                if (consoleMsg.Count > 0)
                {
                    console.ScrollIntoView(consoleMsg.Last());
                }
            });
            
        }




        private void SendAll(string message)
        {
            tasks.Clear();
            int i = 1;
            foreach (PersonModel person in Persons)
            {
                Thread.Sleep(12);
                ChatTask t = new ChatTask();
                t.type = Chat.Core.TaskEnum.MESSAGE;
                t.message = message;
                t.vkId = person.id;
                t.timeExpared = te.setTime(10);
                t.personChatId = "person" + i;
                t.isStopped = false;
                t.personName = CurrentUser.Value;
                tasks.Add(t);
                i++;
            }
            updateTaskList();
        }

        public void updateTaskList()
        {
            rTaskList.ItemsSource = null;
            rTaskList.ItemsSource = tasks.Where(o => !o.isStopped).OrderBy(o => o.sekExpared);
            rTaskList.Items.Refresh();
            //sTaskList.ItemsSource = null;
            //sTaskList.ItemsSource = tasks.Where(o => o.isStopped).OrderBy(o => o.sekExpared);
            //sTaskList.Items.Refresh();
        }

        private void nPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
                {

                    PersonChat pc = kvp.Value;
                    pc.isMin = true;
                    pc.normalize();
                }
            }

            if (e.Key == Key.Tab)
            {
                GNCHTA();
            }
        }
        public PersonChat getPersonChat(string personChatId)
        {
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {

                PersonChat pc = kvp.Value;
                if (pc.personChatId == personChatId)
                {
                    return pc;
                }
            }
            return null;
        }

        public PersonChat getPersonChat(long personId)
        {
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {

                PersonChat pc = kvp.Value;
                if (pc.personId == personId)
                {
                    return pc;
                }
            }
            return null;
        }

        private void gridExpand()
        {
            iconKind.Kind = PackIconModernKind.ArrowRight;
            gridCanvas.Margin = new Thickness(500, 0, 0, 0);
            gridCanvas.Width = 270 + 1092 - 500;
            rTaskList.Width = 270 + 1092 - 500;
        }

        private void gridNormalize()
        {
            iconKind.Kind = PackIconModernKind.ArrowLeft;
            gridCanvas.Margin = new Thickness(1092, 0, 0, 0);
            gridCanvas.Width = 270;
            rTaskList.Width = 270;
        }

        private void onExpandTaskGrid(object sender, RoutedEventArgs e)
        {
            if (iconKind.Kind == PackIconModernKind.ArrowLeft)
            {

                gridExpand();
                return;
            }


            if (iconKind.Kind == PackIconModernKind.ArrowRight)
            {
                gridNormalize();
                return;
            }
        }


        private void Chat_Loaded(object sender, RoutedEventArgs e)
        {

            CurrentUser = ChatCoreHelper.GetCurrentUserInfo();
            console.ItemsSource = consoleMsg;
            resizeItems();
            timerSync = new DispatcherTimer();
            timerSync.Interval = System.TimeSpan.FromMilliseconds(1000);
            timerSync.Tick += timerSync_Tick;
            timerSync.Start();
            te.wire(this);
            te.init();
            log.wire(this);
            log.init();
            bot = new Bot();
            bot.wire(this);
            stage = StageEnum.LOADED;
            addConsoleMsg("Loaded");
        }

        private void onPlay(object sender, RoutedEventArgs e)
        {

            if (stage != StageEnum.CHOSEN)
            {
                return;
            }



            string startMessage = "";
            OpenPhrase phrase = new OpenPhrase();
            phrase.ShowDialog();
            startMessage = phrase.startMessage;

            SendAll(startMessage);
            playedTime = DateTime.Now;
            stage = StageEnum.LAUCHED;
            List<String> bans = new List<string>();
            foreach (PersonModel p in Persons)
            {
                string domain = p.Domain;
                bans.Add(domain);
            }
            if (debug == false)
            {
                FileParser.setBanList(bans);
            }
        }

        private void onSearch(object sender, RoutedEventArgs e)
        {
            FilterWindow form = new FilterWindow();
            var res = form.ShowDialog();
            if (res == true && form.choosenpersons.Count > 0)
            {
                Persons = form.choosenpersons;
                FillPersons();
                stage = StageEnum.CHOSEN;


            }
        }

        private void onUpdateAll(object sender, RoutedEventArgs e)
        {
            te.updateAllChats();

        }

        private void onClosingEvent(object sender, CancelEventArgs e)
        {
            if (stage != StageEnum.LAUCHED)
            {
                e.Cancel = false;
                return;
            }
            var res = MessageBox.Show("Вы действительно хотите выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (res == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }


        //HOTKEYS
        private void mainForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                gridExpand();
            }

            if (e.Key == Key.Right)
            {
                gridNormalize();
            }
        }

        public void GNCHTA()
        {

            List<PersonChat> allchats = new List<PersonChat>();
            List<PersonChat> notanswered = new List<PersonChat>();

            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {

                allchats.Add(kvp.Value);
                if (kvp.Value.lastNotAnsweredMessage != null)
                {
                    notanswered.Add(kvp.Value);
                }
            }

            PersonChat chosen = null;
            DateTime tt = DateTime.Now;

            foreach (PersonChat pc in notanswered)
            {
                ChatMessage ms = pc.chatMessages.Last();
                DateTime dt = ms.time;
                if (chosen == null)
                {
                    chosen = pc;
                    tt = dt;
                    continue;
                }
                if (dt < tt)
                {
                    chosen = pc;
                    tt = dt;
                }
            }

            //if (chosen == null)
            //{
                
            //    return;
            //}

            //а теперь выбираем
            foreach (PersonChat pc in allchats)
            {
                if (chosen!= null && pc.personChatId == chosen.personChatId)
                {
                    pc.isMin = false;
                    pc.maximaze();
                }
                else
                {
                    pc.isMin = true;
                    pc.normalize();
                }
            }

        }

        //получает следующее окно, которое нужно ответить
        private void getNextChatToAnswer(object sender, RoutedEventArgs e)
        {

            GNCHTA();

        }


        //получили бан от чата
        public void ban(long vkId, long code)
        {

            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                if (pc.personId == vkId)
                {
                    pc.banned = true;
                    pc.isActive = false;
                    pc.bannedString = VKERROR.getErrorString(code);

                    pc.UpdateUi();
                    
                }
            }
        }


        //отправляем сообщения всем выделенным чатам
        private void sendAllMessages(object sender, RoutedEventArgs e)
        {

            List<PersonChat> resevers = new List<PersonChat>();
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                if (pc.selected && !pc.banned)
                {
                    resevers.Add(pc);
                }
            }
            if (resevers.Count == 0)
            {
                return;
            }
            //List<PersonChat> resevers = new List<PersonChat> { this };
            SimpleMessageBox box = new SimpleMessageBox(this, resevers);
            bool? res = box.ShowDialog();
        }

        private void cleanSelection(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                pc.selected = false;
                pc.UpdateUi();

            }
        }
    }
}
