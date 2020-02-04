
using DomainModel.Model;
using DomainModel.Repositories;
using Framework.Repositories;

namespace Infrastructure.Repositories
{
    public class OrderAlteraionRepository : Repository<OrderAlteration>, IOrderAlterationRepository
    {
        readonly Context.ApplicationContext _context;
        public OrderAlteraionRepository(Context.ApplicationContext context) : base(context)
        {
           _context = context;
        }

    }
}