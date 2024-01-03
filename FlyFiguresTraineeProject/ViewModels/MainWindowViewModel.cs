using System;
using System.Linq;
using System.Media;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Events;
using FlyFiguresTraineeProject.Exceptions;
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
    private string _selectedFileType = null!;
    private FigureCollection _figures = null!;
    private MovableFigure? _selectedFigure;

    public ViewModelCommand AddFigureCommand { get; }
    public ViewModelCommand ClearFiguresCommand { get; }
    public ViewModelCommand SwitchInMotionOfFigureCommand { get; }
    public ViewModelCommand SaveStateCommand { get; }
    public ViewModelCommand LoadStateCommand { get; }
    public ViewModelCommand AddFiguresTouchedEventCommand { get; }
    public ViewModelCommand RemoveFiguresTouchedEventCommand { get; }

    public Canvas Canvas { get; private set; }

    public FigureCollection Figures
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
    
    public MovableFigure? SelectedFigure
    {
        get => _selectedFigure;
        set => SetField(ref _selectedFigure, value);
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
        Figures = new FigureCollection(Canvas);
        AvailableLanguages = new AvailableLanguages();
        SelectedLanguage = AvailableLanguages.Default;
        SelectedFileType = AvailableFileTypes.Json;

        AddFigureCommand = new ViewModelCommand(AddFigure);
        ClearFiguresCommand = new ViewModelCommand(ClearFigures, _ => Figures.Any());
        SwitchInMotionOfFigureCommand = new ViewModelCommand(SwitchInMotionOfFigure);
        SaveStateCommand = new ViewModelCommand(SaveState);
        LoadStateCommand = new ViewModelCommand(LoadState);
        AddFiguresTouchedEventCommand = new ViewModelCommand(AddFiguresTouchedEvent);
        RemoveFiguresTouchedEventCommand = new ViewModelCommand(RemoveFiguresTouchedEvent);
        
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += MoveAndDrawFigures;
        _dispatcherTimer.Interval = TimeSpan.FromMicroseconds(1000);
        _dispatcherTimer.Start();
    }

    private void MoveAndDrawFigures(object? sender, EventArgs e)
    {
        Figures.MoveAndDraw();
    }
    
    private void AddFigure(object? figureData)
    {
        if (figureData == null) 
            throw new ArgumentNullException(nameof(figureData));
        
        var typedFigureData = (FigureData)figureData;
        Figures.Add(typedFigureData.Factory);
    }
    
    private void ClearFigures(object? _ = null)
    {
        Figures.Clear();
    }

    private void SwitchInMotionOfFigure(object? figure)
    {
        if (figure == null) 
            throw new ArgumentNullException(nameof(figure));
        
        var typedFigure = (MovableFigure)figure;
        typedFigure.InMotion = !typedFigure.InMotion;
        Figures.ChangeFigure(typedFigure);
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
                Figures = Figures.Select(f => f.MakeSnapshot()).ToList(),
            }
        });
    }

    private async void LoadState(object? _)
    {
        var savingState = await Saver.Load();

        if (savingState == null)
            return;
        
        Figures.Restore(savingState.Data.Figures);
    }

    private void AddFiguresTouchedEvent(object? _)
    {
        if (SelectedFigure == null)
            return;
        
        SelectedFigure.FiguresTouched += FiguresTouchedEventHandler;
    }

    private void RemoveFiguresTouchedEvent(object? _)
    {
        if (SelectedFigure == null)
            return;

        SelectedFigure.FiguresTouched -= FiguresTouchedEventHandler;
    }
    
    private void FiguresTouchedEventHandler(object? o, FiguresTouchedEventArgs args)
    {
        Console.WriteLine(@"=================");
        SystemSounds.Beep.Play();
        Console.WriteLine($@"Coord: {JsonSerializer.Serialize(args.PointOfContact)}");
        Console.WriteLine($@"Figure {args.Sender.GetType().Name}({Figures.IndexOf(args.Sender)}) touched {args.TouchedThe.GetType().Name}({Figures.IndexOf(args.TouchedThe)})");
        Console.WriteLine(@"=================");
    }
}