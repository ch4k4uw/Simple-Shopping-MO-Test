using System;

namespace Domain.Base.Command
{
    public interface ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="complete"></param>
        void Exec(Action complete);
    }
}
