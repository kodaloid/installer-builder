using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace InstallerBuilder.Includes
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";

            if (!value.GetType().IsEnum)
                throw new ArgumentException("Value must be an Enumeration type");

            var sourceType = value.GetType();
            var sourceInfo = sourceType.GetMember(value.ToString()).First().GetCustomAttribute<DisplayAttribute>();

            return sourceInfo?.Description ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}