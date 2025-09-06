using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Product.Application.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
    public Guid Id { get; set; }
    }
}