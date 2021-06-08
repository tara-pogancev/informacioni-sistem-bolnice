using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SIMS.ViewPatient.ValidationRules
{
    public class OnlyNumversRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            var number = value as string;
            Regex regex = new Regex("[0-9]+$");
            if (regex.Match(number).Success)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Samo su brojevi dozvoljeni");
            }




        }
    }
}
