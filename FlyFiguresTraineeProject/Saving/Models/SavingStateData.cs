using System;
using System.Collections.Generic;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Saving.Models;

[Serializable]
public class SavingStateData
{
    public required List<MovableFigureSnapshot> Figures { get; set; }
}