using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewCarRental.Application.Interfaces.Authentication;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Validate existed email
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            // Hash password
            var hashedPassword = _passwordHasher.HashPassword(request.PasswordHash);

            var user = new User
            {
                FullName = request.FullName,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword,
                IsBlocked = false,
                RoleId = request.RoleId
            };

            await _userRepository.AddUserAsync(user);

            return user.UserId;

        }
    }
}
