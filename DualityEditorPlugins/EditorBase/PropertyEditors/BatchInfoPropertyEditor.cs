using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.Controls;
using PropertyGrid = DualityEditor.Controls.PropertyGrid;

namespace EditorBase.PropertyEditors
{
	public class BatchInfoPropertyEditor : MemberwisePropertyEditor
	{
		protected	Dictionary<string,PropertyEditor>	shaderVarEditors = new Dictionary<string,PropertyEditor>();

		public BatchInfoPropertyEditor(PropertyEditor parentEditor, PropertyGrid parentGrid) : base(parentEditor, parentGrid, MemberFlags.Default)
		{
		}

		public override void  ClearContent()
		{
 			base.ClearContent();
			this.shaderVarEditors.Clear();
		}

		protected override bool MemberPredicate(MemberInfo info)
		{
			return false;
		}
		protected override void OnAddingEditors()
		{
			base.OnAddingEditors();

			if (this.EditedType == typeof(BatchInfo))
			{
				this.AddEditorForProperty(ReflectionHelper.Property_BatchInfo_MainColor);
				this.AddEditorForProperty(ReflectionHelper.Property_BatchInfo_Technique);
			}
			else if (this.EditedType == typeof(Material))
			{
				this.AddEditorForProperty(ReflectionHelper.Property_Material_MainColor);
				this.AddEditorForProperty(ReflectionHelper.Property_Material_Technique);
			}
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);

			if (values.Any(o => o != null))
			{
				IEnumerable<BatchInfo> batchInfos = null;
				IEnumerable<Material> materials = null;
				DrawTechnique refTech = null;
				if (this.EditedType == typeof(BatchInfo))
				{
					batchInfos = values.Cast<BatchInfo>().NotNull();
					refTech = batchInfos.First().Technique.Res;
				}
				else
				{
					materials = values.Cast<Material>().NotNull();
					refTech = materials.First().Technique.Res;
				}

				// Retrieve data about shader variables
				ShaderVarInfo[] varInfoArray = null;
				if (refTech != null && refTech.Shader.IsAvailable)
				{
					varInfoArray = refTech.Shader.Res.VarInfo;
				}
				else
				{
					varInfoArray = new ShaderVarInfo[] { new ShaderVarInfo() };
					varInfoArray[0].arraySize = 1;
					varInfoArray[0].glVarLoc = -1;
					varInfoArray[0].name = "mainTex";
					varInfoArray[0].scope = ShaderVarScope.Uniform;
					varInfoArray[0].type = ShaderVarType.Sampler2D;
				}

				// Create BatchInfo variables according to Shader uniforms, if not existing yet
				if (!this.ReadOnly)
				{
					foreach (ShaderVarInfo varInfo in varInfoArray)
					{
						if (varInfo.scope != ShaderVarScope.Uniform) continue;

						// Set Texture variables
						if (varInfo.type == ShaderVarType.Sampler2D)
						{
							if (this.EditedType == typeof(BatchInfo))
							{
								foreach (BatchInfo info in batchInfos)
								{
									if (info.Textures == null) info.Textures = new Dictionary<string,ContentRef<Texture>>();
									if (!info.Textures.ContainsKey(varInfo.name))
										info.Textures[varInfo.name] = ContentRef<Texture>.Null;
								}
							}
							else
							{
								foreach (Material info in materials)
								{
									if (info.Textures == null) info.Textures = new Dictionary<string,ContentRef<Texture>>();
									if (!info.Textures.ContainsKey(varInfo.name))
										info.Textures[varInfo.name] = ContentRef<Texture>.Null;
								}
							}
						}
						// Set other uniform variables
						else
						{
							float[] uniformVal = varInfo.InitDataByType();
							if (uniformVal != null)
							{
								if (this.EditedType == typeof(BatchInfo))
								{
									foreach (BatchInfo info in batchInfos)
									{
										if (info.Uniforms == null) info.Uniforms = new Dictionary<string,float[]>();
										if (!info.Uniforms.ContainsKey(varInfo.name))
											info.Uniforms[varInfo.name] = uniformVal;
									}
								}
								else
								{
									foreach (Material info in materials)
									{
										if (info.Uniforms == null) info.Uniforms = new Dictionary<string,float[]>();
										if (!info.Uniforms.ContainsKey(varInfo.name))
											info.Uniforms[varInfo.name] = uniformVal;
									}
								}
							}
						}
					}
				}

				// Create editors according to existing variables
				var texDict = (this.EditedType == typeof(BatchInfo)) ? batchInfos.First().Textures : materials.First().Textures;
				var uniformDict = (this.EditedType == typeof(BatchInfo)) ? batchInfos.First().Uniforms : materials.First().Uniforms;
				Dictionary<string,PropertyEditor> oldEditors = new Dictionary<string,PropertyEditor>(this.shaderVarEditors);
				if (texDict != null)
				{
					foreach (string texName in texDict.Keys)
					{
						if (oldEditors.ContainsKey(texName))
							oldEditors.Remove(texName);
						else
						{
							ContentRefPropertyEditor e = new ContentRefPropertyEditor(this, this.ParentGrid);
							e.EditedType = typeof(ContentRef<Texture>);
							e.Getter = this.CreateTextureValueGetter(texName);
							e.Setter = !this.ReadOnly ? this.CreateTextureValueSetter(texName) : null;
							e.PropertyName = texName;
							this.shaderVarEditors[texName] = e;
							this.AddPropertyEditor(e);
						}
					}
				}
				if (uniformDict != null)
				{
					foreach (string uniformName in uniformDict.Keys)
					{
						if (oldEditors.ContainsKey(uniformName))
							oldEditors.Remove(uniformName);
						else
						{
							ContentRefPropertyEditor e = new ContentRefPropertyEditor(this, this.ParentGrid);
							e.EditedType = typeof(ContentRef<Texture>);
							e.Getter = this.CreateUniformValueGetter(uniformName);
							e.Setter = !this.ReadOnly ? this.CreateUniformValueSetter(uniformName) : null;
							e.PropertyName = uniformName;
							this.shaderVarEditors[uniformName] = e;
							this.AddPropertyEditor(e);
						}
					}
				}

				// Remove old editors that aren't needed anymore
				foreach (var pair in oldEditors)
				{
					this.shaderVarEditors.Remove(pair.Key);
					this.RemovePropertyEditor(pair.Value);
				}
			}
		}
		
