using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using Domain.Base.Repository.Specification;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Entity;
using Domain.ProductCatalog.Base.Repository;
using Infrastructure.Base.Repository.Specification;
using Infrastructure.Base.Rest;
using Infrastructure.Base.Storage;

namespace Infrastructure.Base.Repository
{
    internal class ProductCategoryQueryRepository : Repository, IProductCategoryQueryRepository
    {
        public ProductCategoryQueryRepository(IScheduler uiThreadScheduler, IRestApi restApi, IEntityFactory entityFactory): base(uiThreadScheduler, restApi, entityFactory)
        {
        }

        public void Find(IRepositorySpecification<IProductCategoryEntity, long> repositorySpecification, Action<IList<IProductCategoryEntity>> success, Action<Exception> error)
        {
            if (repositorySpecification is ProductCategoryRepositoryListSpecification)
            {
                ReactiveFunction(() =>
                    {
                        var restCategries = restApi.ListCategories();
                        if (restCategries?.Count > 0)
                        {
                            return restCategries.Select(y => entityFactory.NewProductCategory(y.Id, y.Name)).ToList();
                        }
                        return new List<IProductCategoryEntity>();
                    }
                    , categories =>
                    {
                        success.Invoke(categories);
                    }, error);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void GetById(IByIdRepositorySpecification<IProductCategoryEntity, long> byIdRepositorySpecification, Action<IProductCategoryEntity> success, Action<Exception> error)
        {
            throw new NotImplementedException();
        }
    }
}