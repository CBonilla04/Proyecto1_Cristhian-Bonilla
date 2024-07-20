using Microsoft.EntityFrameworkCore;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto1_CristhianBonilla.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpClient _httpClient;

        public ReservationService(AppDbContext appDbContext, HttpClient httpClient)
        {
            _appDbContext = appDbContext;
            _httpClient = httpClient;
        }

        public async Task<Reservations> insert(Reservations reservation)
        {

            await _appDbContext.Reservations.AddAsync(reservation);
            if (reservation == null)
            {
                return null;
            }

            return reservation;

        }
        public async Task<Users> AddReservations(List<FlightOffer> flights, int userId)
        {
            Users user = await _appDbContext.Users
                .Include(u => u.Reservations) // Incluye las reservas para evitar el problema de seguimiento
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var flightOffer in flights)
                {
                    Reservations reservation = new Reservations
                    {
                        Name = flightOffer.Source,
                        ReservationDate = DateTime.Now,
                        State = "P",
                        TotalPrice = decimal.Parse(flightOffer.Price.GrandTotal),
                        User = user,
                        Flights = new List<Flights>()
                    };

                    // Añade la reserva al usuario
                    user.Reservations.Add(reservation);

                    foreach (var itineraries in flightOffer.Itineraries)
                    {
                        Flights flight = new Flights
                        {
                            Origin = itineraries.Segments[0].Departure.IataCode,
                            Destination = itineraries.Segments[itineraries.Segments.Count - 1].Arrival.IataCode,
                            DepartureDate = DateTime.Parse(itineraries.Segments[0].Departure.At),
                            ArriveDate = DateTime.Parse(itineraries.Segments[itineraries.Segments.Count - 1].Arrival.At),
                            Reservations = reservation
                        };

                        // Añade el vuelo a la reserva
                        reservation.Flights.Add(flight);

                        foreach (var segment in itineraries.Segments)
                        {
                            Scales scale = new Scales
                            {
                                Number = int.Parse(segment.Number),
                                Origin = segment.Departure.IataCode,
                                Destination = segment.Arrival.IataCode,
                                DepartureDate = DateTime.Parse(segment.Departure.At),
                                ArriveDate = DateTime.Parse(segment.Arrival.At),
                            };

                            
                        }
                        //_appDbContext.Flights.Add(flight);
                    }
                    user.Reservations.Add(reservation);
                }

                // Actualiza el usuario y guarda todos los cambios
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Manejar la excepción según sea necesario
                return null;
            }
        }

    }
}

