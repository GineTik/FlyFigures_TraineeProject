using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Saving.Attributes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

public abstract class MovableFigureSnapshot : IMovableSnapshot
{
    [JsonIgnore]
    public string SnapshotName => GetType().GetCustomAttribute<SnapshotNameAttribute>()?.Name ?? throw new InvalidDataException("Snapshot attribute is required");
    
    public CustomPoint Direction { get; set; }
    public CustomPoint CurrentPosition { get; set; }
    public bool InMotion { get; set; }

    public abstract MovableFigure Restore(Canvas canvas);
}