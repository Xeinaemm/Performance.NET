using System;

namespace Performance.NET.ValueTypes
{
	internal struct GoodStruct : IEquatable<GoodStruct>
	{
		public int X;
		public int Y;

		public override bool Equals(object obj)
		{
			if (!(obj is GoodStruct)) return false;
			var other = (GoodStruct) obj;
			return X == other.X && Y == other.Y;
		}

		public bool Equals(GoodStruct other) => X == other.X && Y == other.Y;

		public static bool operator ==(GoodStruct a, GoodStruct b) => a.Equals(b);

		public static bool operator !=(GoodStruct a, GoodStruct b) => !a.Equals(b);

		public override int GetHashCode()
		{
			var hash = 19;
			hash = hash * 29 + X;
			hash = hash * 29 + Y;
			return hash;
		}
	}
}