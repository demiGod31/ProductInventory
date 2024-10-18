﻿using FluentValidation;
using ProductInventory.Application.CQRS.Commands;

namespace ProductInventory.Application.Validations
{
    public class UpdateProductValidator : AbstractValidator<UpdateProduct.Command>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
        }
    }
}