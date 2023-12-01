using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures;

public static class CircleConstants
{
    public static Ellipse Instance => new()
    {
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

public class MovableCircle : MovableFigure
{
    private static readonly IList<string> Colors = new[] { "#ffaacc", "#000", "#004ac2" }; 
    private int _currentColorIndex;
    
    public override string LocalizedName => "Коло";
    
    public MovableCircle(Canvas context) : base(
        context,
        CircleConstants.Instance)
    {
        SetColor(0);
    }

    protected override void TouchedBoundary()
    {
        var nextColorIndex = (_currentColorIndex + 1) % Colors.Count;
        SetColor(nextColorIndex);
    }

    private void SetColor(int colorIndex)
    {
        _currentColorIndex = colorIndex;
        var color = Colors[_currentColorIndex];
        Shape.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(color)!;
    }
}