namespace Performance.NET.Collections
{
	public class DisjointSet<T>
	{
		public DisjointSet(T data)
		{
			Parent = this;
			Rank = 0;
			Data = data;
		}

		public DisjointSet<T> Parent { get; set; }
		public int Rank { get; set; }
		public T Data { get; set; }

		public void Union(DisjointSet<T> other)
		{
			var xRep = Find();
			var yRep = other.Find();
			if (xRep == yRep)
				return;

			if (xRep.Rank < yRep.Rank)
			{
				xRep.Parent = yRep;
			}
			else if (xRep.Rank > yRep.Rank)
			{
				yRep.Parent = xRep;
			}
			else
			{
				yRep.Parent = xRep;
				++xRep.Rank;
			}
		}

		public DisjointSet<T> Find()
		{
			if (Parent != this) Parent = Parent.Find();
			return Parent;
		}
	}
}