﻿using System;
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
    public class BOARD : ICommand
    {
        private IBoardRepository _boardRepository;
        private ITopicRepository _topicRepository;
        private IReplyRepository _replyRepository;

        public BOARD(
            IBoardRepository boardRepository,
            ITopicRepository topicRepository,
            IReplyRepository replyRepository
        )
        {
            _boardRepository = boardRepository;
            _topicRepository = topicRepository;
            _replyRepository = replyRepository;
        }

        public CommandResult CommandResult { get; set; }

        public IEnumerable<ICommand> AvailableCommands { get; set; }

        public string[] Roles
        {
            get { return RoleTemplates.AllLoggedIn; }
        }

        public string Name
        {
            get { return "BOARD"; }
        }

        public string Parameters
        {
            get { return "<BoardID> [Page#/Option(s)]"; }
        }

        public string Description
        {
            get { return "Displays a list of topics posted on the specified board."; }
        }

        public bool ShowHelp
        {
            get { return true; }
        }

        public void Invoke(string[] args)
        {
            bool showHelp = false;
            bool newTopic = false;
            bool modTopic = false;
            bool refresh = false;
            bool? lockBoard = null;

            var options = new OptionSet();
            options.Add(
                "?|help",
                "Show help information.",
                x => showHelp = x != null
            );
            options.Add(
                "R|refresh",
                "Refresh the current board.",
                x => refresh = x != null
            );
            options.Add(
                "nt|newTopic",
                "Create new topic on the specified board.",
                x => newTopic = x != null
            );
            if (this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator)
            {
                options.Add(
                    "mt|modTopic",
                    "Create a topic that only moderators can see.",
                    x =>
                    {
                        newTopic = x != null;
                        modTopic = x != null;
                    }
                );
            }
            if (this.CommandResult.CurrentUser.IsAdministrator)
            {
                options.Add(
                    "l|lock",
                    "Lock the board to prevent creation of topics.",
                    x => lockBoard = x != null
                );
            }

            if (args == null)
            {
                this.CommandResult.WriteLine(DisplayTemplates.InvalidArguments);
            }
            else
            {
                try
                {
                    var parsedArgs = options.Parse(args).ToArray();

                    if (parsedArgs.Length == args.Length)
                    {
                        if (parsedArgs.Length == 1)
                        {
                            if (parsedArgs[0].IsShort())
                            {
                                var boardId = parsedArgs[0].ToShort();
                                var board = _boardRepository.GetBoard(boardId);
                                var page = 1;
                                if (board != null)
                                {
                                    if (board.ModsOnly)
                                    {
                                        if (this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator)
                                        {
                                            WriteTopics(boardId, page, board.Anonymous);
                                        }
                                        else
                                            this.CommandResult.WriteLine("You do not have permission to access this board.");
                                    }
                                    else
                                        WriteTopics(boardId, page, board.Anonymous);
                                }
                                else
                                    this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                            }
                            else
                                this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                        }
                        else if (parsedArgs.Length == 2)
                        {
                            if (parsedArgs[0].IsShort())
                            {
                                var boardId = parsedArgs[0].ToShort();
                                if (parsedArgs[1].IsInt())
                                {
                                    var page = parsedArgs[1].ToInt();
                                    var board = _boardRepository.GetBoard(boardId);
                                    if (board != null)
                                    {
                                        if (board.ModsOnly)
                                        {
                                            if (this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator)
                                            {
                                                WriteTopics(boardId, page, board.Anonymous);
                                            }
                                            else
                                                this.CommandResult.WriteLine("You do not have permission to access this board.");
                                        }
                                        else
                                            WriteTopics(boardId, page, board.Anonymous);
                                    }
                                    else
                                        this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                                }
                                else if (PagingUtility.Shortcuts.Any(x => parsedArgs[1].Is(x)))
                                {
                                    var page = PagingUtility.TranslateShortcut(parsedArgs[1], this.CommandResult.CommandContext.CurrentPage);
                                    WriteTopics(boardId, page, _boardRepository.GetBoard(boardId).Anonymous);
                                    if (parsedArgs[1].Is("last") || parsedArgs[1].Is("prev"))
                                        this.CommandResult.ScrollToBottom = false;
                                }
                                else
                                    this.CommandResult.WriteLine("'{0}' is not a valid page number.", parsedArgs[1]);
                            }
                            else
                                this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                        }
                        else
                            this.CommandResult.WriteLine(DisplayTemplates.InvalidArguments);
                    }
                    else
                    {
                        if (showHelp)
                        {
                            HelpUtility.WriteHelpInformation(
                                this.CommandResult,
                                this.Name,
                                this.Parameters,
                                this.Description,
                                options
                            );
                        }
                        else if (newTopic)
                        {
                            if (parsedArgs.Length >= 1)
                            {
                                if (parsedArgs[0].IsShort())
                                {
                                    var boardId = parsedArgs[0].ToShort();
                                    var board = _boardRepository.GetBoard(boardId);
                                    if (board != null)
                                    {
                                        if (!board.Locked || this.CommandResult.CurrentUser.IsAdministrator)
                                        {
                                            if (!board.ModsOnly || this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator)
                                            {
                                                if (this.CommandResult.CommandContext.PromptData == null)
                                                {
                                                    this.CommandResult.WriteLine("Create a title for your topic.");
                                                    this.CommandResult.CommandContext.SetPrompt(this.Name, args, string.Format("{0} NEW TOPIC Title", boardId));
                                                }
                                                else if (this.CommandResult.CommandContext.PromptData.Length == 1)
                                                {
                                                    this.CommandResult.WriteLine("Create the body for your topic.");
                                                    this.CommandResult.CommandContext.SetPrompt(this.Name, args, string.Format("{0} NEW TOPIC Body", boardId));
                                                }
                                                else if (this.CommandResult.CommandContext.PromptData.Length == 2)
                                                {
                                                    Topic topic = new Topic { };
                                                    if (board.Anonymous)
                                                    {
                                                        topic = new Topic
                                                        {
                                                            BoardID = boardId,
                                                            Title = this.CommandResult.CommandContext.PromptData[0],
                                                            Body = BBCodeUtility.SimplifyComplexTags(
                                                                this.CommandResult.CommandContext.PromptData[1],
                                                                _replyRepository,
                                                                this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator
                                                            ),
                                                            Username = "Anony",
                                                            PostedDate = DateTime.UtcNow,
                                                            LastEdit = DateTime.UtcNow,
                                                            ModsOnly = modTopic && !board.ModsOnly
                                                        };
                                                    }
                                                    else
                                                    {
                                                        topic = new Topic
                                                        {
                                                            BoardID = boardId,
                                                            Title = this.CommandResult.CommandContext.PromptData[0],
                                                            Body = BBCodeUtility.SimplifyComplexTags(
                                                                this.CommandResult.CommandContext.PromptData[1],
                                                                _replyRepository,
                                                                this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator
                                                            ),
                                                            Username = this.CommandResult.CurrentUser.Username,
                                                            PostedDate = DateTime.UtcNow,
                                                            LastEdit = DateTime.UtcNow,
                                                            ModsOnly = modTopic && !board.ModsOnly
                                                        };
                                                    }
                                                    _topicRepository.AddTopic(topic);
                                                    this.CommandResult.CommandContext.Restore();
                                                    var TOPIC = this.AvailableCommands.SingleOrDefault(x => x.Name.Is("TOPIC"));
                                                    TOPIC.Invoke(new string[] { topic.TopicID.ToString() });
                                                    this.CommandResult.WriteLine("New topic succesfully posted.");
                                                }
                                            }
                                            else
                                                this.CommandResult.WriteLine("Board '{0}' is for moderators only.", boardId);
                                        }
                                        else
                                            this.CommandResult.WriteLine("Board '{0}' is locked. You cannot create topics on this board.", boardId);
                                    }
                                    else
                                        this.CommandResult.WriteLine("There is no board with ID '{0}'.", boardId);

                                }
                                else
                                    this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                            }
                            else
                                this.CommandResult.WriteLine("You must supply a board ID.");
                        }
                        else
                        {
                            if (lockBoard != null)
                            {
                                if (parsedArgs.Length > 0)
                                {
                                    if (parsedArgs[0].IsShort())
                                    {
                                        var boardId = parsedArgs[0].ToShort();
                                        var board = _boardRepository.GetBoard(boardId);
                                        if (board != null)
                                        {
                                            board.Locked = (bool)lockBoard;
                                            _boardRepository.UpdateBoard(board);
                                            string status = (bool)lockBoard ? "locked" : "unlocked";
                                            this.CommandResult.WriteLine("Board '{0}' was successfully {1}.", boardId, status);
                                        }
                                        else
                                            this.CommandResult.WriteLine("There is no board with ID '{0}'.", boardId);
                                    }
                                    else
                                        this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                                }
                                else
                                    this.CommandResult.WriteLine("You must supply a board ID.");
                            }
                            if (refresh)
                            {
                                if (parsedArgs.Length > 0)
                                {
                                    if (parsedArgs[0].IsShort())
                                    {
                                        var boardId = parsedArgs[0].ToShort();
                                        WriteTopics(boardId, this.CommandResult.CommandContext.CurrentPage, false);
                                    }
                                    else
                                        this.CommandResult.WriteLine("'{0}' is not a valid board ID.", parsedArgs[0]);
                                }
                                else
                                    this.CommandResult.WriteLine("You must supply a board ID.");
                            }
                        }
                    }
                }
                catch (OptionException ex)
                {
                    this.CommandResult.WriteLine(ex.Message);
                }
            }
        }

        private void WriteTopics(short boardId, int page, bool isAnonymous)
        {
            var board = _boardRepository.GetBoard(boardId);
            if (board != null)
            {
                this.CommandResult.ClearScreen = true;
                this.CommandResult.ScrollToBottom = true;
                var boardPage = _topicRepository.GetTopics(boardId, page, AppSettings.TopicsPerPage, this.CommandResult.CurrentUser.IsModerator || this.CommandResult.CurrentUser.IsAdministrator);
                if (page > boardPage.TotalPages)
                    page = boardPage.TotalPages;
                else if (page < 1)
                    page = 1;
                this.CommandResult.WriteLine(DisplayMode.Inverted | DisplayMode.DontType, "{{{0}}} {1}{2}", board.BoardID,board.Locked ? "[LOCKED] " : string.Empty, board.Name);
                this.CommandResult.WriteLine();
                this.CommandResult.WriteLine(DisplayMode.DontType, "Page {0}/{1}", page, boardPage.TotalPages);
                this.CommandResult.WriteLine();
                this.CommandResult.WriteLine(DisplayMode.Dim | DisplayMode.DontType, new string('-', AppSettings.DividerLength));
                this.CommandResult.WriteLine();
                foreach (var topic in boardPage.Items)
                {
                    var displayMode = DisplayMode.DontType;
                    if ((topic.IsModsOnly() && !board.ModsOnly) || (topic.Board.Hidden && boardId == 0))
                        displayMode |= DisplayMode.Dim;
                    var replies = topic.GetReplies(CommandResult.CurrentUser.IsModerator);
                    var lastReply = replies.LastOrDefault();
                    StringBuilder status = new StringBuilder();
                    if (topic.Board.Hidden && !board.Hidden)
                        status.Append("[HIDDEN] ");
                    if (topic.IsModsOnly() && !board.ModsOnly)
                        status.Append("[MODSONLY] ");
                    if ((boardId != 0 && topic.Stickied) || (boardId == 0 && topic.GlobalSticky))
                        status.Append("[STICKY] ");
                    if (topic.Locked)
                        status.Append("[LOCKED] ");
                    string boardNumber = boardId == 0 ? string.Format("{{{0}}} > ", topic.BoardID) : string.Empty;
                    this.CommandResult.WriteLine(displayMode, "{0}{{{1}}} {2}{3}", boardNumber, topic.TopicID, status, topic.Title);
                    if (isAnonymous)
                        this.CommandResult.WriteLine(displayMode, "   by {0} {1} | {2} replies", 'a', topic.PostedDate.TimePassed(), replies.Count());
                    else
                        this.CommandResult.WriteLine(displayMode, "   by {0} {1} | {2} replies", topic.Username, topic.PostedDate.TimePassed(), replies.Count());
                    if (lastReply != null)
                        if (isAnonymous)
                            this.CommandResult.WriteLine(displayMode, "   last reply by {0} {1}", 'b', lastReply.PostedDate.TimePassed());
                        else
                            this.CommandResult.WriteLine(displayMode, "   last reply by {0} {1}", lastReply.Username, lastReply.PostedDate.TimePassed());
                    this.CommandResult.WriteLine();
                    this.CommandResult.WriteLine(DisplayMode.Dim | DisplayMode.DontType, new string('-', AppSettings.DividerLength));
                    this.CommandResult.WriteLine();
                }
                if (boardPage.TotalItems == 0)
                {
                    this.CommandResult.WriteLine("There are no topics on this board.");
                    this.CommandResult.WriteLine();
                    this.CommandResult.WriteLine(DisplayMode.Dim | DisplayMode.DontType, new string('-', AppSettings.DividerLength));
                    this.CommandResult.WriteLine();
                }
                this.CommandResult.WriteLine(DisplayMode.DontType, "Page {0}/{1}", page, boardPage.TotalPages);
                this.CommandResult.CommandContext.CurrentPage = page;
                this.CommandResult.CommandContext.Set(ContextStatus.Passive, this.Name, new string[] { boardId.ToString() }, string.Format("{0}", boardId));
            }
            else
                this.CommandResult.WriteLine("There is no board with ID '{0}'.", boardId);
        }
    }
}
