using System;
using FlyFiguresTraineeProject.Figures;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Events;

public class FiguresTouchedEventArgs : EventArgs
{
    public MovableFigure Sender { get; private set; }
    public MovableFigure TouchedThe { get; private set; }
    public CustomPoint PointOfContact { get; private set; }

    public FiguresTouchedEventArgs(MovableFigure sender, MovableFigure touchedThe, CustomPoint pointOfContact)
    {
        Sender = sender;
        TouchedThe = touchedThe;
        PointOfContact = pointOfContact;
    }
}