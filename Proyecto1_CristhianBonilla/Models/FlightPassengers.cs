using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class FlightPassengers
    {
        [Key]
        public int IdFliPas { get; set; }

        [Required(ErrorMessage = "El numero de vuelo es requerido.")]
        public int UnitPrice { get; set; }

        [Required(ErrorMessage = "El origen del vuelo es requerido")]
         public int Quantity { get; set; }

        [ForeignKey("IdReservation")]
        public Reservations Reservations { get; set; }

        [ForeignKey("IdPasTyp")]
        public PassengerType PassengerType { get; set; }

    }
}
