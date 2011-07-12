using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.IO;

using Duality.Resources;
using Duality.Components;
using Duality.Components.Renderers;

namespace Duality
{
	public static class ReflectionHelper
	{
		public static readonly PropertyInfo Property_GameObject_Name;
		public static readonly PropertyInfo Property_GameObject_Active;
		public static readonly PropertyInfo Property_GameObject_ActiveSingle;
		public static readonly PropertyInfo Property_GameObject_Parent;
		public static readonly PropertyInfo Property_GameObject_PrefabLink;

		public static readonly PropertyInfo Property_Component_GameObj;
		public static readonly PropertyInfo Property_Component_Active;
		public static readonly PropertyInfo Property_Component_ActiveSingle;
		public static readonly PropertyInfo Property_Component_TypeName;

		public static readonly PropertyInfo	Property_Transform_RelativePos;
		public static readonly PropertyInfo	Property_Transform_RelativeAngle;
		public static readonly PropertyInfo	Property_Transform_RelativeScale;
		public static readonly PropertyInfo	Property_Transform_RelativeVel;
		public static readonly PropertyInfo	Property_Transform_RelativeAngleVel;
		public static readonly PropertyInfo	Property_Transform_DeriveAngle;

		public static readonly PropertyInfo	Property_Renderer_VisibilityGroup;

		public static readonly PropertyInfo	Property_SpriteRenderer_BoundRadius;
		public static readonly PropertyInfo	Property_SpriteRenderer_CustomMaterial;
		public static readonly PropertyInfo	Property_SpriteRenderer_Rect;

		public static readonly PropertyInfo	Property_AnimSpriteRenderer_IsAnimationRunning;

		public static readonly PropertyInfo	Property_TextRenderer_Text;
		public static readonly PropertyInfo	Property_TextRenderer_Metrics;

		public static readonly PropertyInfo	Property_Camera_SceneOrthoAbs;
		public static readonly PropertyInfo	Property_Camera_SceneViewportAbs;
		public static readonly PropertyInfo	Property_Camera_DrawDevice;
		public static readonly PropertyInfo	Property_Camera_SceneTargetSize;
		public static readonly PropertyInfo	Property_Camera_VisibilityMask;
		public static readonly PropertyInfo	Property_Camera_Passes;
		public static readonly PropertyInfo	Property_Camera_ClearColor;
		public static readonly PropertyInfo	Property_Camera_ParallaxRefDist;

		public static readonly PropertyInfo	Property_SoundEmitter_Sources;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Disposed;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Instance;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Sound;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Volume;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Pitch;
		
		public static readonly PropertyInfo	Property_Resource_Disposed;
		public static readonly PropertyInfo	Property_Resource_Path;

		public static readonly PropertyInfo	Property_DrawTechnique_Blending;

		public static readonly PropertyInfo	Property_ShaderProgram_Compiled;
		public static readonly PropertyInfo	Property_ShaderProgram_VarInfo;
		public static readonly PropertyInfo	Property_ShaderProgram_Vertex;
		public static readonly PropertyInfo	Property_ShaderProgram_Fragment;

		public static readonly PropertyInfo	Property_Pixmap_PixelData;
		public static readonly PropertyInfo	Property_Pixmap_PixelDataBasePath;

		public static readonly PropertyInfo	Property_Texture_PxWidth;
		public static readonly PropertyInfo	Property_Texture_PxHeight;
		public static readonly PropertyInfo	Property_Texture_PxDiameter;
		public static readonly PropertyInfo	Property_Texture_OglWidth;
		public static readonly PropertyInfo	Property_Texture_OglHeight;
		public static readonly PropertyInfo	Property_Texture_UVRatio;
		public static readonly PropertyInfo	Property_Texture_Mipmaps;
		public static readonly PropertyInfo	Property_Texture_NeedsReload;
		public static readonly PropertyInfo	Property_Texture_AnimCols;
		public static readonly PropertyInfo	Property_Texture_AnimRows;
		public static readonly PropertyInfo	Property_Texture_AnimFrames;
		public static readonly PropertyInfo	Property_Texture_Atlas;

		public static readonly PropertyInfo	Property_RenderTarget_Targets;

		public static readonly PropertyInfo	Property_BatchInfo_Technique;
		public static readonly PropertyInfo	Property_BatchInfo_MainColor;
		public static readonly PropertyInfo	Property_BatchInfo_Textures;
		public static readonly PropertyInfo	Property_BatchInfo_Uniforms;
		
		public static readonly PropertyInfo Property_Material_Info;
		public static readonly PropertyInfo	Property_Material_Technique;
		public static readonly PropertyInfo	Property_Material_MainColor;
		public static readonly PropertyInfo	Property_Material_Textures;
		public static readonly PropertyInfo	Property_Material_Uniforms;

