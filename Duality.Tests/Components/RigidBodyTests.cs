using System.Linq;
using Duality.Components;
using Duality.Components.Physics;
using NUnit.Framework;

namespace Duality.Tests.Components
{
	[TestFixture]
	public class RigidBodyTests
	{
		private RigidBody _rigidBody;

		[SetUp]
		public void SetUp()
		{
			var gameObject = new GameObject();
			_rigidBody = new RigidBody();
			gameObject.AddComponent(_rigidBody);
			gameObject.AddComponent(new Transform());
		}

		[Test]
		public void When_IsSensor_is_set_before_init_Then_sets_property_on_underlying_body()
		{
			_rigidBody.IsSensor = true;
			
			((ICmpInitializable)_rigidBody).OnInit(Component.InitContext.Activate);
			
			Assert.IsTrue(_rigidBody.PhysicsBody.FixtureList.All(f => f.IsSensor));
		}

		[Test]
		public void When_IsSensor_is_set_after_init_Then_sets_property_on_underlying_body()
		{
			((ICmpInitializable)_rigidBody).OnInit(Component.InitContext.Activate);

			_rigidBody.IsSensor = true;

			Assert.IsTrue(_rigidBody.PhysicsBody.FixtureList.All(f => f.IsSensor));
		}
	}
}
