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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KamikyForms
{
    /// <summary>
    /// Логика взаимодействия для IntervalControl.xaml
    /// </summary>
    public partial class IntervalControl : UserControl
	{
        public IntervalControl()
        {
            InitializeComponent();
        }


	    public bool HasValue
	    {
		    get { return (bool) GetValue(HasValueProperty); }
			set { SetValue(HasValueProperty, value);}
	    }

		public string Caption
		{
			get { return (string) GetValue(CaptionProperty); }
			set { SetValue(CaptionProperty, value); }
		}

		public int MinSliderValue
		{
			get { return (int)GetValue(MinSliderValueProperty); }
			set { SetValue(MinSliderValueProperty, value); }
		}

		public int MaxSliderValue
		{
			get { return (int)GetValue(MaxSliderValueProperty); }
			set { SetValue(MaxSliderValueProperty, value); }
		}

		public int MinValue
		{
			get { return (int)GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}

		public int MaxValue
		{
			get { return (int)GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}

		public static readonly DependencyProperty HasValueProperty;
		public static readonly DependencyProperty MinSliderValueProperty;
		public static readonly DependencyProperty MaxSliderValueProperty;
		public static readonly DependencyProperty MinValueProperty;
		public static readonly DependencyProperty MaxValueProperty;
		public static readonly DependencyProperty CaptionProperty;


	    static IntervalControl()
	    {
		    HasValueProperty = DependencyProperty.Register("HasValue", typeof(bool), typeof(IntervalControl), new FrameworkPropertyMetadata(false));
			CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(IntervalControl), new FrameworkPropertyMetadata(""));
		    MinSliderValueProperty = DependencyProperty.Register("MinSliderValue", typeof(int), typeof(IntervalControl), new FrameworkPropertyMetadata(0));
		    MaxSliderValueProperty = DependencyProperty.Register("MaxSliderValue", typeof(int), typeof(IntervalControl), new FrameworkPropertyMetadata(1000));
		    MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(IntervalControl), new FrameworkPropertyMetadata(0));
		    MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(IntervalControl), new FrameworkPropertyMetadata(0));
		}
		
	}
}