		public static readonly PropertyInfo	Property_Sound_AlBuffer;
		public static readonly PropertyInfo	Property_Sound_MinDist;
		public static readonly PropertyInfo	Property_Sound_MinDistFactor;
		public static readonly PropertyInfo	Property_Sound_MaxDist;
		public static readonly PropertyInfo	Property_Sound_MaxDistFactor;

		public static readonly PropertyInfo	Property_Font_NeedsReload;
		public static readonly PropertyInfo	Property_Font_Material;
		public static readonly PropertyInfo	Property_Font_CustomFamilyData;
		public static readonly PropertyInfo	Property_Font_Family;
		public static readonly PropertyInfo	Property_Font_Size;

		public static readonly PropertyInfo	Property_FormattedText_Elements;
		public static readonly PropertyInfo	Property_FormattedText_DisplayedText;
		public static readonly PropertyInfo	Property_FormattedText_Icons;
		public static readonly PropertyInfo	Property_FormattedText_FlowAreas;
		public static readonly PropertyInfo	Property_FormattedText_Fonts;


		public static readonly FieldInfo Field_GameObject_Name;
		public static readonly FieldInfo Field_GameObject_PrefabLink;

		public static readonly FieldInfo Field_Transform_Pos;
		public static readonly FieldInfo Field_Transform_Angle;
		public static readonly FieldInfo Field_Transform_Scale;

		public static readonly FieldInfo Field_Material_Info;


