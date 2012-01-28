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
	/// <summary>
	/// Provides Reflection data on Properties and Fields.
	/// </summary>
	public static class ReflectionInfo
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
		public static readonly PropertyInfo	Property_SpriteRenderer_RectMode;

		public static readonly PropertyInfo	Property_AnimSpriteRenderer_IsAnimationRunning;

		public static readonly PropertyInfo	Property_TextRenderer_Text;
		public static readonly PropertyInfo	Property_TextRenderer_Metrics;
		public static readonly PropertyInfo	Property_TextRenderer_CustomMaterial;

		public static readonly PropertyInfo	Property_Camera_SceneOrthoAbs;
		public static readonly PropertyInfo	Property_Camera_SceneViewportAbs;
		public static readonly PropertyInfo	Property_Camera_DrawDevice;
		public static readonly PropertyInfo	Property_Camera_SceneTargetSize;
		public static readonly PropertyInfo	Property_Camera_VisibilityMask;
		public static readonly PropertyInfo	Property_Camera_Passes;
		public static readonly PropertyInfo	Property_Camera_ClearColor;
		public static readonly PropertyInfo	Property_Camera_ParallaxRefDist;

		public static readonly PropertyInfo	Property_Camera_RenderPass_Input;
		public static readonly PropertyInfo	Property_Camera_RenderPass_Output;
		public static readonly PropertyInfo	Property_Camera_RenderPass_VisibilityMask;

		public static readonly PropertyInfo	Property_SoundEmitter_Sources;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Disposed;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Instance;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Sound;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Volume;
		public static readonly PropertyInfo	Property_SoundEmitter_Source_Pitch;
		
		public static readonly PropertyInfo	Property_Collider_Friction;
		public static readonly PropertyInfo	Property_Collider_Restitution;
		public static readonly PropertyInfo	Property_Collider_LinearDamping;
		public static readonly PropertyInfo	Property_Collider_AngularDamping;
		public static readonly PropertyInfo	Property_Collider_Shapes;
		public static readonly PropertyInfo	Property_Collider_BoundRadius;

		public static readonly PropertyInfo	Property_Collider_ShapeInfo_Parent;

		public static readonly PropertyInfo	Property_Resource_Disposed;
		public static readonly PropertyInfo	Property_Resource_Path;
		public static readonly PropertyInfo	Property_Resource_Name;
		public static readonly PropertyInfo	Property_Resource_IsDefaultContent;

		public static readonly PropertyInfo	Property_Scene_GlobalGravity;

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


		static ReflectionInfo()
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
			Property_SpriteRenderer_RectMode		= rendererSprite.GetProperty("RectMode");

			Type rendererAnimSprite = typeof(AnimSpriteRenderer);
			Property_AnimSpriteRenderer_IsAnimationRunning	= rendererAnimSprite.GetProperty("IsAnimationRunning");

			Type rendererText = typeof(TextRenderer);
			Property_TextRenderer_Text				= rendererText.GetProperty("Text");
			Property_TextRenderer_Metrics			= rendererText.GetProperty("Metrics");
			Property_TextRenderer_CustomMaterial	= rendererText.GetProperty("CustomMaterial");

			Type camera = typeof(Camera);
			Property_Camera_SceneOrthoAbs		= camera.GetProperty("SceneOrthoAbs");
			Property_Camera_SceneViewportAbs	= camera.GetProperty("SceneViewportAbs");
			Property_Camera_DrawDevice			= camera.GetProperty("DrawDevice");
			Property_Camera_SceneTargetSize		= camera.GetProperty("SceneTargetSize");
			Property_Camera_VisibilityMask		= camera.GetProperty("VisibilityMask");
			Property_Camera_Passes				= camera.GetProperty("Passes");
			Property_Camera_ParallaxRefDist		= camera.GetProperty("ParallaxRefDist");
			Property_Camera_ClearColor			= camera.GetProperty("ClearColor");

			Type cameraRenderPass = typeof(Camera.Pass);
			Property_Camera_RenderPass_Input			= cameraRenderPass.GetProperty("Input");
			Property_Camera_RenderPass_Output			= cameraRenderPass.GetProperty("Output");
			Property_Camera_RenderPass_VisibilityMask	= cameraRenderPass.GetProperty("VisibilityMask");

			Type collider = typeof(Collider);
			Property_Collider_Friction			= collider.GetProperty("Friction");
			Property_Collider_Restitution		= collider.GetProperty("Restitution");
			Property_Collider_LinearDamping		= collider.GetProperty("LinearDamping");
			Property_Collider_AngularDamping	= collider.GetProperty("AngularDamping");
			Property_Collider_Shapes			= collider.GetProperty("Shapes");
			Property_Collider_BoundRadius		= collider.GetProperty("BoundRadius");

			Type colliderShapeInfo = typeof(Collider.ShapeInfo);
			Property_Collider_ShapeInfo_Parent	= colliderShapeInfo.GetProperty("Parent");

			Type resource = typeof(Resource);
			Property_Resource_Disposed			= resource.GetProperty("Disposed");
			Property_Resource_Path				= resource.GetProperty("Path");
			Property_Resource_Name				= resource.GetProperty("Name");
			Property_Resource_IsDefaultContent	= resource.GetProperty("IsDefaultContent");
			
			Type scene = typeof(Scene);
			Property_Scene_GlobalGravity		= scene.GetProperty("GlobalGravity");

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
	}
}
