using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Performance.NET.ValueTypes
{
    public class ValueTypesBenchmark
    {
        private const int NumberOfPoints = 1000000;
        private readonly List<BadStruct> _badStructs = new List<BadStruct>();
        private readonly List<GoodStruct> _goodStructs = new List<GoodStruct>();

        [GlobalSetup(Target = nameof(BadStruct))]
        public void SetupBadStructs()
        {
            for (var i = 0; i < NumberOfPoints; ++i)
                _badStructs.Add(new BadStruct {X = i, Y = i});
        }

        [GlobalSetup(Target = nameof(GoodStruct))]
        public void SetupGoodStructs()
        {
            for (var i = 0; i < NumberOfPoints; ++i)
                _goodStructs.Add(new GoodStruct {X = i, Y = i});
        }

        [Benchmark]
        public bool BadStruct() => _badStructs.Contains(new BadStruct {X = NumberOfPoints + 1, Y = NumberOfPoints + 1});

        [Benchmark]
        public bool GoodStruct() => _goodStructs.Contains(new GoodStruct {X = NumberOfPoints + 1, Y = NumberOfPoints + 1});
    }
}