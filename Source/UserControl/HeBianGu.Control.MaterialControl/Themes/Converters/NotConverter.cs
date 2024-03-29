using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HeBianGu.Controls.MaterialControl
{
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as bool?) ?? !bool.Parse(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value as bool?) ?? !bool.Parse(value.ToString());
        }
    }    
}