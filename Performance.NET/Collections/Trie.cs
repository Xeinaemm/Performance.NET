using System.Collections.Generic;

namespace Performance.NET.Collections
{
	public class Trie
	{
		private readonly Node _root = new Node();

		private Node NodeForWord(string word, bool createPath)
		{
			var current = _root;
			foreach (var c in word)
			{
				current = createPath ? current.GetOrCreate(c) : current[c];
				if (current == null) return null;
			}

			return current;
		}

		public void AddWord(string word)
		{
			var node = NodeForWord(word, true);
			node.WordTerminator = true;
		}

		public bool ContainsWord(string word)
		{
			var node = NodeForWord(word, false);
			return node != null && node.WordTerminator;
		}

		public List<string> PrefixedWords(string prefix)
		{
			var prefixedWords = new List<string>();
			var node = NodeForWord(prefix, false);
			if (node == null) return prefixedWords;

			PrefixedWordsAux(prefix, node, prefixedWords);
			return prefixedWords;
		}

		private static void PrefixedWordsAux(string word, Node node, ICollection<string> prefixedWords)
		{
			if (node.WordTerminator) prefixedWords.Add(word);

			foreach (var nodeAssignedChild in node.AssignedChildren)
				PrefixedWordsAux(word + nodeAssignedChild.Value, nodeAssignedChild.Key, prefixedWords);
		}

		private class Node
		{
			private readonly Node[] _children = new Node[26];

			public IEnumerable<KeyValuePair<Node, char>> AssignedChildren
			{
				get
				{
					for (var i = 0; i < _children.Length; i++)
						if (_children[i] != null)
							yield return new KeyValuePair<Node, char>(_children[i], (char) ('a' + i));
				}
			}

			public Node this[char c]
			{
				get => _children[c - 'a'];
				private set => _children[c - 'a'] = value;
			}

			public bool WordTerminator { get; set; }

			public Node GetOrCreate(char c)
			{
				var child = this[c] ?? (this[c] = new Node());
				return child;
			}
		}
	}
}