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
using KamikyForms.Gui;

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

	    private void wireAdvisec()
	    {
	        List<AdviceControl> advControls =
	            GetVisualChilds<AdviceControl>(this as DependencyObject);
	        foreach (AdviceControl ac in advControls)
	        {
	            ac.wire(ac.Tag, textblock, resevers);
	            ac.setResourcec();

	        }
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


        private void setName()
	    {

	        string name = "SEND TO: ";
            foreach (PersonChat pc in resevers)
            {
                name = name + pc.personChatId + ", ";
            }
	        name = name.Substring(0, name.Length - 2);
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

	    private void SimpleMessageBox_OnLoaded(object sender, RoutedEventArgs e)
	    {
	        wireAdvisec();
	    }
    }
}
