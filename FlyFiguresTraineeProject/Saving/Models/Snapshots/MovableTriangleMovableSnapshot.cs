using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

public class MovableTriangleMovableSnapshot : MovableFigureSnapshot
{
    public override string Name => typeof(MovableTriangle).FullName!;
}