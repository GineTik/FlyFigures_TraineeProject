using System;

namespace FlyFiguresTraineeProject.Saving.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SnapshotNameAttribute : Attribute
{
    public string Name { get; set; }

    public SnapshotNameAttribute(string name)
    {
        Name = name;
    }
}