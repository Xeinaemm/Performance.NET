using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Performance.NET.StructReader
{
    public class StructBenchmark
    {
        private byte[] _input;

        private static byte[] Packet()
        {
            var tcpHeader = new TcpHeader
            {
                SourceIp = 1,
                DestinationIp = 2,
                SourcePort = 3,
                DestinationPort = 4,
                Flags = 5,
                Checksum = 6
            };

            var data = new byte[Marshal.SizeOf(typeof(TcpHeader))];
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Marshal.StructureToPtr(tcpHeader, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return data;
        }

        [GlobalSetup]
        public void SetupPacket()
        {
            _input = Packet();
        }

        [Benchmark]
        public TcpHeader BinaryReader()
        {
            var binaryReader = new BinaryReader(_input);
            return binaryReader.HeaderAtOffset(0);
        }

        [Benchmark]
        public TcpHeader UnsafeGenericReader()
        {
            var unsafeGenericReader = new UnsafeGenericReader<TcpHeader>();
            return unsafeGenericReader.StructureAtOffset(_input, 0);
        }
    }
}