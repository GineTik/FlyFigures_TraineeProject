using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    private readonly Canvas _context;
    private Point _direction;
    private Point _currentPosition;
    private readonly Shape _shape;
    private readonly double _speed;
    
    protected MovableFigure(Canvas context, Shape shape)
    {
        _direction = new Point(RandomHelper.NextDirection(), RandomHelper.NextDirection());
        _currentPosition = new Point((context.ActualWidth - shape.ActualWidth) / 2, (context.ActualHeight - shape.ActualHeight) / 2);
        _shape = shape;
        _context = context;
        _speed = 0.1;
            
        _context.Children.Add(_shape);
    }
    
    private Point ActualExtremeLimit => new(_context.ActualWidth, _context.ActualHeight);
    
    public void Move()
    {
        _currentPosition = new Point(
            _currentPosition.X + _direction.X * _speed,
            _currentPosition.Y + _direction.Y * _speed);

        if (_currentPosition.X <= 0 || _currentPosition.X + _shape.ActualWidth >= ActualExtremeLimit.X)
            _direction.X = -_direction.X;
        
        if (_currentPosition.Y <= 0 || _currentPosition.Y + _shape.ActualHeight >= ActualExtremeLimit.Y)
            _direction.Y = -_direction.Y;
    }

    public void Draw()
    {
        Canvas.SetTop(_shape, _currentPosition.Y);
        Canvas.SetLeft(_shape, _currentPosition.X);
    }
}