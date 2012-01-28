using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using U413.Domain.Enums;
using U413.Domain.Objects;
using U413.Domain.Commands.Interfaces;
using U413.Domain.Entities;
using U413.Domain.ExtensionMethods;
using U413.Domain.Settings;
using System.IO;
using Mono.Options;
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Utilities;

namespace U413.Domain.Commands.Objects
{
    public class REGISTER : ICommand
    {
        private IUserRepository _userRepository;
        private IVariableRepository _variableRepository;

        public REGISTER(IUserRepository userRepository, IVariableRepository variableRepository)
        {
            _userRepository = userRepository;
            _variableRepository = variableRepository;
        }

        public CommandResult CommandResult { get; set; }

        public IEnumerable<ICommand> AvailableCommands { get; set; }

        public string[] Roles
        {
            get { return RoleTemplates.Visitor; }
        }

        public string Name
        {
            get { return "REGISTER"; }
        }

        public string Parameters
        {
            get { return "<Username> <Password> <ConfirmPassword> [Option(s)]"; }
        }

        public string Description
        {
            get { return "Allows a visitor to register for a username."; }
        }

        public bool ShowHelp
        {
            get { return true; }
        }

        public void Invoke(string[] args)
        {
            var options = new OptionSet();
            options.Add(
                "?|help",
                "Show help information.",
                x =>
                {
                    HelpUtility.WriteHelpInformation(
                        this.CommandResult,
                        this.Name,
                        this.Parameters,
                        this.Description,
                        options
                    );
                }
            );

            bool matchFound = false;

            if (args != null)
            {
                try
                {
                    var extra = options.Parse(args);
                    matchFound = args.Length != extra.Count;
                }
                catch (OptionException ex)
                {
                    this.CommandResult.WriteLine(ex.Message);
                }
            }

            if (!matchFound)
            {
                var registrationStatus = _variableRepository.GetVariable("Registration");
                if (registrationStatus.Value.Equals("Open", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (args.IsNullOrEmpty())
                    {
                        this.CommandResult.WriteLine("Enter your desired username.");
                        this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, args, "Username");
                    }
                    else if (args.Length == 1)
                    {
                        if (args[0].Length > 3)
                        {
                            if (!_userRepository.CheckUserExists(args[0]))
                            {
                                this.CommandResult.WriteLine("Enter your desired password.");
                                this.CommandResult.PasswordField = true;
                                this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, args, "Password");
                            }
                            else
                            {
                                this.CommandResult.WriteLine("Username already exists.");
                                this.CommandResult.WriteLine("Enter a different username.");
                                this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, null, "Username");
                            }
                        }
                        else
                        {
                            this.CommandResult.WriteLine("Username must be at least four characters long.");
                            this.CommandResult.WriteLine("Enter a different username.");
                            this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, null, "Username");
                        }
                    }
                    else if (args.Length == 2)
                    {
                        if (args[0].Length > 3)
                        {
                            if (!_userRepository.CheckUserExists(args[0]))
                            {
                                this.CommandResult.WriteLine("Re-enter your desired password.");
                                this.CommandResult.PasswordField = true;
                                this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, args, "Confirm Password");
                            }
                            else
                            {
                                this.CommandResult.WriteLine("Username already exists.");
                                this.CommandResult.WriteLine("Enter your desired username.");
                                this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, null, "Username");
                            }
                        }
                        else
                        {
                            this.CommandResult.WriteLine("Username must be at least four characters long.");
                            this.CommandResult.WriteLine("Enter a different username.");
                            this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, null, "Username");
                        }
                    }
                    else if (args.Length == 3)
                    {
                        if (args[0].Length > 3)
                        {
                            var user = this._userRepository.GetUser(args[0]);
                            if (user == null)
                            {
                                if (args[1] == args[2])
                                {
                                    user = new User
                                    {
                                        Username = args[0],
                                        Password = args[1],
                                        JoinDate = DateTime.UtcNow,
                                        LastLogin = DateTime.UtcNow,
                                        TimeZone = "UTC",
                                        Sound = true
                                    };
                                    _userRepository.AddRoleToUser(user, "User");
                                    _userRepository.AddUser(user);

                                    this.CommandResult.CurrentUser = user;
                                    this.CommandResult.WriteLine("Thank you for registering.");
                                    this.CommandResult.WriteLine();
                                    var STATS = this.AvailableCommands.SingleOrDefault(x => x.Name.Is("STATS"));
                                    STATS.Invoke(new string[] { "-users" });
                                    this.CommandResult.WriteLine();
                                    this.CommandResult.WriteLine("You are now logged in as {0}.", this.CommandResult.CurrentUser.Username);
                                    this.CommandResult.CommandContext.Deactivate();
                                }
                                else
                                {
                                    this.CommandResult.WriteLine("Passwords did not match.");
                                    this.CommandResult.WriteLine("Enter your desired password.");
                                    this.CommandResult.PasswordField = true;
                                    this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, new string[] { args[0] }, "Password");
                                }
                            }
                            else
                            {
                                this.CommandResult.WriteLine("Username already exists.");
                                this.CommandResult.WriteLine("Enter your desired username.");
                                this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, null, "Username");
                            }
                        }
                        else
                        {
                            this.CommandResult.WriteLine("Username must be at least four characters long.");
                            this.CommandResult.WriteLine("Enter a different username.");
                            this.CommandResult.CommandContext.Set(ContextStatus.Forced, this.Name, null, "Username");
                        }
                    }
                }
                else
                    this.CommandResult.WriteLine("Registration is currently closed.");
            }
        }
    }
}
