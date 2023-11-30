using System;
using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Models;

public class FigureFactory
{
    public Func<Canvas, MovableFigure> Factory { get; set; } = null!;
}