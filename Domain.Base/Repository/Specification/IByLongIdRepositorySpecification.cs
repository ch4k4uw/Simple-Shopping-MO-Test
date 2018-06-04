using Domain.Base.Entity;

namespace Domain.Base.Repository.Specification
{
    public interface IByLongIdRepositorySpecification<T>: IByIdRepositorySpecification<T, long> where T: ILongIdEntity
    {
    }
}
