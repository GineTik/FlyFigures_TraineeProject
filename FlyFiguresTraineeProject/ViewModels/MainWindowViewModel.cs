using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Figures.Data;
using FlyFiguresTraineeProject.Languages;
using FlyFiguresTraineeProject.ViewModels.Base;

namespace FlyFiguresTraineeProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly DispatcherTimer _dispatcherTimer;
    private Language _selectedLanguage = null!;
    
    public ViewModelCommand AddFigureCommand { get; }
    public ViewModelCommand ClearFiguresCommand { get; }
    public ViewModelCommand SwitchInMotionOfFigureCommand { get; }

    public Canvas Canvas { get; private set; }
    public ObservableCollection<MovableFigure> Figures { get; private set; }
    public AvailableLanguages AvailableLanguages {get; private set; }
    public Language SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            SetField(ref _selectedLanguage, value);
            SwitchLanguage();
        }
    }

    public MainWindowViewModel()
    {
        Canvas = new Canvas();
        Figures = new ObservableCollection<MovableFigure>();
        AvailableLanguages = new AvailableLanguages();
        SelectedLanguage = AvailableLanguages.Default;

        AddFigureCommand = new ViewModelCommand(AddFigure);
        ClearFiguresCommand = new ViewModelCommand(ClearFigures, _ => Figures.Any());
        SwitchInMotionOfFigureCommand = new ViewModelCommand(SwitchInMotionOfFigure);
        
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += MoveAndDrawFigures;
        _dispatcherTimer.Interval = TimeSpan.FromMicroseconds(1000);
        _dispatcherTimer.Start();
    }

    private void MoveAndDrawFigures(object? sender, EventArgs e)
    {
        foreach (var figure in Figures)
        {
            figure.Move();
            figure.Draw();
        }
    }
    
    private void AddFigure(object? figureData)
    {
        if (figureData == null) 
            throw new ArgumentNullException(nameof(figureData));
        
        var typedFigureData = (FigureData)figureData;
        var movableFigure = typedFigureData.Factory.Invoke(Canvas);
        
        Figures.Add(movableFigure);
    }
    
    private void ClearFigures(object? _)
    {
        Canvas.Children.Clear();
        Figures.Clear();
        _dispatcherTimer.Stop();
    }

    private void SwitchInMotionOfFigure(object? figure)
    {
        if (figure == null) 
            throw new ArgumentNullException(nameof(figure));
        
        var typedFigure = (MovableFigure)figure;
        var figureIndex = Figures.IndexOf(typedFigure);
        
        typedFigure.InMotion = !typedFigure.InMotion;
        Figures.RemoveAt(figureIndex);
        Figures.Insert(figureIndex, typedFigure);
    }

    public void SwitchLanguage()
    {
        LanguageSwitcher.Switch(SelectedLanguage.CultureInfo);
    }
}