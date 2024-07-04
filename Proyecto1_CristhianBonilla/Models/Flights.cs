using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Flights
    {
        [Key]
        public int IdFlight { get; set; }

        [Required(ErrorMessage = "El numero de vuelo es requerido.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "El origen del vuelo es requerido")]
        [MaxLength(50)]
        public string Origin { get; set; }

        [Required(ErrorMessage = "El destino del vuelo es requerido")]
        [MaxLength(50)]
        public string Destination { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "La fecha de salida es requerida.")]
        public DateTime DepartureDate { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "La fecha de llegada es requerida.")]
        public DateTime ArriveDate { get; set; }

        [Required(ErrorMessage = "El precio total es requerido")]
        public int totalPrice { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
        public ICollection<FlightScales> FlightScales { get; set; }
    }
}
