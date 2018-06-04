using Domain.Common.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entity
{
    internal class ShoppingEntity : IShoppingEntity
    {
        private readonly DateTime creation;
        private DateTime finished;

        private static IShoppingEntity empty = null;
        public static IShoppingEntity Empty
        {
            get
            {
                if(empty == null)
                {
                    empty = new ShoppingEntity();
                }
                return empty;
            }
        }

        public ShoppingEntity(): this(0, DateTime.Now, DateTime.MinValue)
        {

        }

        public ShoppingEntity(long id, DateTime creation, DateTime finished)
        {
            this.Id = id;
            this.creation = creation;
            this.finished = finished;
        }

        public long Id { get; internal set; }

        public DateTime Creation => new DateTime(creation.Ticks);

        public DateTime Finished => new DateTime(finished.Ticks);

        public bool IsFinished => finished.CompareTo(DateTime.MinValue) != 0;

        public void Finish()
        {
            if(!IsFinished)
            {
                finished = DateTime.Now;
            }
        }
    }
}
