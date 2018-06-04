using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Application.Dto.Result
{
    public class ProductDetail: ISerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Price { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory ProductCategory { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public float Discount { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; internal set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
        }
    }
}
