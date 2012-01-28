using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using U413.Domain.Enums;
using U413.Domain.Objects;
using U413.Domain.Commands.Interfaces;
using U413.Domain.Entities;
using U413.Domain.Settings;
using System.IO;
using Mono.Options;
using U413.Domain.Utilities;

namespace U413.Domain.Commands.Objects
{
    public class CLS : ICommand
    {
        public CommandResult CommandResult { get; set; }

        public IEnumerable<ICommand> AvailableCommands { get; set; }

        public string[] Roles
        {
            get { return RoleTemplates.Everyone; }
        }

        public string Name
        {
            get { return "CLS"; }
        }

        public string Parameters
        {
            get { return "[Option(s)]"; }
        }

        public string Description
        {
            get { return "Clears the screen."; }
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

            if (args == null)
            {
                this.CommandResult.ClearScreen = true;
                this.CommandResult.CommandContext.Deactivate();
            }
            else
                try
                {
                    options.Parse(args);
                }
                catch (OptionException ex)
                {
                    this.CommandResult.WriteLine(ex.Message);
                }
        }
    }
}
