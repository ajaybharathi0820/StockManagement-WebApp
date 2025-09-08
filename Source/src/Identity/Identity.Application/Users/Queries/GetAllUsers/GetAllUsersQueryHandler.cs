using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Application.Users.DTOs;
using MediatR;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepo;

        public GetAllUsersQueryHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepo.GetAllAsync(cancellationToken);

            return users.Select(p => new UserDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                DateOfBirth = p.DateOfBirth,
                Email = p.Email,
                Role = p.Role.Name
            });
        }
    }
}