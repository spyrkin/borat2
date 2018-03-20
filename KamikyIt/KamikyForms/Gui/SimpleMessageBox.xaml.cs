using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Chat.Gui
{
	/// <summary>
	/// Логика взаимодействия для SimpleMessageBox.xaml
	/// </summary>
	public partial class SimpleMessageBox : Window
	{
	    public ChatWindow ch;
	    private List<PersonChat> resevers;
        public string msg;
		public SimpleMessageBox(ChatWindow ch, List<PersonChat> resevers)
		{
            this.ch = this.ch;
		    this.resevers = resevers;
			InitializeComponent();
		    setName();

		}

	    private void setName()
	    {

	        string name = "";
            foreach (PersonChat pc in resevers)
            {
                name = name + pc.personChatId + ",";
            }
	        name = name.Substring(0, name.Length - 1);
	        whois.Content = name;
	    }

		private void onSubmit(object sender, RoutedEventArgs e)
		{
			msg = textblock.Text;
		    if (String.IsNullOrEmpty(msg))
		    {
		        Close();
		        return;
		    }
		    foreach (PersonChat pc in resevers)
		    {
	        
                pc.writeMyMessage(msg);

		    }
		    Close();
		    return;

        }
	}
}
