using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Figures.Metadata;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    protected static int DefaultSpeed => 3;
    
    private readonly Canvas _context;
    private Point _direction;

    protected int Speed { get; set; }
    protected Shape Shape { get; }
    protected Point CurrentPosition { get; set; }
    protected Point ExtremeLimit => new(_context.ActualWidth, _context.ActualHeight);
    
    public bool InMotion { get; set; }

    protected MovableFigure(Canvas context, Shape shape)
    {
        _context = context;
        _direction = RandomHelper.NextDirection();
        CurrentPosition = new Point((context.ActualWidth - shape.ActualWidth) / 2, (context.ActualHeight - shape.ActualHeight) / 2);
        Speed = DefaultSpeed;
        Shape = shape;
        InMotion = true;
    }

    public void Move()
    {
        if (InMotion == false)
            return;
        
        CurrentPosition = new Point(
            CurrentPosition.X + _direction.X * Speed,
            CurrentPosition.Y + _direction.Y * Speed);

        if (CurrentPosition.X <= 0)
        {
            _direction.X = Math.Abs(_direction.X);
            TouchedBoundary();
        }
        else if (CurrentPosition.X + Shape.ActualWidth >= ExtremeLimit.X)
        {
            _direction.X = -Math.Abs(_direction.X);
            TouchedBoundary();
        }

        if (CurrentPosition.Y <= 0)
        {
            _direction.Y = Math.Abs(_direction.Y);
            TouchedBoundary();
        }
        else if (CurrentPosition.Y + Shape.ActualHeight >= ExtremeLimit.Y)
        {
            _direction.Y = -Math.Abs(_direction.Y);
            TouchedBoundary();
        }
    }

    public void Draw()
    {
        Canvas.SetTop(Shape, CurrentPosition.Y);
        Canvas.SetLeft(Shape, CurrentPosition.X);

        if (_context.Children.Contains(Shape) == false)
            _context.Children.Add(Shape);
    }
    
    protected virtual void TouchedBoundary() {}
}