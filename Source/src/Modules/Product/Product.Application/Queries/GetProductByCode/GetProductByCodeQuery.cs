using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Queries.GetProductByCode
{
    public record GetProductByCodeQuery(string ProductCode) : IRequest<ProductDto>;
}