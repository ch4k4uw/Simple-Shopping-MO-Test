using Domain.Base.Entity;
using Domain.Base.Repository.Specification;

namespace Domain.Base.Factory.Specification
{
    public interface IByIdRepositorySpecificationFactory<T, TId> where T: IEntity<TId>
    {
    }
}
