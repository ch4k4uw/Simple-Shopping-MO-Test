using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entity
{
    internal class FavoriteProductEntity : IFavoriteProductEntity
    {
        public FavoriteProductEntity(long id, bool favorite)
        {
            Id = id;
            IsFavorite = favorite;
        }

        public long Id { get; private set; }

        public bool IsFavorite { get; private set; }

    }
}
