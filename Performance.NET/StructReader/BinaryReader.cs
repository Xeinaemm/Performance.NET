using System.IO;

namespace Performance.NET.StructReader
{
	public class BinaryReader
	{
		private readonly System.IO.BinaryReader _reader;
		private readonly MemoryStream _stream;

		public BinaryReader(byte[] input)
		{
			_stream = new MemoryStream(input);
			_reader = new System.IO.BinaryReader(_stream);
		}

		public TcpHeader HeaderAtOffset(int offset)
		{
			_stream.Seek(offset, SeekOrigin.Begin);
			var result = new TcpHeader
			{
				SourceIp = _reader.ReadUInt32(),
				DestinationIp = _reader.ReadUInt32(),
				SourcePort = _reader.ReadUInt16(),
				DestinationPort = _reader.ReadUInt16(),
				Flags = _reader.ReadUInt32(),
				Checksum = _reader.ReadUInt32()
			};
			return result;
		}
	}
}