using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Generic.Application.Infrastructure.AutofacModules
{
    public class ApplicationModule
       : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            

        }
    }
}
