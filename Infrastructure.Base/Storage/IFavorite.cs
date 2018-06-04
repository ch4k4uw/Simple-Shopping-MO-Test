using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage
{
    public interface IFavorite
    {
        long Id { get; set; }

        bool IsFavorite { get; set; }

    }
}
