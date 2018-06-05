using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Application.Dto.Result
{
    public class ProductCategory: ISerializable
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
        }
    }
}
