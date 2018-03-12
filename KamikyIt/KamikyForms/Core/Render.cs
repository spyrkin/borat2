using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace KamikyForms.Core
{
    public class Render
    {
        public static void DoAction(Action a)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, a);
            }
            catch
            { }
        }
    }
}
