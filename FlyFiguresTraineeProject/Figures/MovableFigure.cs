using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    private readonly Canvas _context;
    private Point _direction;
    
    protected double Speed { get; set; }
    protected Point CurrentPosition { get; set; }
    protected Shape Shape { get; }
    protected Point ActualExtremeLimit => new(_context.ActualWidth, _context.ActualHeight);
    public abstract string LocalizedName { get; }

    protected MovableFigure(Canvas context, Shape shape)
    {
        _direction = RandomHelper.NextDirection();
        _context = context;
        CurrentPosition = new Point((context.ActualWidth - shape.ActualWidth) / 2, (context.ActualHeight - shape.ActualHeight) / 2);
        Speed = 3;
        Shape = shape;
    }
    
    public void Move()
    {
        CurrentPosition = new Point(
            CurrentPosition.X + _direction.X * Speed,
            CurrentPosition.Y + _direction.Y * Speed);

        if (CurrentPosition.X <= 0 || CurrentPosition.X + Shape.ActualWidth >= ActualExtremeLimit.X)
        {
            _direction.X = -_direction.X;
            TouchedBoundary();
        }

        if (CurrentPosition.Y <= 0 || CurrentPosition.Y + Shape.ActualHeight >= ActualExtremeLimit.Y)
        {
            _direction.Y = -_direction.Y;
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