using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModel
{
    public class OrderAlterationViewModel
    {
        public int Id { get; set; }
        public short LeftSleeve { get; set; }
        public short RightSleeve { get; set; }
        public short LeftTrouser { get; set; }
        public short RightTrouser { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerName { get; set; }

        public OrderAlterationViewModel()
        { }

    }
}
