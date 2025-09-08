using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Application.Users.DTOs;
using Identity.Domain.Repositories;
using MediatR;

namespace Identity.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepo;

        public GetUserByIdQueryHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.Id,cancellationToken);
            if (user == null)
                throw new Exception("User not found");

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Role = user.Role.Name
            };
        }
    }
}