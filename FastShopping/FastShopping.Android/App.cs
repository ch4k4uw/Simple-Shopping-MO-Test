using Android.App;
using Android.Content;
using Android.Runtime;
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

        public App(): base()
        {
        }

        public App(IntPtr javaReference, JniHandleOwnership transfer): base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Observable
                .Start(() =>
                {
                    Register(this);
                })
                .ObserveOn(ReactiveUI.HandlerScheduler.MainThreadScheduler)
                .SubscribeOn(DefaultScheduler.Instance)
                .Subscribe(result => 
                {
                    
                }, (Exception err) => 
                {
                    
                });

        }

        private void StartSystem()
        {

        }

        private void InterruptSystem()
        {

        }

        private static void Register(Context context)
        {
            if(Container == null)
            {
                Container = new UnityContainer();

                Container.RegisterInstance(context, new ContainerControlledLifetimeManager());
                Container.RegisterInstance(ReactiveUI.HandlerScheduler.MainThreadScheduler, new ContainerControlledLifetimeManager());

                DI.Register(Container);
                CrossCutting.IoC.DI.Register(Container);
            }
        }
    }
}