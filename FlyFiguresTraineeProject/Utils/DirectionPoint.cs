namespace FlyFiguresTraineeProject.Utils;

public struct DirectionPoint
{
    private double _x;
    private double _y;
    
    public double X 
    { 
        get => _x;
        set
        {
            if (value >= -1 && value != 0 && value <= 1) 
                _x = value;            
        } 
    }
    public double Y 
    { 
        get => _y;
        set
        {
            if (value >= -1 && value != 0 && value <= 1) 
                _y = value;          
        } 
    }

    public DirectionPoint(double x, double y)
    {
        X = x;
        Y = y;
    }
}