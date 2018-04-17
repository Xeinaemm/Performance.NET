using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Attributes;

namespace Performance.NET.Collections
{
    public class CollectionsBenchmark
    {
        private readonly Random _rand = new Random();
        private readonly List<DisjointSet<int>> _sets = new List<DisjointSet<int>>();
        private readonly List<DisjointSetList<int, int>> _setsList = new List<DisjointSetList<int, int>>();
        private readonly HashSet<string> _wordsHashSet = new HashSet<string>();
        private readonly Trie _trie = new Trie();

        [Params(10000)] public int NumberOfSets { get; set; }

        [GlobalSetup(Targets = new[] {nameof(LookupWordTrie), nameof(ShortWordPrefixTrie), nameof(LongWordPrefixTrie)})]
        public void SetupTrie()
        {
            foreach (var word in File.ReadLines("english-words")) _trie.AddWord(word.ToLower());
        }
        
        [GlobalSetup(Targets = new[] {nameof(CreateHashSet), nameof(LookupWordHashSet)})]
        public void SetupHashSet()
        {
            foreach (var word in File.ReadLines("english-words")) _wordsHashSet.Add(word.ToLower());
        }

        [GlobalSetup(Target = nameof(MeasureLists))]
        public void SetupSetList()
        {
            for (var i = 0; i < NumberOfSets; ++i) _setsList.Add(new DisjointSetList<int, int>(i, i));
        }
        
        [GlobalSetup(Target = nameof(MeasureDisjointSet))]
        public void SetupSet()
        {
            for (var i = 0; i < NumberOfSets; ++i) _sets.Add(new DisjointSet<int>(i));
        }

        [Benchmark]
        public Trie CreateTrie()
        {
            var trie = new Trie();
            foreach (var word in File.ReadLines("english-words")) trie.AddWord(word.ToLower());
            return trie;
        }

        [Benchmark]
        public bool LookupWordTrie() => _trie.ContainsWord("palindrome");

        [Benchmark]
        public List<string> ShortWordPrefixTrie() => _trie.PrefixedWords("ba");

        [Benchmark]
        public List<string> LongWordPrefixTrie() => _trie.PrefixedWords("strea");

        [Benchmark]
        public HashSet<string> CreateHashSet()
        {
            var hashSet = new HashSet<string>();
            foreach (var word in File.ReadLines("english-words")) hashSet.Add(word.ToLower());
            return hashSet;
        }

        [Benchmark]
        public bool LookupWordHashSet() => _wordsHashSet.Contains("palindrome");

        [Benchmark]
        public List<DisjointSet<int>> MeasureDisjointSet()
        {
            for (var i = 0; i < NumberOfSets; ++i)
            {
                var x = _rand.Next(NumberOfSets);
                var y = _rand.Next(NumberOfSets);
                _sets[x]
                    .Union(_sets[y]);
            }

            return _sets;
        }

        [Benchmark]
        public List<DisjointSetList<int, int>> MeasureLists()
        {
            for (var i = 0; i < NumberOfSets; ++i)
            {
                var x = _rand.Next(NumberOfSets);
                var y = _rand.Next(NumberOfSets);
                _setsList[x]
                    .Union(_setsList[y]);
            }

            return _setsList;
        }
    }
}