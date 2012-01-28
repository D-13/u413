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
    public class TEST : ICommand
    {
        private EntityContainer _entityContainer;

        public TEST(EntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        public CommandResult CommandResult { get; set; }

        public IEnumerable<ICommand> AvailableCommands { get; set; }

        public string[] Roles
        {
            get { return new string[] { "Administrator" }; }
        }

        public string Name
        {
            get { return "TEST"; }
        }

        public string Parameters
        {
            get { return "[Option(s)]"; }
        }

        public string Description
        {
            get { return "Development only."; }
        }

        public bool ShowHelp
        {
            get { return true; }
        }

        public void Invoke(string[] args)
        {
            
        }
    }
}
