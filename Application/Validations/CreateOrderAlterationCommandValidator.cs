using Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{

    public class CreateOrderAlterationCommandValidator : AbstractValidator<CreateOrderAlterationCommand>
    {
        public CreateOrderAlterationCommandValidator()
        {
            RuleFor(command => command.LeftSleeve).NotNull().Must(x => x >= -5 && x <= 5);
            RuleFor(command => command.RightSleeve).NotNull().Must(x => x >= -5 && x <= 5); 
            RuleFor(command => command.LeftTrouser).NotNull().Must(x => x >= -5 && x <= 5);
            RuleFor(command => command.RightTrouser).NotNull().Must(x => x >= -5 && x <= 5);
        }
    }

}
