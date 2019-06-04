using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Panacea.Modules.Education.Converters
{
    class EducationTypeToMaterialIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "EBooks":
                    return "book";
                case "Audio":
                    return "audiotrack";
                case "Webpages":
                    return "web";
                case "Games":
                    return "games";
                case "Videos":
                    return "video_library";
                case "Questionnaires":
                    return "question_answer";
                default:
                    return "stop";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
