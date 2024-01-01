using System;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures.Systems;

public class FigureReflectionSystem
{
    private readonly MovableFigure _movableFigure;
    private CustomPoint CurrentPosition => _movableFigure.CurrentPosition;
    private Shape Shape => _movableFigure.Shape;

    public FigureReflectionSystem(MovableFigure movableFigure)
    {
        _movableFigure = movableFigure;
    }

    public bool CheckBoundaryTouch(CustomPoint extremeLimit)
    {
        if (CurrentPosition.X <= 0 ||
            CurrentPosition.X + Shape.ActualWidth >= extremeLimit.X)
        {
            return true;
        }
        
        if (CurrentPosition.Y <= 0 ||
            CurrentPosition.Y + Shape.ActualHeight >= extremeLimit.Y)
        {
            return true;
        }

        return false;
    }

    public CustomPoint UpdateDirection(CustomPoint oldDirection, CustomPoint extremeLimit)
    {
        if (CurrentPosition.X <= 0)
        {
            oldDirection.X = Math.Abs(oldDirection.X);
        }
        else if (CurrentPosition.X + Shape.ActualWidth >= extremeLimit.X)
        {
            oldDirection.X = -Math.Abs(oldDirection.X);
        }

        if (CurrentPosition.Y <= 0)
        {
            oldDirection.Y = Math.Abs(oldDirection.Y);
        }
        else if (CurrentPosition.Y + Shape.ActualHeight >= extremeLimit.Y)
        {
            oldDirection.Y = -Math.Abs(oldDirection.Y);
        }

        return oldDirection;
    }
}