using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFC_Clone.Logging
{
    public class Logging : ILogging
    {
        public void Log(params string[] details)
        {
            foreach (string detail in details)
            {
                Console.WriteLine($"\n {detail}");
            }
            Console.ReadLine();
        }
    }
}