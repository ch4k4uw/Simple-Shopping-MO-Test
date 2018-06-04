using Domain.Base.Entity;
using Domain.Base.Repository.Specification;

namespace Domain.Base.Factory.Specification
{
    public interface IByLongIdRepositorySpecificationFactory<T>: IByIdRepositorySpecificationFactory<T, long> where T: ILongIdEntity
    {
    }
}
