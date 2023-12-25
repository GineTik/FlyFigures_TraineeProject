using System.Collections.Generic;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Saving.Models;

public class SavingState
{
    public required IEnumerable<MovableFigureSnapshot> Figures { get; set; }
    public required string CultureInfo { get; set; }
}