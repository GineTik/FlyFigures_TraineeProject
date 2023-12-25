using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Saving.Models.Snapshots;

public class MovableFigureSnapshot : IMovableSnapshot
{
    public virtual string Name { get; }
    public CustomPoint Direction { get; set; }
    public CustomPoint CurrentPosition { get; set; }
    public bool InMotion { get; set; }
}