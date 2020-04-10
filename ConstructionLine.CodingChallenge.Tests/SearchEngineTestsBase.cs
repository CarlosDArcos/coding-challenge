using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        protected static void AssertResults(List<Shirt> shirts, SearchOptions options)
        {
            shirts.Should().NotBeNull();

            var resultingShirtIds = shirts.Select(s => s.Id).ToList();
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in shirts)
            {
                if (sizeIds.Contains(shirt.Size.Id)
                    && colorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }

        protected static void AssertSizeCounts(List<SizeCount> expectedSizeCounts, List<SizeCount> actualSizeCounts)
        {
            // we should know what we're asserting against rather than have to calculate it
            // from all the shirts. Too much logic in this assertion method, almost a mini search-engine.
            actualSizeCounts.Should().NotBeNull();

            Size.All.Count().Should().Be(actualSizeCounts.Select(s => s.Size).Count(), "Expected counts for all sizes");

            foreach (var sizeCount in actualSizeCounts)
            {
                var actualSizeCount = sizeCount.Count;
                var expectedSizeCount = expectedSizeCounts.Single(s => s.Size.Id == sizeCount.Size.Id).Count;
                actualSizeCount.Should().Be(expectedSizeCount, $"the count for {sizeCount.Size.Name} was wrong");
            }
        }

        protected static void AssertColorCounts(List<ColorCount> expectedColorCounts, List<ColorCount> actualColorCounts)
        {
            // we should know what we're asserting against rather than have to calculate it
            // from all the shirts. Too much logic in this assertion method, almost a mini search-engine.
            actualColorCounts.Should().NotBeNull();

            Color.All.Count().Should().Be(actualColorCounts.Select(c => c.Color).Count(),"Expected counts for all colors");

            foreach (var colorCount in actualColorCounts)
            {
                var actualColorCount = colorCount.Count;
                var expectedColorCount = expectedColorCounts.Single(c => c.Color.Id == colorCount.Color.Id).Count;
                actualColorCount.Should().Be(expectedColorCount, $"the count for {colorCount.Color.Name} was wrong");
            }
        }
    }
}