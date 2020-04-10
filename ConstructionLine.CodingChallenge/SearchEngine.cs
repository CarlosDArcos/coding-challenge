using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private Dictionary<(Size, Color), List<Shirt>> _shirtsLookup = new Dictionary<(Size, Color), List<Shirt>>();

        public SearchEngine(List<Shirt> shirts)
        {
            if (shirts == null)
            {
                throw new ArgumentException("collection of shirts must not be null", nameof(shirts));
            }

            _shirtsLookup = shirts.GroupBy(s => (s.Size, s.Color)).ToDictionary(s => s.Key, s => s.ToList());
        }

        public SearchResults Search(SearchOptions options)
        {
            options ??= new SearchOptions();

            if (options.Colors.Count == 0)
                options.Colors = Color.All.ToList();
            if (options.Sizes.Count == 0)
                options.Sizes = Size.All.ToList();

            var combos = from col in options.Colors
                         from size in options.Sizes
                         select (size, col);

            var results = new List<Shirt>();
            var sizeCounts = Size.All.Select(s => new SizeCount() { Size = s, Count = 0 }).ToList();
            var colorCounts = Color.All.Select(c => new ColorCount() { Color = c, Count = 0 }).ToList();

            foreach (var searchCombo in combos)
            {
                if (_shirtsLookup.TryGetValue(searchCombo, out List<Shirt> matchedShirts))
                {
                    results.AddRange(matchedShirts);
                    sizeCounts.Find(s => s.Size == searchCombo.size).Count += matchedShirts.Count;
                    colorCounts.Find(c => c.Color == searchCombo.col).Count += matchedShirts.Count;
                }
            }

            return new SearchResults
            {
                Shirts = results,
                ColorCounts = colorCounts,
                SizeCounts = sizeCounts
            };
        }
    }
}