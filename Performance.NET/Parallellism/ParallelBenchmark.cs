using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Performance.NET.Parallellism
{
    public class ParallelBenchmark
    {
        private const int From = 2;
        private const int To = 1000000;

        [Benchmark]
        public List<uint> AllPrimes() => Primes.AllPrimes(From, To);

        [Benchmark]
        public List<uint> AllPrimesParallelWithLock() => Primes.AllPrimesParallelWithLock(From, To);

        [Benchmark]
        public List<uint> AllPrimesParallelAggregated() => Primes.AllPrimesParallelAggregated(From, To);
    }
}