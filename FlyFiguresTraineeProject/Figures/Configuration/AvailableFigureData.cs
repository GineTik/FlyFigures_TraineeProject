using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures.Configuration;

public class AvailableFigureData : IEnumerable<FigureData>
{
    public static FigureData MovableCircle = new()
    {
        Icon = new Ellipse
        {
            Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
            Width = 20,
            Height = 20
        },
        LocalizationKey = "MovableCircle",
        Factory = () => new MovableCircle(),
    };
    
    public static FigureData MovableRectangle = new()
    {
        Icon = new Rectangle
        {
            Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
            Width = 15,
            Height = 23
        },
        LocalizationKey = "MovableRectangle",
        Factory = () => new MovableRectangle(),
    };
    
    public static FigureData MovableTriangle = new()
    {
        Icon = new Polygon
        {
            Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
            Points = new PointCollection(new[]
            {
                new Point(0, 0),
                new Point(15, 10),
                new Point(0, 20)
            })
        },
        LocalizationKey = "MovableTriangle",
        Factory = () => new MovableTriangle(),
    };

    public static AvailableFigureData Instance { get; } = new AvailableFigureData();

    public IEnumerator<FigureData> GetEnumerator()
    {
        yield return MovableCircle;
        yield return MovableRectangle;
        yield return MovableTriangle;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}