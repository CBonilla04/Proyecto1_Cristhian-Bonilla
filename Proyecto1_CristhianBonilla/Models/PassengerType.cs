﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Proyecto1_CristhianBonilla.Models
{
    public class PassengerType
    {
        [Key]
        public int IdPasTyp { get; set; }

        [Required(ErrorMessage = "El tipo de pasajero es obligatorio")]
        [MaxLength(50)]
        public string Type { get; set; }

        [AllowNull]
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<FlightPassengers> FlightPassengers { get; set; }

    }
}
