using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures;

public static class EllipseConstants
{
    public static Ellipse Instance => new()
    {
        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
        Width = 50,
        Height = 50
    };
    
    public static Ellipse Icon => new()
    {
        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
        Width = 20,
        Height = 20
    };
}

public class MovableEllipse : MovableFigure
{
    public MovableEllipse(Canvas context) : base(
        context,
        EllipseConstants.Instance)
    {}
}  