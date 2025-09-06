using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.Application.DTOs;
using MediatR;

namespace BagType.Application.Commands.DeleteBagType
{
    public class DeleteBagTypeCommand : IRequest<bool>
    {
    public Guid Id { get; set; }
    }
}