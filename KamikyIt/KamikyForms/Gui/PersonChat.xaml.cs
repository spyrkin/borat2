using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using Chat.Core;

namespace Chat.Gui
{
    /// <summary>
    /// Логика взаимодействия для PersonChat.xaml
    /// </summary>
    public partial class PersonChat : Canvas
    {

        public int top;
        public int left;
        public int height = 282;
        public int width = 137;
        public bool isMin = true;
        public ChatWindow ch;
        public long _v;
        public string _s;
        public long personId
        {
            get
            {
                return _v; 
                
            }
            set
            {
                _v = value;
                
            }
        }    //vkId

        public string personChatId
        {
            get { return _s; }
            set
            {
                _s = value;
            }
        }

        public List<ChatMessage> chatMessages = new List<ChatMessage>();

        public PersonChat()
        {
            InitializeComponent();
            profileImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/notFound2.png" ));
            profileChatNumber.Content = personChatId;


        }

        public void wire(ChatWindow ch)
        {
            this.ch = ch;
        }


        public void normalize()
        {
            Canvas.SetTop(this, top);
            Canvas.SetLeft(this, left);
            this.Height = height;
            this.Width = width;
            Canvas.SetZIndex(this, 1);
            Background = Brushes.DarkGray;


            //max
            Canvas.SetTop(maxLabel, -5);
            Canvas.SetLeft(maxLabel, 115);
            maxLabel.FontSize = 12;

            //image
            profileImage.Width = 40;
            profileImage.Height = 40;

            //name
            Canvas.SetTop(profileName, 0);
            Canvas.SetLeft(profileName, 40);
            profileName.FontSize = 8;


            //age
            Canvas.SetTop(profileAge, 14);
            Canvas.SetLeft(profileAge, 40);
            profileAge.FontSize = 8;

            //profileLastTimeAnswer
            Canvas.SetTop(profileChatNumber, 28);
            Canvas.SetLeft(profileChatNumber, 40);
            profileChatNumber.FontSize = 8;

            //datagrid
            Canvas.SetTop(datagrid, 50);
            datagrid.Width = 128;
            datagrid.Height = 210;

			//bmessage
			Canvas.SetTop(bmessage, 260);
			Canvas.SetLeft(bmessage, 0);
			bmessage.Width = 45;
			bmessage.Height = 10;

			//bmessage
			Canvas.SetTop(bwrite, 260);
			Canvas.SetLeft(bwrite, 45);
			bwrite.Width = 45;
			bwrite.Height = 10;

			//bclose
			Canvas.SetTop(bclose, 260);
			Canvas.SetLeft(bclose, 92);
			bclose.Width = 45;
			bclose.Height = 10;



		}

		public void maximaze()
        {

            Canvas.SetTop(this, 0);
            Canvas.SetLeft(this, 0);
            this.Height = ch.Height -40;
            this.Width = ch.Width - 15;
            Canvas.SetZIndex(this, 10);
            Background = Brushes.Gray;


            //max
            Canvas.SetTop(maxLabel, -5);
            Canvas.SetLeft(maxLabel, this.Width - 55);
            maxLabel.FontSize = 20;

            //image
            profileImage.Width = 200;
            profileImage.Height = 200;

            //name
            Canvas.SetTop(profileName, 0);
            Canvas.SetLeft(profileName, 240);
            profileName.FontSize = 20;

            //age
            Canvas.SetTop(profileAge, 50);
            Canvas.SetLeft(profileAge, 240);
            profileAge.FontSize = 20;

            //profileLastTimeAnswer
            Canvas.SetTop(profileChatNumber, 100);
            Canvas.SetLeft(profileChatNumber, 240);
            profileChatNumber.FontSize = 20;


            //datagrid
            Canvas.SetTop(datagrid, 200);
            datagrid.Width = Width - 10;
            datagrid.Height = 600;

			//bmessage
			Canvas.SetTop(bmessage, Height - 55);
			Canvas.SetLeft(bmessage, 10);
			bmessage.Width = 100;
			bmessage.Height = 50;

			//bmessage
			Canvas.SetTop(bwrite, Height - 55);
			Canvas.SetLeft(bwrite, 200);
			bwrite.Width = 100;
			bwrite.Height = 50;

			//bclose
			Canvas.SetTop(bclose, Height - 55);
			Canvas.SetLeft(bclose, 380);
			bclose.Width = 100;
			bclose.Height = 50;
		}

        private void onChangeState(object sender, RoutedEventArgs e)
        {
            isMin = !isMin;
            changeState();
        }

        private void changeState()
        {
            if (isMin)
            {
                normalize();
            }
            else
            {
                maximaze();
            }
        }



		public void writeMsg(Object sender,
					   EventArgs e)
		{
			SimpleMessageBox box = new SimpleMessageBox();
			var res = box.ShowDialog();
			if (res == true)
			{
			    ChatTask t = new ChatTask();
			    t.type = Chat.Core.TaskEnum.MESSAGE;
			    t.message = box.msg;
			    t.vkId = personId;
			    t.timeExpared = ch.te.setTime(5);
			    t.personChatId = personChatId;
			    t.isStopped = false;
			    t.personName = ch.CurrentUser.Value;
			    ch.tasks.Add(t);
			    ch.updateTaskList();

                //ch.createTaskMessage(box.msg, personId, localDate);
            }


        }


        public void sendVirtualMessage(ChatTask task)
        {
            ChatMessage message = new ChatMessage();
            message.isVirtual = true;
            message.message = task.message;
            message.isBot = true;
            message.personChatId = personChatId;
            message.time = DateTime.Now;
	        message.vkId = ch.CurrentUser.Key;
            message.personName = task.personName;
            chatMessages.Add(message);
            UpdateUi();

        }

        private void UpdateUi()
        {
            datagrid.ItemsSource = chatMessages.OrderBy(o => o.time);
            datagrid.Items.Refresh();

        }

	    public void updateMessage(List<string[]> messages)
	    {
		    ChatMessage firstMessage = chatMessages.First();
		    int i = 0;
			List<String[]> receved = new List<string[]>();
		    foreach (String[] mess in messages)
		    {
			    string message = mess[0];
			    string recever = mess[1];
			    string sdate = mess[2];
			    if (recever == firstMessage.vkId.ToString() && message == firstMessage.message)
			    {
				    receved.Add(mess);
					break;
			    }
			    receved.Add(mess);

				i++;
		    }
			chatMessages.Clear();

		    foreach (String[] rec in receved)
		    {

				string message = rec[0];
			    string recever = rec[1];
			    string sdate = rec[2];

			    string pName = recever == ch.CurrentUser.Key.ToString() ? ch.CurrentUser.Value : this.profileName.Content.ToString();

				ChatMessage newmessage = new ChatMessage();
			    newmessage.isVirtual = false;
			    newmessage.message = message;
			    newmessage.isBot = recever == ch.CurrentUser.Key.ToString();
			    newmessage.personChatId = personChatId;
			    newmessage.time = DateTime.Parse(sdate);
				newmessage.vkId = Convert.ToInt64(recever);
			    newmessage.personName = pName;
			    chatMessages.Add(newmessage);
			}
		    chatMessages = chatMessages.OrderBy(o => o.time).ToList();
		    UpdateUi();

		}

        private void update(object sender, RoutedEventArgs e)
        {
            ch.te.addUpdateTask(personChatId, 1);
        }
    }
}
