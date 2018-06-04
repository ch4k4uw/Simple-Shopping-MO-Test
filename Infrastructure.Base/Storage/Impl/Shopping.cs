using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base.Storage.Impl
{
    public class Shopping : IShopping
    {
        private readonly ISimpleDb db;
        private DateTime finished;

        public Shopping(ISimpleDb db): this(db, 0, DateTime.Now, DateTime.MinValue)
        {
        }

        public Shopping(ISimpleDb db, long id, DateTime created, DateTime finished)
        {
            this.db = db;
            Id = id;
            Created = created;
            this.finished = finished;
        }

        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Finished
        {
            get
            {
                return finished;
            }
            set
            {
                if(Id != 0)
                {
                    finished = value;
                    db.SetShopping(this);
                }
            }
        }

    }
}
