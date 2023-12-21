using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Controls;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Figures.Metadata;
using FlyFiguresTraineeProject.Resources.Languages;
using FlyFiguresTraineeProject.ViewModels.Base;
using WPFLocalizeExtension.Engine;

namespace FlyFiguresTraineeProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly Canvas _canvas;
    private readonly ObservableCollection<MovableFigure> _figures;
    private readonly DispatcherTimer _dispatcherTimer;
    
    public ViewModelCommand AddFigureCommand { get; }
    public ViewModelCommand ClearFiguresCommand { get; }
    public ViewModelCommand SwitchDisabledOfFigureCommand { get; }

    public Canvas Canvas => _canvas;
    public IEnumerable<MovableFigure> Figures => _figures;

    public MainWindowViewModel()
    {
        _canvas = new Canvas();
        _figures = new ObservableCollection<MovableFigure>();

        AddFigureCommand = new ViewModelCommand(AddFigure);
        ClearFiguresCommand = new ViewModelCommand(ClearFigures);
        SwitchDisabledOfFigureCommand = new ViewModelCommand(SwitchDisabledOfFigure);
        
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += MoveAndDrawFigures;
        _dispatcherTimer.Interval = TimeSpan.FromMicroseconds(1000);
        
        LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
        LocalizeDictionary.Instance.Culture = new CultureInfo("");
    }

    private void MoveAndDrawFigures(object? sender, EventArgs e)
    {
        foreach (var figure in _figures)
        {
            figure.Move();
            figure.Draw();
        }
    }
    
    private void AddFigure(object? figureData)
    {
        if (figureData == null) 
            throw new ArgumentNullException(nameof(figureData));

        if (_dispatcherTimer.IsEnabled == false)
            _dispatcherTimer.Start();

        var typedFigureData = (FigureData)figureData;
        var movableFigure = typedFigureData.Factory.Invoke(_canvas);
        
        _figures.Add(movableFigure);
    }
    
    private void ClearFigures(object? _)
    {
        _canvas.Children.Clear();
        _figures.Clear();
        _dispatcherTimer.Stop();
    }

    private void SwitchDisabledOfFigure(object? figure)
    {
        if (figure == null) 
            throw new ArgumentNullException(nameof(figure));
        
        var typedFigure = (MovableFigure)figure;
        var figureIndex = _figures.IndexOf(typedFigure);
        
        typedFigure.InMotion = !typedFigure.InMotion;
        _figures.RemoveAt(figureIndex);
        _figures.Insert(figureIndex, typedFigure);

        OnPropertyChanged(nameof(Figures));
    }
}