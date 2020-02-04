
using DomainModel.Model;
using System.Linq;

namespace DomainModel.Context
{
    public interface IContextReadOnly
    {
         IQueryable<OrderAlteration> OrderAlterations { get; }
    }  
}
