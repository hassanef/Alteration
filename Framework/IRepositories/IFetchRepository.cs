using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.IRepositories
{
    public interface IFetchRepository<T> where T : class
    {
        IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
         
        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate);

    }
}
