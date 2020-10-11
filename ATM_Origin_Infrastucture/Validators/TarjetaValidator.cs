using ATM_Origin.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin_Infrastucture.Validators
{
    public class TarjetaValidator : AbstractValidator<TarjetaDto>
    {
        public TarjetaValidator ()
        {
            RuleFor(tarjeta => tarjeta.Balance)
                .NotNull();
        }
    }
}
