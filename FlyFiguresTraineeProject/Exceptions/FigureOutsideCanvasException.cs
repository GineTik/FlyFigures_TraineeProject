using System;
using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Exceptions;

public class FigureOutsideCanvasException : Exception
{
    public MovableFigure Sender { get; }
    
    public FigureOutsideCanvasException(MovableFigure sender) : base("The figure is over the extreme limits")
    {
        Sender = sender;
    }
}