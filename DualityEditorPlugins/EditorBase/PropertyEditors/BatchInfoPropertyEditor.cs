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
			this.Header.AcquireParentColors(true);
			this.Header.Height = GroupedPropertyEditorHeader.DefaultBigHeight;
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
			this.AddEditorForProperty(ReflectionHelper.Property_BatchInfo_MainColor);
			this.AddEditorForProperty(ReflectionHelper.Property_BatchInfo_Technique);
		}
		protected override void OnUpdateFromObjects(object[] values)
		{
			base.OnUpdateFromObjects(values);

			if (values.Any(o => o != null))
			{
				IEnumerable<BatchInfo> batchInfos = null;
				DrawTechnique refTech = null;
				batchInfos = values.Cast<BatchInfo>().NotNull();
				refTech = batchInfos.First().Technique.Res;

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
							foreach (BatchInfo info in batchInfos)
							{
								if (info.Textures == null) info.Textures = new Dictionary<string,ContentRef<Texture>>();
								if (!info.Textures.ContainsKey(varInfo.name))
									info.Textures[varInfo.name] = ContentRef<Texture>.Null;
							}
						}
						// Set other uniform variables
						else
						{
							float[] uniformVal = varInfo.InitDataByType();
							if (uniformVal != null)
							{
								foreach (BatchInfo info in batchInfos)
								{
									if (info.Uniforms == null) info.Uniforms = new Dictionary<string,float[]>();
									if (!info.Uniforms.ContainsKey(varInfo.name))
										info.Uniforms[varInfo.name] = uniformVal;
								}
							}
						}
					}
				}

				// Create editors according to existing variables
				var texDict = batchInfos.First().Textures;
				var uniformDict = batchInfos.First().Uniforms;
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
			return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)o.Textures[name] : null);
		}
		protected Func<IEnumerable<object>> CreateUniformValueGetter(string name)
		{
			return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)o.Uniforms[name] : null);
		}
		protected Action<IEnumerable<object>> CreateTextureValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<ContentRef<Texture>> valuesEnum = values.Cast<ContentRef<Texture>>().GetEnumerator();
				BatchInfo[] batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();

				ContentRef<Texture> curValue = ContentRef<Texture>.Null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (BatchInfo info in batchInfoArray)
				{
					if (info != null) info.Textures[name] = curValue;
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(ReflectionHelper.Property_BatchInfo_Textures, batchInfoArray);
				this.UpdateModifiedState();
			};
		}
		protected Action<IEnumerable<object>> CreateUniformValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<float[]> valuesEnum = values.Cast<float[]>().GetEnumerator();
				BatchInfo[] batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();

				float[] curValue = null;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (BatchInfo info in batchInfoArray)
				{
					if (info != null) info.Uniforms[name] = curValue;
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(ReflectionHelper.Property_BatchInfo_Textures, batchInfoArray);
				this.UpdateModifiedState();
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
				IEnumerable<BatchInfo> batchInfos = values.Cast<BatchInfo>().NotNull();
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
				this.PerformGetValue();
			}
		}

		protected override void OnPropertySet(PropertyInfo property, IEnumerable<object> targets)
		{
			base.OnPropertySet(property, targets);
			// BatchInfos aren't usually referenced, they're nested. Make sure the change notification is passed on.
			this.Setter(targets);
		}
	}
}
