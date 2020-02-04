
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.IRepositories
{
    public interface IRepository<T> : ISaveRepository<T>, IDeleteRepository<T>, IFetchRepository<T> where T : class
    {


        System.Threading.Tasks.Task<int> SaveChangesAsync();

        void SaveChanges();
    }
}
