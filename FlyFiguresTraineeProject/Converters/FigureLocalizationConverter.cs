using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Figures.Configuration;

namespace FlyFiguresTraineeProject.Converters;

public class FigureLocalizationConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object Convert(object? figure, Type targetType, object? parameter, CultureInfo culture)
    {
        if (figure == null) throw new ArgumentNullException(nameof(figure));
        if (figure is not MovableFigure typedFigure) throw new ArgumentException(nameof(figure) + " is not MovableFigure");

        return typedFigure.FigureData.LocalizationKey;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}