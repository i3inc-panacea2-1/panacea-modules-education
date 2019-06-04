using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Panacea.Modules.Education.Converters
{
    class EducationTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "EBooks":
                    return new SolidColorBrush(Color.FromRgb(255, 145, 0));
                case "Audio":
                    return new SolidColorBrush(Color.FromRgb(139, 195, 74));
                case "Webpages":
                    return new SolidColorBrush(Color.FromRgb(59, 89, 152));
                case "Games":
                    return new SolidColorBrush(Color.FromRgb(76, 175, 80));
                case "Videos":
                    return new SolidColorBrush(Color.FromRgb(244, 67, 54));
                case "Questionnaires":
                    return new SolidColorBrush(Color.FromRgb(92, 0, 238));
                default:
                    return new SolidColorBrush(Color.FromRgb(92, 0, 238));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
