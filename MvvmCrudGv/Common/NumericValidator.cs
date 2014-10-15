using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MvvmCrudGv.Common
{
    class NumericValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var regex = @"^\d$";
                var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                   return ValidationResult.ValidResult;
                }
                else
                {
                    // does not match
                    return new ValidationResult
                    (false, "Please enter numeric value only!");
                }

        }
    }
}
