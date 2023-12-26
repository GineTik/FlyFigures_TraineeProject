using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FlyFiguresTraineeProject.Saving.Attributes;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Saving;

public static class AvailableFigureSnapshots
{
    public static Dictionary<string, Type> SnapshotTypesByName { get; } = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.IsSubclassOf(typeof(MovableFigureSnapshot)))
        .ToDictionary(t => 
            t.GetCustomAttribute<SnapshotNameAttribute>()?.Name ?? throw new InvalidDataException("Snapshot attribute is required"), 
            t => t);
}