using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Saving.Models.Snapshots;

namespace FlyFiguresTraineeProject.Figures.Data;

public static class ConfiguredFigureData
{
    public static Dictionary<Type, FigureData> Data => new()
    {
        { 
            typeof(MovableCircle), new FigureData
            {
                Icon = new Ellipse
                {
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
                    Width = 20,
                    Height = 20
                },
                LocalizationKey = "MovableCircle",
                Factory = (canvas) => new MovableCircle(canvas),
                FigureSnapshotType = typeof(MovableCircleMovableSnapshot)
            }
        },
        { 
            typeof(MovableRectangle), new FigureData
            { 
                Icon = new Rectangle
                {
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
                    Width = 15,
                    Height = 23
                },
                LocalizationKey = "MovableRectangle",
                Factory = (canvas) => new MovableRectangle(canvas),
                FigureSnapshotType = typeof(MovableRectangleMovableSnapshot)
            }
        },
        { 
            typeof(MovableTriangle), new FigureData
            { 
                Icon = new Polygon
                {
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")!,
                    Points = new PointCollection(new[]
                    {
                        new Point(0, 0),
                        new Point(15, 10),
                        new Point(0, 20)
                    })
                },
                LocalizationKey = "MovableTriangle",
                Factory = (canvas) => new MovableTriangle(canvas),
                FigureSnapshotType = typeof(MovableTriangleMovableSnapshot)
            }
        }
    };
}