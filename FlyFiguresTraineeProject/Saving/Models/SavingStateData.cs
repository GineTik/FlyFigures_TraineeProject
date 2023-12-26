using System.Collections.Generic;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Saving.Models;

public class SavingStateData
{
    public required string CultureInfo { get; set; }
    public required IEnumerable<MovableFigureSnapshot> Figures { get; set; }
}