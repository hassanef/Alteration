using Application.Behaviors;
using Application.Commands;
using Application.Validations;
using Autofac;
using Autofac.Core;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Generic.Application.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            //// Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateOrderAlterationCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(ChangeStatusOrderAlterationCommend).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));


            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(CreateOrderAlterationCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();


            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }

    }
}
