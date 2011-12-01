//   **************************************************************************
//   *                                                                        *
//   *  This program is free software: you can redistribute it and/or modify  *
//   *  it under the terms of the GNU General Public License as published by  *
//   *  the Free Software Foundation, either version 3 of the License, or     *
//   *  (at your option) any later version.                                   *
//   *                                                                        *
//   *  This program is distributed in the hope that it will be useful,       *
//   *  but WITHOUT ANY WARRANTY; without even the implied warranty of        *
//   *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
//   *  GNU General Public License for more details.                          *
//   *                                                                        *
//   *  You should have received a copy of the GNU General Public License     *
//   *  along with this program.  If not, see <http://www.gnu.org/licenses/>. *
//   *                                                                        *
//   **************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using U413.Domain.Commands.Objects;
using U413.Domain.Repositories.Interfaces;
using U413.Domain.Repositories.Objects;
using U413.Domain.Commands.Interfaces;
using U413.Domain.Entities;
using U413.Domain.Settings;
using System.Data.EntityClient;

namespace U413.Domain.Ninject
{
    /// <summary>
    /// This module will automatically register all U413.Domain related bindings.
    /// </summary>
    public class U413Bindings : NinjectModule
    {
        bool _isWebApplication = false;

        public U413Bindings(bool isWebApplication)
        {
            _isWebApplication = isWebApplication;
        }

        /// <summary>
        /// Load all U413 bindings.
        /// </summary>
        public override void Load()
        {
            BindEntityContainer();
            BindRepositories();
            BindCommands();
        }

        /// <summary>
        /// Register the entity container and pass in a hard-coded connection string.
        /// </summary>
        private void BindEntityContainer()
        {
            if (!_isWebApplication)
                this.Bind<EntityContainer>().ToSelf().InSingletonScope()
                    .WithConstructorArgument("connectionString", x => AppSettings.ConnectionString);
            else
                this.Bind<EntityContainer>().ToSelf().InRequestScope()
                    .WithConstructorArgument("connectionString", x => AppSettings.ConnectionString);
        }

        /// <summary>
        /// Register all available U413 commands.
        /// </summary>
        private void BindCommands()
        {
            this.Bind<ICommand>().To<INITIALIZE>();
            this.Bind<ICommand>().To<CLS>();
            this.Bind<ICommand>().To<EXIT>();
            this.Bind<ICommand>().To<REGISTER>();
            this.Bind<ICommand>().To<LOGIN>();
            this.Bind<ICommand>().To<LOGOUT>();
            this.Bind<ICommand>().To<BOARDS>();
            this.Bind<ICommand>().To<BOARD>();
            this.Bind<ICommand>().To<TOPIC>();
            this.Bind<ICommand>().To<MESSAGES>();
            this.Bind<ICommand>().To<MESSAGE>();
            this.Bind<ICommand>().To<ALIAS>();
            this.Bind<ICommand>().To<SETTINGS>();
            this.Bind<ICommand>().To<STATS>();
            this.Bind<ICommand>().To<USER>();
            this.Bind<ICommand>().To<TEST>();
        }

        /// <summary>
        /// Register all available repositories.
        /// </summary>
        private void BindRepositories()
        {
            this.Bind<IAliasRepository>().To<AliasRepository>();
            this.Bind<IBoardRepository>().To<BoardRepository>();
            this.Bind<IChannelStatusRepository>().To<ChannelStatusRepository>();
            this.Bind<IChatBufferItemRepository>().To<ChatBufferItemRepository>();
            this.Bind<IReplyRepository>().To<ReplyRepository>();
            this.Bind<IShortUrlRepository>().To<ShortUrlRepository>();
            this.Bind<ITopicRepository>().To<TopicRepository>();
            this.Bind<IUserRepository>().To<UserRepository>();
            this.Bind<IMessageRepository>().To<MessageRepository>();
            this.Bind<IBanRepository>().To<BanRepository>();
            this.Bind<IVariableRepository>().To<VariableRepository>();
        }
    }
}
