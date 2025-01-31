﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Application.UseCases.Queries.GetAnnouncementById.Dtos;
using Rentful.Domain.Entities.Enums;
using Rentful.Domain.Exceptions;
using System.Net;

namespace Rentful.Application.UseCases.Queries.GetAnnouncementById
{
    public class GetAnnouncementByIdUseCase
    {
        public record Query(int AnnouncementId) : IRequest<AnnouncementDetailsDto>;

        internal class Handler(IRepository repository) : IRequestHandler<Query, AnnouncementDetailsDto>
        {
            public async Task<AnnouncementDetailsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var announcement = await repository.Announcements
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.Images)
                    .Include(x => x.Apartment)
                    .ThenInclude(x => x.Location)
                    .Include(x => x.User)
                    .Include(x => x.Reservations)
                    .FirstOrDefaultAsync(x => x.Id == request.AnnouncementId, cancellationToken)
                    ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Błąd podczas pobierania oferty", "Nie znaleziono ogłoszenia");

                var announcementDetails = new AnnouncementDetailsDto()
                {
                    Id = announcement.Apartment.Id,
                    Title = announcement.Title,
                    Description = announcement.Description,
                    Price = announcement.Apartment.Price,
                    Rent = announcement.Apartment.Rent ?? 0,
                    Deposit = announcement.Apartment.Deposit ?? 0,
                    Area = announcement.Apartment.Area,
                    DateAdded = announcement.DateAdded,
                    HasBalcony = announcement.Apartment.HasBalcony,
                    HasElevator = announcement.Apartment.HasElevator,
                    HasParkingSpace = announcement.Apartment.HasParkingSpace,
                    Images = announcement.Apartment.Images.OrderByDescending(x => x.IsThumbnail).Select(x => x.Source).ToList(),
                    IsAnimalFriendly = announcement.Apartment.IsAnimalFriendly,
                    IsFurnished = announcement.Apartment.IsFurnished,
                    NumberOfRooms = announcement.Apartment.NumberOfRooms,
                    City = announcement.Apartment.Location.City,
                    Province = announcement.Apartment.Location.Province,
                    Longitude = announcement.Apartment.Location.Longitude,
                    Latitude = announcement.Apartment.Location.Latitude,
                    IsPrecise = announcement.Apartment.Location.IsPrecise,
                    FirstName = announcement.User.FirstName,
                    LastName = announcement.User.LastName,
                    Email = announcement.User.Email,
                    TelephoneNumber = announcement.User.TelephoneNumber ?? string.Empty,
                    UserId = announcement.User.Id,
                    Reservations = announcement.Reservations
                        .Where(x => x.Status == ReservationStatusEnum.Available)
                        .Select(x => new ReservationDto
                        {
                            Id = x.Id,
                            Date = x.Date
                        })
                        .ToList(),
                };
                return announcementDetails;
            }
        }
    }
}
