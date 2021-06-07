using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SIMS.ViewPatient
{
    public class AddressRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            var mail = value as string;
            Regex regex = new Regex(@"[\w\s]+[,][0-9]+[,][\w\s]+");
            if (regex.Match(mail).Success)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Format adrese:Ulica,Broj,Grad");
            }




        }
    }
}
