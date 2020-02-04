using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using DomainModel.Repositories;
using Framework.BackgroundServiceConfigurations;
using DomainModel.Model;
using IntegrationEvent.Events;
using Infrastructure.NServiceBus.Interfaces;
using DomainModel.IntegrationEventModels;
using NServiceBus;

namespace Application.CommandHandlers
{
    public class ChangeStatusOrderAlterationCommandHandler :
        IRequestHandler<ChangeStatusOrderAlterationCommend, bool>
    {

        readonly IOrderAlterationRepository _orderAlterationRepository;
        readonly INServiceBusEndpoint _endpoint;
        readonly ILocalIntegrationEventRepository _localIntegrationEventRepository;

        public ChangeStatusOrderAlterationCommandHandler(IOrderAlterationRepository orderAlterationRepository,
                                                         INServiceBusEndpoint endpoint,
                                                         ILocalIntegrationEventRepository localIntegrationEventRepository)
        {
            _orderAlterationRepository = orderAlterationRepository;
            _localIntegrationEventRepository = localIntegrationEventRepository;
            _endpoint = endpoint;
        }

        public async Task<bool> Handle(ChangeStatusOrderAlterationCommend request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new NullReferenceException("CangeOrderStatusCommand is null!");

            var orderAlter = await _orderAlterationRepository.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (orderAlter == null)
                return false;

            orderAlter.SetOrderStatus(request.OrderStatus);

            try
            {
                _orderAlterationRepository.UpdateUoW(orderAlter);


                ////Save Local Integration Event with pending status (InsertOrderPaidEvent Or InsertAlterationFinishedEvent)
                //var orderPaidEvent = await InsertOrderPaidEvent(orderAlter);
                //var alterationFinishedEvent = await InsertAlterationFinishedEvent(orderAlter);

                await _orderAlterationRepository.SaveChangesAsync();


                try
                {
                    // publish Integration event
                    if (orderAlter.OrderStatusId == Status.Paid.Id)
                    {
                        var orderPaidEvent = new OrderPaidEvent(orderAlter.Id);
                        await _endpoint.Publish(orderPaidEvent);
                    }
                    else if (orderAlter.OrderStatusId == Status.Done.Id)
                    {
                        var alterationFinishedEvent = new AlterationFinishedEvent(orderAlter.Id);
                        await _endpoint.Publish(alterationFinishedEvent);
                    }
                }
                catch (Exception ex)
                {
                    //update Local Integration Event to ready status
                    //await _localIntegrationEventRepository.UpdateLocalIntegrationEvent(orderPaidEvent.Id);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }

        //public async Task<LocalIntegrationEventHelper<OrderPaidEvent>> InsertOrderPaidEvent(OrderAlteration orderALteration)
        //{
        //    var orderPaidEvent = new OrderPaidEvent(orderALteration.Id);

        //    var messageId = Guid.NewGuid();
        //    LocalIntegrationEvent localIntegrationEvent = await _localIntegrationEventRepository.SaveLocalIntegrationEvent(messageId, orderALteration);

        //    var response = new LocalIntegrationEventHelper<OrderPaidEvent>()
        //    {
        //        Id = localIntegrationEvent.Id,
        //        MessageId = messageId,
        //        Event = orderPaidMessage
        //    };

        //    return response;
        //}
    }
}
