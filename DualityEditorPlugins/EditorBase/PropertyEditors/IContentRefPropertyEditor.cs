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
	public class IContentRefPropertyEditor : ObjectRefPropertyEditor
	{
		protected	Type		editedResType		= null;
		protected	string		contentPath			= null;
		protected	string		prevImagePath		= null;
		
		public override object DisplayedValue
		{
			get 
			{ 
				IContentRef ctRef = ReflectionHelper.CreateInstanceOf(this.EditedType) as IContentRef;
				ctRef.Path = this.contentPath;
				ctRef.MakeAvailable();
				return ctRef;
			}
		}
		public override string ReferenceName
		{
			get 
			{
				IContentRef r = this.DisplayedValue as IContentRef;
				return r.IsExplicitNull ? null : r.FullName;
			}
		}
		public override bool ReferenceBroken
		{
			get
			{
				IContentRef r = this.DisplayedValue as IContentRef;
				return !r.IsExplicitNull && !r.IsAvailable;
			}
		}


		public override void ShowReferencedContent()
		{
			if (string.IsNullOrEmpty(this.contentPath)) return;
			ProjectFolderView view = EditorBasePlugin.Instance.RequestProjectView();
			view.FlashNode(view.NodeFromPath(this.contentPath));
			System.Media.SystemSounds.Beep.Play();
		}
		public override void ResetReference()
		{
			if (this.ReadOnly) return;
			this.contentPath = null;
			this.PerformSetValue();
			this.OnValueChanged();
			this.PerformGetValue();
			this.OnEditingFinished();
		}
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			IContentRef[] values = this.GetValue().Cast<IContentRef>().ToArray();

			this.BeginUpdate();
			if (!values.Any())
			{
				this.contentPath = null;
			}
			else
			{
				IContentRef first = values.NotNull().FirstOrDefault();
				this.contentPath = first.Path;
				this.multiple = (values.Any(o => o == null) || values.Any(o => o.Path != first.Path));

				this.GeneratePreviewImage();
			}
			this.EndUpdate();
		}
		protected void GeneratePreviewImage()
		{
			if (this.prevImagePath == this.contentPath) return;
			this.prevImagePath = this.contentPath;

			if (this.prevImage != null) this.prevImage.Dispose();
			this.prevImage = null;
			this.Height = 22;

			Resource res = (this.DisplayedValue as IContentRef).Res;
			if (res != null)
			{
				this.prevImage = PreviewProvider.GetPreviewImage(res, this.ClientRectangle.Width - 4 - 22, 64 - 4, PreviewSizeMode.FixedHeight);
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
			data.SetContentRefs(new[] { this.DisplayedValue as IContentRef });
		}
		protected override void DeserializeFromData(DataObject data)
		{
			ConvertOperation convert = new ConvertOperation(data, ConvertOperation.Operation.Convert);
			if (convert.CanPerform(this.editedResType))
			{
				var refQuery = convert.Perform(this.editedResType);
				if (refQuery != null)
				{
					Resource[] refArray = refQuery.Cast<Resource>().ToArray();
					this.contentPath = (refArray != null && refArray.Length > 0) ? refArray[0].Path : null;
					this.PerformSetValue();
					this.OnValueChanged();
					this.PerformGetValue();
					this.OnEditingFinished();
				}
			}
		}
		protected override bool CanDeserializeFromData(DataObject data)
		{
			return new ConvertOperation(data, ConvertOperation.Operation.Convert).CanPerform(this.editedResType);
		}

		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			if (this.EditedType.IsGenericType)
				this.editedResType = this.EditedType.GetGenericArguments()[0];
			else
				this.editedResType = typeof(Resource);
		}
	}
}

