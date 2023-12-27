using System;
using System.Windows.Controls;
using System.Xml.Serialization;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableRectangleSnapshot")]
[XmlInclude(typeof(MovableRectangleSnapshot))]
[Serializable]
public class MovableRectangleSnapshot : MovableFigureSnapshot
{
    public override MovableFigure Restore(Canvas canvas)
    {
        return new MovableRectangle(canvas, this);
    }
}