using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Panacea.Modules.Education.Converters
{
    class EducationTypeToActionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "EBooks":
                    return "Read";
                case "Audio":
                    return "Listen";
                case "Webpages":
                    return "View";
                case "Games":
                    return "Play";
                case "Videos":
                    return "Watch";
                case "Questionnaires":
                    return "Answer";
                default:
                    return "Open";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