		protected Func<IEnumerable<object>> CreateTextureValueGetter(string name)
		{
			if (this.EditedType == typeof(BatchInfo))
				return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)o.Textures[name] : null);
			else
				return () => this.Getter().Cast<Material>().Select(o => o != null ? (object)o.Textures[name] : null);
		}
		protected Func<IEnumerable<object>> CreateUniformValueGetter(string name)
		{
			if (this.EditedType == typeof(BatchInfo))
				return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)o.Uniforms[name] : null);
			else
				return () => this.Getter().Cast<Material>().Select(o => o != null ? (object)o.Uniforms[name] : null);
		}
		protected Action<IEnumerable<object>> CreateTextureValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<ContentRef<Texture>> valuesEnum = values.Cast<ContentRef<Texture>>().GetEnumerator();
				BatchInfo[] batchInfoArray = null;
				Material[] materialArray = null;
				if (this.EditedType == typeof(BatchInfo))
					batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();
				else
					materialArray = this.Getter().Cast<Material>().ToArray();

				ContentRef<Texture> curValue = ContentRef<Texture>.Null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				if (this.EditedType == typeof(BatchInfo))
				{
					foreach (BatchInfo info in batchInfoArray)
					{
						if (info != null) info.Textures[name] = curValue;
						if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
					}
					this.OnPropertySet(ReflectionHelper.Property_BatchInfo_Textures, batchInfoArray);
					this.UpdateModifiedState();
				}
				else
				{
					foreach (Material info in materialArray)
					{
						if (info != null) info.Textures[name] = curValue;
						if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
					}
					this.OnPropertySet(ReflectionHelper.Property_Material_Textures, materialArray);
					this.UpdateModifiedState();
				}
			};
		}
		protected Action<IEnumerable<object>> CreateUniformValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<float[]> valuesEnum = values.Cast<float[]>().GetEnumerator();
				BatchInfo[] batchInfoArray = null;
				Material[] materialArray = null;
				if (this.EditedType == typeof(BatchInfo))
					batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();
				else
					materialArray = this.Getter().Cast<Material>().ToArray();

				float[] curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				if (this.EditedType == typeof(BatchInfo))
				{
					foreach (BatchInfo info in batchInfoArray)
					{
						if (info != null) info.Uniforms[name] = curValue;
						if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
					}
					this.OnPropertySet(ReflectionHelper.Property_BatchInfo_Textures, batchInfoArray);
					this.UpdateModifiedState();
				}
				else
				{
					foreach (Material info in materialArray)
					{
						if (info != null) info.Uniforms[name] = curValue;
						if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
					}
					this.OnPropertySet(ReflectionHelper.Property_Material_Textures, materialArray);
					this.UpdateModifiedState();
				}
			};
		}

		public override void UpdateReadOnlyState()
		{
			base.UpdateReadOnlyState();
			this.AllowDrop = !this.ReadOnly;
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsContentRefs<Material>())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;
			}
		}
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			DataObject dragDropData = e.Data as DataObject;
			if (dragDropData != null && dragDropData.ContainsContentRefs<Material>())
			{
				// Accept drop
				e.Effect = e.AllowedEffect;

				ContentRef<Material>[] ctRefs = dragDropData.GetContentRefs<Material>();
				IEnumerable<object> values = this.Getter();
				IEnumerable<BatchInfo> batchInfos = null;
				IEnumerable<Material> materials = null;
				if (this.EditedType == typeof(BatchInfo))
				{
					batchInfos = values.Cast<BatchInfo>().NotNull();
					if (batchInfos.Any())
					{
						foreach (BatchInfo info in batchInfos) ctRefs[0].Res.Info.CopyTo(info);
						// BatchInfos aren't usually referenced, they're nested. Make sure the change notification is passed on.
						this.Setter(batchInfos);
					}
					else
					{
						this.SetterSingle(ctRefs[0].Res.Info);
					}
				}
				else
				{
					materials = values.Cast<Material>().NotNull();
					foreach (Material info in materials) ctRefs[0].Res.CopyTo(info);

					// If it's a Material, make sure editor events are fired
					EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
						new ObjectSelection(materials),
						ReflectionHelper.Property_Material_MainColor,
						ReflectionHelper.Property_Material_Technique,
						ReflectionHelper.Property_Material_Textures,
						ReflectionHelper.Property_Material_Uniforms);
				}
				this.PerformGetValue();
			}
		}

		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);

			// BatchInfos aren't usually referenced, they're nested. Make sure the change notification is passed on.
			if (this.EditedType == typeof(BatchInfo))
				this.Setter(targets);
			// If it's a Material, make sure editor events are fired
			else
			{
				EditorBasePlugin.Instance.EditorForm.NotifyObjPropChanged(this,
					new ObjectSelection(targets),
					property);
			}
		}
		protected override void OnEditedTypeChanged()
		{
			base.OnEditedTypeChanged();
			if (this.EditedType == typeof(BatchInfo))
			{
				this.Header.ForeColor	= GroupedPropertyEditorHeader.DefaultBackColor;
				this.Header.BackColor	= GroupedPropertyEditorHeader.DefaultMidColor;
				this.Header.Height		= GroupedPropertyEditorHeader.DefaultBigHeight;
			}
			else
			{
				this.Header.ForeColor	= GroupedPropertyEditorHeader.DefaultForeColor;
				this.Header.BackColor	= GroupedPropertyEditorHeader.DefaultBackColor;
				this.Header.Height		= GroupedPropertyEditorHeader.DefaultHeight;
			}
		}
	}
}
