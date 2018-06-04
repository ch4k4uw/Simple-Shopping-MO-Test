using Domain.Common.Base.Factory.Command;
using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Factory.Value;
using Domain.Common.Factory.Command;
using Domain.Common.Factory.Entity;
using Domain.Common.Factory.Value;
using Domain.ProductCatalog.Base.Command;
using Domain.ProductCatalog.Base.Factory.Command;
using Domain.ProductCatalog.Command;
using Domain.ProductCatalog.Factory.Command;
using Domain.ShoppingCart.Base.Factory.Command;
using Domain.ShoppingCart.Factory.Command;
using Unity;
using Unity.Lifetime;

namespace FastShopping.CrossCutting.IoC.Domain
{
    internal class DI
    {
        public static void Register(IUnityContainer container)
        {
            RegisterInfrastructure(container);
            RegisterDomain(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterInfrastructure(IUnityContainer container)
        {
            Infrastructure.Base.CrossCutting.Ioc.DI.Register(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterDomain(IUnityContainer container)
        {
            RegisterDomainCommandFactories(container);
            RegisterDomainCommands(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterDomainCommandFactories(IUnityContainer container)
        {
            container.RegisterSingleton<IEntityFactory, EntityFactory>();
            container.RegisterSingleton<IValueFactory, ValueFactory>();
            container.RegisterSingleton<IProductCategoryCommandFactory, ProductCategoryCommandFactory>();
            container.RegisterSingleton<IProductCatalogCommandFactory, ProductCatalogCommandFactory>();
            container.RegisterSingleton<IShoppingCommandFactory, ShoppingCommandFactory>();
            container.RegisterSingleton<IProductDetailCommandFactory, ProductDetailCommandFactory>();
            container.RegisterSingleton<IShoppingCartCommandFactory, ShoppingCartCommandFactory>();
            container.RegisterSingleton<ISetAsFavoriteCommandFactory, SetAsFavoriteCommandFactory>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterDomainCommands(IUnityContainer container)
        {
            /*container.RegisterType<IFinalizeShoppingCommand, FinalizeShoppingCommand>();
            container.RegisterType<IInitializeShoppingCommand, InitializeShoppingCommand>();
            container.RegisterType<IProductCatalogQuery, ProductCatalogQuery>();
            container.RegisterType<IProductCategoryQuery, ProductCategoryQuery>();*/
        }
    }
}
