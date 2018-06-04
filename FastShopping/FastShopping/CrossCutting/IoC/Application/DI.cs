using Application.Dto.Result;
using Application.Services;
using Domain.Common.Base.Application.Service;
using Domain.ProductCatalog.Base.Application;
using Domain.ShoppingCart.Base.Application;
using Unity;

namespace FastShopping.CrossCutting.IoC.Application
{
    internal class DI
    {
        public static void Register(IUnityContainer container)
        {
            RegisterApplicationServices(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterApplicationServices(IUnityContainer container)
        {
            container.RegisterSingleton<IListCategoryApplicationService<ProductCategory>, ListCategoryService>();
            container.RegisterSingleton<IInitializeShoppingApplicationService, InitializeShoppingService>();
            container.RegisterSingleton<IListProductsApplicationService<ProductListItem>, ListProductsService>();
            container.RegisterSingleton<IDetailProductApplicationService<ProductDetail>, DetailProductService>();
            container.RegisterSingleton<IIncProductQuantityApplicationService<ProductDetail>, IncProductQuantityService>();
            container.RegisterSingleton<IDecProductQuantityApplicationService<ProductDetail>, DecProductQuantityService>();
            container.RegisterSingleton<IFinalizeShoppingApplicationService, FinalizeShoppingService>();
            container.RegisterSingleton<IShoppingCartApplicationService<ProductListItem>, ShoppingCartService>();
        }
    }
}
