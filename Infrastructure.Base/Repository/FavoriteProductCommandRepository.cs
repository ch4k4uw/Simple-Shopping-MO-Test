using Domain.Base.Repository.Specification;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Repository;
using Infrastructure.Base.Repository.Specification;
using Infrastructure.Base.Rest;
using Infrastructure.Base.Storage;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Text;

namespace Infrastructure.Base.Repository
{
    internal class Favorite : IFavorite
    {
        public long Id { get; set; }
        public bool IsFavorite { get; set; }
    }

    internal class FavoriteProductCommandRepository : Repository, IFavoriteProductCommandRepository
    {
        public FavoriteProductCommandRepository(IScheduler uiThreadScheduler, IRestApi restApi, IEntityFactory entityFactory) : base(uiThreadScheduler, restApi, entityFactory)
        {
        }

        public void Insert(IFavoriteProductEntity entity, Action<long> success, Action<Exception> error)
        {
            throw new NotImplementedException();
        }

        public void Update(IFavoriteProductEntity entity, IByIdRepositorySpecification<IFavoriteProductEntity, long> spec, Action<IFavoriteProductEntity> success, Action<Exception> error)
        {
            if(spec is FavoriteProductRepositoryByIdSpecification)
            {
                ReactiveFunction(() => 
                {
                    var favorite = new Favorite
                    {
                        Id = entity.Id,
                        IsFavorite = entity.IsFavorite
                    };

                    simpleDb.SetFavorite(favorite);

                    return entity;
                }
                
                , success, error);
            } else
            {
                throw new ArgumentException();
            }
        }
    }
}
