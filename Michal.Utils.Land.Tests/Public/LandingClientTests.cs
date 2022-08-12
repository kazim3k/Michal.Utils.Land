using FluentAssertions;
using Michal.Utils.Land.Public;
using NUnit.Framework;

namespace Michal.Utils.Land.Tests.Public;

public class LandingClientTests
{
    private LandingClient landingClient;

    [SetUp]
    public void Init()
    {
        this.landingClient = new LandingClient();
    }
    
    [Test]
    public void GetTrajectoryAvailability_WhenCoordinateNotPartOfLandingArea_ReturnsOutOfPlatformMessage()
    {
        LandingAreaInitializer.InitLandingArea(3, 3);

        var result = this.landingClient.GetTrajectoryAvailability(87, 90);

        result.Should().Be(ResponseMessage.OutOfPlatform);
    }
    
    [TestCase(8, 8)]
    [TestCase(8, 7)]
    [TestCase(8, 9)]
    [TestCase(7, 8)]
    [TestCase(9, 8)]
    [TestCase(9, 9)]
    [TestCase(7, 7)]
    [TestCase(7, 9)]
    [TestCase(8, 7)]
    public void GetTrajectoryAvailability_WhenCoordinateClashingWithDifferentRocket_ReturnsClashMessage(int xAxis, int yAxis)
    {
        LandingAreaInitializer.InitLandingArea(5, 5);
        this.landingClient.GetTrajectoryAvailability(8, 8);
        
        var result = this.landingClient.GetTrajectoryAvailability(xAxis, yAxis);

        result.Should().Be(ResponseMessage.Clash);
    }

    [TestCase(5, 5)]
    [TestCase(10, 10)]
    [TestCase(5, 10)]
    [TestCase(10, 5)]
    [TestCase(8, 7)]
    public void GetTrajectoryAvailability_WhenFirstCallForTrajectoryAndWithinLandingRange_ReturnsOkForLanding(
        int xDimensionLength,
        int yDimensionLength)
    {
        LandingAreaInitializer.InitLandingArea(5, 5);

        var result = this.landingClient.GetTrajectoryAvailability(xDimensionLength, xDimensionLength);

        result.Should().Be(ResponseMessage.OkForLanding);
    }
    
    [Test]
    public void GetTrajectoryAvailability_WhenCoordinateNotClashingWithDifferentRocket_ReturnsOkForLanding()
    {
        LandingAreaInitializer.InitLandingArea(5, 5);
        this.landingClient.GetTrajectoryAvailability(8, 8);
        
        var result = this.landingClient.GetTrajectoryAvailability(10, 10);
        
        result.Should().Be(ResponseMessage.OkForLanding);
    }
}