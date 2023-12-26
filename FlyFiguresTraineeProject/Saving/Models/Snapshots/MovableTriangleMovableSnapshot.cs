using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableTriangleSnapshot")]
public class MovableTriangleSnapshot : MovableFigureSnapshot
{
    public override MovableFigure Restore(Canvas canvas)
    {
        return new MovableTriangle(canvas, this);
    }
}