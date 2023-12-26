using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Figures;

internal static class CircleConstants
{
    public static Ellipse Instance => new()
    {
        Width = 50,
        Height = 50
    };
}

public class MovableCircle : MovableFigure
{
    private static readonly IList<string> Colors = new[] { "#ffaacc", "#000", "#004ac2" }; 
    private int _currentColorIndex;
    
    public MovableCircle(Canvas context) : base(
        context,
        CircleConstants.Instance,
        AvailableFigureData.MovableCircle)
    {
        SetColor(0);
    }

    public MovableCircle(Canvas context, MovableCircleSnapshot snapshot) : base(
        context, snapshot, CircleConstants.Instance, AvailableFigureData.MovableCircle)
    {
        SetColor(snapshot.CurrentColorIndex);
    }

    protected override void TouchedBoundary()
    {
        var nextColorIndex = (_currentColorIndex + 1) % Colors.Count;
        SetColor(nextColorIndex);
    }

    public override MovableFigureSnapshot MakeSnapshot()
    {
        return new MovableCircleSnapshot
        {
            CurrentColorIndex = _currentColorIndex,
            CurrentPosition = CurrentPosition,
            Direction = Direction,
            InMotion = InMotion
        };
    }

    private void SetColor(int colorIndex)
    {
        _currentColorIndex = colorIndex;
        var color = Colors[_currentColorIndex];
        Shape.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(color)!;
    }
}