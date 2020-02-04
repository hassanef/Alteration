using Framework.Repositories;
using System.Threading.Tasks;


namespace Framework.IRepositories
{
    public interface IDeleteRepository<T> where T : class
    {

        event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;


        bool DeleteUoW(T item);
        void Delete(T item);
        Task DeleteAsync(T item);


    }

}
