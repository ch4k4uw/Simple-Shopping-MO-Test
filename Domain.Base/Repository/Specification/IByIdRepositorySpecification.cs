using Domain.Base.Entity;

namespace Domain.Base.Repository.Specification
{
    public interface IByIdRepositorySpecification<T, TId> where T : IEntity<TId>
    {
    }
}
