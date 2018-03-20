using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KamikyForms.Bot
{
    //помогает боту, лол
    public class BotHelper
    {
        public static string prepareString(String str)
        {
            String res = str.ToLower();
            //удаялем лишние символы
            res = Regex.Replace(res, "[-.?!)(,:#$^*&@]", "");
            //удаляем лишние пробелы
            while (res.IndexOf("  ") != -1) res = res.Replace("  ", " ");

            //сортируем предложение в строке
            List<String> lstr = res.Split(new string[]{" "}, StringSplitOptions.None).ToList();
            lstr = lstr.OrderBy(q => q).ToList();
            res = String.Join<string>(" ", lstr);
            return res;
        } 
    }
}
