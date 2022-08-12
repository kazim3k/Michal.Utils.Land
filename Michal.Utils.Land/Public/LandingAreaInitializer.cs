namespace Michal.Utils.Land.Public;

public static class LandingAreaInitializer
{
    internal static HashSet<Coordinate>? LandingAreaCoordinates;
    
    internal static Coordinate? LastOccupiedCoordinate;
    
    public static void InitLandingArea(int xAxisLength, int yAxisLength)
    {
        LandingAreaCoordinates = new HashSet<Coordinate>();
        LastOccupiedCoordinate = null;
        ValidateLength(xAxisLength);
        ValidateLength(yAxisLength);

        for (var i = AreaConsts.LandingAreaStartingXCoordinate;
             i <= xAxisLength + AreaConsts.LandingAreaStartingXCoordinate;
             i++)
        {
            for (var j = AreaConsts.LandingAreaStartingYCoordinate;
                 j <= yAxisLength + AreaConsts.LandingAreaStartingYCoordinate;
                 j++)
            {
                LandingAreaCoordinates.Add(new Coordinate(i, j));
            }
        }
    }
    
    private static void ValidateLength(int length)
    {
        if (length is > 100 or <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length),
                $"Landing area length: {length}, is not within range of the whole area");
        }
    }
}