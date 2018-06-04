using Domain.Base.Entity;
using Domain.Base.Repository.Specification;

namespace Domain.Base.Factory.Specification
{
    public interface ILongIdRepositorySpecificationFactory<T>: IRepositorySpecificationFactory<T, long> where T: ILongIdEntity
    {
    }
}
