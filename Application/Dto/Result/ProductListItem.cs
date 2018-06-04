using System;
using System.Runtime.Serialization;

namespace Application.Dto.Result
{
    public class ProductListItem : ISerializable
    {
        public long Id { get; internal set; }

        public string Name { get; internal set; }

        public string Photo { get; internal set; }

        public int Quantity { get; internal set; }

        public float Discount { get; internal set; }

        public double Price { get; internal set; }

        public ProductCategory Category { get; internal set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
        }
    }
}