		static ReflectionHelper()
		{
			// Retrieve PropertyInfo data
			Type gameobject = typeof(GameObject);
			Property_GameObject_Name			= gameobject.GetProperty("Name");
			Property_GameObject_Active			= gameobject.GetProperty("Active");
			Property_GameObject_ActiveSingle	= gameobject.GetProperty("ActiveSingle");
			Property_GameObject_Parent			= gameobject.GetProperty("Parent");
			Property_GameObject_PrefabLink		= gameobject.GetProperty("PrefabLink");

			Type component = typeof(Component);
			Property_Component_GameObj		= component.GetProperty("GameObj");
			Property_Component_Active		= component.GetProperty("Active");
			Property_Component_ActiveSingle	= component.GetProperty("ActiveSingle");
			Property_Component_TypeName		= component.GetProperty("TypeName");

			Type transform = typeof(Transform);
			Property_Transform_RelativePos		= transform.GetProperty("RelativePos");
			Property_Transform_RelativeAngle	= transform.GetProperty("RelativeAngle");
			Property_Transform_RelativeScale	= transform.GetProperty("RelativeScale");
			Property_Transform_RelativeVel		= transform.GetProperty("RelativeVel");
			Property_Transform_RelativeAngleVel	= transform.GetProperty("RelativeAngleVel");
			Property_Transform_DeriveAngle		= transform.GetProperty("DeriveAngle");

			Type renderer = typeof(Renderer);
			Property_Renderer_VisibilityGroup	= renderer.GetProperty("VisibilityGroup");
			
			Type rendererSprite = typeof(SpriteRenderer);
			Property_SpriteRenderer_BoundRadius		= rendererSprite.GetProperty("BoundRadius");
			Property_SpriteRenderer_CustomMaterial	= rendererSprite.GetProperty("CustomMaterial");
			Property_SpriteRenderer_Rect			= rendererSprite.GetProperty("Rect");

			Type rendererAnimSprite = typeof(AnimSpriteRenderer);
			Property_AnimSpriteRenderer_IsAnimationRunning	= rendererAnimSprite.GetProperty("IsAnimationRunning");

			Type rendererText = typeof(TextRenderer);
			Property_TextRenderer_Text		= rendererText.GetProperty("Text");
			Property_TextRenderer_Metrics	= rendererText.GetProperty("Metrics");

			Type camera = typeof(Camera);
			Property_Camera_SceneOrthoAbs		= camera.GetProperty("SceneOrthoAbs");
			Property_Camera_SceneViewportAbs	= camera.GetProperty("SceneViewportAbs");
			Property_Camera_DrawDevice			= camera.GetProperty("DrawDevice");
			Property_Camera_SceneTargetSize		= camera.GetProperty("SceneTargetSize");
			Property_Camera_VisibilityMask		= camera.GetProperty("VisibilityMask");
			Property_Camera_Passes				= camera.GetProperty("Passes");
			Property_Camera_ParallaxRefDist		= camera.GetProperty("ParallaxRefDist");
			Property_Camera_ClearColor			= camera.GetProperty("ClearColor");
			
			Type resource = typeof(Resource);
			Property_Resource_Disposed	= resource.GetProperty("Disposed");
			Property_Resource_Path		= resource.GetProperty("Path");
			
			Type drawTech = typeof(DrawTechnique);
			Property_DrawTechnique_Blending		= drawTech.GetProperty("Blending");
			
			Type shaderProgram = typeof(ShaderProgram);
			Property_ShaderProgram_Compiled		= shaderProgram.GetProperty("Compiled");
			Property_ShaderProgram_VarInfo		= shaderProgram.GetProperty("VarInfo");
			Property_ShaderProgram_Vertex		= shaderProgram.GetProperty("Vertex");
			Property_ShaderProgram_Fragment		= shaderProgram.GetProperty("Fragment");

			Type pixmap = typeof(Pixmap);
			Property_Pixmap_PixelData			= pixmap.GetProperty("PixelData");
			Property_Pixmap_PixelDataBasePath	= pixmap.GetProperty("PixelDataBasePath");

			Type texture = typeof(Texture);
			Property_Texture_PxWidth		= texture.GetProperty("PxWidth");
			Property_Texture_PxHeight		= texture.GetProperty("PxHeight");
			Property_Texture_PxDiameter		= texture.GetProperty("PxDiameter");
			Property_Texture_OglWidth		= texture.GetProperty("OglWidth");
			Property_Texture_OglHeight		= texture.GetProperty("OglHeight");
			Property_Texture_UVRatio		= texture.GetProperty("UVRatio");
			Property_Texture_Mipmaps		= texture.GetProperty("Mipmaps");
			Property_Texture_NeedsReload	= texture.GetProperty("NeedsReload");
			Property_Texture_AnimCols		= texture.GetProperty("AnimCols");
			Property_Texture_AnimRows		= texture.GetProperty("AnimRows");
			Property_Texture_AnimFrames		= texture.GetProperty("AnimFrames");
			Property_Texture_Atlas			= texture.GetProperty("Atlas");

			Type renderTarget = typeof(RenderTarget);
			Property_RenderTarget_Targets	= renderTarget.GetProperty("Targets");

			Type batchInfo = typeof(BatchInfo);
			Property_BatchInfo_Technique	= batchInfo.GetProperty("Technique");
			Property_BatchInfo_MainColor	= batchInfo.GetProperty("MainColor");
			Property_BatchInfo_Textures		= batchInfo.GetProperty("Textures");
			Property_BatchInfo_Uniforms		= batchInfo.GetProperty("Uniforms");
			
			Type material = typeof(Material);
			Property_Material_Info		= material.GetProperty("Info");
			Property_Material_Technique	= material.GetProperty("Technique");
			Property_Material_MainColor	= material.GetProperty("MainColor");
			Property_Material_Textures	= material.GetProperty("Textures");
			Property_Material_Uniforms	= material.GetProperty("Uniforms");

			Type sound = typeof(Sound);
			Property_Sound_AlBuffer			= sound.GetProperty("AlBuffer");
			Property_Sound_MinDist			= sound.GetProperty("MinDist");
			Property_Sound_MinDistFactor	= sound.GetProperty("MinDistFactor");
			Property_Sound_MaxDist			= sound.GetProperty("MaxDist");
			Property_Sound_MaxDistFactor	= sound.GetProperty("MaxDistFactor");

			Type font = typeof(Font);
			Property_Font_NeedsReload		= font.GetProperty("NeedsReload");
			Property_Font_Material			= font.GetProperty("Material");
			Property_Font_Family			= font.GetProperty("Family");
			Property_Font_CustomFamilyData	= font.GetProperty("CustomFamilyData");
			Property_Font_Size				= font.GetProperty("Size");

			Type formattedText = typeof(FormattedText);
			Property_FormattedText_DisplayedText	= formattedText.GetProperty("DisplayedText");
			Property_FormattedText_Elements			= formattedText.GetProperty("Elements");
			Property_FormattedText_Icons			= formattedText.GetProperty("Icons");
			Property_FormattedText_FlowAreas		= formattedText.GetProperty("FlowAreas");
			Property_FormattedText_Fonts			= formattedText.GetProperty("Fonts");

			Type soundEmitter = typeof(SoundEmitter);
			Property_SoundEmitter_Sources	= soundEmitter.GetProperty("Sources");

			Type soundEmitterSource = typeof(SoundEmitter.Source);
			Property_SoundEmitter_Source_Disposed	= soundEmitterSource.GetProperty("Disposed");
			Property_SoundEmitter_Source_Instance	= soundEmitterSource.GetProperty("Instance");
			Property_SoundEmitter_Source_Sound		= soundEmitterSource.GetProperty("Sound");
			Property_SoundEmitter_Source_Volume		= soundEmitterSource.GetProperty("Volume");
			Property_SoundEmitter_Source_Pitch		= soundEmitterSource.GetProperty("Pitch");

			// Retrieve FieldInfo data
			BindingFlags fieldFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
			Field_GameObject_Name		= gameobject.GetField("name", fieldFlags);
			Field_GameObject_PrefabLink	= gameobject.GetField("prefabLink", fieldFlags);

			Field_Transform_Pos		= transform.GetField("pos", fieldFlags);
			Field_Transform_Angle	= transform.GetField("angle", fieldFlags);
			Field_Transform_Scale	= transform.GetField("scale", fieldFlags);

			Field_Material_Info	= material.GetField("info", fieldFlags);
		}


