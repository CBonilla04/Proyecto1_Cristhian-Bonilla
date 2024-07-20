using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class FlightScales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFliSca { get; set; }

        [ForeignKey("IdScale")]
        public Scales Scales { get; set; }

        [ForeignKey("IdFlight")]
        public Flights Flights { get; set; }

    }
}
