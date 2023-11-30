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
    private static Dictionary<Type, FigureFactory> AvailableFigureFactories => new()
    {
        { typeof(MovableEllipse), new FigureFactory { Factory = (canvas) => new MovableEllipse(canvas) } },
        { typeof(MovableTriangle), new FigureFactory { Factory = (canvas) => new MovableTriangle(canvas) } },
        { typeof(MovableRectangle), new FigureFactory { Factory = (canvas) => new MovableRectangle(canvas) } }
    };
    
    private readonly Canvas _canvas = null!;
    private readonly ICollection<string> _nameOfAddedFigures = null!;
    private readonly ICollection<MovableFigure> _figures;
    private readonly DispatcherTimer _dispatcherTimer;
    
    public ViewModelCommand AddFigureCommand { get; }
    public ViewModelCommand ClearFiguresCommand { get; }
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
    
    public MainWindowViewModel()
    {
        Canvas = new Canvas();
        NameOfAddedFigures = new ObservableCollection<string>();
        _figures = new List<MovableFigure>();

        AddFigureCommand = new ViewModelCommand(AddFigure);
        ClearFiguresCommand = new ViewModelCommand(ClearFigures);
        
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += MoveAndDrawFigures;
        _dispatcherTimer.Interval = TimeSpan.FromMicroseconds(1000);
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

        if (_dispatcherTimer.IsEnabled == false)
            _dispatcherTimer.Start();
        
        var figureFactory = AvailableFigureFactories[(Type)figure];
        var movableFigure = figureFactory.Factory.Invoke(Canvas);
        
        NameOfAddedFigures.Add(movableFigure.LocalizedName + " додано");
        _figures.Add(movableFigure);
    }
    
    private void ClearFigures(object? _)
    {
        Canvas.Children.Clear();
        _figures.Clear();
        _nameOfAddedFigures.Clear();
        _dispatcherTimer.Stop();
    }
}