namespace Performance.NET.StructReader
{
	public struct TcpHeader
	{
		public uint SourceIp;
		public uint DestinationIp;
		public ushort SourcePort;
		public ushort DestinationPort;
		public uint Flags;
		public uint Checksum;
	}
}