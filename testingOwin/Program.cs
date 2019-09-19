using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingOwin
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:3001";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Application deployed and hosted in {0}", url);
                Console.WriteLine("Press any key to terminate...");
                Console.ReadLine();
            }
        }
    }
}
