using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Languages;
using FlyFiguresTraineeProject.Saving;
using FlyFiguresTraineeProject.Saving.Models;
using FlyFiguresTraineeProject.ViewModels.Base;

namespace FlyFiguresTraineeProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly DispatcherTimer _dispatcherTimer;
    private Language _selectedLanguage = null!;
    private bool _isOpen;
    private string _selectedFileType;
    private ObservableCollection<MovableFigure> _figures;

    public ViewModelCommand AddFigureCommand { get; }
    public ViewModelCommand ClearFiguresCommand { get; }
    public ViewModelCommand SwitchInMotionOfFigureCommand { get; }
    public ViewModelCommand SaveStateCommand { get; }
    public ViewModelCommand LoadStateCommand { get; }

    public Canvas Canvas { get; private set; }

    public ObservableCollection<MovableFigure> Figures
    {
        get => _figures;
        private set => SetField(ref _figures, value);
    }
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

    public bool IsOpen
    {
        get => _isOpen;
        set => SetField(ref _isOpen, value);
    }

    public string SelectedFileType
    {
        get => _selectedFileType;
        set => SetField(ref _selectedFileType, value);
    }
    
    public MainWindowViewModel()
    {
        Canvas = new Canvas();
        Figures = new ObservableCollection<MovableFigure>();
        AvailableLanguages = new AvailableLanguages();
        SelectedLanguage = AvailableLanguages.Default;
        SelectedFileType = "json";

        AddFigureCommand = new ViewModelCommand(AddFigure);
        ClearFiguresCommand = new ViewModelCommand(ClearFigures, _ => Figures.Any());
        SwitchInMotionOfFigureCommand = new ViewModelCommand(SwitchInMotionOfFigure);
        SaveStateCommand = new ViewModelCommand(SaveState);
        LoadStateCommand = new ViewModelCommand(LoadState);
        
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
    
    private void ClearFigures(object? _ = null)
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

    private void SwitchLanguage()
    {
        LanguageSwitcher.Switch(SelectedLanguage.CultureInfo);
    }

    private async void SaveState(object? _)
    {
        await Saver.Save(SelectedFileType, new SavingState
        {
            Data = new SavingStateData
            {
                Figures = Figures.Select(f => f.MakeSnapshot()),
                CultureInfo = SelectedLanguage.CultureInfo.Name
            }
        });
    }

    private async void LoadState(object? _)
    {
        var savingState = await Saver.Load();

        if (savingState == null)
            return;
        
        SelectedLanguage = AvailableLanguages.FirstOrDefault(l => l.CultureInfo.Name == savingState.Data.CultureInfo) ?? AvailableLanguages.Default;
        ClearFigures();
        Figures = new ObservableCollection<MovableFigure>(savingState.Data.Figures.Select(s => s.Restore(Canvas)));
        _dispatcherTimer.Start();
    }
}