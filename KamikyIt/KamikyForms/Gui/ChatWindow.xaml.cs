using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ApiWrapper.Core;
using Chat.Core;
using MahApps.Metro.Controls;
using System.Threading;
using System.Threading.Tasks;
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
        public ChatWindow()
        {
            InitializeComponent();



        }



        private void timerSync_Tick(object sender, EventArgs e)
        {
            UpdateUI();

        }

        private void UpdateUI()
        {
            DateTime localDate = DateTime.Now;
            timer.Content = localDate.ToString("HH:mm:ss");
            //updateTaskList();
        }

        private void FillPersons()
        {
            var i = 1;
            foreach (var person in Persons)
            {
                try
                {
                    var persWindow = personWindows["person" + i++];
                    persWindow.personId = person.id.ToString();
                    persWindow.profileName.Content = person.name;
                    persWindow.profileAge.Content = person.birthDate == null ? "xxx" : person.birthDate.ToString();
                    persWindow.profileImage.Source = new BitmapImage(new Uri(person.photoUrlMax.ToString()));
                }
                catch (Exception e)
                {
                    addConsoleMsg(e.Message);
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
            int startX = 0;
            int startY = 0;

            int xSumm = 0;
            int ySumm = 0;
            foreach (KeyValuePair<string, PersonChat> kvp in personWindows)
            {

                PersonChat pc = kvp.Value;
                xSumm = startX + i * 142;
                ySumm = startY + j * 287;
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
                pc.personId = "person" + i;
            }
            Canvas.SetLeft(console, xSumm + 142);
            Canvas.SetTop(console, ySumm);
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

        public void addConsoleMsg(string msg)
        {
            DateTime localDate = DateTime.Now;
            ConsoleMessage message = new ConsoleMessage();
            message.time = localDate;
            message.message = msg;
            consoleMsg.Add(message);
            console.ItemsSource = consoleMsg;
        }




        private void loadedAction()
        {
            DateTime localDate = DateTime.Now;
            tasks.Clear();
            int i = 1;
            foreach (PersonModel person in Persons)
            {
                Random random = new Random(unchecked((int)(DateTime.Now.Ticks)));
                Thread.Sleep(12);
                int dsek = random.Next(10, 11);
                ChatTask t = new ChatTask();
                t.type = Chat.Core.TaskEnum.MESSAGE;
                t.message = "test message";
                t.vkId = person.id;
                t.timeExpared = localDate.AddSeconds(dsek);
                t.personChatId = "person" + i;
                t.isStopped = false;
                t.personName = CurrentUser.Value;
                tasks.Add(t);
                i++;
            }
            updateTaskList();
        }

        internal void sendMessage(string v, string personId, DateTime localDate)
        {
            ChatTask t = new ChatTask();
            t.type = Chat.Core.TaskEnum.MESSAGE;
            t.message = v;
            t.vkId = CurrentUser.Key;
            t.timeExpared = localDate;
            t.personChatId = personId;
            t.isStopped = false;
            tasks.Add(t);
            updateTaskList();
        }

        public void updateTaskList()
        {

            rTaskList.ItemsSource = null;

            rTaskList.ItemsSource = tasks.Where(o => !o.isStopped).OrderBy(o => o.sekExpared);
            rTaskList.Items.Refresh();

            sTaskList.ItemsSource = null;
            sTaskList.ItemsSource = tasks.Where(o => o.isStopped).OrderBy(o => o.sekExpared);

            sTaskList.Items.Refresh();
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

        private void onExpandTaskGrid(object sender, RoutedEventArgs e)
        {
            if (iconKind.Kind == PackIconModernKind.ArrowLeft)
            {
                iconKind.Kind = PackIconModernKind.ArrowRight;
                gridCanvas.Margin = new Thickness(500, 0, 0, 0);
                gridCanvas.Width = 270 + 997 - 500;
                rTaskList.Width = 270 + 997 - 500;
                sTaskList.Width = 270 + 997 - 500;
                return;
            }


            if (iconKind.Kind == PackIconModernKind.ArrowRight)
            {
                iconKind.Kind = PackIconModernKind.ArrowLeft;
                gridCanvas.Margin = new Thickness(997, 0, 0, 0);
                gridCanvas.Width = 270;
                rTaskList.Width = 270;
                sTaskList.Width = 270;

                return;
            }
        }


        private void Chat_Loaded(object sender, RoutedEventArgs e)
        {

            //Persons = SearchInstrument.Test();
            CurrentUser = ChatCoreHelper.GetCurrentUserInfo();

            //Persons.Add(new PersonModel(4259275));
            //Persons.Add(new PersonModel(5499654));
            console.ItemsSource = consoleMsg;
            resizeItems();
            timerSync = new DispatcherTimer();
            timerSync.Interval = System.TimeSpan.FromMilliseconds(1000);
            timerSync.Tick += timerSync_Tick;
            timerSync.Start();
            te.wire(this);
            te.init();
            addConsoleMsg("Loaded");

            //FillPersons();
            loadedAction();

        }

        private void onPlay(object sender, RoutedEventArgs e)
        {

            List<String> bans = new List<string>();
            foreach (PersonModel p in Persons)
            {
                string domain = p.Domain;
                bans.Add(domain);
            }
            BanList.setBanList(bans);

        }

        private void onSearch(object sender, RoutedEventArgs e)
        {
            FilterWindow form = new FilterWindow();
            var res = form.ShowDialog();
            if (res == true && form.choosenpersons.Count > 0)
            {
                Persons = form.choosenpersons;
                FillPersons();

            }
        }
    }
}
