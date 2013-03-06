﻿using System;
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
			this.PerformGetValue();
			this.OnEditingFinished(FinishReason.LeapValue);
		}
		public override void PerformGetValue()
		{
			base.PerformGetValue();
			GameObject[] values = this.GetValue().Cast<GameObject>().ToArray();

			this.BeginUpdate();
			GameObject lastCmp = this.gameObj;
			bool lastMultiple = this.multiple;
			if (!values.Any())
			{
				this.gameObj = null;
			}
			else
			{
				GameObject first = values.NotNull().FirstOrDefault();
				this.gameObj = first;
				this.multiple = (values.Any(o => o == null) || values.Any(o => o != first));

				this.GeneratePreview();
			}
			this.EndUpdate();
			if (lastCmp != this.gameObj || lastMultiple != this.multiple) this.Invalidate();
		}

		protected void GeneratePreview()
		{
			int prevHash = this.GetPreviewHash();
			if (this.prevImageHash == prevHash) return;
			this.prevImageHash = prevHash;
			
			this.StopPreviewSound();
			if (this.prevSound != null) this.prevSound.Dispose();
			this.prevSound = null;

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

				this.prevSound = PreviewProvider.GetPreviewSound(this.gameObj);
			}
		}
		protected override int GetPreviewHash()
		{
			return this.gameObj != null ? this.gameObj.GetHashCode() : 0;
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
					this.PerformGetValue();
					this.OnEditingFinished(FinishReason.LeapValue);
				}
			}
		}
		protected override bool CanDeserializeFromData(DataObject data)
		{
			return new ConvertOperation(data, ConvertOperation.Operation.Convert).CanPerform(typeof(GameObject));
		}
	}
}

