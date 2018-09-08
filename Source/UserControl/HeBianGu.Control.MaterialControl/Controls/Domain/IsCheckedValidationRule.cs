﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace HeBianGu.Controls.MaterialControl
{
    public class IsCheckedValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if( value is bool && (bool) value)
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Option must be checked");
        }
    }
}