		public static Stream GetEmbeddedResourceStream(Assembly asm, string fileName)
		{
			if (String.IsNullOrEmpty(fileName)) return null;
			string resName = asm.GetName().Name + '.' + fileName.
				Replace(Path.DirectorySeparatorChar, '.').
				Replace(Path.AltDirectorySeparatorChar, '.').
				Trim('.');

			string[] names = asm.GetManifestResourceNames();
			ManifestResourceInfo info = asm.GetManifestResourceInfo(resName);
			if (info == null) return null;

			return asm.GetManifestResourceStream(resName);
		}

		public static object CreateInstanceOf(Type instanceType)
		{
			try
			{
				if (instanceType == typeof(string))
					return "";
				else if (typeof(Array).IsAssignableFrom(instanceType))
					return Array.CreateInstance(instanceType.GetElementType(), 0);
				else
					return Activator.CreateInstance(instanceType, true);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static bool MemberInfoEquals(MemberInfo lhs, MemberInfo rhs)
		{
			if (lhs == rhs)
				return true;
 
			if (lhs.DeclaringType != rhs.DeclaringType)
				return false;
 
			// Methods on arrays do not have metadata tokens but their ReflectedType
			// always equals their DeclaringType
			if (lhs.DeclaringType != null && lhs.DeclaringType.IsArray)
				return false;
 
			if (lhs.MetadataToken != rhs.MetadataToken || lhs.Module != rhs.Module)
				return false;
 
			if (lhs is MethodInfo)
			{
				MethodInfo lhsMethod = lhs as MethodInfo;
 
				if (lhsMethod.IsGenericMethod)
				{
					MethodInfo rhsMethod = rhs as MethodInfo;
 
					Type[] lhsGenArgs = lhsMethod.GetGenericArguments();
					Type[] rhsGenArgs = rhsMethod.GetGenericArguments();
					for (int i = 0; i < rhsGenArgs.Length; i++)
					{
						if (lhsGenArgs[i] != rhsGenArgs[i])
							return false;
					}
				}
			}
			return true;
		}
		public static int GetTypeHierarchyLevel(Type t)
		{
			int level = 0;
			while (t.BaseType != null) { t = t.BaseType; level++; }
			return level;
		}

		/// <summary>
		/// A configuration enum for GetTypeString
		/// </summary>
		public enum TypeStringAttrib
		{
			/// <summary>
			/// The method will return a type keyword, its "short" name. Just the types "base", no generic
			/// type parameters or array specifications.
			/// </summary>
			Keyword,
			/// <summary>
			/// The types full name, but without generic arguments, element types or assembly names
			/// </summary>
			LongName,
			/// <summary>
			/// A type name / definition as you would see it in normal C# code. Complete with generic parameters
			/// or possible array specifications.
			/// </summary>
			CSCodeIdent,
			/// <summary>
			/// As CSCodeIdent, but shortened to Keywords
			/// </summary>
			CSCodeIdentShort
		}
		/// <summary>
		/// Returns a string describing a certain Type.
		/// </summary>
		/// <param name="T">The Type to describe</param>
		/// <param name="attrib">How to describe the Type</param>
		/// <returns></returns>
		public static string GetTypeString(Type T, TypeStringAttrib attrib)
		{
			if (attrib == TypeStringAttrib.LongName)
			{
				return T.FullName.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			else if (attrib == TypeStringAttrib.Keyword)
			{
				return T.Name.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries)[0].Replace('+', '.');
			}
			else if (attrib == TypeStringAttrib.CSCodeIdent || attrib == TypeStringAttrib.CSCodeIdentShort)
			{
				StringBuilder typeStr = new StringBuilder();

				if (T.IsArray)
				{
					typeStr.Append(GetTypeString(T.GetElementType(), attrib));
					typeStr.Append('[');
					typeStr.Append(',', T.GetArrayRank() - 1);
					typeStr.Append(']');
				}
				else
				{
					typeStr.Append(GetTypeString(T, attrib == TypeStringAttrib.CSCodeIdentShort ? TypeStringAttrib.Keyword : TypeStringAttrib.LongName));
					
					if (T.IsGenericTypeDefinition)
					{
						typeStr.Append('<');
						Type[] genArg = T.GetGenericArguments();
						typeStr.Append(',', genArg.Length - 1);
						typeStr.Append('>');
					}
					else if (T.IsGenericType)
					{
						typeStr.Append('<');
						Type[] genArg = T.GetGenericArguments();
						for (int i = 0; i < genArg.Length; i++)
						{
							typeStr.Append(GetTypeString(genArg[i], attrib));
							if (i < genArg.Length - 1)
								typeStr.Append(',');
						}
						typeStr.Append('>');
					}
				}

				return typeStr.Replace('+', '.').ToString();
			}
			return null;
		}
		/// <summary>
		/// Converts a full name type string from CS code style to GetType compatible. Warning:
		/// As GetType only searches in a specific assembly, type identifiers combined out of types from
		/// different Assemblies won't be found.
		/// </summary>
		/// <param name="csCodeType"></param>
		/// <returns></returns>
		public static string ConvertTypeCodeToGetType(string csCodeType)
		{
			if (csCodeType.IndexOfAny(new char[]{'<','>'}) != -1)
			{
				int first = csCodeType.IndexOf('<');
				int last = 0;
				while (csCodeType.IndexOf('>', last + 1) > last)
				{
					last = csCodeType.IndexOf('>', last + 1);
				}
				string[] tokenTemp = new string[3];
				tokenTemp[0] = csCodeType.Substring(0, first);
				tokenTemp[1] = csCodeType.Substring(first + 1, last - (first + 1));
				tokenTemp[2] = csCodeType.Substring(last + 1, csCodeType.Length - (last + 1));
				string[] tokenTemp2 = tokenTemp[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < tokenTemp2.Length; i++)
				{
					tokenTemp2[i] = ConvertTypeCodeToGetType(tokenTemp2[i]);
				}
				return tokenTemp[0] + '`' + tokenTemp2.Length.ToString() + '[' + String.Join(", ", tokenTemp2) + ']' + ((tokenTemp.Length > 2) ? tokenTemp[2] : "");
			}
			else
				return csCodeType;
		}
		/// <summary>
		/// Searches a specific type (specified as would be valid in C# code) in an array of Assemblies.
		/// Generates the type if neccessary (generic). Also supports generic types combined using types 
		/// from different Assemblies.
		/// </summary>
		/// <param name="csCodeType"></param>
		/// <param name="asmSearch"></param>
		/// <returns></returns>
		public static Type FindType(string csCodeType, Assembly[] asmSearch)
		{
			Type result = null;
			csCodeType = csCodeType.Trim();
			
			bool		array	= (csCodeType.IndexOfAny(new char[]{'[',']'}) != -1);
			string[]	arrRank	= null;
			if (array)
			{
				int first = 0;
				while (csCodeType.IndexOf('[', first + 1) > first)
				{
					first = csCodeType.IndexOf('[', first + 1);
				}
				int last = 0;
				while (csCodeType.IndexOf(']', last + 1) > last)
				{
					last = csCodeType.IndexOf(']', last + 1);
				}

				if (csCodeType.IndexOf('>', first + 1) == -1)
				{
					arrRank		= csCodeType.Substring(first + 1, last - (first + 1)).Split(new char[] {','}, StringSplitOptions.None);
					csCodeType	= csCodeType.TrimEnd('[', ']');
				}
				else
				{
					array	= false;
					arrRank	= null;
				}
			}

			if (csCodeType.IndexOfAny(new char[]{'<','>'}) != -1)
			{
				int first = csCodeType.IndexOf('<');
				int last = 0;
				while (csCodeType.IndexOf('>', last + 1) > last)
				{
					last = csCodeType.IndexOf('>', last + 1);
				}
				string[] tokenTemp = new string[3];
				tokenTemp[0] = csCodeType.Substring(0, first);
				tokenTemp[1] = csCodeType.Substring(first + 1, last - (first + 1));
				tokenTemp[2] = csCodeType.Substring(last + 1, csCodeType.Length - (last + 1));
				string[] tokenTemp2 = tokenTemp[1].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
				
				Type[]	types		= new Type[tokenTemp2.Length];
				Type	mainType	= FindType(tokenTemp[0] + '`' + tokenTemp2.Length, asmSearch);;
				for (int i = 0; i < tokenTemp2.Length; i++)
				{
					types[i] = FindType(tokenTemp2[i], asmSearch);
				}

				result = mainType.MakeGenericType(types);
			}
			else
			{
				Type[]	asmTypes;
				string	nameTemp;
				for (int i = 0; i < asmSearch.Length; i++)
				{
					asmTypes = asmSearch[i].GetTypes();
					for (int j = 0; j < asmTypes.Length; j++)
					{
						nameTemp = asmTypes[j].FullName;
						if (csCodeType == nameTemp || 
							csCodeType == nameTemp.Replace('+', '.'))
						{
							result = asmTypes[j];
							break;
						}
					}
					if (result != null) break;
				}
			}

			if (result == null)
				return null;
			else if (array && arrRank.Length != 1)
				return result.MakeArrayType(arrRank.Length);
			else if (array)
				return result.MakeArrayType();
			else
				return result;
		}
	}

