﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Reflection;

using AdamsLair.PropertyGrid;
using AdamsLair.PropertyGrid.PropertyEditors;
using PropertyGrid = AdamsLair.PropertyGrid.PropertyGrid;

using Duality;
using Duality.EditorHints;

using DualityEditor.UndoRedoActions;

namespace DualityEditor.Controls
{
	public class DualitorPropertyGrid : PropertyGrid, IHelpProvider
	{
		public DualitorPropertyGrid()
		{
			this.ControlRenderer.ColorMultiple = Color.FromArgb(242, 212, 170);
			this.ControlRenderer.ColorGrayText = Color.FromArgb(96, 96, 96);
			this.ControlRenderer.ColorVeryLightBackground = Color.FromArgb(224, 224, 224);
			this.ControlRenderer.ColorLightBackground = Color.FromArgb(212, 212, 212);
			this.ControlRenderer.ColorBackground = Color.FromArgb(196, 196, 196);
		}

		public override void ConfigureEditor(PropertyEditor editor, object configureData = null)
		{
			IEnumerable<EditorHintMemberAttribute> hintOverride = configureData as IEnumerable<EditorHintMemberAttribute>;
			IEnumerable<EditorHintMemberAttribute> parentHint = null;
			if (editor.ParentEditor != null)
			{
				IEnumerable<EditorHintMemberAttribute> parentHintOverride = editor.ParentEditor.ConfigureData as IEnumerable<EditorHintMemberAttribute>;
				if (editor.ParentEditor.EditedMember != null)
					parentHint = editor.ParentEditor.EditedMember.GetEditorHints<EditorHintMemberAttribute>(parentHintOverride);
				else
					parentHint = parentHintOverride;
			}

			if (hintOverride == null && parentHint != null)
			{
				// No configuration data available? Allow to derive certain types from parent list or dictionary.
				if (editor.ParentEditor is IListPropertyEditor || editor.ParentEditor is IDictionaryPropertyEditor)
				{
					hintOverride = parentHint.Where(a => 
						a is EditorHintDecimalPlacesAttribute ||
						a is EditorHintIncrementAttribute ||
						a is EditorHintRangeAttribute);
				}
				// That way we can specify the decimal places of an array of Vector2-structs and actually change those Vector2 editors.
			}

			// Invoke the PropertyEditor's configure method
			base.ConfigureEditor(editor, hintOverride);

			// Do some final configuration for editors that do not behave as intended by default.
			if (editor is MemberwisePropertyEditor)
			{
				MemberwisePropertyEditor memberEditor = editor as MemberwisePropertyEditor;
				memberEditor.MemberPredicate = this.EditorMemberPredicate;
				memberEditor.MemberAffectsOthers = this.EditorMemberAffectsOthers;
				memberEditor.MemberPropertySetter = this.EditorMemberPropertySetter;
				memberEditor.MemberFieldSetter = this.EditorMemberFieldSetter;
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
		protected override void PrepareSetValue()
		{
			base.PrepareSetValue();
			UndoRedoManager.BeginMacro();
		}
		protected override void PostSetValue()
		{
			base.PostSetValue();
			UndoRedoManager.EndMacro(UndoRedoManager.MacroDeriveName.FromFirst);
		}
		protected override void OnEditingFinished(PropertyEditorValueEventArgs e)
		{
			base.OnEditingFinished(e);
			UndoRedoManager.Finish();
		}

		private bool EditorMemberPredicate(MemberInfo info)
		{
			if (this.ShowNonPublic)
			{
				// Show member, if not declared inside Duality itself
				if (info.DeclaringType.Assembly != typeof(DualityApp).Assembly) return true;

				// Reject non-public fields
				FieldInfo field = info as FieldInfo;
				if (field != null && !field.IsPublic) return false;

				// Reject non-public properties
				PropertyInfo property = info as PropertyInfo;
				if (property != null && property.GetGetMethod(false) == null && property.GetSetMethod(false) == null) return false;
			}
			else
			{
				// Don't show fields of a Component - we don't want to encourage using them, since Duality basically works with Properties.
				if (info is FieldInfo && typeof(Component).IsAssignableFrom(info.DeclaringType)) return false;
			}

			// Reject invisible members
			EditorHintFlagsAttribute flagsAttrib = info.GetEditorHint<EditorHintFlagsAttribute>();
			if (flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.Invisible) != MemberFlags.None) return false;

			return true;
		}
		private bool EditorMemberAffectsOthers(MemberInfo info)
		{
			EditorHintFlagsAttribute flagsAttrib = info.GetEditorHint<EditorHintFlagsAttribute>();
			return this.ShowNonPublic || (flagsAttrib != null && (flagsAttrib.Flags & MemberFlags.AffectsOthers) != MemberFlags.None);
		}
		private void EditorMemberPropertySetter(PropertyInfo property, IEnumerable<object> targetObjects, IEnumerable<object> values)
		{
			UndoRedoManager.Do(new EditPropertyAction(this, property, targetObjects, values));
		}
		private void EditorMemberFieldSetter(FieldInfo field, IEnumerable<object> targetObjects, IEnumerable<object> values)
		{
			UndoRedoManager.Do(new EditFieldAction(this, field, targetObjects, values));
		}

		HelpInfo IHelpProvider.ProvideHoverHelp(Point localPos, ref bool captured)
		{
			// A dropdown is opened. Provide dropdown help.
			IPopupControlHost dropdownEdit = this.FocusEditor as IPopupControlHost;
			if (dropdownEdit != null && dropdownEdit.IsDropDownOpened)
			{
				EnumPropertyEditor enumEdit = dropdownEdit as EnumPropertyEditor;
				FlaggedEnumPropertyEditor enumFlagEdit = dropdownEdit as FlaggedEnumPropertyEditor;
				ObjectSelectorPropertyEditor objectSelectorEdit = dropdownEdit as ObjectSelectorPropertyEditor;

				// Its able to provide help. Redirect.
				if (dropdownEdit is IHelpProvider)
				{
					captured = true;
					Point dropdownEditorPos = this.GetEditorLocation(dropdownEdit as PropertyEditor, true);
					return (dropdownEdit as IHelpProvider).ProvideHoverHelp(new Point(localPos.X - dropdownEditorPos.X, localPos.Y - dropdownEditorPos.Y), ref captured);
				}
				// Special case: Its a known basic dropdown.
				else if (enumEdit != null)
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
				else if (objectSelectorEdit != null)
				{
					captured = true;
					if (objectSelectorEdit.DropDownHoveredObject != null)
						return HelpInfo.FromObject(objectSelectorEdit.DropDownHoveredObject.Value);
					else
						return HelpInfo.FromObject(objectSelectorEdit.DisplayedValue);
				}

				// No help available.
				return null;
			}
			captured = false;

			// Pick an editor and see if it has access to an actual IHelpProvider
			PropertyEditor pickedEditor = this.PickEditorAt(localPos.X, localPos.Y, true);
			PropertyEditor helpEditor = pickedEditor;
			while (helpEditor != null)
			{
				Point helpEditorPos = this.GetEditorLocation(helpEditor, true);
				if (helpEditor is IHelpProvider)
				{
					IHelpProvider localProvider = helpEditor as IHelpProvider;
					HelpInfo localHelp = localProvider.ProvideHoverHelp(new Point(localPos.X - helpEditorPos.X, localPos.Y - helpEditorPos.Y), ref captured);
					if (localHelp != null)
						return localHelp;
				}
				helpEditor = helpEditor.ParentEditor;
			}

			// If not, default to member or type information
			if (pickedEditor != null)
			{
				if (!string.IsNullOrEmpty(pickedEditor.PropertyDesc))
					return HelpInfo.FromText(pickedEditor.PropertyName, pickedEditor.PropertyDesc);
				else if (pickedEditor.EditedMember != null)
					return HelpInfo.FromMember(pickedEditor.EditedMember);
				else if (pickedEditor.EditedType != null)
					return HelpInfo.FromMember(pickedEditor.EditedType);
			}

			return null;
		}
	}
}
