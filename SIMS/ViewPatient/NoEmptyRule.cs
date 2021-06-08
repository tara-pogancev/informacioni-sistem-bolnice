using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;


namespace SIMS.ViewPatient
{
    class NoEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            var input = value as string;
           
            if (input.Length!=0)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Obavezno polje");
            }




        }
    }
}