	public static class SerializationHelper
	{
		/// <summary>
		/// Returns whether the specified type may just be assigned in a clone operation (even if deep)
		/// instead of being investigated further.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static bool IsSafeAssignType(Type t)
		{
			return t.IsPrimitive || t.IsEnum || t == typeof(string) || typeof(MemberInfo).IsAssignableFrom(t) || typeof(IContentRef).IsAssignableFrom(t);
		}

		public static T DeepCloneObject<T>(T instance)
		{
			return (T)DeepCloneObject(instance, new VisitedGraph());
		}
		public static object DeepCloneObject(object instance)
		{
			return DeepCloneObject(instance, new VisitedGraph());
		}
		public static T DeepCloneObjectExplicit<T>(T instance, params Type[] unwrapTypes)
		{
			return (T)DeepCloneObjectExplicit(instance, new VisitedGraph(), unwrapTypes);
		}
		public static object DeepCloneObjectExplicit(object instance, params Type[] unwrapTypes)
		{
			return DeepCloneObjectExplicit(instance, new VisitedGraph(), unwrapTypes);
		}

		public static void DeepCopyFields(FieldInfo[] fields, object source, object target)
		{
			DeepCopyFields(fields, source, target, new VisitedGraph());
		}
		public static void DeepCopyFieldsExplicit(FieldInfo[] fields, object source, object target, params Type[] unwrapTypes)
		{
			DeepCopyFieldsExplicit(fields, source, target, new VisitedGraph(), unwrapTypes);
		}

