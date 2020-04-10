﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private List<SizeCount> _sizeCounts;
        private List<ColorCount> _colorCounts;

        [SetUp]
        public void Setup()
        {
            _sizeCounts = Size.All.Select(s => new SizeCount { Count = 0, Size = s }).ToList();
            _colorCounts = Color.All.Select(s => new ColorCount { Count = 0, Color = s }).ToList();
        }

        [Test]
        public void SearchByRed_ShouldReturnRedShirt()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            SetupExpectedSizeCounts(Size.Small, 1);
            SetupExpectedColorCounts(Color.Red, 1);

            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }

            };
            //Act
            var results = searchEngine.Search(searchOptions);
    
            //Assert
            results.Shirts.Count.Should().Be(1);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchBySmallRed_InSingleSmallRed_ShouldReturnSmallRedShirt()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            SetupExpectedSizeCounts(Size.Small, 1);
            SetupExpectedColorCounts(Color.Red, 1);

            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(1);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchBySmallRed_InMultipleSmallRed_ShouldReturnSmallRedShirts()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            SetupExpectedSizeCounts(Size.Small, 2);
            SetupExpectedColorCounts(Color.Red, 2);

            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(2);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchBySmallRed_WithNoMatch_ShouldReturnNoShirts()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };
                         
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(0);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchBySmall_ShouldReturnSmallShirt()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            SetupExpectedSizeCounts(Size.Small, 1);
            SetupExpectedColorCounts(Color.Red, 1);

            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small }
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(1);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchByAllSizesRed_ShouldReturnAllSizesRedShirts()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            SetupExpectedSizeCounts(Size.Small, 1);
            SetupExpectedSizeCounts(Size.Medium, 1);
            SetupExpectedSizeCounts(Size.Large, 1);
            SetupExpectedColorCounts(Color.Red, 3);

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = Size.All
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(3);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchBySmallAllColors_ShouldReturnSmallAllColorsShirts()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Yellow - Medium", Size.Medium, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "White - Medium", Size.Medium, Color.White),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            SetupExpectedSizeCounts(Size.Small, 4);
            SetupExpectedColorCounts(Color.Red, 1);
            SetupExpectedColorCounts(Color.Yellow, 1);
            SetupExpectedColorCounts(Color.White, 1);
            SetupExpectedColorCounts(Color.Blue, 1);

            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = Color.All,
                Sizes = new List<Size> { Size.Small }
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(4);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchBySmallMediumRed_AndSmallMediumYellow_ShouldReturnSmallRedAndMediumRedAndSmallYellowAndMediumYellowShirts()
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Yellow - Medium", Size.Medium, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "White - Medium", Size.Medium, Color.White),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue)
            };

            SetupExpectedSizeCounts(Size.Small, 2);
            SetupExpectedSizeCounts(Size.Medium, 2);
            SetupExpectedColorCounts(Color.Red, 2);
            SetupExpectedColorCounts(Color.Yellow, 2);

            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Yellow },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(4);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_sizeCounts, results.SizeCounts);
            AssertColorCounts(_colorCounts, results.ColorCounts);
        }

        [Test]
        public void SearchWithNoShirtsProvided_ShouldReturnArgumentException()
        {
            Action act = () => new SearchEngine(null);
            act.Should()
               .Throw<ArgumentException>()
               .And.Message.Should().Be("collection of shirts must not be null (Parameter 'shirts')");
        }

        [Test]
        [TestCaseSource("GenerateSearchOptions")]
        public void SearchWithNoSearchOptionsProvided_ShouldReturnEverything(SearchOptions searchOptions)
        {
            //Arrange
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow)
            };
            var searchEngine = new SearchEngine(shirts);

            //Act
            var results = searchEngine.Search(searchOptions);

            //Assert
            results.Shirts.Count.Should().Be(2);
        }

        private static SearchOptions[] GenerateSearchOptions()
        {
            return new[] { null, new SearchOptions() };
        }

        private void SetupExpectedSizeCounts(Size size, int sizeTotal)
        {
            _sizeCounts.Single(s => s.Size.Id == size.Id).Count = sizeTotal;
        }

        private void SetupExpectedColorCounts(Color color, int colorTotal)
        {
            _colorCounts.Single(c => c.Color.Id == color.Id).Count = colorTotal;
        }
    }
}