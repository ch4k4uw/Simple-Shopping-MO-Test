using Domain.Common.Base.Factory.Entity;
using Domain.Common.Base.Factory.Value;
using Infrastructure.Base.Rest;
using Infrastructure.Base.Storage;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;

namespace Infrastructure.Base.Repository
{
    internal class Repository
    {
        protected readonly IScheduler uiThreadScheduler;
        protected readonly ISimpleDb simpleDb;
        protected readonly IRestApi restApi;
        protected readonly IEntityFactory entityFactory;
        protected readonly IValueFactory valueFactory;

        public Repository(IScheduler uiThreadScheduler, ISimpleDb simpleDb, IRestApi restApi, IEntityFactory entityFactory, IValueFactory valueFactory)
        {
            this.uiThreadScheduler = uiThreadScheduler;
            this.simpleDb = simpleDb;
            this.restApi = restApi;
            this.entityFactory = entityFactory;
            this.valueFactory = valueFactory;
        }

        public Repository(IScheduler uiThreadScheduler, ISimpleDb simpleDb, IEntityFactory entityFactory) : this(uiThreadScheduler, simpleDb, null, entityFactory, null)
        {

        }

        public Repository(IScheduler uiThreadScheduler, ISimpleDb simpleDb, IRestApi restApi, IEntityFactory entityFactory): this(uiThreadScheduler, simpleDb, restApi, entityFactory, null)
        {
        }

        public Repository(IScheduler uiThreadScheduler, IRestApi restApi, IEntityFactory entityFactory): this(uiThreadScheduler, null, restApi, entityFactory)
        {
            
        }

        protected void ReactiveFunction<T>(Func<T> func, Action<T> success, Action<Exception> error)
        {
            Observable
                    .Start(() =>
                    {
                        return func.Invoke();
                    })
                    .ObserveOn(uiThreadScheduler)
                    .SubscribeOn(DefaultScheduler.Instance)
                    .Subscribe(result =>
                    {
                        success.Invoke(result);
                    }, error);
        }
    }
}
