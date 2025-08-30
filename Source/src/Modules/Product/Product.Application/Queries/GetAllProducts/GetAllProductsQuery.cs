using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.DTOs;

namespace Product.Application.Queries.GetAllProducts
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
}