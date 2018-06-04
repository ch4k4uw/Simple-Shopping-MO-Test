using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace FastShopping.CrossCutting.IoC
{
    public class DI
    {
        public static void Register(IUnityContainer container)
        {
            Domain.DI.Register(container);
            Application.DI.Register(container);
        }
    }
}
