using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.ProductCatalog.Base.Factory.Specification;
using Domain.ProductCatalog.Base.Repository;
using Domain.ProductCatalog.Base.Repository.Specification;
using Infrastructure.Base.Factory.Specification;
using Infrastructure.Base.Repository;
using Infrastructure.Base.Repository.Specification;
using Infrastructure.Base.Rest;
using Infrastructure.Base.Rest.Impl;
using Unity;
using Unity.Lifetime;

namespace Infrastructure.Base.CrossCutting.Ioc
{
    public class DI
    {
        public static void Register(IUnityContainer container)
        {
            RegisterRestApiClient(container);
            RegisterRepositories(container);
            RegisterRepositoriesSpecificationsFactories(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterRestApiClient(IUnityContainer container)
        {
            container.RegisterSingleton<IRestApi, RestApi>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<IShoppingRepository, ShoppingRepository>();
            container.RegisterType<IProductCategoryQueryRepository, ProductCategoryQueryRepository>();
            container.RegisterType<IProductQueryRepository, ProductQueryRepository>();
            container.RegisterType<IShoppingItemRepository, ShoppingItemRepository>();
            container.RegisterType<IFavoriteProductCommandRepository, FavoriteProductCommandRepository>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterRepositoriesSpecificationsFactories(IUnityContainer container)
        {
            container.RegisterSingleton<IProductCategoryRepositoryListSpecificationFactory, ProductCategoryRepositoryListSpecificationFactory>();
            container.RegisterSingleton<IShoppingRepositoryByIdSpecificationFactory, ShoppingRepositoryByIdSpecificationFactory>();
            container.RegisterSingleton<IShoppingQueryRepositorySpecificationFactory, ShoppingQueryRepositorySpecificationFactory>();
            container.RegisterSingleton<IShoppingItemByIdRepositorySpecificationFactory, ShoppingItemByIdRepositorySpecificationFactory>();
            container.RegisterSingleton<IProductQueryRepositorySpecificationFactory, ProductQueryRepositorySpecificationFactory>();
            container.RegisterSingleton<IProductQueryByIdRepositorySpecificationFactory, ProductQueryByIdRepositorySpecificationFactory>();
            container.RegisterSingleton<IFavoriteProductByIdRepositorySpecificationFactory, FavoriteProductByIdRepositorySpecificationFactory>();
        }
    }
}
