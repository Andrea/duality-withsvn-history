using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Cloning;
using Duality.Resources;

using DualityEditor.EditorRes;

using OpenTK;

namespace DualityEditor.UndoRedoActions
{
	public class CloneGameObjectAction : UndoRedoAction
	{
		private	GameObject[]	targetObj		= null;
		private GameObject[]	resultObj		= null;

		public override string Name
		{
			get { return this.targetObj.Length == 1 ? 
				string.Format(GeneralRes.UndoRedo_CloneGameObject, this.targetObj[0].Name) :
				string.Format(GeneralRes.UndoRedo_CloneGameObjectMulti, this.targetObj.Length); }
		}
		public IEnumerable<GameObject> Result
		{
			get { return this.resultObj; }
		}

		public CloneGameObjectAction(IEnumerable<GameObject> obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			this.targetObj = obj.Where(o => o != null && !o.Disposed).ToArray();
		}

		public override void Do()
		{
			if (this.resultObj == null)
			{
				this.resultObj = new GameObject[this.targetObj.Length];
				for (int i = 0; i < this.targetObj.Length; i++)
					this.resultObj[i] = this.targetObj[i].Clone();
			}
			else
			{
				for (int i = 0; i < this.targetObj.Length; i++)
					this.targetObj[i].CopyTo(this.resultObj[i]);
			}

			for (int i = 0; i < this.targetObj.Length; i++)
			{
				GameObject clone = this.resultObj[i];

				// Prevent physics from getting crazy.
				if (clone.Transform != null && clone.RigidBody != null)
					clone.Transform.Pos += Vector3.UnitX * 0.001f;

				Scene.Current.RegisterObj(clone);
			}
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(Scene.Current));
		}
		public override void Undo()
		{
			if (this.resultObj == null) throw new InvalidOperationException("Can't undo what hasn't been done yet");
			foreach (GameObject clone in this.resultObj)
			{
				clone.Dispose();
				Scene.Current.UnregisterObj(clone);
			}
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(Scene.Current));
		}
	}
}
