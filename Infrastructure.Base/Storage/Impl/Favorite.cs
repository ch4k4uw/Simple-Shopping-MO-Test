using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage.Impl
{
    public class Favorite : IFavorite
    {
        private readonly ISimpleDb db;
        private bool favorite;

        public Favorite(ISimpleDb db): this(db, 0, false)
        {
        }

        public Favorite(ISimpleDb db, long id, bool favorite)
        {
            this.db = db;
            Id = id;
            this.favorite = favorite;
        }

        public long Id { get; set; }

        public bool IsFavorite
        {
            get { return favorite; }
            set
            {
                favorite = value;
                if(Id != 0)
                {
                    db.SetFavorite(this);
                }
            }
        }
    }
}
