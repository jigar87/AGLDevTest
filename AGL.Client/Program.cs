using AGL.Service.Service;
using System;
using System.Configuration;

namespace AGLDevTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings["APIUrl"];
            var catList = new DisplayService().GetCatsGroupedByOwnerGender(url);

            foreach (var item in catList)
            {
                Console.WriteLine(item.OwnersGender);
                Console.WriteLine();
                foreach(var cat in item.CatNames)
                {
                    Console.WriteLine("  - "+ cat);
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("Press Enter to Exit");
            Console.ReadKey();
        }
    }
}
