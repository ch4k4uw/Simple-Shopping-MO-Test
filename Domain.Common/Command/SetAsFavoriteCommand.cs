using Domain.Common.Base.Command;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Command
{
    internal class SetAsFavoriteCommand : ISetAsFavoriteCommand
    {
        private readonly long id;
        private readonly bool favorite;
        private readonly IFavoriteProductCommandRepository productCommandRepository;
        private readonly IFavoriteProductByIdRepositorySpecificationFactory favoriteProductByIdRepositorySpecificationFactory;

        public SetAsFavoriteCommand(long id, bool favorite, IFavoriteProductCommandRepository favoriteProductCommandRepository, IFavoriteProductByIdRepositorySpecificationFactory favoriteProductByIdRepositorySpecificationFactory)
        {
            this.id = id;
            this.favorite = favorite;
            this.productCommandRepository = favoriteProductCommandRepository;
            this.favoriteProductByIdRepositorySpecificationFactory = favoriteProductByIdRepositorySpecificationFactory;
        }

        public void Exec(Action complete)
        {
            productCommandRepository.Update(
                new FavoriteProductEntity(id, favorite), 
                favoriteProductByIdRepositorySpecificationFactory.NewUpdateSpec(id), 
                product => complete.Invoke(), 
                err => complete.Invoke()
            );
        }
    }
}
