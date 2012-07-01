using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.Renderer;
using ButtonState = AdamsLair.PropertyGrid.Renderer.ButtonState;
using BorderStyle = AdamsLair.PropertyGrid.Renderer.BorderStyle;

using Duality;
using DualityEditor;
using DualityEditor.CorePluginInterface;

namespace EditorBase.PropertyEditors
{
	public class GameObjectRefPropertyEditor : ObjectRefPropertyEditor
	{
		protected	GameObject	gameObj			= null;
		protected	GameObject	prevImageObj	= null;
		
		public override object DisplayedValue
		{
			get { return this.gameObj; }
		}
		public override string ReferenceName
		{
			get { return this.gameObj != null ? this.gameObj.FullName : null; }
		}
		public override bool ReferenceBroken
		{
			get { return this.gameObj != null && this.gameObj.Disposed; }
		}


		public override void ShowReferencedContent()
		{
			if (this.gameObj == null) return;
			SceneView view = EditorBasePlugin.Instance.RequestSceneView();
			view.FlashNode(view.FindNode(this.gameObj));
			System.Media.SystemSounds.Beep.Play();
		}
		public override void ResetReference()
		{
			if (this.ReadOnly) return;
			this.gameObj = null;
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
			this.OnEditingFinished();
		}
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			GameObject[] values = this.GetValue().Cast<GameObject>().ToArray();

			this.BeginUpdate();
			if (!values.Any())
			{
				this.gameObj = null;
			}
			else
			{
				GameObject first = values.NotNull().FirstOrDefault();
				this.gameObj = first;
				this.multiple = (values.Any(o => o == null) || values.Any(o => o != first));

				this.GeneratePreviewImage();
			}
			this.EndUpdate();
		}
		protected void GeneratePreviewImage()
		{
			if (this.prevImageObj == this.gameObj) return;
			this.prevImageObj = this.gameObj;

			if (this.prevImage != null) this.prevImage.Dispose();
			this.prevImage = null;
			this.Height = 22;

			if (this.gameObj != null)
			{
				this.prevImage = PreviewProvider.GetPreviewImage(this.gameObj, this.ClientRectangle.Width - 4 - 22, 64 - 4, PreviewSizeMode.FixedHeight);
				if (this.prevImage != null)
				{
					this.Height = 64;
					var avgColor = this.prevImage.GetAverageColor();
					this.prevImageLum = avgColor.GetLuminance();
				}
			}
		}
		
		protected override void SerializeToData(DataObject data)
		{
			data.SetGameObjectRefs(new[] { this.gameObj });
		}
		protected override void DeserializeFromData(DataObject data)
		{
			ConvertOperation convert = new ConvertOperation(data, ConvertOperation.Operation.Convert);
			if (convert.CanPerform(typeof(GameObject)))
			{
				var refQuery = convert.Perform(typeof(GameObject));
				if (refQuery != null)
				{
					GameObject[] refArray = refQuery.Cast<GameObject>().ToArray();
					this.gameObj = (refArray != null && refArray.Length > 0) ? refArray[0] : null;
					this.PerformSetValue();
					this.OnValueChanged();
					this.PerformGetValue();
					this.OnEditingFinished();
				}
			}
		}
		protected override bool CanDeserializeFromData(DataObject data)
		{
			return new ConvertOperation(data, ConvertOperation.Operation.Convert).CanPerform(typeof(GameObject));
		}
	}
}

