using Domain.Common.Base.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Repository.Specification
{
    internal class FavoriteProductRepositoryByIdSpecification: IFavoriteProductRepositoryByIdSpecification
    {
        public FavoriteProductRepositoryByIdSpecification(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
