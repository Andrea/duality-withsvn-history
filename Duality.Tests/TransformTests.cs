using System;
using Duality;
using Duality.Components;
using NUnit.Framework;
using OpenTK;


namespace DualityDebuggingTest
{
	[TestFixture]
	public class TransformIntegrationTests
	{

	}

	[TestFixture]
	public class TransformTests
	{
		private Vector3 _expectedPosition;
		private Transform _transform;
		private Vector3 _parentPosition;

		[SetUp]
		public void Setup()
		{
			_expectedPosition = new Vector3(1, 1, 1);
		}

		[TearDown]
		public void TearDown()
		{
			DisposeTransform();
		}

		private void DisposeTransform()
		{
			if (_transform == null)
				return;

			if (_transform.GameObj != null && _transform.GameObj.Parent != null)
				_transform.GameObj.Parent.Dispose();
			else if (_transform.GameObj != null)
				_transform.GameObj.Dispose();

			_transform = null;
		}

		[TestCase(true)]
		[TestCase(false)]
		public void When_ComitChanges_Then_EventTransformChangedCalled(bool hasParent)
		{
			bool called = false;
			CreateTransform(hasParent);
			_transform.EventTransformChanged += (sender, args) => called = true;

			_transform.CommitChanges();

			Assert.IsTrue(called);
			Assert.AreEqual(_expectedPosition, _transform.Pos);
		}

		[Test]
		public void When_Relativeforward_Then_calculate_based_in_relativeAngle()
		{
			CreateTransform(false);
			_transform.RelativeAngle = 5.4f;
			var expected = new Vector3(MathF.Sin(_transform.RelativeAngle), -MathF.Cos(_transform.RelativeAngle), 0.0f);
			Assert.AreEqual(expected, _transform.RelativeForward);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void When_SetTransform_Then_no_event_fired(bool hasParent)
		{
			CreateTransform(hasParent);
			bool called = false;
			_transform.EventTransformChanged += (sender, args) => called = true;

			_transform.SetTransform(_expectedPosition, 1, (float)Math.PI);

			Assert.IsFalse(called);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void When_SetTransform_Then_values_set(bool useparent)
		{
			CreateTransform(useparent);

			_transform.SetTransform(_expectedPosition, 1, (float)Math.PI);

			Assert.AreNotEqual(0, _transform.Angle);
			Assert.AreNotEqual(false, _transform.DeriveAngle);
			Assert.AreNotEqual(Vector3.Zero, _transform.Forward);
			Assert.AreNotEqual(Vector3.Zero, _transform.Pos);
			Assert.AreNotEqual(0, _transform.RelativeAngle);
			Assert.AreNotEqual(Vector3.Zero, _transform.RelativeForward);
			Assert.AreNotEqual(Vector3.Zero, _transform.RelativePos);
			Assert.AreNotEqual(Vector3.Zero, _transform.RelativeRight);
			Assert.AreNotEqual(0, _transform.RelativeScale);
			Assert.AreNotEqual(Vector3.Zero, _transform.Right);
			Assert.AreNotEqual(0, _transform.Scale);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void When_SetRelativeTransform_Then_values_set(bool useparent)
		{
			CreateTransform(useparent);

			_transform.SetRelativeTransform(_expectedPosition, 1, (float)Math.PI);

			Assert.AreNotEqual(0, _transform.Angle);
			Assert.AreNotEqual(false, _transform.DeriveAngle);
			Assert.AreNotEqual(Vector3.Zero, _transform.Forward);
			Assert.AreNotEqual(Vector3.Zero, _transform.Pos);
			Assert.AreNotEqual(0, _transform.RelativeAngle);
			Assert.AreNotEqual(Vector3.Zero, _transform.RelativeForward);
			Assert.AreNotEqual(Vector3.Zero, _transform.RelativePos);
			Assert.AreNotEqual(Vector3.Zero, _transform.RelativeRight);
			Assert.AreNotEqual(0, _transform.RelativeScale);
			Assert.AreNotEqual(Vector3.Zero, _transform.Right);
			Assert.AreNotEqual(0, _transform.Scale);
		}


		[TestCase(true)]
		[TestCase(false)]
		public void When_MoveByAbs_Then_move_object_by_given_abs_vector(bool useParent)
		{
			var added = new Vector3(2, 2, 2);
			CreateTransform(useParent);

			_transform.MoveByAbs(added);

			Assert.AreEqual(_expectedPosition + added, _transform.Pos);
		}

		[Test]
		public void When_RelativePosition_set_with_no_aprent_Then_same_as_PositionSet()
		{
			CreateTransform(false);

			_transform.RelativePos = _expectedPosition;

			Assert.AreEqual(_transform.Pos, _transform.RelativePos);
		}

		[Test]
		public void When_RelativePosition_set_with_no_parent_Then_relativePosition()
		{
			CreateTransform(true);

			_transform.RelativePos = _expectedPosition;

			Assert.AreEqual(_expectedPosition, _transform.RelativePos);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void When_RelativeAngle_set_Then_angleNormalized(bool hasParent)
		{
			CreateTransform(hasParent);

			_transform.RelativeAngle = 10.5f;

			Assert.AreEqual(MathF.NormalizeAngle(10.5f), _transform.RelativeAngle);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void When_Angle_set_Then_angleNormalized(bool hasParent)
		{
			CreateTransform(hasParent);

			_transform.Angle = 10.5f;

			Assert.AreEqual(MathF.NormalizeAngle(10.5f), _transform.Angle);
		}

		private void CreateTransform(bool createWithParent)
		{
			var gameObject = new GameObject("Object");
			_transform = gameObject.AddComponent<Transform>();
			_transform.Pos = _expectedPosition;

			if (createWithParent)
			{
				_parentPosition = new Vector3(3, 3, 3);

				var parent = new GameObject("Parent");
				var parentTransform = parent.AddComponent<Transform>();
				parentTransform.Pos = _parentPosition;

				gameObject.Parent = parent;
			}


		}
	}

	public interface Bla
	{
		void Bloo();
	}

	internal class Blaaa : Bla
	{
		public void Bloo()
		{
			throw new NotImplementedException();
		}
	}
}
