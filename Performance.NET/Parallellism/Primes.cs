using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Performance.NET.Parallellism
{
	public class Primes
	{
		private static bool IsPrime(uint n)
		{
			if (n % 2 == 0 && n != 2) return false;
			var root = (uint) Math.Ceiling(Math.Sqrt(n));
			for (uint i = 3; i <= root; i += 2)
				if (n % i == 0 && n != i)
					return false;
			return true;
		}

		public static List<uint> AllPrimes(uint from, uint to)
		{
			var result = new List<uint>();
			for (var i = from; i <= to; ++i)
				if (IsPrime(i))
					result.Add(i);
			return result;
		}

		public static List<uint> AllPrimesParallelWithLock(uint from, uint to)
		{
			var result = new List<uint>();
			Parallel.For((int) from, (int) to, i =>
			{
				if (!IsPrime((uint) i)) return;
				lock (result)
					result.Add((uint) i);
			});
			return result;
		}

		public static List<uint> AllPrimesParallelAggregated(uint from, uint to)
		{
			var result = new List<uint>();
			Parallel.For((int) from, (int) to,
				() => new List<uint>(), // Local state initializer
				(i, pls, local) => // Loop body
				{
					if (IsPrime((uint) i)) local.Add((uint) i);
					return local;
				},
				local => // Local to global state combiner
				{
					lock (result) result.AddRange(local);
				});
			return result;
		}
	}
}