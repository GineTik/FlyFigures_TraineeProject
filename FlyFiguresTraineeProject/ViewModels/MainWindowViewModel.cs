using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Languages;
using FlyFiguresTraineeProject.Saving.Models;
using FlyFiguresTraineeProject.Saving.Strategies;
using FlyFiguresTraineeProject.Saving.Strategies.JsonSavingStrategy;
using FlyFiguresTraineeProject.ViewModels.Base;
using Microsoft.Win32;

namespace FlyFiguresTraineeProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly DispatcherTimer _dispatcherTimer;
    private Language _selectedLanguage = null!;
    private bool _isOpen;
    private string _selectedFileType;

    public ViewModelCommand AddFigureCommand { get; }
    public ViewModelCommand ClearFiguresCommand { get; }
    public ViewModelCommand SwitchInMotionOfFigureCommand { get; }
    public ViewModelCommand SaveStateCommand { get; }
    public ViewModelCommand LoadStateCommand { get; }

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

    public bool IsOpen
    {
        get => _isOpen;
        set => SetField(ref _isOpen, value);
    }

    public IEnumerable<string> AvailableFileTypes => new[]
    {
        "bin",
        "xml",
        "json"
    };

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

    private void SwitchLanguage()
    {
        LanguageSwitcher.Switch(SelectedLanguage.CultureInfo);
    }

    private async void SaveState(object? _)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = $"{SelectedFileType} files (*.{SelectedFileType})|*.{SelectedFileType}",
            FilterIndex = 2,
            RestoreDirectory = true
        };

        if (saveFileDialog.ShowDialog() == false)
            return;

        await using var stream = saveFileDialog.OpenFile();

        var strategy = SelectedFileType switch
        {
            "json" => new JsonSavingStrategy()
        };
        
        await strategy.Save(stream, new SavingState
        {
            Data = new SavingStateData
            {
                Figures = Figures.Select(f => f.MakeSnapshot()),
                CultureInfo = SelectedLanguage.CultureInfo.Name
            }
        });
        
        stream.Close();
    }

    private async void LoadState(object? _)
    {
        var saveFileDialog = new OpenFileDialog
        {
            Filter = $"{SelectedFileType} files (*.{SelectedFileType})|*.{SelectedFileType}",
            FilterIndex = 2,
            RestoreDirectory = true
        };
        
        if (saveFileDialog.ShowDialog() == false)
            return;

        await using var stream = (FileStream)saveFileDialog.OpenFile();

        var strategy = Path.GetExtension(stream.Name) switch
        {
            ".json" => new JsonSavingStrategy()
        };

        var state = await strategy.Load(stream);
        
    }
}