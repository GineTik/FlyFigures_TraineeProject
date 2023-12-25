using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

public class MovableRectangleMovableSnapshot : MovableFigureSnapshot
{
    public override string Name => typeof(MovableRectangle).FullName!;
}