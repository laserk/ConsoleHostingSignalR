using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR;
namespace ConsoleHosting
{
    internal class ConsoleListener : IConsoleListener
    {
        private TextWriter _originalOut;
        private ConsoleWriter _consoleWriter;
        private Regex _codedPrefixRegex = new Regex(@"^(<info>|<error>|<warning>).*");
        //as SignalR client
        //private IHubProxy _hubProxy;

        public ConsoleListener()
        {
            //as SignalR client
            //var hubConnection = new HubConnection(ConfigurationManager.AppSettings["NotificationHubUrl"]);
            //_hubProxy = hubConnection.CreateHubProxy("notificationHub");
            //hubConnection.Start().Wait();
            //as SignalR client
        }

        public void StartListening()
        {
            _consoleWriter = new ConsoleWriter();
            _consoleWriter.WriteLineEvent += consoleWriter_WriteLineEvent;

            _originalOut = Console.Out;

            Console.SetOut(_consoleWriter);
        }

        public void StopListen()
        {
            Console.SetOut(_originalOut);
            _consoleWriter.WriteLineEvent -= consoleWriter_WriteLineEvent;
            _consoleWriter = null;
        }

        private void NotifyUpdates(string message)
        {
            //as SignalR client
            //_hubProxy.Invoke("Send", message);
            //as SignalR server
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            if (hubContext != null)
            {
                hubContext.Clients.All.BroadcastMessage(message);
            }
        }

        private void consoleWriter_WriteLineEvent(object sender, ConsoleWriterEventArgs e)
        {
//            _originalOut.WriteLine(e.Value);

            if(isCodedMessage(e.Value))
                NotifyUpdates(e.Value);
        }

        private bool isCodedMessage(string message)
        {
            return true;
            return _codedPrefixRegex.IsMatch(message);
        }
    }
}