using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Scales
    {
        [Key]
        public int IdScale { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "Fecha de llegada es requerida")]
        public DateTime ArriveDate { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "Fecha de salida es requerida")]
        public DateTime DepartureDate { get; set; }

        public ICollection<FlightScales> FlightScales { get; set; }
    }
}
