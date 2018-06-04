using Domain.Common.Base.Command;
using Domain.Common.Base.Factory.Command;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Factory.Command
{
    public class SetAsFavoriteCommandFactory : ISetAsFavoriteCommandFactory
    {
        private readonly IFavoriteProductCommandRepository favoriteProductCommandRepository;
        private readonly IFavoriteProductByIdRepositorySpecificationFactory favoriteProductByIdRepositorySpecificationFactory;

        public SetAsFavoriteCommandFactory(IFavoriteProductCommandRepository productCommandRepository, IFavoriteProductByIdRepositorySpecificationFactory favoriteProductByIdRepositorySpecificationFactory)
        {
            this.favoriteProductCommandRepository = productCommandRepository;
            this.favoriteProductByIdRepositorySpecificationFactory = favoriteProductByIdRepositorySpecificationFactory;
        }

        public ISetAsFavoriteCommand NewCommand(long id, bool favorite)
        {
            return new SetAsFavoriteCommand(id, favorite, favoriteProductCommandRepository, favoriteProductByIdRepositorySpecificationFactory);
        }
    }
}
