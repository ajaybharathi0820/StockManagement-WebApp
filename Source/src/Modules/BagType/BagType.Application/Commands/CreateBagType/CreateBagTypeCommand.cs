using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagType.Application.DTOs;
using MediatR;

namespace BagType.Application.Commands.CreateBagType
{
    public class CreateBagTypeCommand : IRequest<int>
    {
        public BagTypeDto BagType { get; set; }
    }
}