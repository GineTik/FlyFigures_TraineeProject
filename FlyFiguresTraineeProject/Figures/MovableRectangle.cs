using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures;

public static class RectangleConstants
{
    public static Rectangle Instance => new()
    {
        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
        Width = 50,
        Height = 70
    };
    
    public static Rectangle Icon => new()
    {
        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
        Width = 15,
        Height = 23
    };
}


public class MovableRectangle : MovableFigure
{
    public override string LocalizedName => "Прямокутник";
    
    public MovableRectangle(Canvas context) : base(context, RectangleConstants.Instance)
    {
    }
}