using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace KisiRehberi
{
    internal class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            var val = (byte[])value;
            Stream stream = new MemoryStream(val);
            return ImageSource.FromStream(() =>
             {
                 return stream;
             });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