		/// <summary>
		/// Resets all references of object types assignable to any of the specified. typeof(Component) will
		/// result in all references to any kind of Component to be cleared / set null.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="resetTypes"></param>
		public static void DeepResetReferences(object instance, params Type[] resetTypes)
		{
			DeepResetReferenceFields(
				instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
				instance, new HashSet<object>(), resetTypes);
		}
		public static void DeepResetReferenceFields(FieldInfo[] fields, object source, params Type[] resetTypes)
		{
			DeepResetReferenceFields(fields, source, new HashSet<object>(), resetTypes);
		}

		/// <summary>
		/// Re-resolves all MemberInfo references using current Type information including Plugin data. When reloading
		/// Plugins, calling this method for an object will re-map its previously reflected MemberInfo references to
		/// the newly loaded plugin Assemblies equivalent
		/// </summary>
		/// <param name="instance"></param>
		public static void DeepResolveTypeReferences(object instance, SerializationBinder binder)
		{
			DeepResolveTypeReferenceFields(
				instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
				instance, binder, new HashSet<object>());
		}
		public static void DeepResolveTypeReferenceFields(FieldInfo[] fields, object source, SerializationBinder binder)
		{
			DeepResolveTypeReferenceFields(fields, source, binder, new HashSet<object>());
		}


		#region Private Methods
		private class VisitedGraph : Dictionary<object, object>
		{
			public new bool ContainsKey(object key)
			{
				if (key == null)
					return true;
				return base.ContainsKey(key);
			}
			public new object this[object key]
			{
				get { if (key == null) return null; return base[key]; }
			}
		}

		private static object DeepCloneObject(object instance, VisitedGraph visited)
		{
			if (instance == null) return null;
			if (visited.ContainsKey(instance)) return visited[instance];
			Type instanceType = instance.GetType();

			// Primitive types or anything else we don't want to clone
			if (IsSafeAssignType(instanceType))
			{
				return instance;
			}
			// Arrays
			else if (instanceType.IsArray)
			{
				int length = ((Array)instance).Length;
				Array copy = (Array)Activator.CreateInstance(instanceType, length);
				visited.Add(instance, copy);

				for (int i = 0; i < length; ++i)
					copy.SetValue(DeepCloneObject(((Array)instance).GetValue(i), visited), i);

				return copy;
			}
			// Reference types / complex objects
			else
			{
				object copy = ReflectionHelper.CreateInstanceOf(instanceType);
				if (instanceType.IsClass) visited.Add(instance, copy);

				DeepCopyFields(
					instanceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
					instance, copy, visited);

				return copy;
			}
		}
		private static object DeepCloneObjectExplicit(object instance, VisitedGraph visited, Type[] unwrapTypes)
		{
			if (instance == null) return null;
			if (visited.ContainsKey(instance)) return visited[instance];
			Type instanceType = instance.GetType();

