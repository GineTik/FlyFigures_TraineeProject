﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Events;
using FlyFiguresTraineeProject.Exceptions;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Figures.Systems;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures;

public abstract class MovableFigure
{
    protected static int DefaultSpeed => 3;
    
    private Canvas _canvas = null!;
    private CustomPoint _direction;
    private IReadOnlyCollection<MovableFigure> _figuresOfContext = null!;
    private FigureIntersectSystem _intersectSystem = null!;
    private FigureReflectionSystem _reflectionSystem = null!;
    private PushingInsideSystem _pushingInsideSystem = null!;
    
    public Shape Shape { get; }
    public FigureData FigureData { get; private set; }
    public bool InMotion { get; set; }
    public CustomPoint CurrentPosition { get; private set; }
    public CustomPoint ExtremeLimit => new(_canvas.ActualWidth, _canvas.ActualHeight);
    public int Speed { get; protected set; }
    public CustomPoint Direction => _direction;
    
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

    public void Initialization(Canvas canvas, IReadOnlyCollection<MovableFigure> figures)
    {
        _canvas = canvas;
        _figuresOfContext = figures;
        _intersectSystem = new FigureIntersectSystem(this, _canvas, _figuresOfContext);
        _reflectionSystem = new FigureReflectionSystem(this, _canvas);
        _pushingInsideSystem = new PushingInsideSystem(this);
        
        CurrentPosition = new CustomPoint((canvas.ActualWidth - Shape.ActualWidth) / 2, (canvas.ActualHeight - Shape.ActualHeight) / 2);
        Draw();
        _canvas.Children.Add(Shape);
    }

    public void Move()
    {
        if (InMotion == false)
            return;

        if (Shape.ActualWidth >= ExtremeLimit.X
            || Shape.ActualHeight >= ExtremeLimit.Y)
            return;
        
        CurrentPosition = new CustomPoint(
            CurrentPosition.X + _direction.X * Speed,
            CurrentPosition.Y + _direction.Y * Speed);
        
        if (_pushingInsideSystem.CheckExitFromBoundary())
        {
            CurrentPosition = _pushingInsideSystem.UpdatePosition();
            throw new FigureOutsideCanvasException(this);
        }
        
        if (_reflectionSystem.CheckBoundaryTouch())
        {
            _direction = _reflectionSystem.UpdateDirection();
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