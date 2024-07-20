using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;
using static System.Formats.Asn1.AsnWriter;

namespace Proyecto1_CristhianBonilla.Services
{
    public class ReservationOrder : IReservationOrder
    {
        private readonly AppDbContext _reserOrderContext;
        private readonly IUserService _userContext;
        private readonly IReservationService _reservationService;
        private readonly IFlightScaleService _flightScaleServiceContext;

        public ReservationOrder(AppDbContext reserOrderContext, IUserService userContext, IReservationService reservationService, IFlightScaleService flightScaleServiceContext)
        {
            _reserOrderContext = reserOrderContext;
            _userContext = userContext;
            _reservationService = reservationService;
            _flightScaleServiceContext = flightScaleServiceContext;
        }
        public async Task<ReservationService> IntegrateReservation(List<FlightOffer> flights, int userId)
        {
            try
            {
                using (var transaction = _reserOrderContext.Database.BeginTransaction())
                {
                    Users user = await _userContext.getUserById(userId);
                    List<Reservations> reservationsToSave = new List<Reservations>();
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

                        // Añade la reserva al usuario
                        //await _reservationService.insert(reservation);
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
                                //await _flightScaleServiceContext.insert(flightScales);// DbContext.FlightScales.AddAsync(flightScales);


                            }

                            


                                // Añade el vuelo a la reserva
                                reservation.Flights.Add(flight);
                            //_appDbContext.Flights.Add(flight);
                        }

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

                        //user.Reservations.Add(reservation);

                        //foreach (var itineraries in flightOffer.Itineraries)
                        //{
                        //    Flights flight = new Flights
                        //    {
                        //        Origin = itineraries.Segments[0].Departure.IataCode,
                        //        Destination = itineraries.Segments[itineraries.Segments.Count - 1].Arrival.IataCode,
                        //        DepartureDate = DateTime.Parse(itineraries.Segments[0].Departure.At),
                        //        ArriveDate = DateTime.Parse(itineraries.Segments[itineraries.Segments.Count - 1].Arrival.At),
                        //        Reservations = reservation
                        //    };

                        //    // Añade el vuelo a la reserva
                        //    reservation.Flights.Add(flight);
                        //    //_appDbContext.Flights.Add(flight);
                        //}
                        
                    }
                    user.Reservations = reservationsToSave;
                    user = await _userContext.updateUser(user);
                    await _reserOrderContext.SaveChangesAsync();
                    transaction.Commit();
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
