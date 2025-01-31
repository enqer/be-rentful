﻿using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Commands.RegisterUser
{
    public static class RegisterUserUseCase
    {
        public record Command(string FirstName, string LastName, string Email, string Password) : IRequest;

        internal class Handler : IRequestHandler<Command>
        {
            private readonly IRepository repository;

            public Handler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var isUserAlreadyExist = repository.Users.Any(x => x.Email == request.Email);
                if (isUserAlreadyExist)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd rejestracji", "Użytkownik już istnieje");
                }
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var defaultRole = repository.Roles.FirstOrDefault(x => x.Type == RoleEnum.User)
                    ?? throw new HttpResponseException(HttpStatusCode.BadRequest, "Błąd rejestracji", "Nie można przypisać roli do uzytkownika");
                var user = new User()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = hashedPassword,
                    Address = new Address(),
                    GlobalId = Guid.NewGuid(),
                    Roles = new List<Role>()
                    {
                        defaultRole
                    }
                };
                repository.Users.Add(user);
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
