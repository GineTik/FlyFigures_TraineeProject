using System;
using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Models;

public class FigureFactoryWithLogMessage
{
    public string LogMessage { get; set; } = null!;
    public Func<Canvas, MovableFigure> Factory { get; set; } = null!;
}