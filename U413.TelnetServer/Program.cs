using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Ninject;
using U413.Domain.Ninject;
using U413.Domain.Objects;
using U413.Domain;
using U413.Domain.Enums;
using U413.Domain.Settings;
using System.Configuration;

namespace U413.TelnetServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AppSettings.ConnectionString = ConfigurationManager.ConnectionStrings["EntityContainer"].ConnectionString;
            var server = new TcpListener(IPAddress.Any, 23);
            server.Start();
            DoBeginAcceptTcpClient(server);
            Console.ReadLine();
        }

        static int count = 0;

        public static void DoBeginAcceptTcpClient(TcpListener listener)
        {
            listener.BeginAcceptTcpClient(x =>
            {
                var newClient = listener.EndAcceptTcpClient(x);
                string ipaddress = newClient.Client.RemoteEndPoint.ToString();
                Console.WriteLine(ipaddress + " has connected.");
                DoBeginAcceptTcpClient(listener);
                new Thread(() =>
                    {
                        string clientName = ipaddress;
                        string _username = null;
                        CommandContext _commandContext = null;
                        TerminalApi _terminalApi;
                        bool _appRunning = true;
                        bool _passwordField = false;

                        var stream = newClient.GetStream();
                        var streamReader = new StreamReader(stream);
                        var streamWriter = new StreamWriter(stream);
                        streamWriter.AutoFlush = true;
                        streamWriter.WriteLine("Welcome to the U413 telnet server.");

                        Action<string> invokeCommand = commandString =>
                            {
                                var kernel = new StandardKernel(new U413Bindings(false));
                                _terminalApi = kernel.Get<TerminalApi>();
                                _terminalApi.Username = _username;
                                _terminalApi.CommandContext = _commandContext;

                                streamWriter.WriteLine("Loading...");

                                var commandResult = _terminalApi.ExecuteCommand(commandString);

                                // Set the terminal API command context to the one returned in the result.
                                _commandContext = commandResult.CommandContext;

                                // If the result calls for the screen to be cleared, clear it.
                                if (commandResult.ClearScreen)
                                    for (int count = 0; count <= 25; count++)
                                        streamWriter.WriteLine();

                                // Set the terminal API current user to the one returned in the result and display the username in the console title bar.
                                _username = commandResult.CurrentUser != null ? commandResult.CurrentUser.Username : null;
                                //Console.Title = commandResult.TerminalTitle;

                                // Add a blank line to the console before displaying results.
                                if (commandResult.Display.Count > 0)
                                    streamWriter.WriteLine();

                                // Iterate over the display collection and perform relevant display actions based on the type of the object.
                                foreach (var displayItem in commandResult.Display)
                                {
                                    if ((displayItem.DisplayMode & DisplayMode.Parse) != 0)
                                        displayItem.Text = U413.Domain.Utilities.BBCodeUtility.ConvertTagsForConsole(displayItem.Text);

                                    streamWriter.WriteLine(displayItem.Text);
                                }
                                streamWriter.WriteLine();

                                //if (!commandResult.EditText.IsNullOrEmpty())
                                //    SendKeys.SendWait(commandResult.EditText.Replace("\n", "--"));


                                // If the terminal is prompting for a password then set the global PasswordField bool to true.
                                _passwordField = commandResult.PasswordField;

                                // If the terminal is asking to be closed then kill the runtime loop for the console.
                                _appRunning = !commandResult.Exit;
                            };

                        invokeCommand("INITIALIZE");
                        streamWriter.Write("> ");
                        while (!streamReader.EndOfStream)
                        {
                            var commandString = streamReader.ReadLine();
                            Console.WriteLine("{0}: {1}", (_username ?? clientName), commandString);
                            invokeCommand(commandString);
                            streamWriter.Write("> ");
                        }
                    }).Start();
            }, null);
        }
    }
}
