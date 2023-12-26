using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableRectangleSnapshot")]
public class MovableRectangleSnapshot : MovableFigureSnapshot
{
    public override MovableFigure Restore(Canvas canvas)
    {
        return new MovableRectangle(canvas, this);
    }
}