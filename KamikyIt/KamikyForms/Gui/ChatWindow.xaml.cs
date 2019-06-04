using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using KamikyForms.Bot;
using KamikyForms.Core;
using KamikyForms.Gui;
using KamikyForms.Simulator;
using MahApps.Metro.IconPacks;
using VkNet.Examples.ForChat;
using Brushes = System.Windows.Media.Brushes;
using Clipboard = System.Windows.Forms.Clipboard;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

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
        public DateTime StartUpTime; //время запуска приложения
        public Statistic stat;
        public StageEnum stage;
        public bool debug;
        public Hero hero;
        public Bot bot;
        public Logger log = new Logger();

        

        public ChatWindow(bool debug, Hero hero)
        {

            this.debug = debug;
            this.hero = hero;
            InitializeComponent();
            stage = StageEnum.INIT;
            StartUpTime = DateTime.Now;
            stat = new Statistic(this);
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
                    //интересные для них
                    if (person.interests != null)
                    {
                        if (person.interests.Contains("кальян") || person.interests.Contains("дорам") ||
                            person.interests.Contains("аниме") ||
                            person.interests.Contains("дота") || person.interests.Contains("it") ||
                            person.interests.Contains("велоспорт"))
                        {
                            persWindow.profileName.Foreground = Brushes.Green;
                        }
                    }

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
            //Canvas.SetLeft(console, xSumm + hh);
            //Canvas.SetTop(console, ySumm);
            //console.Width = hh - 5;
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
                //console.ItemsSource = consoleMsg;
                //console.Items.Refresh();
                //if (consoleMsg.Count > 0)
                //{
                //    console.ScrollIntoView(consoleMsg.Last());
                //}
            });
            
        }




        private void SendAll(string message, bool isStartUp=false)
        {
            tasks.Clear();

            int i = 1;
  

            foreach (PersonModel person in Persons)
            {
                string m = message;
                if (isStartUp)
                {
                    Thread.Sleep(1);
                    Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
                    double r = rand.NextDouble();
                    if (r > 2)
                    {
                        m = "Привет, $name, скучно на работе сидеть. Давай пообщаемся 😈";

                    }

                }


                Thread.Sleep(12);
                ChatTask t = new ChatTask();
                t.type = Chat.Core.TaskEnum.MESSAGE;
                t.message = m;
                t.vkId = person.id;
                t.timeExpared = te.setTime(10);
                t.personChatId = "person" + i;
                t.isStopped = false;
                t.personName = CurrentUser.Value;

                if (isStartUp)
                {
                    PersonChat pc = personWindows[t.personChatId];
                    pc.startUpFraze = m;
                }

                string fullname = person.name;
                string[] words = fullname.Split(new string[] { " " }, StringSplitOptions.None);
                string name = words[0];


                t.message = t.message.Replace("$name", name);
                t.message = t.message.Replace("$fullname", fullname);



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
            //console.ItemsSource = consoleMsg;
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
            List<int> ss = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8};
            for (int k = ss.Count - 1; k >= 0; k--)
            {
                //var p = Persons[i];
                //if (p == null)
                //{
                //    continue;
                //}
                int j = ss.IndexOf(k);
                Console.WriteLine(k  + "  " + j + "    " +(ss.Count - 1) );
            }
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine(i);
            //}

            // writeMessage("sdfsdf", 0, null);
        }

        private void onPlay(object sender, RoutedEventArgs e)
        {

            if (stage != StageEnum.CHOSEN)
            {
                return;
            }

            string startMessage = "";
            OpenPhrase phrase = new OpenPhrase(debug);
            phrase.ShowDialog();
            startMessage = phrase.startMessage;

            SendAll(startMessage, true);
            playedTime = DateTime.Now;
            stage = StageEnum.LAUCHED;
            List<String> bans = new List<string>();
            foreach (PersonModel p in Persons)
            {
                string domain = p.id.ToString();
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
                //FillPersons();
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
            if (debug)
            {
                e.Cancel = false;
                return;
            }
            var res = MessageBox.Show("Вы действительно хотите выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (res == MessageBoxResult.Yes)
            {
                stat.Write();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }


        //получаем активный personChat
        public PersonChat getActiverChat()
        {
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {

                if (!kvp.Value.isMin)
                {
                    return kvp.Value;
                }
            }
            return null;
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
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                PersonChat ph = getActiverChat();
                if (ph == null)
                {
                    return;
                }
                if (e.Key == Key.N)
                {
                    ph.textblock.Text = ph.textblock.Text + "$name";
                }
                if (e.Key == Key.F)
                {
                    ph.textblock.Text = ph.textblock.Text + "$fullname";

                }
                if (e.Key == Key.P)
                {
                    ph.textblock.Text = ph.textblock.Text + "Хорошо, $name. Если вдруг с парнем не сложится, напиши мне потом на эту страницу, отвечу сразу же, не пожалеешь.";

                }
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


            //открыть всем в VK
            //List<PersonChat> resevers = new List<PersonChat>();
            //foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            //{
            //    PersonChat pc = kvp.Value;
            //    if (!pc.banned)
            //    {
            //        resevers.Add(pc);
            //    }
            //}
            //if (resevers.Count == 0)
            //{
            //    return;
            //}\

            if (stage != StageEnum.CHOSEN)
            {
                return;
            }



            string startMessage = "";
            OpenPhrase phrase = new OpenPhrase(debug);
            phrase.ShowDialog();
            startMessage = phrase.startMessage;
            Clipboard.SetText(startMessage);

            //SendAll(startMessage, true);
            playedTime = DateTime.Now;
            //stage = StageEnum.LAUCHED;

            //баним
            List<String> bans = new List<string>();
            List<String> bansId = new List<string>();
            foreach (PersonModel p in Persons)
            {
                string domain = p.Domain;
                bans.Add(domain);
                bansId.Add(p.id.ToString());
            }
            if (debug == false)
            {
                FileParser.setBanList(bansId);
            }

            //открываем vkокна
            int count = bans.Count;
            System.Diagnostics.Process.Start("http://google.com");
            Thread.Sleep(2000);
            prepareBrouser();



                int i = 0;
                while (i<count)
                {
                    try
                    {
                        string domain = bans[i];
                        string url = "https://vk.com/" + domain;
                        System.Diagnostics.Process.Start(url);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    int stable = 10 * 1000;
                    Random rnd = new Random();
                    int dice = rnd.Next(1, 1500);
                    Thread.Sleep(stable + dice);
                    i++;
                }
                //спим минутку перед написанием;
                Thread.Sleep(60*1000);


            for (int k = Persons.Count - 1; k >= 0; k--)
            {
                var p = Persons[k];
                if (p == null)
                {
                    continue;
                }
                int j = Persons.IndexOf(p);
                writeMessage(startMessage, j, p);
            }
            //foreach (PersonModel p in Persons)
            //{
            //    if (p == null)
            //    {
            //        continue;
            //    }
            //    int j = Persons.IndexOf(p);
            //    writeMessage(startMessage, j, p);
            //}



        }

        private void cleanSelection(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {
                PersonChat pc = kvp.Value;
                pc.selected = false;
                pc.datagrid.SelectedItem = null;
                pc.datagrid.SelectedItems.Clear();
                pc.datagrid.Items.Refresh();
                pc.UpdateUi();

            }
        }

        //открытие Tooltip for window
        private void tolTimerOpening(object sender, ToolTipEventArgs e)
        {
            DateTime time = DateTime.Now;
            TimeSpan ts = time - StartUpTime;
            string s = new DateTime(ts.Ticks).ToString("HH:mm:ss");
            tolTimer.Content = s;
        }


        public void prepareBrouser()
        {

            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(200);
                SendKeys.SendWait("^+{W}");
            }
        }

        public void writeMessage(string mess, int member, PersonModel p)
        {
            String mess2 = mess;
            mess2 = mess2.Replace("$name", p.name);
            mess2 = mess2.Replace("$fullname", p.name);
            Clipboard.Clear();
            Clipboard.SetText(mess2);

            //ждем немножно
            Thread.Sleep(getRandom(1000, 1500));
            if (member != Persons.Count - 1) //переключаемся
            {
                SendKeys.SendWait("^+{TAB}");
            }
            //двигаемся на кнопку
            Thread.Sleep(getRandom(100, 200));

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(p.photoUrl200.OriginalString);
            Bitmap bitmap;
            bitmap = new Bitmap(stream);
            int height = 420 + bitmap.Height - 243;
            System.Windows.Point point1 = new System.Windows.Point(720 + 20 - getRandom(0, 40), height + 5 - getRandom(0, 10));   //+-20  , +-10 
            MouseSimulator.LinearSmoothMove(point1, new TimeSpan(0, 0, 0, 0, getRandom(400, 500)));

            Thread.Sleep(getRandom(10, 50));

            MouseSimulator.ClickLeftMouseButton();

            Thread.Sleep(getRandom(800, 1200));

            System.Windows.Point point3 = new System.Windows.Point(770 + 20 - getRandom(0, 40), 470 + 5 - getRandom(0, 10));   //+-20  , +-10 
            MouseSimulator.LinearSmoothMove(point3, new TimeSpan(0, 0, 0, 0, getRandom(100, 200)));

            Thread.Sleep(getRandom(100, 200));
            MouseSimulator.ClickLeftMouseButton();
            Thread.Sleep(getRandom(100, 200));

            Console.WriteLine(mess);
            SendKeys.SendWait("^+{V}");
            Thread.Sleep(getRandom(200, 300));
            System.Windows.Point point2 = new System.Windows.Point(1120 + 20 - getRandom(0, 40), 580 + 10 - getRandom(0, 20));   //+-20  , +-10 

            MouseSimulator.LinearSmoothMove(point2, new TimeSpan(0, 0, 0, 0, getRandom(400, 500)));
            Thread.Sleep(getRandom(200, 300));
            MouseSimulator.ClickLeftMouseButton();
        }

        public int getRandom(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }
    }
}
