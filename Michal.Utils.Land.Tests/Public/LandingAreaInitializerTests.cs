using System;
using System.Collections.Generic;
using FluentAssertions;
using Michal.Utils.Land.Public;
using NUnit.Framework;

namespace Michal.Utils.Land.Tests.Public;

public class LandingAreaInitializerTests
{
    [TestCase(10, -1)]
    [TestCase(-1, 10)]
    [TestCase(0, 10)]
    [TestCase(10, 0)]
    [TestCase(10, 101)]
    [TestCase(101, 10)]
    public void InitLandingArea_WhenLandingAreaLengthOutOfRange_ThrowsArgumentOutOfRangeException(int xDimensionLength,
        int yDimensionLength)
    {
        var act = () =>
        {
            LandingAreaInitializer.InitLandingArea(xDimensionLength, yDimensionLength);
        };
        
        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    

    [Test]
    public void InitLandingArea_WhenLandingAreaLengthWithinRange_ProperCoordinatesPresentInLandingAreaCoordinates()
    {
        var expectedCoordinates = new HashSet<Coordinate>
        {
            new(5, 5),
            new(5, 6),
            new(5, 7),
            new(6, 5),
            new(6, 6),
            new(6, 7),
            new(7, 5),
            new(7, 6),
            new(7, 7)
        };
        
        LandingAreaInitializer.InitLandingArea(2, 2);

        LandingAreaInitializer.LandingAreaCoordinates?.Should().Equal(expectedCoordinates);
    }

    [Test]
    public void InitLandingArea_WhenInitLandingAreaCalledFirstTime_LastOccupiedCoordinateIsNull()
    {
        LandingAreaInitializer.InitLandingArea(2, 2);

        LandingAreaInitializer.LastOccupiedCoordinate.Should().BeNull();
    }
}