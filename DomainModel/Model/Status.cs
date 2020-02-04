using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Model
{
    public class Status : Enumeration
    {
        public static Status All = new Status(0, nameof(All).ToLowerInvariant());
        public static Status Created = new Status(1, nameof(Created).ToLowerInvariant());
        public static Status Paid = new Status(2, nameof(Paid).ToLowerInvariant());
        public static Status Done = new Status(3, nameof(Done).ToLowerInvariant());



        public Status(Status status) : base(status.Id, status.Name)
        {
        }
        public Status()
        {

        }

        public Status(byte id, string name)
         : base(id, name)
        {
        }
    }
}
