using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

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

    public MovableTriangle(Canvas context) : base(context, TriangleConstants.Instance)
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
}