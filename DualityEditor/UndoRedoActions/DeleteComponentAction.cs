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
	public class DeleteComponentAction : UndoRedoAction
	{
		private	Component[]		targetObj		= null;
		private	GameObject[]	backupParentObj	= null;
		private Component[]		backupObj		= null;

		public override string Name
		{
			get { return this.targetObj.Length == 1 ? 
				string.Format(GeneralRes.UndoRedo_DeleteComponent, this.targetObj[0].GetType().Name) :
				string.Format(GeneralRes.UndoRedo_DeleteComponentMulti, this.targetObj.Length); }
		}
		public override bool IsVoid
		{
			get { return this.targetObj == null || this.targetObj.Length == 0; }
		}

		public DeleteComponentAction(IEnumerable<Component> obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			this.targetObj = obj.Where(o => o != null && !o.Disposed).ToArray();
		}

		public override void Do()
		{
			if (this.backupObj == null)
			{
				this.backupObj = new Component[this.targetObj.Length];
				this.backupParentObj = new GameObject[this.targetObj.Length];
				for (int i = 0; i < this.backupObj.Length; i++)
				{
					this.backupObj[i] = CloneProvider.DeepClone(this.targetObj[i], BackupCloneContext);
					this.backupParentObj[i] = this.targetObj[i].GameObj;
				}
			}

			foreach (Component obj in this.targetObj)
			{
				obj.Dispose();
			}
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(Scene.Current));
		}
		public override void Undo()
		{
			if (this.backupObj == null) throw new InvalidOperationException("Can't undo what hasn't been done yet");
			for (int i = this.backupObj.Length - 1; i >= 0; i--)
			{
				CloneProvider.DeepCopyTo(this.backupObj[i], this.targetObj[i], BackupCloneContext);
				this.targetObj[i].GameObj = this.backupParentObj[i];
			}
			DualityEditorApp.NotifyObjPropChanged(this, new ObjectSelection(Scene.Current));
		}
	}
}
