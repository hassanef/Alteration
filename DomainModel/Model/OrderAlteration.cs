using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Model
{
    public class OrderAlteration: Entity, IAggregateRoot
    {
        public Shorten ShortenSleeves { get; private set; }
        public Shorten ShortenTrousers { get; private set; }
        public Status OrderStatus { get; private set; }
        public virtual byte OrderStatusId { get; private set; }
        //This property is just for test
        public string CustomerName { get; private set; }

        public OrderAlteration(short leftSleeve, short rightSleeve, short leftTrouser, short rightTrouser, Status orderStatus, string customerName)
        {
            ShortenSleeves = new Shorten(leftSleeve, rightSleeve);
            ShortenTrousers = new Shorten(leftTrouser, rightTrouser);
            OrderStatusId = orderStatus.Id;
            CustomerName = customerName;
        }

        public OrderAlteration()
        { }
        public void SetOrderStatus(byte orderStatusId) => OrderStatusId = orderStatusId;
    }
}
