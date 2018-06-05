using Android.App;
using Android.Content;
using Android.Runtime;
using FastShopping.Droid.ProductCatalog.Base.Mvp.Presenter;
using FastShopping.Droid.ProductCatalog.Presenter;
using Infrastructure.Android.CrossCutting.Ioc;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Unity;
using Unity.Lifetime;

namespace FastShopping.Droid
{
    [Application]
    public class App: Android.App.Application
    {
        public static IUnityContainer Container { get; private set; }

        public static bool AlreadyStartedUp { get; private set; }

        public App(): base()
        {
        }

        public App(IntPtr javaReference, JniHandleOwnership transfer): base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AlreadyStartedUp = false;

            Observable
                .Defer(() =>
                    Observable.Start(() =>
                    {
                        Register(this);
                    })
                )
                .ObserveOn(ReactiveUI.HandlerScheduler.MainThreadScheduler)
                .SubscribeOn(DefaultScheduler.Instance/*NewThreadScheduler.Default*/)
                .Subscribe(result => 
                {
                    StartSystem(this);
                    AlreadyStartedUp = true;
                }, (Exception err) => 
                {
                    InterruptSystem(this, err);
                });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public static void StartSystem(Context context)
        {
            var mainActivity = new Intent(context, typeof(MainActivity));
            context.StartActivity(mainActivity);
        }

        private static void InterruptSystem(Context context, Exception err)
        {
            var mainActivity = new Intent(context, typeof(MainActivity));
            mainActivity.PutExtra("error_message", err.Message);
            context.StartActivity(mainActivity);
        }

        private static void Register(Context context)
        {
            if(Container == null)
            {
                Container = new UnityContainer();

                Container.RegisterInstance(context, new ContainerControlledLifetimeManager());
                Container.RegisterInstance(ReactiveUI.HandlerScheduler.MainThreadScheduler, new ContainerControlledLifetimeManager());
                Container.RegisterType<IProductCatalogPresenter, ProductCatalogPresenter>();

                DI.Register(Container);
                CrossCutting.IoC.DI.Register(Container);
            }
        }
    }
}