using System.ComponentModel.DataAnnotations;

namespace Proyecto1_CristhianBonilla.Utils
{
    public class EmailValidator : RegularExpressionAttribute
    {
        public EmailValidator() : base(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
        {
            ErrorMessage = "El email no es válido.";
        }
    }
}
