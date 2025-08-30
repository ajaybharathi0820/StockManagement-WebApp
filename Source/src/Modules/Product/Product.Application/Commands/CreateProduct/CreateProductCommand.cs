using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public ProductDto Product { get; set; }
    }
}