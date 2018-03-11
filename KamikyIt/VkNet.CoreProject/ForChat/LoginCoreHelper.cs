using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Examples.Core;

namespace VkNet.Examples.ForChat
{
    public static class LoginCoreHelper
    {
        public static string Login(string userId, string pass)
        {
            var error = "";
            if (VkApiInstrument.Login(userId, pass, out error) == false)
            {
                
                return error;
            }
            return "";
        }

    }
}
