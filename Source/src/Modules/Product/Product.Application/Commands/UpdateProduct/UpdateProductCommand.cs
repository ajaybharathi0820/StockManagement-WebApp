using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Commands.UpdateProduct
{
    public class UpdateProductCommand: IRequest<bool>
    {
        public ProductDto Product { get; set; }
    public string? CurrentUserId { get; set; }
    }
}