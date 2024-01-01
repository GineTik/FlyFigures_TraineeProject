using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Events;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Figures.Systems;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    protected static int DefaultSpeed => 3;
    
    private Canvas _context = null!;
    private CustomPoint _direction;
    private IReadOnlyCollection<MovableFigure> _figuresOfContext = null!;
    private FigureIntersectSystem _intersectSystem = null!;
    private FigureReflectionSystem _reflectionSystem = null!;
    
    public Shape Shape { get; }
    public FigureData FigureData { get; private set; }
    public bool InMotion { get; set; }
    public CustomPoint CurrentPosition { get; private set; }
    
    protected int Speed { get; set; }
    protected CustomPoint ExtremeLimit => new(_context.ActualWidth, _context.ActualHeight);
    protected CustomPoint Direction => _direction;
    
    public event EventHandler<FiguresTouchedEventArgs>? FiguresTouched;

    protected MovableFigure(Shape shape, FigureData figureData)
    {
        _direction = RandomHelper.NextDirection();
        Speed = DefaultSpeed;
        Shape = shape;
        FigureData = figureData;
        InMotion = true;
    }

    protected MovableFigure(MovableFigureSnapshot snapshot, Shape shape, FigureData figureData) 
        : this(shape, figureData)
    {
        _direction = snapshot.Direction;
        CurrentPosition = snapshot.CurrentPosition;
        InMotion = snapshot.InMotion;
    }

    public void Initialization(Canvas context, IReadOnlyCollection<MovableFigure> figures)
    {
        _context = context;
        _figuresOfContext = figures;
        _intersectSystem = new FigureIntersectSystem(this, _context, _figuresOfContext);
        _reflectionSystem = new FigureReflectionSystem(this);
        
        CurrentPosition = new CustomPoint((context.ActualWidth - Shape.ActualWidth) / 2, (context.ActualHeight - Shape.ActualHeight) / 2);
        Draw();
        _context.Children.Add(Shape);
    }

    public void Move()
    {
        if (InMotion == false)
            return;
        
        CurrentPosition = new CustomPoint(
            CurrentPosition.X + _direction.X * Speed,
            CurrentPosition.Y + _direction.Y * Speed);

        if (_reflectionSystem.CheckBoundaryTouch(ExtremeLimit))
        {
            _direction = _reflectionSystem.UpdateDirection(Direction, ExtremeLimit);
            TouchedBoundary();
        }
        
        if (FiguresTouched != null && _intersectSystem.CheckIntersect(out var participants))
        {
            OnFiguresTouched(participants!);
        }
    }

    public void Draw()
    {
        Canvas.SetTop(Shape, CurrentPosition.Y);
        Canvas.SetLeft(Shape, CurrentPosition.X);
    }

    private void OnFiguresTouched(FiguresTouchedEventArgs e)
    {
        FiguresTouched?.Invoke(this, e);
    }

    protected virtual void TouchedBoundary() {}
    public abstract MovableFigureSnapshot MakeSnapshot();
}