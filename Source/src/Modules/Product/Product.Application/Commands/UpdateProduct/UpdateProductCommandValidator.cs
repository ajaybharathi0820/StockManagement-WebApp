using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Product.Domain.Repositories;

namespace Product.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product Id must not be empty");
            
            RuleFor(x => x.Product.ProductCode)
                .NotEmpty().MaximumLength(50)
                .MustAsync(async (command, code, cancellation) => !await _productRepository.IsCodeExistsAsync(code, command.Product.Id, cancellation))
                .WithMessage("Product code already exists. Please choose a different code.");

            RuleFor(x => x.Product.Name)
                .NotEmpty().MaximumLength(100)
                .MustAsync(async (command, name, cancellation) => !await _productRepository.IsNameExistsAsync(name, command.Product.Id, cancellation))
                .WithMessage("Product name already exists. Please choose a different name.");

            RuleFor(x => x.Product.Weight).GreaterThan(0);
        }
    }
}