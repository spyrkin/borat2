using System;
using System.Linq;
using ApiWrapper.Core;

namespace VkNet.Examples
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var api = Api.GetInstance();
            //api.Auth();
            //Console.WriteLine("api.Token=" + api.Token);

	        //var test = SearchInstrument.Test();

	        var test = SearchInstrument.Test();
	        var photos = test.Select(x => x.photoUrl400).ToList();
	        //SearchInstrument.SendMessage("60150803", "Серег, ты тут?");

			//var res = api.Groups.Get(new GroupsGetParams());

            //Console.WriteLine(res.TotalCount);

            Console.ReadLine();
        }
    }
}