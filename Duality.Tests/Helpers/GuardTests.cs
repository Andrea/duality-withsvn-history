using System;
using Duality.Helpers;
using NUnit.Framework;

namespace Duality.Tests.Helpers
{
	[TestFixture]
	public class GuardTests
	{
		[Test]
		public void When_collection_empty_Then_throws()
		{
			Assert.Throws<ArgumentException>(() => Guard.NotEmpty(new string[] { }));
		}

		[Test]
		public void When_collection_null_Then_throws()
		{
			Assert.Throws<ArgumentException>(() => Guard.NotEmpty(null));
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase(" ")]
		public void When_string_null_Then_throws(string param)
		{
			Assert.Throws<ArgumentException>(() => Guard.StringNotNullEmpty(param));
		}
	}
}