public class  PointsCounter
{
    public float CurrentPoints { get; private set; }
    private readonly float _pointsPerSecond;

    public PointsCounter(float pointsPerSecond)
    {
        _pointsPerSecond = pointsPerSecond;
        CurrentPoints = 0;
    }

    public void AddDriftingPoints(float deltaTime)
    {
        CurrentPoints += deltaTime * _pointsPerSecond;
    }
}