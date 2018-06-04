using Domain.Base.Factory.Command;
using Domain.Common.Base.Entity;
using Domain.ProductCatalog.Base.Command;

namespace Domain.ProductCatalog.Base.Factory.Command
{
    public interface IProductCategoryCommandFactory: ILongIdCommandFactory<IProductCategoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IProductCategoryQuery NewQuery();
    }
}
