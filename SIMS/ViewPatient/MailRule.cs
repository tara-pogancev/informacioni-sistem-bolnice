using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SIMS.ViewPatient
{
    public class MailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            
                var mail = value as string;
                Regex regex = new Regex("[A-Za-z0-9-_.]+@[A-Za-z0-9-_]+(?:\\.[A-Za-z0-9]+)+");
                if (regex.Match(mail).Success)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Pogresan format mail adrese");
                }
                
               
            
            
        }
    }
}
