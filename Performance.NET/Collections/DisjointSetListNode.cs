namespace Performance.NET.Collections
{
	public class DisjointSetListNode<T, S>
	{
		public DisjointSetListNode(T data, DisjointSetList<T, S> list)
		{
			Data = data;
			List = list;
		}

		public DisjointSetList<T, S> List { get; set; }
		public T Data { get; set; }

		public S Find() => List.Set;

		public void Union(DisjointSetListNode<T, S> other)
		{
			List.Union(other.List);
		}
	}
}