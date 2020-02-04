using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.Domain
{
    public class OrderAlterationTest
    {

        [Fact]
        public void New_orderAlteration_is_not_null()
        {
            OrderAlteration orderAlteration = null;

            // Arrange
            orderAlteration = new OrderAlteration(1, -5, 0, 5, Status.Created, "testCustomer");

            // Assert
            Assert.NotNull(orderAlteration);

        }
     
    }
}
