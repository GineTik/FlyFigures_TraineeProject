using System;

namespace FlyFiguresTraineeProject.Utils;

[Serializable]
public struct CustomPoint
{
    public double X { get; set; }
    public double Y { get; set; }

    public CustomPoint()
    {
        X = 0;
        Y = 0;
    }

    public CustomPoint(double x, double y)
    {
        X = x;
        Y = y;
    }
}