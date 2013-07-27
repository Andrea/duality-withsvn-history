using Duality.Helpers;
using Duality.Resources;
using NUnit.Framework;

namespace Duality.Tests.Messaging
{
	[TestFixture]
	public class MessagingTests
	{
		[Test]
		public void CanBroadcastMessagesToGameObjects()
		{
			var gameObject = new GameObject();
			var receiver = new TestComponent();
			gameObject.AddComponent(receiver);
			Scene.Current.RegisterObj(gameObject);

			receiver.TestBroadcastMessage();

			Assert.IsTrue(receiver.MessageHandled);
		}

		[Test]
		public void CanBroadcastToNamedGameObject()
		{
			var gameObject = new GameObject {Name = "TestGameObject"};
			var receiver = new TestComponent();
			gameObject.AddComponent(receiver);
			Scene.Current.RegisterObj(gameObject);

			var gameObject2 = new GameObject();
			var receiver2 = new TestComponent();
			gameObject2.AddComponent(receiver2);
			Scene.Current.RegisterObj(gameObject2);

			receiver2.TestBroadcastMessageToNamedGameObject();

			Assert.IsTrue(receiver.MessageHandled);
			Assert.IsFalse(receiver2.MessageHandled);
		}

		[Test]
		public void OnlyBroadcastsToActiveGameObjects()
		{
			var listener = (TestComponent)RegisterInactiveObject();

			listener.TestBroadcastMessage();

			Assert.IsFalse(listener.MessageHandled);
		}

		[Test]
		public void DoesNotBroadcastToInactiveNamedObjects()
		{
			var listener = (TestComponent) RegisterInactiveObject();
			listener.GameObj.Name = "TestGameObject";

			listener.TestBroadcastMessageToNamedGameObject();

			Assert.IsFalse(listener.MessageHandled);
		}

		private static Component RegisterInactiveObject()
		{
			var gameObject = new GameObject {Active = false};
			var component = gameObject.AddComponent<TestComponent>();
			Scene.Current.RegisterObj(gameObject);
			return component;
		}

		private class TestComponent : Component, ICmpHandlesMessages
		{
			public bool MessageHandled { get; set; }

			public void HandleMessage(Component sender, GameMessage msg)
			{
				MessageHandled = true;
			}

			public void TestBroadcastMessage()
			{
				this.BroadcastMessage(new TestGameMessage());
			}

			public void TestBroadcastMessageToNamedGameObject()
			{
				this.BroadcastMessage(new TestGameMessage(), "TestGameObject");
			}
		}

		private class TestGameMessage : GameMessage
		{
			
		}
	}
}
