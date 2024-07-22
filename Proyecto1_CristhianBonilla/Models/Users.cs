using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Proyecto1_CristhianBonilla.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido.")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string? SecondSurname { get; set; }

        [Required(ErrorMessage = "La edad es requerida.")]
        [Range(18, 100, ErrorMessage = "La edad debe ser mayor a 17.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El email es requerido.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Preferences { get; set; }

        public ICollection<Reservations> Reservations { get; set; }

        public Users()
        {
            Reservations = new List<Reservations>();
        }
    }
}
