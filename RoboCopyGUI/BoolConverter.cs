using System;
using System.Windows.Data;

namespace Zpy.Wpf.Convertrers
{
    /// <summary>
    /// 用于将bool值转换为其相反值的转换器
    /// </summary>
    public class BoolNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            return value;
        }
    }
}
