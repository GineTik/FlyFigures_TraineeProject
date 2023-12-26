using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableCircleSnapshot")]
public class MovableCircleSnapshot : MovableFigureSnapshot
{
    public int CurrentColorIndex { get; set; }
    
    public override MovableFigure Restore(Canvas canvas)
    {
        return new MovableCircle(canvas, this);
    }
}