using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Scales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdScale { get; set; }

        [Required(ErrorMessage = "El numero de vuelo es requerido.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "El origen del vuelo es requerido")]
        [MaxLength(50)]
        public string Origin { get; set; }

        [Required(ErrorMessage = "El destino del vuelo es requerido")]
        [MaxLength(50)]
        public string Destination { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "Fecha de llegada es requerida")]
        public DateTime ArriveDate { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Required(ErrorMessage = "Fecha de salida es requerida")]
        public DateTime DepartureDate { get; set; }

        public ICollection<FlightScales> FlightScales { get; set; }
    }
}
