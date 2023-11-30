using System;
using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Models;

public class FigureFactory
{
    public string LocalizedName { get; set; } = null!;
    public Func<Canvas, MovableFigure> Factory { get; set; } = null!;
}