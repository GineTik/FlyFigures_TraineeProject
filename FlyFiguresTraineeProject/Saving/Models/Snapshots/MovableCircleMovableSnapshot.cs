using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

public class MovableCircleMovableSnapshot : MovableFigureSnapshot
{
    public override string Name => typeof(MovableCircle).FullName!;
    public int CurrentColorIndex { get; set; }
}