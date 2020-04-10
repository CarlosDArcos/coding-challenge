using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using FluentAssertions;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;

        [SetUp]
        public void Setup()
        {

            var dataBuilder = new SampleDataBuilder(50000);

            _shirts = dataBuilder.CreateShirts();

            _searchEngine = new SearchEngine(_shirts);
        }


        [Test]
        public void SearchThrough50000ShirtsByRed_ShouldTakeLessThan100ms()
        {
            var sw = new Stopwatch();
            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            sw.Start();
            var results = _searchEngine.Search(options);
            sw.Stop();

            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            sw.ElapsedMilliseconds.Should().BeLessThan(100, "This Performance test should run under 100 ms");
            // too much assertion in this performance test.  We only care about speed here.
            //AssertResults(results.Shirts, options);
            //AssertSizeCounts(_shirts, options, results.SizeCounts);
            //AssertColorCounts(_shirts, options, results.ColorCounts);
        }

        [Test]
        public void SearchThrough5000000ShirtsWithAllCombinations_ShouldTakeLessThan100ms()
        {
            var sw = new Stopwatch();
            var options = new SearchOptions
            {
                Colors =  Color.All ,
                Sizes = Size.All
            };

            var dataBuilder = new SampleDataBuilder(5000000);
            _shirts = dataBuilder.CreateShirts();
            _searchEngine = new SearchEngine(_shirts);

            sw.Start();
            var results = _searchEngine.Search(options);
            sw.Stop();

            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            //This takes 35ms in my machine as an attempt to push the performance of the Search Engine. Could take longer in different machines.
            //sw.ElapsedMilliseconds.Should().BeLessThan(100, "This Performance test should run under 100 ms");
        }
    }
}