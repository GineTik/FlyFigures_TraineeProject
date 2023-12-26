using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableCircleSnapshot")]
public class MovableCircleSnapshot : MovableFigureSnapshot
{
    public int CurrentColorIndex { get; set; }
}