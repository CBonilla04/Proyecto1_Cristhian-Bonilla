using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Reservations
    {
        [Key]
        public int IdReservation { get; set; }

        [Required(ErrorMessage = "El nombre de la reserva es requerido.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El estado del pago es requerido")]
        [MaxLength(1)]
        [AllowedValues("P","R")] // P = Pending, R = Ready
        public string PayState { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "La fecha de la reserva es requerida.")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "El estado de la reserva es requerido.")]
        [AllowedValues("C","P","R")] // C = Car, P = Pending, R = Ready
        public string State { get; set; }

        [ForeignKey("IdUser")]
        public Users User { get; set; }

        [ForeignKey("IdFlight")]
        public Flights Flight { get; set; }

        public ICollection<FlightPassengers> FlightPassengers { get; set; }
    }
}
