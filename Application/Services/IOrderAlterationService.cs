using Common.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IOrderAlterationService 
    {
         Task<List<OrderAlterationViewModel>> Get(byte status);
    }
}