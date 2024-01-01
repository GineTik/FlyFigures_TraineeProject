using System;
using System.Windows.Controls;
using System.Xml.Serialization;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

[SnapshotName("MovableCircleSnapshot")]
[XmlInclude(typeof(MovableCircleSnapshot))]
[Serializable]
public class MovableCircleSnapshot : MovableFigureSnapshot
{
    public int CurrentColorIndex { get; set; }
    
    public override MovableFigure Restore()
    {
        return new MovableCircle(this);
    }
}