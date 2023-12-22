using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WPFLocalizeExtension.Extensions;

namespace FlyFiguresTraineeProject.Converters;

public class InMotionConverter : MarkupExtension, IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        var inMotion = (bool)value;
        return inMotion ? "GoStop" : "GoMove";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}