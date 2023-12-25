using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FlyFiguresTraineeProject.Figures.Data;

public class FigureData
{
    public required Shape Icon { get; set; }    
    public required string LocalizationKey { get; set; }
    public required Func<Canvas, MovableFigure> Factory { get; set; }
    public required Type FigureSnapshotType { get; set; }
}