using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public ProductDto Product { get; set; }
    public string? CurrentUserId { get; set; }
    }
}