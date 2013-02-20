using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Cloning;
using Duality.Resources;

using DualityEditor.EditorRes;

namespace DualityEditor.UndoRedoActions
{
	public class DeleteGameObjectAction : UndoRedoAction
	{
		private	GameObject[]	targetObj		= null;
		private	GameObject[]	backupParentObj	= null;
		private GameObject[]	backupObj		= null;

		public override string Name
		{
			get { return this.targetObj.Length == 1 ? 
				string.Format(GeneralRes.UndoRedo_DeleteGameObject, this.targetObj[0].Name) :
				string.Format(GeneralRes.UndoRedo_DeleteGameObjectMulti, this.targetObj.Length); }
		}

		public DeleteGameObjectAction(IEnumerable<GameObject> obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			this.targetObj = obj.Where(o => o != null && !o.Disposed).ToArray();
		}

		public override void Do()
		{
			if (this.backupObj == null)
			{
				this.backupObj = new GameObject[this.targetObj.Length];
				this.backupParentObj = new GameObject[this.targetObj.Length];
				for (int i = 0; i < this.backupObj.Length; i++)
				{
					this.backupObj[i] = CloneProvider.DeepClone(this.targetObj[i], BackupCloneContext);
					this.backupParentObj[i] = this.targetObj[i].Parent;
				}
			}

			foreach (GameObject obj in this.targetObj)
			{
				obj.Dispose();
				Scene.Current.UnregisterObj(obj); 
			}
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(Scene.Current));
		}
		public override void Undo()
		{
			if (this.backupObj == null) throw new InvalidOperationException("Can't undo what hasn't been done yet");
			for (int i = 0; i < this.backupObj.Length; i++)
			{
				CloneProvider.DeepCopyTo(this.backupObj[i], this.targetObj[i], BackupCloneContext);
				Scene.Current.RegisterObj(this.targetObj[i]);
				this.targetObj[i].Parent = this.backupParentObj[i];
			}
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(Scene.Current));
		}
	}
}
