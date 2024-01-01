using System;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures.Configuration;

public class FigureData
{
    public required Shape Icon { get; set; }    
    public required string LocalizationKey { get; set; }
    public required Func<MovableFigure> Factory { get; set; }
}