using NUnit.Framework;

namespace Duality.Tests.Log
{
	[TestFixture]
	public class LogTests
	{
		[Test]
		public void When_writeError_and_there_is_no_params_Then_it_does_not_throw()
		{
			Assert.DoesNotThrow(() => Duality.Log.Game.WriteError("This is a test {0}"));
		}

		[Test]
		public void When_writeWarning_and_there_is_no_params_Then_it_does_not_throw()
		{
			Assert.DoesNotThrow(() => Duality.Log.Game.WriteWarning("This is a test {0}"));
		}

		[Test]
		public void When_write_and_there_is_no_params_Then_it_does_not_throw()
		{
			Assert.DoesNotThrow(() => Duality.Log.Game.Write("This is a test {0}"));
		}
	}
}
