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
using DualityEditor.Controls.PropertyEditors;
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
			this.AddEditorForProperty(ReflectionInfo.Property_BatchInfo_MainColor);
			this.AddEditorForProperty(ReflectionInfo.Property_BatchInfo_Technique);
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
									float[] oldVal;
									if (!info.Uniforms.TryGetValue(varInfo.name, out oldVal) || oldVal == null)
										info.Uniforms[varInfo.name] = uniformVal;
									else if (oldVal.Length != uniformVal.Length)
									{
										for (int i = 0; i < Math.Min(oldVal.Length, uniformVal.Length); i++) uniformVal[i] = oldVal[i];
										info.Uniforms[varInfo.name] = uniformVal;
									}
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
					foreach (var uniform in uniformDict)
					{
						PropertyEditor e = null;

						PropertyEditor oldEditor;
						oldEditors.TryGetValue(uniform.Key, out oldEditor);

						if (uniform.Value.Length == 1)
						{
							if (oldEditor is NumericPropertyEditor)
								oldEditors.Remove(uniform.Key);
							else
							{
								e = new NumericPropertyEditor(this, this.ParentGrid);
								e.EditedType = typeof(float);
								e.Getter = this.CreateUniformFloatValueGetter(uniform.Key);
								e.Setter = !this.ReadOnly ? this.CreateUniformFloatValueSetter(uniform.Key) : null;
								(e as NumericPropertyEditor).Editor.Increment = 0.1m;
							}
						}
						else if (uniform.Value.Length == 2)
						{
							if (oldEditor is Vector2PropertyEditor)
								oldEditors.Remove(uniform.Key);
							else
							{
								e = new Vector2PropertyEditor(this, this.ParentGrid);
								e.EditedType = typeof(OpenTK.Vector2);
								e.Getter = this.CreateUniformVec2ValueGetter(uniform.Key);
								e.Setter = !this.ReadOnly ? this.CreateUniformVec2ValueSetter(uniform.Key) : null;
								(e as Vector2PropertyEditor).EditorX.Increment = 0.1m;
								(e as Vector2PropertyEditor).EditorY.Increment = 0.1m;
							}
						}
						else if (uniform.Value.Length == 3)
						{
							if (oldEditor is Vector3PropertyEditor)
								oldEditors.Remove(uniform.Key);
							else
							{
								e = new Vector3PropertyEditor(this, this.ParentGrid);
								e.EditedType = typeof(OpenTK.Vector3);
								e.Getter = this.CreateUniformVec3ValueGetter(uniform.Key);
								e.Setter = !this.ReadOnly ? this.CreateUniformVec3ValueSetter(uniform.Key) : null;
								(e as Vector3PropertyEditor).EditorX.Increment = 0.1m;
								(e as Vector3PropertyEditor).EditorY.Increment = 0.1m;
								(e as Vector3PropertyEditor).EditorZ.Increment = 0.1m;
							}
						}
						else
						{
							if (oldEditor is IListPropertyEditor)
								oldEditors.Remove(uniform.Key);
							else
							{
								e = new IListPropertyEditor(this, this.ParentGrid);
								e.EditedType = typeof(float[]);
								e.Getter = this.CreateUniformValueGetter(uniform.Key);
								e.Setter = !this.ReadOnly ? this.CreateUniformValueSetter(uniform.Key) : null;
								(e as IListPropertyEditor).EditorAdded += this.UniformList_EditorAdded;
							}
						}

						if (e != null)
						{
							e.PropertyName = uniform.Key;
							this.shaderVarEditors[uniform.Key] = e;
							this.AddPropertyEditor(e);
						}
					}
				}

				// Remove old editors that aren't needed anymore
				foreach (var pair in oldEditors)
				{
					if (this.shaderVarEditors[pair.Key] == pair.Value) this.shaderVarEditors.Remove(pair.Key);
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
		protected Func<IEnumerable<object>> CreateUniformFloatValueGetter(string name)
		{
			return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)o.Uniforms[name][0] : null);
		}
		protected Func<IEnumerable<object>> CreateUniformVec2ValueGetter(string name)
		{
			return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)
				new OpenTK.Vector2(o.Uniforms[name][0], o.Uniforms[name][1])
				: null);
		}
		protected Func<IEnumerable<object>> CreateUniformVec3ValueGetter(string name)
		{
			return () => this.Getter().Cast<BatchInfo>().Select(o => o != null ? (object)
				new OpenTK.Vector3(o.Uniforms[name][0], o.Uniforms[name][1], o.Uniforms[name][2])
				: null);
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
				this.OnPropertySet(ReflectionInfo.Property_BatchInfo_Textures, batchInfoArray);
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
				this.OnPropertySet(ReflectionInfo.Property_BatchInfo_Uniforms, batchInfoArray);
				this.UpdateModifiedState();
			};
		}
		protected Action<IEnumerable<object>> CreateUniformFloatValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<float> valuesEnum = values.Cast<float>().GetEnumerator();
				BatchInfo[] batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();

				float curValue = 0.0f;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (BatchInfo info in batchInfoArray)
				{
					if (info != null) info.Uniforms[name][0] = curValue;
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(ReflectionInfo.Property_BatchInfo_Uniforms, batchInfoArray);
				this.UpdateModifiedState();
			};
		}
		protected Action<IEnumerable<object>> CreateUniformVec2ValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<OpenTK.Vector2> valuesEnum = values.Cast<OpenTK.Vector2>().GetEnumerator();
				BatchInfo[] batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();

				OpenTK.Vector2 curValue = OpenTK.Vector2.Zero;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (BatchInfo info in batchInfoArray)
				{
					if (info != null)
					{
						info.Uniforms[name][0] = curValue.X;
						info.Uniforms[name][1] = curValue.Y;
					}
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(ReflectionInfo.Property_BatchInfo_Uniforms, batchInfoArray);
				this.UpdateModifiedState();
			};
		}
		protected Action<IEnumerable<object>> CreateUniformVec3ValueSetter(string name)
		{
			return delegate(IEnumerable<object> values)
			{
				IEnumerator<OpenTK.Vector3> valuesEnum = values.Cast<OpenTK.Vector3>().GetEnumerator();
				BatchInfo[] batchInfoArray = this.Getter().Cast<BatchInfo>().ToArray();

				OpenTK.Vector3 curValue = OpenTK.Vector3.Zero;
				if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				foreach (BatchInfo info in batchInfoArray)
				{
					if (info != null)
					{
						info.Uniforms[name][0] = curValue.X;
						info.Uniforms[name][1] = curValue.Y;
						info.Uniforms[name][2] = curValue.Z;
					}
					if (valuesEnum.MoveNext()) curValue = valuesEnum.Current;
				}
				this.OnPropertySet(ReflectionInfo.Property_BatchInfo_Uniforms, batchInfoArray);
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

		private void UniformList_EditorAdded(object sender, PropertyEditorEventArgs e)
		{
			NumericPropertyEditor numEdit = e.Editor as NumericPropertyEditor;
			if (numEdit != null) numEdit.Editor.Increment = 0.1m;
		}
	}
}
