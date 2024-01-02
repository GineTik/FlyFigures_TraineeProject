using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using FlyFiguresTraineeProject.Events;
using FlyFiguresTraineeProject.Utils;

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
            if (IntersectWith(figure.Shape, out var pointOfContact) == false) continue;

            participants = new FiguresTouchedEventArgs(_movableFigure, figure, pointOfContact);
            return true;
        }

        return false;
    }
    
    private bool IntersectWith(Shape touchedWithShape, out CustomPoint pointOfContact)
    {
        pointOfContact = new CustomPoint(double.NaN, double.NaN);
        
        var rect1 = _movableFigure.Shape.RenderedGeometry.Bounds;
        var rect2 = touchedWithShape.RenderedGeometry.Bounds;

        var transform1 = _movableFigure.Shape.TransformToAncestor(_canvas);
        rect1 = transform1.TransformBounds(rect1);

        var transform2 = touchedWithShape.TransformToAncestor(_canvas);
        rect2 = transform2.TransformBounds(rect2);

        if (rect1.IntersectsWith(rect2) == false)
            return false;
        
        pointOfContact = CalculatePointOfContact(rect1, rect2);
        return true;
    }

    private static CustomPoint CalculatePointOfContact(Rect rect1, Rect rect2)
    {
        var intersectionRect = Rect.Intersect(rect1, rect2);

        return new CustomPoint(intersectionRect.X + intersectionRect.Width / 2,
            intersectionRect.Y + intersectionRect.Height / 2);
    }
}