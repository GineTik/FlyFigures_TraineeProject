using System;
using FlyFiguresTraineeProject.Figures;

namespace FlyFiguresTraineeProject.Events;

public class FiguresTouchedEventArgs : EventArgs
{
    public MovableFigure Sender { get; private set; }
    public MovableFigure TouchedThe { get; private set; }

    public FiguresTouchedEventArgs(MovableFigure sender, MovableFigure touchedThe)
    {
        Sender = sender;
        TouchedThe = touchedThe;
    }
}