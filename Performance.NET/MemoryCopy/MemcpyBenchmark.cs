using System;
using BenchmarkDotNet.Attributes;

namespace Performance.NET.MemoryCopy
{
    public class MemcpyBenchmark
    {
        private static readonly Random Rand = new Random();

        private byte[] _source;

        [Params(2, /*4, 8, 16, 32, 64, 128, 256, 512,*/ 1024, /*2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144,*/ 524288,
            /*1048576, 2097152, 4194304, 8388608,*/ 16777216)]
        public int Size { get; set; }

        private static byte[] GetRandomArray(int size)
        {
            var array = new byte[size];
            for (var i = 0; i < array.Length; i++) array[i] = (byte) Rand.Next(byte.MaxValue);
            return array;
        }

        [GlobalSetup]
        public void SetupSource()
        {
            _source = GetRandomArray(Size);
        }

        [Benchmark]
        public byte[] ArrayCopy()
        {
            var destination = new byte[Size];
            Array.Copy(_source, destination, _source.Length);
            return destination;
        }

        [Benchmark]
        public byte[] BufferBlockCopy()
        {
            var destination = new byte[Size];
            Buffer.BlockCopy(_source, 0, destination, 0, _source.Length);
            return destination;
        }

        [Benchmark]
        public byte[] MemoryCopy()
        {
            var destination = new byte[Size];
            Memory.Copy(_source, destination);
            return destination;
        }
    }
}