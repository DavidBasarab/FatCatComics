using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SplashPageComics.Converters
{
    /// <summary>
    ///     Converts a Boolean into a Visibility.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     If set to True, conversion is reversed: True will become Collapsed.
        /// </summary>
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, targetType.ToString(), parameter, language);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, string typeName, object parameter, string language)
        {
            var val = System.Convert.ToBoolean(value);
            
            if (IsReversed) val = !val;

            return val ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, string typeName, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}