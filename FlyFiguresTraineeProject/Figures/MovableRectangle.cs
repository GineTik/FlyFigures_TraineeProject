using System.Management;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Figures;

public static class RectangleConstants
{
    public static Rectangle Instance => new()
    {
        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
        Width = 50,
        Height = 70
    };
}


public class MovableRectangle : MovableFigure
{
    public MovableRectangle(Canvas context) : base(context, RectangleConstants.Instance, AvailableFigureData.MovableRectangle)
    {
    }
    
    public MovableRectangle(Canvas context, MovableRectangleSnapshot snapshot) : base(
        context, snapshot, RectangleConstants.Instance, AvailableFigureData.MovableRectangle)
    {
    }

    public override MovableFigureSnapshot MakeSnapshot()
    {
        return new MovableRectangleSnapshot
        {
            CurrentPosition = CurrentPosition,
            Direction = Direction,
            InMotion = InMotion
        };
    }
}