using Application.CommandHandlers;
using Application.Commands;
using DomainModel.Model;
using DomainModel.Repositories;
using Infrastructure.NServiceBus.Interfaces;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Application
{
    public class CreateOrderAlterationCommandHandlerTest
    {
        private readonly Mock<IOrderAlterationRepository> _orderAlterationRepositoryMock;
        private readonly Mock<ILocalIntegrationEventRepository> _localIntegrationEventRepositoryMock;
        private readonly Mock<INServiceBusEndpoint> _endpointMock;

        public CreateOrderAlterationCommandHandlerTest()
        {
            _orderAlterationRepositoryMock = new Mock<IOrderAlterationRepository>();
            _localIntegrationEventRepositoryMock = new Mock<ILocalIntegrationEventRepository>();
            _endpointMock = new Mock<INServiceBusEndpoint>();
        }

        [Fact]
        public async void Handle_Return_true()
        {

            // setup
            _orderAlterationRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<OrderAlteration>()))
                           .Returns(Task.FromResult<OrderAlteration>(FakeOrderAlteration()));

            _orderAlterationRepositoryMock.Setup(repo => repo.SaveChangesAsync())
            .Returns(Task.FromResult(1));

            // action
            var handler = new CreateOrderAlterationCommandHandler(_orderAlterationRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            CreateOrderAlterationCommand command = new CreateOrderAlterationCommand(5,0,-2, 1, "testCustomer");

            //Act
            var result = await handler.Handle(command, cltToken);


            // assert
            Assert.True(result);
        }

        private OrderAlteration FakeOrderAlteration()
        {
            return new OrderAlteration(5, 0, -2, 1, Status.Created, "testCustomer");
        }
    }
}
