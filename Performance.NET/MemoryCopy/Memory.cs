using System;

namespace Performance.NET.MemoryCopy
{
	public static class Memory
	{
		public static unsafe void Copy(byte[] source, byte[] destination)
		{
			fixed (byte* p1 = &destination[0])
			fixed (byte* p2 = &source[0])
			{
				var pDestination = (long*) p1;
				var pSource = (long*) p2;
				var iterations = Math.DivRem(source.Length, sizeof(long),
					out var remainder);
				for (var i = 0; i < iterations; ++i)
				{
					*pDestination = *pSource;
					++pDestination;
					++pSource;
				}

				if (remainder <= 0) return;
				{
					var endPos = source.Length - remainder;
					for (var i = 0; i < remainder; ++i) p1[endPos + i] = p2[endPos + i];
				}
			}
		}
	}
}