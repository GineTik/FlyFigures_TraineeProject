using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Events;

namespace FlyFiguresTraineeProject.Figures.Systems;

public class FigureIntersectSystem
{
    private readonly MovableFigure _movableFigure;
    private readonly Canvas _canvas;
    private readonly IEnumerable<MovableFigure> _figures;

    public FigureIntersectSystem(MovableFigure movableFigure, Canvas canvas, IEnumerable<MovableFigure> figures)
    {
        _movableFigure = movableFigure;
        _canvas = canvas;
        _figures = figures;
    }

    public bool CheckIntersect(out FiguresTouchedEventArgs? participants)
    {
        participants = null;
        
        foreach (var figure in _figures)
        {
            if (_movableFigure == figure) continue;
            if (_movableFigure.GetType() != figure.GetType()) continue;
            if (IntersectWith(figure.Shape) == false) continue;

            participants = new FiguresTouchedEventArgs(_movableFigure, figure);
            return true;
        }

        return false;
    }
    
    private bool IntersectWith(Shape touchedWithShape)
    {
        var rect1 = _movableFigure.Shape.RenderedGeometry.Bounds;
        var rect2 = touchedWithShape.RenderedGeometry.Bounds;

        var transform1 = _movableFigure.Shape.TransformToAncestor(_canvas);
        rect1 = transform1.TransformBounds(rect1);

        var transform2 = touchedWithShape.TransformToAncestor(_canvas);
        rect2 = transform2.TransformBounds(rect2);

        return rect1.IntersectsWith(rect2);
    }
}