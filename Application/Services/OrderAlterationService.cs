

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModel;
using DomainModel.Context;
using DomainModel.Model;
using Microsoft.EntityFrameworkCore;

namespace  Application.Service
{
    public class OrderAlterationService : IOrderAlterationService
    {
        private readonly IContextReadOnly _contextReadonly;
        
        public OrderAlterationService(IContextReadOnly contextReadonly)
        {
            _contextReadonly = contextReadonly;
        }

        public async Task<List<OrderAlterationViewModel>> Get(byte status)
        {
            var query = _contextReadonly.OrderAlterations.AsQueryable();

            if (status != Status.All.Id)
            {
                query =  query.Where(x => x.OrderStatusId == status).AsQueryable();
            }

            return await query.Select(x => new OrderAlterationViewModel()
            {
                Id = x.Id,
                LeftSleeve = x.ShortenSleeves.Left,
                RightSleeve = x.ShortenSleeves.Right,
                LeftTrouser = x.ShortenTrousers.Left,
                RightTrouser = x.ShortenTrousers.Right,
                CustomerName = x.CustomerName,
                OrderStatus = (x.OrderStatusId == Status.Created.Id) ? "Created" : (x.OrderStatusId == Status.Paid.Id) ? "Paid" : (x.OrderStatusId == Status.Done.Id) ? "Done" : string.Empty
            }).ToListAsync();
        }
    }
}