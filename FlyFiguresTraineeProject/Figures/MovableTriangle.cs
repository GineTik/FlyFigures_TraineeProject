﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Figures.Configuration;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Figures;

public static class TriangleConstants
{
    public static Polygon Instance => new()
    {
        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
        Points = new PointCollection(new[]
        {
            new Point(0, 0),
            new Point(80, 50),
            new Point(0, 100)
        }),
    };
}

public class MovableTriangle : MovableFigure
{
    private static int AccelerationDelayInMilliseconds => 300;
    private static int AccelerationSpeed => 7;

    public MovableTriangle() : base(TriangleConstants.Instance, AvailableFigureData.MovableTriangle)
    {
    }
    
    public MovableTriangle(MovableTriangleSnapshot snapshot) : base(
        snapshot, TriangleConstants.Instance, AvailableFigureData.MovableTriangle)
    {
    }

    protected override void TouchedBoundary()
    {
        Speed = AccelerationSpeed;
        Task.Run(async () =>
        {
            await Task.Delay(AccelerationDelayInMilliseconds);
            Speed = DefaultSpeed;
        });
    }

    public override MovableFigureSnapshot MakeSnapshot()
    {
        return new MovableTriangleSnapshot
        {
            CurrentPosition = CurrentPosition,
            Direction = Direction,
            InMotion = InMotion
        };
    }
}