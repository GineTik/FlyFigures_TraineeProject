using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    private readonly Canvas _context;
    private Point _direction;
    private Point _currentPosition;
    private readonly Shape _shape;
    
    protected MovableFigure(Canvas context, Shape shape)
    {
        var random = new Random();

        _direction = new Point(
            random.Next(-2, 1) * random.NextDouble(), 
            random.Next(-2, 1) * random.NextDouble());
        _currentPosition = new Point((context.ActualWidth - shape.ActualWidth) / 2, (context.ActualHeight - shape.ActualHeight) / 2);
        _shape = shape;
        _context = context;
        
        context.Children.Add(_shape);
        Draw();
    }
    
    private Point ActualExtremeLimit => new(_context.ActualWidth, _context.ActualHeight);
    
    public void Move()
    {
        _currentPosition = new Point(
            _currentPosition.X + _direction.X,
            _currentPosition.Y + _direction.Y);

        if (_currentPosition.X <= 0 || _currentPosition.X + _shape.ActualHeight >= ActualExtremeLimit.X)
            _direction.X = -_direction.X;
        
        if (_currentPosition.Y <= 0 || _currentPosition.Y + _shape.ActualHeight >= ActualExtremeLimit.X)
            _direction.Y = -_direction.Y;
    }

    public void Draw()
    {
        Canvas.SetTop(_shape, _currentPosition.Y);
        Canvas.SetLeft(_shape, _currentPosition.X);
    }
}