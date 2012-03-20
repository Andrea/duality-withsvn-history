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
	public class DualitorPropertyGrid : PropertyGrid, IHelpProvider
	{
		public DualitorPropertyGrid()
		{
			this.ControlRenderer.ColorMultiple = Color.FromArgb(242, 212, 170);
			this.ControlRenderer.ColorGrayText = Color.FromArgb(160, 160, 160);
			this.ControlRenderer.ColorVeryLightBackground = Color.FromArgb(224, 224, 224);
			this.ControlRenderer.ColorLightBackground = Color.FromArgb(212, 212, 212);
			this.ControlRenderer.ColorBackground = Color.FromArgb(196, 196, 196);
		}

		public override void ConfigureEditor(PropertyEditor editor, object configureData = null)
		{
			base.ConfigureEditor(editor, configureData);
			var hintOverride = configureData as IEnumerable<EditorHintMemberAttribute>;

			if (editor is MemberwisePropertyEditor)
			{
				MemberwisePropertyEditor memberEditor = editor as MemberwisePropertyEditor;
				memberEditor.MemberPredicate = this.EditorMemberPredicate;
				memberEditor.MemberAffectsOthers = this.EditorMemberAffectsOthers;
			}

			var flagsAttrib = editor.EditedMember.GetEditorHint<EditorHintFlagsAttribute>(hintOverride);
			if (flagsAttrib != null)
			{
				editor.ForceWriteBack = (flagsAttrib.Flags & MemberFlags.ForceWriteback) == MemberFlags.ForceWriteback;
				if ((flagsAttrib.Flags & MemberFlags.ReadOnly) == MemberFlags.ReadOnly)
					editor.Setter = null;
			}

			if (editor is NumericPropertyEditor)
			{
				var rangeAttrib = editor.EditedMember.GetEditorHint<EditorHintRangeAttribute>(hintOverride);
				var incAttrib = editor.EditedMember.GetEditorHint<EditorHintIncrementAttribute>(hintOverride);
				var placesAttrib = editor.EditedMember.GetEditorHint<EditorHintDecimalPlacesAttribute>(hintOverride);
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

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			// A dropdown is opened. Provide dropdown help.
			IPopupControlHost dropdownEdit = this.FocusEditor as IPopupControlHost;
			if (dropdownEdit != null && dropdownEdit.IsDropDownOpened)
			{
				// Special case: Its a known basic dropdown.
				EnumPropertyEditor enumEdit = dropdownEdit as EnumPropertyEditor;
				FlaggedEnumPropertyEditor enumFlagEdit = dropdownEdit as FlaggedEnumPropertyEditor;
				if (enumEdit != null)
				{
					captured = true;
					if (enumEdit.DropDownHoveredName != null)
						return HelpInfo.FromMember(enumEdit.EditedType.GetField(enumEdit.DropDownHoveredName, ReflectionHelper.BindAll));
					else
					{
						FieldInfo field = enumEdit.EditedType.GetField(enumEdit.DisplayedValue.ToString(), ReflectionHelper.BindAll);
						if (field != null) return HelpInfo.FromMember(field);
					}
				}
				else if (enumFlagEdit != null)
				{
					captured = true;
					if (enumFlagEdit.DropDownHoveredItem != null)
						return HelpInfo.FromMember(enumFlagEdit.EditedType.GetField(enumFlagEdit.DropDownHoveredItem.Caption, ReflectionHelper.BindAll));
					else
					{
						FieldInfo field = enumFlagEdit.EditedType.GetField(enumFlagEdit.DisplayedValue.ToString(), ReflectionHelper.BindAll);
						if (field != null) return HelpInfo.FromMember(field);
					}
				}
				// Its able to provide help. Redirect.
				else if (dropdownEdit is IHelpProvider)
				{
					captured = true;
					Point dropdownEditorPos = this.GetEditorLocation(dropdownEdit as PropertyEditor, true);
					return (dropdownEdit as IHelpProvider).ProvideHoverHelp(new Point(localPos.X - dropdownEditorPos.X, localPos.Y - dropdownEditorPos.Y), ref captured);
				}

				// No help available.
				return null;
			}
			captured = false;

			// Pick an editor and see if it has access to an actual IHelpProvider
			PropertyEditor pickedEditor = this.PickEditorAt(localPos.X, localPos.Y, true);
			IHelpProvider localProvider = null;
			PropertyEditor helpEditor = pickedEditor;
			Point helpEditorPos = Point.Empty;
			HelpInfo localHelp = null;
			while (helpEditor != null)
			{
				helpEditorPos = this.GetEditorLocation(helpEditor, true);
				if (helpEditor is IHelpProvider)
				{
					localProvider = helpEditor as IHelpProvider;
					localHelp = localProvider.ProvideHoverHelp(new Point(localPos.X - helpEditorPos.X, localPos.Y - helpEditorPos.Y), ref captured);
					if (localHelp != null)
						return localHelp;
				}
				helpEditor = helpEditor.ParentEditor;
			}

			// If not, default to member or type information
			if (pickedEditor != null)
			{
				if (pickedEditor.EditedMember != null)
					return HelpInfo.FromMember(pickedEditor.EditedMember);
				else if (pickedEditor.EditedType != null)
					return HelpInfo.FromMember(pickedEditor.EditedType);
			}

			return null;
		}
		bool IHelpProvider.PerformHelpAction(HelpInfo info)
		{
			return (this as IHelpProvider).DefaultPerformHelpAction(info);
		}
	}
}
