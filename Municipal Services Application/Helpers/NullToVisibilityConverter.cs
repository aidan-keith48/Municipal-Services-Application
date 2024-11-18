using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Municipal_Services_Application.Helpers
{
    // Converter class to convert a null value to Visibility.Collapsed and non-null value to Visibility.Visible
    public class NullToVisibilityConverter : IValueConverter
    {
        // Convert method to handle the conversion logic
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return Visibility.Collapsed if the value is null, otherwise return Visibility.Visible
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        // ConvertBack method is not implemented as it is not needed for this converter
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //-----------------------------------------------------------------------------------

    // Converter class to convert a null or empty string to Visibility.Visible and non-empty string to Visibility.Collapsed
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        // Convert method to handle the conversion logic
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return Visibility.Visible if the value is null or an empty string, otherwise return Visibility.Collapsed
            return string.IsNullOrWhiteSpace(value as string) ? Visibility.Visible : Visibility.Collapsed;
        }

        // ConvertBack method is not implemented as it is not needed for this converter
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //-----------------------------------------------------------------------------------

}
