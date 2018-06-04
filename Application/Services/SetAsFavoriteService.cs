using Domain.Common.Base.Application.Service;
using Domain.Common.Base.Factory.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class SetAsFavoriteService : ISetAsFavoriteApplicationService
    {
        private readonly ISetAsFavoriteCommandFactory setAsFavoriteCommandFactory;
        public SetAsFavoriteService(ISetAsFavoriteCommandFactory setAsFavoriteCommandFactory)
        {
            this.setAsFavoriteCommandFactory = setAsFavoriteCommandFactory;
        }
        public void SetAsFavorite(long id, bool favorite, Action complete)
        {
            setAsFavoriteCommandFactory.NewCommand(id, favorite)
                .Exec(complete);
        }
    }
}
