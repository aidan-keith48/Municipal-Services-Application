using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Municipal_Services_Application.FromValidation
{
    public class CategoryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string selectedItem && !string.IsNullOrEmpty(selectedItem))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Selection is required.");
        }
    }
}
