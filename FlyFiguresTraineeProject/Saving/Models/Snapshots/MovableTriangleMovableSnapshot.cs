using System;
using System.Xml.Serialization;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableTriangleSnapshot")]
[XmlInclude(typeof(MovableTriangleSnapshot))]
[Serializable]
public class MovableTriangleSnapshot : MovableFigureSnapshot
{
    public override MovableFigure Restore()
    {
        return new MovableTriangle(this);
    }
}