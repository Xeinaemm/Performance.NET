using System.Collections.Generic;

namespace Performance.NET.Collections
{
	public class DisjointSetList<T, S>
	{
		private readonly List<DisjointSetListNode<T, S>> _nodes = new List<DisjointSetListNode<T, S>>();

		public DisjointSetList(T data, S set)
		{
			Set = set;
			_nodes.Add(new DisjointSetListNode<T, S>(data, this));
		}

		public S Set { get; private set; }

		public void Union(DisjointSetList<T, S> other)
		{
			if (other == this) return;

			other.Set = Set;

			foreach (var disjointSetListNode in other._nodes) disjointSetListNode.List = this;
			_nodes.AddRange(other._nodes);
		}
	}
}