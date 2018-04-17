using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace Performance.NET.Rgx
{
	public class RegexBenchmark
	{
		private const string EmailPattern =
			@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

		private static readonly Regex CompiledRegex = new Regex(EmailPattern, RegexOptions.Compiled);
		private static readonly Regex StandardRegex = new Regex(EmailPattern);

        [Benchmark]
        public bool StandardRegexIsMatch() => StandardRegex.IsMatch("foo1");

        [Benchmark]
        public bool CompiledRegexIsMatch() => CompiledRegex.IsMatch("foo1");

	}
}