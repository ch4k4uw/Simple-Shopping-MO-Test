using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Common.Base.Application.Service
{
    public interface ISetAsFavoriteApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="favorite"></param>
        /// <param name="complete"></param>
        void SetAsFavorite(long id, bool favorite, Action complete);
    }
}
