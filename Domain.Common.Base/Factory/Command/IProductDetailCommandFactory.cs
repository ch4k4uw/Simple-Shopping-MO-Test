using Domain.Base.Factory.Command;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Base.Factory.Command
{
    public interface IProductDetailCommandFactory: ILongIdCommandFactory<IProductAggregation>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IProductDetailQuery NewQuery(long id);
    }
}
