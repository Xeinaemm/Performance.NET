using System;
using BenchmarkDotNet.Running;
using Performance.NET.Collections;
using Performance.NET.MemoryCopy;
using Performance.NET.Parallellism;
using Performance.NET.Rgx;
using Performance.NET.StructReader;
using Performance.NET.ValueTypes;

namespace Performance.NET
{
	public class Program
	{
		private static void Main()
		{
            //BenchmarkRunner.Run<CollectionsBenchmark>();
            //BenchmarkRunner.Run<MemcpyBenchmark>();
            //BenchmarkRunner.Run<ParallelBenchmark>();
            //BenchmarkRunner.Run<RegexBenchmark>();
            //BenchmarkRunner.Run<StructBenchmark>();
            BenchmarkRunner.Run<ValueTypesBenchmark>();
            Console.ReadKey();
        }
	}
}