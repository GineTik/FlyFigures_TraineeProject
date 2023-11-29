using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.ViewModels.Base;

namespace FlyFiguresTraineeProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static Dictionary<AvailableFigures, Func<Canvas, MovableFigure>> AvailableFigureFactories => new()
    {
        { AvailableFigures.Ellipse, (canvas) => new MovableEllipse(canvas) },
        { AvailableFigures.Triangle, (canvas) => new MovableTriangle(canvas) }
    };
    
    private readonly Canvas _canvas = null!;
    private readonly ICollection<MovableFigure> _figures;
    
    public MainWindowViewModel()
    {
        Canvas = new Canvas();
        _figures = new List<MovableFigure>();

        AddFigureCommand = new ViewModelCommand((figure) =>
        {
            ArgumentNullException.ThrowIfNull(figure);
            var newFigure = AvailableFigureFactories[(AvailableFigures)figure].Invoke(Canvas);
            _figures.Add(newFigure);
        });
        
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += MoveAndDrawFigures;
        dispatcherTimer.Interval = TimeSpan.FromMicroseconds(100);
        dispatcherTimer.Start();
    }
    
    public ViewModelCommand AddFigureCommand { get; }

    public Shape StyledIconEllipse => EllipseConstants.Icon;
    
    public Shape StyledIconTriangle => TriangleConstants.Icon;
    
    public Canvas Canvas
    {
        get => _canvas;
        private init => SetField(ref _canvas, value);
    }

    private void MoveAndDrawFigures(object? sender, EventArgs e)
    {
        foreach (var figure in _figures)
        {
            figure.Move();
            figure.Draw();
        }
    }
}