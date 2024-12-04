﻿using MediatR;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
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
                var user = new User()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = hashedPassword
                };
                repository.Users.Add(user);
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
