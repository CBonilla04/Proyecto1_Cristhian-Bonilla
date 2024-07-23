using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public class ReservationOrder : IReservationOrder
    {
        private readonly AppDbContext _reserOrderContext;
        private readonly IUserService _userContext;
        private readonly IReservationService _reservationService;
        private readonly IFlightScaleService _flightScaleServiceContext;
        private readonly IEmailSender _emailSender;

        public ReservationOrder(AppDbContext reserOrderContext, IUserService userContext, IReservationService reservationService, IFlightScaleService flightScaleServiceContext, IEmailSender emailSender)
        {
            _reserOrderContext = reserOrderContext;
            _userContext = userContext;
            _reservationService = reservationService;
            _flightScaleServiceContext = flightScaleServiceContext;
            _emailSender = emailSender;
        }
        // Method to integrate a reservation
        public async Task<ReservationService> IntegrateReservation(List<FlightOffer> flights, int userId, string userName)
        {
            int reservationsQuantity = 0;
            decimal TotalAmount = 0;
            try
            {
                // Start the transaction
                using (var transaction = _reserOrderContext.Database.BeginTransaction())
                {
                    Users user = await _userContext.getUserById(userId);
                    List<Reservations> reservationsToSave = new List<Reservations>();

                    reservationsQuantity = flights.Count;
                    // Loop through the list of flights
                    foreach (var flightOffer in flights)
                    {
                        Reservations reservation = new Reservations
                        {
                            Name = flightOffer.Source,
                            ReservationDate = DateTime.Now,
                            State = "P",
                            TotalPrice = decimal.Parse(flightOffer.Price.GrandTotal),
                            User = user,
                            Flights = new List<Flights>(),
                            FlightPassengers = new List<FlightPassengers>()
                        };
                        TotalAmount += reservation.TotalPrice;


                        // Loop through the list of itineraries
                        foreach (var itineraries in flightOffer.Itineraries)
                        {
                            Flights flight = new Flights
                            {
                                Origin = itineraries.Segments[0].Departure.IataCode,
                                Destination = itineraries.Segments[itineraries.Segments.Count - 1].Arrival.IataCode,
                                DepartureDate = DateTime.Parse(itineraries.Segments[0].Departure.At),
                                ArriveDate = DateTime.Parse(itineraries.Segments[itineraries.Segments.Count - 1].Arrival.At),
                                Reservations = reservation,
                                FlightScales = new List<FlightScales>()
                            };
                            // Loop through the list of segments
                            foreach (var segment in itineraries.Segments)
                            {
                                Scales scale = new Scales
                                {
                                    Number = int.Parse(segment.Number),
                                    Origin = segment.Departure.IataCode,
                                    Destination = segment.Arrival.IataCode,
                                    DepartureDate = DateTime.Parse(segment.Departure.At),
                                    ArriveDate = DateTime.Parse(segment.Arrival.At),
                                    FlightScales = new List<FlightScales>()
                                };
                                FlightScales flightScales = new FlightScales
                                {
                                    Flights = flight,
                                    Scales = scale
                                };

                                scale.FlightScales.Add(flightScales);
                                flight.FlightScales.Add(flightScales);


                            }
                            reservation.Flights.Add(flight);
                        }
                        // Loop through the list of travelerPricings
                        foreach (var pricing in flightOffer.TravelerPricings)
                        {
                            PassengerType passenger = new PassengerType
                            {
                                Type = pricing.TravelerType,
                                Description = pricing.FareOption,
                                FlightPassengers = new List<FlightPassengers>()
                            };
                            FlightPassengers flightPassengers = new FlightPassengers
                            {
                                UnitPrice = decimal.Parse(pricing.Price.Total),
                                Quantity = 1,
                                Reservations = reservation,
                                PassengerType = passenger
                            };
                            passenger.FlightPassengers.Add(flightPassengers);
                            reservation.FlightPassengers.Add(flightPassengers);
                        }
                        reservationsToSave.Add(reservation);

                    }
                    // Save the reservation
                    user.Reservations = reservationsToSave;
                    user = await _userContext.updateUser(user);
                    await _reserOrderContext.SaveChangesAsync();
                    // Commit the transaction
                    transaction.Commit();
                    // Send the email to user
                    await _emailSender.SendEmailReservation(user.Name, user.Email, TotalAmount.ToString(), reservationsQuantity.ToString(), "¡Reservación de vuelo!");
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