			// Primitive types or anything else we don't want to clone
			if (IsSafeAssignType(instanceType))
			{
				return instance;
			}
			// Arrays
			else if (instanceType.IsArray)
			{
				Array src = (Array)instance;
				Array copy = (Array)Activator.CreateInstance(instanceType, src.Length);
				Type elemType = instanceType.GetElementType();
				visited.Add(instance, copy);

				bool unwrap = elemType.IsValueType && !IsSafeAssignType(elemType);
				if (!unwrap)
				{
					for (int i = 0; i < unwrapTypes.Length; i++)
					{
						if (unwrapTypes[i].IsAssignableFrom(elemType))
						{
							unwrap = true;
							break;
						}
					}
				}

				if (unwrap)
				{
					for (int i = 0; i < src.Length; ++i)
						copy.SetValue(DeepCloneObjectExplicit(((Array)instance).GetValue(i), visited, unwrapTypes), i);
				}
				else
				{
					src.CopyTo(copy, 0);
				}

				return copy;
			}
			// Reference types / complex objects
			else
			{
				object copy = ReflectionHelper.CreateInstanceOf(instanceType);
				if (instanceType.IsClass) visited.Add(instance, copy);

				DeepCopyFieldsExplicit(
					instanceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
					instance, copy, visited, unwrapTypes);

				return copy;
			}
		}

		private static void DeepCopyFields(FieldInfo[] fields, object source, object target, VisitedGraph visited)
		{
			foreach (FieldInfo field in fields)
			{
				field.SetValue(target, DeepCloneObject(field.GetValue(source), visited));
			}
		}
		private static void DeepCopyFieldsExplicit(FieldInfo[] fields, object source, object target, VisitedGraph visited, Type[] unwrapTypes)
		{
			foreach (FieldInfo f in fields)
			{
				bool unwrap = f.FieldType.IsValueType && !IsSafeAssignType(f.FieldType);
				if (!unwrap)
				{
					for (int i = 0; i < unwrapTypes.Length; i++)
					{
						if (unwrapTypes[i].IsAssignableFrom(f.FieldType))
						{
							unwrap = true;
							break;
						}
					}
				}

				if (unwrap)
					f.SetValue(target, DeepCloneObjectExplicit(f.GetValue(source), visited, unwrapTypes));
				else
					f.SetValue(target, f.GetValue(source));
			}
		} 
		
		private static object DeepResetReferenceObject(object instance, HashSet<object> visited, Type[] resetTypes)
		{
			if (instance == null) return null;
			if (visited.Contains(instance)) return instance;
			Type instanceType = instance.GetType();

			// Primitive types or anything else that isn't a reference object we want to reset
			if (IsSafeAssignType(instanceType))
			{
				return instance;
			}
			// Reset check
			else if (!instanceType.IsValueType)
			{
				for (int i = 0; i < resetTypes.Length; i++)
				{
					if (resetTypes[i].IsAssignableFrom(instanceType))
					{
						return null;
					}
				}
			}

			// Arrays
			if (instanceType.IsArray)
			{
				Array src = (Array)instance;
				Type elemType = instanceType.GetElementType();
				visited.Add(instance);

				if (!IsSafeAssignType(elemType))
				{
					for (int i = 0; i < src.Length; ++i)
						src.SetValue(DeepResetReferenceObject(((Array)instance).GetValue(i), visited, resetTypes), i);
				}

				return instance;
			}
			// Reference types / complex objects
			else
			{
				if (instanceType.IsClass) visited.Add(instance);
				DeepResetReferenceFields(
					instanceType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
					instance, visited, resetTypes);
				return instance;
			}
		}
		private static void DeepResetReferenceFields(FieldInfo[] fields, object source, HashSet<object> visited, Type[] resetTypes)
		{
			foreach (FieldInfo f in fields)
			{
				f.SetValue(source, DeepResetReferenceObject(f.GetValue(source), visited, resetTypes));
			}
		} 

		private static object DeepResolveTypeReferenceObject(object instance, SerializationBinder binder, HashSet<object> visited)
		{
			if (instance == null) return null;
			if (visited.Contains(instance)) return instance;
			Type instanceType = instance.GetType();

