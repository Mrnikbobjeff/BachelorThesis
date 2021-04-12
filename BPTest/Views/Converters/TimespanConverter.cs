using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BPTest.Views.Converters
{
    public class TimespanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is TimeSpan span)
            {
                if (span.TotalHours == 0)
                    return $"{span.TotalHours}:{span.TotalMinutes}:{span.TotalSeconds}";
                return $"{span.TotalMinutes}:{span.TotalSeconds}";
            }
            throw new InvalidOperationException("Tried casting timespan in binding which is not a timespan.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
