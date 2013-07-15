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

		[Test]
		public void When_copied_Then_sensor_value_is_copied()
		{
			_rigidBody.IsSensor = true;

			var clone = new RigidBody();
			_rigidBody.CopyTo(clone);

			Assert.IsTrue(clone.IsSensor);
		}

		[Test]
		public void When_BodyType_is_set_to_kinematic_after_init_Then_underlying_body_is_kinematic()
		{
			((ICmpInitializable)_rigidBody).OnInit(Component.InitContext.Activate);

			_rigidBody.BodyType = BodyType.Kinematic;

			Assert.AreEqual(FarseerPhysics.Dynamics.BodyType.Kinematic, _rigidBody.PhysicsBody.BodyType);
		}

		[Test]
		public void When_BodyType_is_set_to_kinematic_before_init_Then_underlying_body_is_kinematic()
		{
			_rigidBody.BodyType = BodyType.Kinematic;
			
			((ICmpInitializable)_rigidBody).OnInit(Component.InitContext.Activate);

			Assert.AreEqual(FarseerPhysics.Dynamics.BodyType.Kinematic, _rigidBody.PhysicsBody.BodyType);
		}
	}
}
