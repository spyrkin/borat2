using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamikyForms.Core
{
    public static class VKERROR
    {
        public static bool isError(long code)
        {
            List<long> errorCodes = new List<long>(){900, 901, 902, 913, 914, 917, 912};
            return errorCodes.Contains(code);
        }

        public static bool banned(long code)
        {
            List<long> errorCodes = new List<long>() { 900, 901, 902, 917, 912 };
            return errorCodes.Contains(code);
        }

        public static string getErrorString(long code)
        {
            if (code == 900)
            {
                return "Нельзя отправлять сообщение пользователю из черного списка";
            }

            if (code == 901)
            {
                return "Пользователь запретил отправку сообщений от имени сообщества";
            }

            if (code == 902)
            {
                return "Нельзя отправлять сообщения этому пользователю в связи с настройками приватности";
            }


            if (code == 913)
            {
                return "Слишком много пересланных сообщений";
            }

            if (code == 914)
            {
                return "Сообщение слишком длинное";
            }


            if (code == 917)
            {
                return "You don't have access to this chat";
            }

            if (code == 921)
            {
                return "Невозможно переслать выбранные сообщения";
            }
            return "";
        }
    }
}
