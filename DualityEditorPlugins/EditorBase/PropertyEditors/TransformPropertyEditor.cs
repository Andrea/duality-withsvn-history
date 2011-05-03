using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Components;
using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public partial class TransformPropertyEditor : PropertyEditor
	{
		private	bool	updatingFromObj		= false;

		public TransformPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		public override void PerformGetValue()
		{
			base.PerformGetValue();
			Transform[] values = this.Getter().Cast<Transform>().ToArray();

			this.updatingFromObj = true;
			if (values.Any())
			{
				// ToDo
				this.Invalidate();
			}
			this.updatingFromObj = false;
		}
		public override void PerformSetValue()
		{
			base.PerformSetValue();
			if (this.ReadOnly) return;

			//this.SetterSingle(this.DisplayedValue);
		}
		public override void UpdateReadOnlyState()
		{
			base.UpdateReadOnlyState();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(
				new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(200, 200, 200), Color.FromArgb(220, 220, 220), 90.0f), 
				this.ClientRectangle);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(64, Color.White)),
				this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Right, this.ClientRectangle.Top);
			e.Graphics.DrawLine(
				new Pen(Color.FromArgb(128, Color.Black)),
				this.ClientRectangle.Left, this.ClientRectangle.Bottom - 1, this.ClientRectangle.Right, this.ClientRectangle.Bottom - 1);
		}
	}

	public class TransformPropertyEditorContainer : ComponentPropertyEditor
	{
		public TransformPropertyEditorContainer(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid)
		{
			this.Indent = 0;
		}

		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();
			TransformPropertyEditor transformEdit = new TransformPropertyEditor(this, this.ParentGrid);
			transformEdit.EditedType = this.EditedType;
			transformEdit.Getter = this.Getter;
			transformEdit.Setter = this.Setter;
			transformEdit.PropertyName = this.PropertyName;
			this.AddPropertyEditor(transformEdit);
		}
		protected override bool MemberPredicate(MemberInfo info)
		{
			if (info.DeclaringType == typeof(Transform))
			{
				return false;
			}
			return base.MemberPredicate(info);
		}
	}
}
