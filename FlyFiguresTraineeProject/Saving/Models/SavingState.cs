namespace FlyFiguresTraineeProject.Saving.Models;

public class SavingState
{
    public string SavingVersion { get; set; } = "v1";
    public required SavingStateData Data { get; set; }
}