using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HeBianGu.Controls.MaterialControl
{
	public class TextFieldHintVisibilityConverter : IValueConverter
	{
        public Visibility IsEmptyValue { get; set; } = Visibility.Visible;
        public Visibility IsNotEmptyValue { get; set; } = Visibility.Hidden;
    
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.IsNullOrEmpty((value ?? "").ToString()) ? IsEmptyValue : IsNotEmptyValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}