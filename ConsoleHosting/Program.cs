using System;
using Microsoft.Owin.Hosting;

namespace ConsoleHosting
{
    class Program
    {
        private readonly static IConsoleListener _consoleListener=new ConsoleListener();
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:11111"))
            {
                Console.WriteLine("Server running at http://localhost:11111");
                while (true)
                {
                    var str = Console.ReadLine();
                    _consoleListener.StartListening();
                    Console.WriteLine("User in put: "+str+ " from console at " + DateTime.Now.ToLongTimeString());
                    _consoleListener.StopListen();
                }
            }
        }
    }
}
