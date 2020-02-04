using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Framework.IRepositories;
using Z.EntityFramework.Plus;

namespace Framework.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region [Eventing Section]
        IRepository<T> implementation;

        #region [Events]
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated; 
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;
        #endregion

        public void PopulateEvents(IRepository<T> _implementation)
        {
            implementation = _implementation;

            implementation.BeforeSavingRecord += new EventHandler<EntitySavingEventArgs<T>>(this.OnBeforeSavingRecord);
            implementation.SavingRecord += new EventHandler<EntitySavingEventArgs<T>>(this.OnSavingRecord);
            implementation.RecordSaved += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordSaved);
            implementation.BeforeDeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnBeforeDeletingRecord);
            implementation.DeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnDeletingRecord);
            implementation.RecordDeleted += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnRecordDeleted);
            implementation.UpdatingRecord += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnUpdatingRecord);
            implementation.RecordUpdated += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordUpdated);

        }

        #region [Virtual Mothods]
        public virtual void OnBeforeSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordSaved(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnUpdatingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordUpdated(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnBeforeDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnRecordDeleted(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        #endregion

        #endregion
        private readonly DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (BeforeSavingRecord != null)
                BeforeSavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            _dbContext.Set<T>().Add(item);

            if (SavingRecord != null)
                SavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });


            _dbContext.SaveChanges();

            if (RecordSaved != null)
                RecordSaved.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
        }

        public virtual async Task<T> CreateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (BeforeSavingRecord != null)
                BeforeSavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            await _dbContext.Set<T>().AddAsync(item);

            if (SavingRecord != null)
                SavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });


            await _dbContext.SaveChangesAsync();

            if (RecordSaved != null)
                RecordSaved.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            return item;

        }

        public virtual async Task<T> CreateAsyncUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            await _dbContext.Set<T>().AddAsync(item);

            return item;
        }

   
        public virtual void Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Entry(item).State = EntityState.Modified;
            if (UpdatingRecord != null)
                UpdatingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            _dbContext.SaveChanges();
            if (RecordUpdated != null)
                RecordUpdated.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

        }
        public virtual async Task<T> UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Entry(item).State = EntityState.Modified;
            if (UpdatingRecord != null)
                UpdatingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            await _dbContext.SaveChangesAsync();
            if (RecordUpdated != null)
                RecordUpdated.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            return item;
        }

        public virtual T UpdateUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Update(item);

            return item;
        }
        public virtual async Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return await _dbContext.Set<T>().Where(predicate).UpdateAsync(updatePredicate);
        }

        public virtual void SaveChanges()
        {
                _dbContext.SaveChanges();
        }

        public virtual async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }


        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().FirstOrDefault() : _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _dbContext.Set<T>().FirstOrDefaultAsync() : await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        


        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? await _dbContext.Set<T>().SingleOrDefaultAsync() : await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().AsNoTracking() :
                 _dbContext.Set<T>().AsNoTracking().Where(predicate);
        }

        public virtual bool DeleteUoW(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return true;
        }
        public virtual void Delete(T item)
        {
            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            if (DeletingRecord != null)
                DeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            _dbContext.SaveChanges();

            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }
        public virtual async Task DeleteAsync(T item)
        {
            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            if (DeletingRecord != null)
                DeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            await _dbContext.SaveChangesAsync();

            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }

    }
}
