using System;  
using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;  
using System.Linq;  
using System.Web;
using MediatR;
  
namespace Application.Commands
{  
    public class CreateOrderAlterationCommand  : IRequest<bool>
    {
        public short LeftSleeve{ get; private set; }  
        public short RightSleeve{ get; private set; }  
        public short LeftTrouser { get; private set; }
        public short RightTrouser { get; private set; }
        public string CustomerName { get; private set; }

        public CreateOrderAlterationCommand(short leftSleeve, short rightSleeve, short leftTrouser, short rightTrouser, string customerName)
        {
            LeftSleeve = leftSleeve;
            RightSleeve = rightSleeve;
            LeftTrouser = leftTrouser;
            RightTrouser = rightTrouser;
            CustomerName = customerName;
        }

    }  
} 