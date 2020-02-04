using MediatR;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using DomainModel.Repositories;
using DomainModel.Model;
using Infrastructure.NServiceBus.Interfaces;

namespace Application.CommandHandlers
{
    public class CreateOrderAlterationCommandHandler :
        IRequestHandler<CreateOrderAlterationCommand, bool>
    {

        readonly IOrderAlterationRepository _orderAlterationRepository;

        public CreateOrderAlterationCommandHandler(IOrderAlterationRepository orderAlterationRepository)
        {
            _orderAlterationRepository = orderAlterationRepository;

        }

        public async Task<bool> Handle(CreateOrderAlterationCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new NullReferenceException("CreateOrderAlterCommand is null!");

            OrderAlteration orderALteraion = new OrderAlteration(request.LeftSleeve, request.RightSleeve, request.LeftTrouser, request.RightTrouser, Status.Created, request.CustomerName);

            await _orderAlterationRepository.CreateAsyncUoW(orderALteraion);

            await _orderAlterationRepository.SaveChangesAsync();

            return true;
        }
    }
}
