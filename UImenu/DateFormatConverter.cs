using System.Globalization;
using System.Windows.Data;
using EmployeeLib;

namespace UImenu;




public class DateFormatConverter : IValueConverter
{
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            DateTime.TryParseExact(str, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
            return date;
        }
        return value;
    }
}

