using Moq;
using NUnit.Framework;

namespace Duality.Tests.Input
{
	[TestFixture]
	public class JoystickInputTests
	{
		[Test]
		public void When_Updating_then_it_does_not_throw()
		{
			var joystickInput = new JoystickInput {Source = CreateJoystickInputSourceMock().Object};

			Assert.DoesNotThrow(joystickInput.Update);
		}

		[Test]
		public void When_Created_then_It_isAvailable()
		{
			var joystickInput = new JoystickInput { Source = CreateJoystickInputSourceMock().Object };

			Assert.True(joystickInput.IsAvailable);
		}

		private static Mock<IJoystickInputSource> CreateJoystickInputSourceMock()
		{
			var joystickInpputMock = new Mock<IJoystickInputSource>();
			joystickInpputMock.Setup(x => x.ButtonCount).Returns(10);
			joystickInpputMock.Setup(x => x.IsAvailable).Returns(true);

			return joystickInpputMock;
		}
	}
}
