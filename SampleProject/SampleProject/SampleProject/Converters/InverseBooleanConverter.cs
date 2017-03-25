using System;
using System.Globalization;
using Xamarin.Forms;

namespace SampleProject.Converters
{
    /// <summary>
    /// Converter that returns the inverse of a boolean.
    /// This will convert True to False, and visa-versa
    /// This is used inside of the XAML when you need to check if something is NOT equal to something.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.IValueConverter" />
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is bool))
            {
                return value;
            }

            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is bool))
            {
                return value;
            }

            return !(bool) value;
        }
    }
}
