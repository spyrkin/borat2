using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Chat.Gui;
using VkNet.Examples.ForChat;

namespace Chat.Core
{

    //будет выполнять таски
    public class TaskExecuter
    {
        public DispatcherTimer timerExecute;
        public ChatWindow ch;
        public void wire(ChatWindow ch)
        {
            this.ch = ch;
        }

        public void init()
        {
            timerExecute = new DispatcherTimer();
            timerExecute.Interval = System.TimeSpan.FromMilliseconds(300);
            timerExecute.Tick += timerExecute_Tick;
            timerExecute.Start();
        }

        private void timerExecute_Tick(object sender, EventArgs e)
        {
            List<ChatTask> ct = ch.tasks;
            lock (ct)
            {
                List<ChatTask> rt = ct.Where(o => !o.isStopped && o.sekExpared < 0).ToList();
                if (rt.Count > 0)
                {
                    //выполняем
                    executeTasks(rt);
                    //удаляем
                    deleteTasks(rt);
                    //обновляем task дшые
                    ch.updateTaskList();
                }
            }
        }

        private void deleteTasks(List<ChatTask> rt)
        {
            foreach (ChatTask task in rt)
            {
                ch.tasks.Remove(task);
            }
        }

        private void executeTasks(List<ChatTask> rt)
        {
            //типа выполняем
            foreach (ChatTask task in rt)
            {
                if (task.type == TaskEnum.MESSAGE)
                {
				    //ChatCoreHelper.WriteMessage(task.vkId, task.message);
                    //шлем непроверенное сообщение
                    PersonChat pchat = ch.getPersonChat(task.personChatId);
                    pchat.sendVirtualMessage(task);
	                addUpdateList(task);
                }
	            if (task.type == TaskEnum.UPDATE)
	            {
		            //List<string[]> messages = ChatCoreHelper.GetMessagesFromUser(task.vkId);
		            //PersonChat pchat = ch.getPersonChat(task.personChatId);
					//pchat.updateMessage(messages);
				}
			}
        }

	    private void addUpdateList(ChatTask task)
	    {
		    DateTime localDate = DateTime.Now;
			ChatTask t = new ChatTask();
		    t.type = Chat.Core.TaskEnum.UPDATE;
		    t.message = "UPDATE";
		    t.vkId = task.vkId;
		    t.timeExpared = localDate.AddSeconds(30);
		    t.personChatId = task.personChatId;
		    t.isStopped = false;
		    t.personName = ch.CurrentUser.Value;
		    ch.tasks.Add(t);
			ch.updateTaskList();
		}
    }

}
