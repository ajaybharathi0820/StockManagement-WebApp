using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Product.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product Id must not be empty");
            RuleFor(x => x.Product.ProductCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Product.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Product.Weight).GreaterThan(0);
        }
    }
}