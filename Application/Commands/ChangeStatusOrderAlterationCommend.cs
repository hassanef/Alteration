using System;  
using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;  
using System.Linq;  
using System.Web;
using MediatR;
  
namespace Application.Commands
{  
    public class ChangeStatusOrderAlterationCommend : IRequest<bool>
    {  
        public int Id { get; private set; }
        public byte OrderStatus { get; private set; }

        public ChangeStatusOrderAlterationCommend(int id, byte orderStatus)
        {
            Id = id;
            OrderStatus = orderStatus;
        }
    }
} 