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
	public class ComponentRefPropertyEditor : ObjectRefPropertyEditor
	{
		protected	Type		editedCmpType		= null;
		protected	Component	component			= null;
		protected	Component	prevImageObj		= null;
		
		public override object DisplayedValue
		{
			get { return this.component; }
		}
		public override string ReferenceName
		{
			get { return this.component != null ? this.component.ToString() : null; }
		}
		public override bool ReferenceBroken
		{
			get { return this.component != null && this.component.Disposed; }
		}


		public override void ShowReferencedContent()
		{
			if (this.component == null) return;
			SceneView view = EditorBasePlugin.Instance.RequestSceneView();
			view.FlashNode(view.FindNode(this.component));
			System.Media.SystemSounds.Beep.Play();
		}
		public override void ResetReference()
		{
			if (this.ReadOnly) return;
			this.component = null;
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
			this.OnEditingFinished();
		}
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			Component[] values = this.GetValue().Cast<Component>().ToArray();

			this.BeginUpdate();
			if (!values.Any())
			{
				this.component = null;
			}
			else
			{
				Component first = values.NotNull().FirstOrDefault();
				this.component = first;
				this.multiple = (values.Any(o => o == null) || values.Any(o => o != first));

				this.GeneratePreviewImage();
			}
			this.EndUpdate();
		}
		protected void GeneratePreviewImage()
		{
			if (this.prevImageObj == this.component) return;
			this.prevImageObj = this.component;

			if (this.prevImage != null) this.prevImage.Dispose();
			this.prevImage = null;
			this.Height = 22;

			if (this.component != null)
			{
				this.prevImage = PreviewProvider.GetPreviewImage(this.component, this.ClientRectangle.Width - 4 - 22, 64 - 4, PreviewSizeMode.FixedHeight);
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
			data.SetComponentRefs(new[] { this.component });
		}
		protected override void DeserializeFromData(DataObject data)
		{
			ConvertOperation convert = new ConvertOperation(data, ConvertOperation.Operation.All);
			if (convert.CanPerform(this.editedCmpType))
			{
				var refQuery = convert.Perform(this.editedCmpType);
				if (refQuery != null)
				{
					Component[] refArray = refQuery.Cast<Component>().ToArray();
					this.component = (refArray != null && refArray.Length > 0) ? refArray[0] : null;
					this.PerformSetValue();
					this.OnValueChanged();
					this.PerformGetValue();
					this.OnEditingFinished();
				}
			}
		}
		protected override bool CanDeserializeFromData(DataObject data)
		{
			return new ConvertOperation(data, ConvertOperation.Operation.All).CanPerform(this.editedCmpType);
		}

		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			if (typeof(Component).IsAssignableFrom(this.EditedType))
				this.editedCmpType = this.EditedType;
			else
				this.editedCmpType = typeof(Component);
		}
	}
}

