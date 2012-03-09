using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;

using OpenTK;
using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;
using PropertyGrid = AdamsLair.PropertyGrid.PropertyGrid;

using Duality;
using Duality.EditorHints;

namespace DualityEditor.Controls
{
	public class DualitorPropertyGrid : PropertyGrid
	{
		public override void ConfigureEditor(PropertyEditor editor)
		{
			base.ConfigureEditor(editor);
			if (editor is MemberwisePropertyEditor)
			{
				MemberwisePropertyEditor memberEditor = editor as MemberwisePropertyEditor;
				memberEditor.MemberPredicate = this.EditorMemberPredicate;
				memberEditor.MemberAffectsOthers = this.EditorMemberAffectsOthers;
			}
			if (editor.EditedMember != null)
			{
				EditorHintFlagsAttribute flagsAttrib = editor.EditedMember.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
				if (flagsAttrib != null)
				{
					editor.ForceWriteBack = (flagsAttrib.Flags & MemberFlags.ForceWriteback) == MemberFlags.ForceWriteback;
					if ((flagsAttrib.Flags & MemberFlags.ReadOnly) == MemberFlags.ReadOnly)
						editor.Setter = null;
				}

				if (editor is NumericPropertyEditor)
				{
					EditorHintRangeAttribute rangeAttrib = editor.EditedMember.GetCustomAttributes(typeof(EditorHintRangeAttribute), true).FirstOrDefault() as EditorHintRangeAttribute;
					EditorHintIncrementAttribute incAttrib = editor.EditedMember.GetCustomAttributes(typeof(EditorHintIncrementAttribute), true).FirstOrDefault() as EditorHintIncrementAttribute;
					EditorHintDecimalPlacesAttribute placesAttrib = editor.EditedMember.GetCustomAttributes(typeof(EditorHintDecimalPlacesAttribute), true).FirstOrDefault() as EditorHintDecimalPlacesAttribute;
					NumericPropertyEditor numEditor = editor as NumericPropertyEditor;
					if (rangeAttrib != null)
					{
						numEditor.Maximum = rangeAttrib.Max;
						numEditor.Minimum = rangeAttrib.Min;
					}
					if (incAttrib != null) numEditor.Increment = incAttrib.Increment;
					if (placesAttrib != null) numEditor.DecimalPlaces = placesAttrib.Places;
				}
			}
		}

		protected bool EditorMemberPredicate(MemberInfo info)
		{
			EditorHintFlagsAttribute flagsAttrib = info.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			return flagsAttrib == null || (flagsAttrib.Flags & MemberFlags.Invisible) == MemberFlags.None;
		}
		protected bool EditorMemberAffectsOthers(MemberInfo info)
		{
			EditorHintFlagsAttribute flagsAttrib = info.GetCustomAttributes(typeof(EditorHintFlagsAttribute), true).FirstOrDefault() as EditorHintFlagsAttribute;
			return flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.AffectsOthers) != MemberFlags.None;
		}
	}
}
