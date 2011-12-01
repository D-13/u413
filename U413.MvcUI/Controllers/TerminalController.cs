using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U413.Domain;
using U413.Domain.Objects;
using U413.MvcUI.ViewModels;
using U413.Domain.ExtensionMethods;
using System.Web.Security;
using U413.Domain.Settings;
using System.Text;
using U413.Domain.Enums;
using System.Net;
using System.Configuration;

namespace U413.MvcUI.Controllers
{
    [ValidateInput(false)]
    public class TerminalController : Controller
    {
        private TerminalApi _terminalApi;
        private CommandContext _commandContext;

        public TerminalController(TerminalApi terminalApi)
        {
            _terminalApi = terminalApi;
        }

        public ViewResult Index(string Cli, string Display)
        {
            AppSettings.ConnectionString = ConfigurationManager.ConnectionStrings["EntityContainer"].ConnectionString;

            if (Session["apiSessionId"] == null)
                Session["apiSessionId"] = new WebClient().DownloadString(ConfigurationManager.AppSettings["ApiUrl"] + "GetSessionId");

            ModelState.Clear();
            if (Session["commandContext"] != null)
                _commandContext = (CommandContext)Session["commandContext"];
            _terminalApi.Username = User.Identity.IsAuthenticated ? User.Identity.Name : null;
            _terminalApi.CommandContext = _commandContext;
            _terminalApi.ParseAsHtml = true;
            var commandResult = _terminalApi.ExecuteCommand(Cli);

            if (commandResult.ClearScreen)
                Display = null;

            if (User.Identity.IsAuthenticated)
            {
                if (commandResult.CurrentUser == null)
                    FormsAuthentication.SignOut();
            }
            else
            {
                if (commandResult.CurrentUser != null)
                    FormsAuthentication.SetAuthCookie(commandResult.CurrentUser.Username, false);
            }

            Session["commandContext"] = commandResult.CommandContext;

            var display = new StringBuilder();
            foreach (var displayItem in commandResult.Display)
            {
                display.Append(displayItem.Text);
                display.Append("<br />");
            }

            if (Display != null)
                Display += "<br />";

            var viewModel = new TerminalViewModel
            {
                Cli = commandResult.EditText,
                ContextText = commandResult.CommandContext.Command
                + (commandResult.CommandContext.Text.IsNullOrEmpty() 
                ? null : string.Format(" {0}", _terminalApi.CommandContext.Text)),
                Display = Display + display.ToString(),
                PasswordField = commandResult.PasswordField,
                Notifications = string.Empty,
                Title = commandResult.TerminalTitle,
                SessionId = Session["apiSessionId"].ToString()
            };

            return View(viewModel);
        }
    }
}
