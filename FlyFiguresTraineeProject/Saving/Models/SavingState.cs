using System;

namespace FlyFiguresTraineeProject.Saving.Models;

[Serializable]
public class SavingState
{
    public string SavingVersion { get; set; } = "v1";
    public required SavingStateData Data { get; set; }
}