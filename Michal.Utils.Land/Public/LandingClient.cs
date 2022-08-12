namespace Michal.Utils.Land.Public;

public class LandingClient
{
    public string GetTrajectoryAvailability(int xCoordinate, int yCoordinate)
    {
        var coordinate =
            LandingAreaInitializer.LandingAreaCoordinates?.FirstOrDefault(c =>
                c.X == xCoordinate && c.Y == yCoordinate);

        if (coordinate == null)
        {
            return ResponseMessage.OutOfPlatform;
        }

        if (IsCoordinateClashing(coordinate))
        {
            return ResponseMessage.Clash;
        }

        LandingAreaInitializer.LastOccupiedCoordinate = new Coordinate(coordinate.X, coordinate.Y);
        return ResponseMessage.OkForLanding;
    }

    private static bool IsCoordinateClashing(Coordinate coordinate)
    {
        if (LandingAreaInitializer.LastOccupiedCoordinate != null)
        {
            return coordinate.X == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X + 1 == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X - 1 == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y + 1 == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y - 1 == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X + 1 == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y + 1 == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X + 1 == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y - 1 == LandingAreaInitializer.LastOccupiedCoordinate.Y ||
                   coordinate.X - 1 == LandingAreaInitializer.LastOccupiedCoordinate.X &&
                   coordinate.Y - 1 == LandingAreaInitializer.LastOccupiedCoordinate.Y;
        }

        return false;
    }
}