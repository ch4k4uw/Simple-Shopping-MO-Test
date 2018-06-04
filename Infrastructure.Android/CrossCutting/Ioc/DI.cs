using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Infrastructure.Android.Storage;
using Infrastructure.Base.Storage;
using Unity;
using Unity.Lifetime;

namespace Infrastructure.Android.CrossCutting.Ioc
{
    public class DI
    {
        public static void Register(IUnityContainer container)
        {
            RegisterDb(container);
            RegisterBase(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterDb(IUnityContainer container)
        {
            container.RegisterSingleton<ISimpleDb, AndroidSimpleDb>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterBase(IUnityContainer container)
        {
            Base.CrossCutting.Ioc.DI.Register(container);
        }
    }
}