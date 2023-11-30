using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    private readonly Canvas _context;
    private readonly double _speed;
    private Point _direction;
    private Point _currentPosition;
    
    protected Shape Shape { get; }
    private Point ActualExtremeLimit => new(_context.ActualWidth, _context.ActualHeight);
    public abstract string LocalizedName { get; }

    protected MovableFigure(Canvas context, Shape shape)
    {
        _direction = RandomHelper.NextDirection();
        _currentPosition = new Point((context.ActualWidth - shape.ActualWidth) / 2, (context.ActualHeight - shape.ActualHeight) / 2);
        _context = context;
        _speed = 3;

        Shape = shape;
    }
    
    public void Move()
    {
        _currentPosition = new Point(
            _currentPosition.X + _direction.X * _speed,
            _currentPosition.Y + _direction.Y * _speed);

        if (_currentPosition.X <= 0 || _currentPosition.X + Shape.ActualWidth >= ActualExtremeLimit.X)
        {
            _direction.X = -_direction.X;
            TouchedBoundary();
        }

        if (_currentPosition.Y <= 0 || _currentPosition.Y + Shape.ActualHeight >= ActualExtremeLimit.Y)
        {
            _direction.Y = -_direction.Y;
            TouchedBoundary();
        }
    }

    public void Draw()
    {
        Canvas.SetTop(Shape, _currentPosition.Y);
        Canvas.SetLeft(Shape, _currentPosition.X);

        if (_context.Children.Contains(Shape) == false)
            _context.Children.Add(Shape);
    }
    
    protected virtual void TouchedBoundary() {}
}