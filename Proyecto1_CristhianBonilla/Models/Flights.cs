using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Flights
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFlights { get; set; }

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

        public Reservations Reservations { get; set; }

        public virtual ICollection<FlightScales> FlightScales { get; set; }
    }
}
