using Proyecto1_CristhianBonilla.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Proyecto1_CristhianBonilla.ViewModels
{
    public class UserView
    {
        public int IdUser { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string SecondSurname { get; set; }

        public int Age { get; set; }

        public string Password { get; set; }
        public string PasswordCheck { get; set; }

        public string Email { get; set; }

        public string Preferences { get; set; }

    }

}
