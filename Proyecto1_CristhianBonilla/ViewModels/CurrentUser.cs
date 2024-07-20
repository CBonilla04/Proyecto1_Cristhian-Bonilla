

using Proyecto1_CristhianBonilla.Models;

namespace Proyecto1_CristhianBonilla.ViewModels
{
    public class CurrentUser
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Preferences { get; set; }

        public CurrentUser(Users user)
        {
            IdUser = user.IdUser;
            Name = user.Name;
            Surname = user.Surname;
            SecondSurname = user.SecondSurname;
            Age = user.Age;
            Email = user.Email;
            Preferences = user.Preferences;
        }

        public CurrentUser() { }
    }
}
