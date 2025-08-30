using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BagType.Application.DTOs;

namespace BagType.Application.Queries.GetBagTypes
{
    public record GetBagTypesQuery() : IRequest<List<BagTypeDto>>;
}