using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.Application.DTOs;
using MediatR;

namespace BagType.Application.Commands.CreateBagType
{
    public class CreateBagTypeCommand : IRequest<Guid>
    {
        public BagTypeDto BagType { get; set; }
    public string? CurrentUserId { get; set; }
    }
}