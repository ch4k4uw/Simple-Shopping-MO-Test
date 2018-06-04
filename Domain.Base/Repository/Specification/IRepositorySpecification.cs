using Domain.Base.Entity;

namespace Domain.Base.Repository.Specification
{
    public interface IRepositorySpecification<T, TId> where T: IEntity<TId>
    {
    }
}
