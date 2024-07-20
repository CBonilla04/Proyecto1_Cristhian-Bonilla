using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Reservations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReservation { get; set; }

        [Required(ErrorMessage = "El nombre de la reserva es requerido.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "La fecha de la reserva es requerida.")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "El estado de la reserva es requerido.")]
        [AllowedValues("C","P","R")] // C = Car, P = Pending, R = Ready
        public string State { get; set; }

        [Required(ErrorMessage = "El precio total es requerido")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("IdUser")]
        public Users User { get; set; }

        [ForeignKey("IdFlight")]
        public virtual ICollection<Flights> Flights { get; set; }

        public virtual ICollection<FlightPassengers> FlightPassengers { get; set; }
    }
}
