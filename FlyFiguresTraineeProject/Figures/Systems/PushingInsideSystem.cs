using System.Windows.Shapes;
using FlyFiguresTraineeProject.Utils;

namespace FlyFiguresTraineeProject.Figures.Systems;

public class PushingInsideSystem
{
    private readonly MovableFigure _movableFigure;

    private CustomPoint CurrentPosition => _movableFigure.CurrentPosition;
    private CustomPoint ExtremeLimit => _movableFigure.ExtremeLimit;
    private Shape Shape => _movableFigure.Shape;
    private int Speed => _movableFigure.Speed;
    
    private CustomPoint _updatedPosition;

    public PushingInsideSystem(MovableFigure movableFigure)
    {
        _movableFigure = movableFigure;
    }

    public bool CheckExitFromBoundary()
    {
        _updatedPosition = CurrentPosition;
        
        if (CurrentPosition.X < -Speed)
            _updatedPosition.X = 0;
        else if (CurrentPosition.X > ExtremeLimit.X)
            _updatedPosition.X = ExtremeLimit.X - Shape.ActualWidth;

        if (CurrentPosition.Y < -Speed)
            _updatedPosition.Y = 0;
        else if (CurrentPosition.Y > ExtremeLimit.Y)
            _updatedPosition.Y = ExtremeLimit.Y - Shape.ActualHeight;

        return IsPositionUpdated();
    }

    public CustomPoint UpdatePosition()
    {
        return _updatedPosition;
    }
    
    private bool IsPositionUpdated()
    {
        return _updatedPosition.Equals(CurrentPosition) == false;
    }
}