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
using U413.Domain.Repositories.Interfaces;
using U413.Domain.ExtensionMethods;
using U413.Domain.Utilities;

namespace U413.Domain.Commands.Objects
{
    public class BOARDS : ICommand
    {
        private IBoardRepository _boardRepository;
        private ITopicRepository _topicRepository;

        public BOARDS(
            IBoardRepository boardRepository,
            ITopicRepository topicRepository)
        {
            _boardRepository = boardRepository;
            _topicRepository = topicRepository;
        }

        public CommandResult CommandResult { get; set; }

        public IEnumerable<ICommand> AvailableCommands { get; set; }

        public string[] Roles
        {
            get { return RoleTemplates.AllLoggedIn; }
        }

        public string Name
        {
            get { return "BOARDS"; }
        }

        public string Parameters
        {
            get { return "[Option(s)]"; }
        }

        public string Description
        {
            get { return "Displays a list of available discussion boards."; }
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
                this.CommandResult.ScrollToBottom = false;
                this.CommandResult.CommandContext.Deactivate();
                this.CommandResult.ClearScreen = true;
                this.CommandResult.WriteLine(DisplayMode.Inverted | DisplayMode.DontType, "Available Discussion Boards");
                var boards = _boardRepository.GetBoards(this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator);
                foreach (var board in boards)
                {
                    this.CommandResult.WriteLine();
                    var displayMode = DisplayMode.DontType;
                    if (board.ModsOnly || board.Hidden)
                        displayMode |= DisplayMode.Dim;
                    long topicCount = board.BoardID == 0
                        ? _topicRepository.AllTopicsCount()
                        : board.TopicCount(this.CommandResult.CurrentUser.IsModerator);
                    this.CommandResult.WriteLine(displayMode, "{{{0}}} {1}{2}{3}{4} | {5} topics",
                        board.BoardID,
                        board.Hidden ? "[HIDDEN] " : string.Empty,
                        board.ModsOnly ? "[MODSONLY] " : string.Empty,
                        board.Locked ? "[LOCKED] " : string.Empty,
                        board.Name,
                        topicCount);
                    if (!board.Description.IsNullOrEmpty())
                        this.CommandResult.WriteLine(displayMode, "{0}", board.Description);
                }
                if (boards.Count() == 0)
                    this.CommandResult.WriteLine("There are no discussion boards.");
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
