using Domain.Base.Entity;
using Domain.Base.Repository.Specification;

namespace Domain.Base.Factory.Specification
{
    public interface IRepositorySpecificationFactory<T, TId> where T: IEntity<TId>
    {
    }
}