			// Check for Reflection Referecnes such as Type, FieldInfo, etc.
			if (typeof(MemberInfo).IsAssignableFrom(instanceType))
			{
				// Re-resolve it
				MemberInfo info = instance as MemberInfo;
				if (info is Type)
				{
					Type infoType = info as Type;
					return binder.BindToType(infoType.Assembly.FullName, infoType.FullName);
				}
				else
				{
					BindingFlags bindFlagsAll = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
					Type infoDeclarerType = binder.BindToType(info.DeclaringType.Assembly.FullName, info.DeclaringType.FullName);
					if (info is FieldInfo)
					{
						return infoDeclarerType.GetField(info.Name, bindFlagsAll);
					}
					else if (info is PropertyInfo)
					{
						return infoDeclarerType.GetProperty(info.Name, bindFlagsAll);
					}
					else if (info is EventInfo)
					{
						return infoDeclarerType.GetEvent(info.Name, bindFlagsAll);
					}
					else if (info is MethodBase)
					{
						MethodBase infoMethodBase = info as MethodBase;
						ParameterInfo[] parameters = infoMethodBase.GetParameters();
						Type[] paramTypes = new Type[parameters.Length];
						for (int i = 0; i < parameters.Length; i++)
						{
							paramTypes[i] = parameters[i].ParameterType;
						}
						if (info is MethodInfo)
						{
							return infoDeclarerType.GetMethod(info.Name, bindFlagsAll, Type.DefaultBinder, infoMethodBase.CallingConvention, paramTypes, null);
						}
						else if (info is ConstructorInfo)
						{
							return infoDeclarerType.GetConstructor(bindFlagsAll, Type.DefaultBinder, infoMethodBase.CallingConvention, paramTypes, null);
						}
					}
				}
				Log.Core.WriteWarning("Could not resolve MemberInfo reference of type '{0}': Unknown type.", info.GetType().FullName);
				return null;
			}
			else if (IsSafeAssignType(instanceType))
			{
				return instance;
			}

			// Arrays
			if (instanceType.IsArray)
			{
				Array src = (Array)instance;
				Type elemType = instanceType.GetElementType();
				visited.Add(instance);

				if (!IsSafeAssignType(elemType) || typeof(MemberInfo).IsAssignableFrom(elemType))
				{
					for (int i = 0; i < src.Length; ++i)
						src.SetValue(DeepResolveTypeReferenceObject(((Array)instance).GetValue(i), binder, visited), i);
				}

				return instance;
			}
			// Special case: Dictionary<T,U> with T == type reference type: Need to rebuild due to GetHashCode-Stuff
			else if (
				instanceType.IsGenericType && 
				instanceType.GetGenericTypeDefinition() == typeof(Dictionary<,>) && 
				typeof(MemberInfo).IsAssignableFrom(instanceType.GetGenericArguments()[0]))
			{
				visited.Add(instance);

				IDictionary dict = instance as IDictionary;
				List<DictionaryEntry> entries = new List<DictionaryEntry>(dict.Count);
				foreach (DictionaryEntry pair in dict)
					entries.Add(pair);
				
				MethodInfo m_Clear = instanceType.GetMethod("Clear");
				MethodInfo m_Add = instanceType.GetMethod("Add");

				m_Clear.Invoke(instance, null);
				foreach (DictionaryEntry pair in entries)
					m_Add.Invoke(instance, new object[] { DeepResolveTypeReferenceObject(pair.Key, binder, visited), pair.Value });

				return instance;
			}
			// Special case: HashSet<T> with T == type reference type: Need to rebuild due to GetHashCode-Stuff
			else if (
				instanceType.IsGenericType && 
				instanceType.GetGenericTypeDefinition() == typeof(HashSet<>) && 
				typeof(MemberInfo).IsAssignableFrom(instanceType.GetGenericArguments()[0]))
			{
				visited.Add(instance);

				IEnumerable set = instance as IEnumerable;
				List<object> entries = new List<object>();
				foreach (object obj in set)
					entries.Add(obj);
				
				MethodInfo m_Clear = instanceType.GetMethod("Clear");
				MethodInfo m_Add = instanceType.GetMethod("Add");

				m_Clear.Invoke(instance, null);
				foreach (DictionaryEntry pair in entries)
					m_Add.Invoke(instance, new object[] { DeepResolveTypeReferenceObject(pair.Key, binder, visited) });

				return instance;
			}
			// Reference types / complex objects
			else
			{
				if (instanceType.IsClass) visited.Add(instance);
				DeepResolveTypeReferenceFields(
					instanceType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), 
					instance, binder, visited);
				return instance;
			}
		}
		private static void DeepResolveTypeReferenceFields(FieldInfo[] fields, object source, SerializationBinder binder, HashSet<object> visited)
		{
			foreach (FieldInfo f in fields)
			{
				f.SetValue(source, DeepResolveTypeReferenceObject(f.GetValue(source), binder, visited));
			}
		} 
		#endregion
	}
}
