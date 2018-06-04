using Domain.Base.Repository.Specification;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Repository;
using Infrastructure.Base.Repository.Specification;
using Infrastructure.Base.Rest;
using Infrastructure.Base.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;

namespace Infrastructure.Base.Repository
{
    internal class ShoppingRepository : Repository, IShoppingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        internal class Shopping : IShopping
        {
            public long Id { get; set; }
            public DateTime Created { get; set; }
            public DateTime Finished { get; set; }
        }

        public ShoppingRepository(IScheduler uiThreadScheduler, ISimpleDb simpleDb, IRestApi restApi, IEntityFactory entityFactory) : base(uiThreadScheduler, simpleDb, restApi, entityFactory)
        {
        }

        public void Find(IRepositorySpecification<IShoppingEntity, long> repositorySpecification, Action<IList<IShoppingEntity>> success, Action<Exception> error)
        {
            if (repositorySpecification is ShoppingRepositorySpecification)
            {

                ReactiveFunction(() =>
                {
                    var rawShoppings = simpleDb.ListShoppings();
                    if (rawShoppings?.Count > 0)
                    {
                        return rawShoppings.Select(x => entityFactory.NewShopping(x.Id, x.Created, x.Finished)).ToList();
                    }
                    return new List<IShoppingEntity>();
                }
                , list =>
                {
                    success.Invoke(list);
                }, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void GetById(IByIdRepositorySpecification<IShoppingEntity, long> byIdRepositorySpecification, Action<IShoppingEntity> success, Action<Exception> error)
        {
            throw new NotImplementedException();
        }

        public void Insert(IShoppingEntity entity, Action<long> success, Action<Exception> error)
        {
            ReactiveFunction(() =>
            {
                var rawShopping = new Shopping
                {
                    Id = entity.Id,
                    Created = entity.Creation,
                    Finished = entity.Finished
                };

                simpleDb.SetShopping(rawShopping);
                return rawShopping.Id;
            }
            , id =>
            {
                success.Invoke(id);
            }, error);
        }

        public void Update(IShoppingEntity entity, IByIdRepositorySpecification<IShoppingEntity, long> spec, Action<IShoppingEntity> success, Action<Exception> error)
        {
            if (spec is ShoppingRepositoryByIdSpecification)
            {
                ReactiveFunction(() =>
                {
                    var id = (spec as ShoppingRepositoryByIdSpecification).Id;
                    var rawShopping = simpleDb.GetShoppingById(id);

                    if (rawShopping.Id == id)
                    {
                        rawShopping.Created = entity.Creation;
                        rawShopping.Finished = entity.Finished;
                        simpleDb.SetShopping(rawShopping);

                        return entity;
                    }

                    return entityFactory.EmptyShopping();
                }
                , list =>
                {
                    success.Invoke(list);
                }, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
