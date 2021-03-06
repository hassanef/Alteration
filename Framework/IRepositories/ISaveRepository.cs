﻿
using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.IRepositories
{
    public interface ISaveRepository<T> where T : class
    {

        event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;
        event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated;

        void Create(T item);

        Task<T> CreateAsync(T item);

        Task<T> CreateAsyncUoW(T item);


        void Update(T item);

        Task<T> UpdateAsync(T item);

        T UpdateUoW(T item);
        Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);

    }
}
