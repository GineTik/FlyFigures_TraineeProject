using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures.Systems;

public class FigureReflectionSystem
{
    private readonly MovableFigure _movableFigure;
    private readonly Canvas _canvas;
    private CustomPoint CurrentPosition => _movableFigure.CurrentPosition;
    private Shape Shape => _movableFigure.Shape;
    private CustomPoint ExtremeLimit => _movableFigure.ExtremeLimit;
    private CustomPoint Direction => _movableFigure.Direction;

    private CustomPoint _updatedDirection;

    public FigureReflectionSystem(MovableFigure movableFigure, Canvas canvas)
    {
        _movableFigure = movableFigure;
        _canvas = canvas;
    }
    
    public bool CheckBoundaryTouch()
    {
        _updatedDirection = Direction;
        
        if (CurrentPosition.X <= 0)
            _updatedDirection.X = Math.Abs(Direction.X);
        else if (CurrentPosition.X + Shape.ActualWidth >= ExtremeLimit.X)
            _updatedDirection.X = -Math.Abs(Direction.X);

        if (CurrentPosition.Y <= 0)
            _updatedDirection.Y = Math.Abs(Direction.Y);
        else if (CurrentPosition.Y + Shape.ActualHeight >= ExtremeLimit.Y)
            _updatedDirection.Y = -Math.Abs(Direction.Y);

        return IsDirectionUpdated();
    }

    public CustomPoint UpdateDirection()
    {
        return _updatedDirection;
    }
    
    private bool IsDirectionUpdated()
    {
        return _updatedDirection.Equals(Direction) == false;
    }
}