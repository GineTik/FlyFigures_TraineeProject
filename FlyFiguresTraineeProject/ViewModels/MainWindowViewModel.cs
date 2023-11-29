using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Models;
using FlyFiguresTraineeProject.ViewModels.Base;

namespace FlyFiguresTraineeProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static Dictionary<AvailableFigures, FigureFactoryWithLogMessage> AvailableFigureFactories => new()
    {
        { AvailableFigures.Ellipse, new FigureFactoryWithLogMessage { LogMessage = "Еліпс додано", Factory = (canvas) => new MovableEllipse(canvas) } },
        { AvailableFigures.Triangle, new FigureFactoryWithLogMessage { LogMessage = "Трикутник додано", Factory = (canvas) => new MovableTriangle(canvas) } }
    };
    
    private readonly Canvas _canvas = null!;
    private readonly ICollection<string> _nameOfAddedFigures = null!;
    private readonly ICollection<MovableFigure> _figures;
    
    public MainWindowViewModel()
    {
        Canvas = new Canvas();
        NameOfAddedFigures = new ObservableCollection<string>();
        _figures = new List<MovableFigure>();

        AddFigureCommand = new ViewModelCommand(AddFigure);
        
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += MoveAndDrawFigures;
        dispatcherTimer.Interval = TimeSpan.FromMicroseconds(1000);
        dispatcherTimer.Start();
    }

    public ViewModelCommand AddFigureCommand { get; }
    
    public Canvas Canvas
    {
        get => _canvas;
        private init => SetField(ref _canvas, value);
    }

    public ICollection<string> NameOfAddedFigures
    {
        get => _nameOfAddedFigures;
        private init => SetField(ref _nameOfAddedFigures, value);
    }

    private void MoveAndDrawFigures(object? sender, EventArgs e)
    {
        foreach (var figure in _figures)
        {
            figure.Move();
            figure.Draw();
        }
    }
    
    private void AddFigure(object? figure)
    {
        ArgumentNullException.ThrowIfNull(figure);

        var figureFactoryWithName = AvailableFigureFactories[(AvailableFigures)figure];

        NameOfAddedFigures.Add(figureFactoryWithName.LogMessage);
        _figures.Add(figureFactoryWithName.Factory.Invoke(Canvas));
    }
}