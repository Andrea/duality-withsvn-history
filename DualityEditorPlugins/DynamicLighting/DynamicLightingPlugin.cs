using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Resources;

using DualityEditor;
using DualityEditor.EditorRes;
using DualityEditor.CorePluginInterface;

using DynamicLighting.PluginRes;

namespace DynamicLighting
{
	public class DynamicLightingPlugin : EditorPlugin
	{
		public override string Id
		{
			get { return "DynamicLighting"; }
		}

		protected override void LoadPlugin()
		{
			base.LoadPlugin();
			CorePluginHelper.RegisterTypeImage(typeof(LightingTechnique), DynLightRes.IconResLightingTechnique, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(LightingSpriteRenderer), DynLightRes.IconCmpLightingSpriteRenderer, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(LightingAnimSpriteRenderer), DynLightRes.IconCmpLightingSpriteRenderer, CorePluginHelper.ImageContext_Icon);
			CorePluginHelper.RegisterTypeImage(typeof(Light), DynLightRes.IconLight, CorePluginHelper.ImageContext_Icon);

			CorePluginHelper.RegisterTypeCategory(typeof(LightingTechnique), GeneralRes.Category_Graphics, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(LightingSpriteRenderer), GeneralRes.Category_Graphics, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(LightingAnimSpriteRenderer), GeneralRes.Category_Graphics, CorePluginHelper.CategoryContext_General);
			CorePluginHelper.RegisterTypeCategory(typeof(Light), GeneralRes.Category_Graphics, CorePluginHelper.CategoryContext_General);

			CorePluginHelper.RegisterDataConverter<Component>(new LightingRendererFromMaterial());
		}
	}

	public class LightingRendererFromMaterial : DataConverter
	{
		public override int Priority
		{
			get { return CorePluginHelper.Priority_Specialized; }
		}
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.CanPerform<Material>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			List<ContentRef<Material>> dropdata = new List<ContentRef<Material>>();
			var matSelectionQuery = convert.Perform<Material>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Generate objects
			foreach (ContentRef<Material> matRef in dropdata)
			{
				if (convert.IsObjectHandled(matRef.Res)) continue;
				if (!matRef.IsAvailable) continue;

				Material mat = matRef.Res;
				DrawTechnique tech = mat.Technique.Res;
				LightingTechnique lightTech = tech as LightingTechnique;
				if (tech == null) continue;

				bool isDynamicLighting = lightTech != null ||
					tech.PreferredVertexFormat == VertexC1P3T2A4.VertexTypeIndex ||
					tech.PreferredVertexFormat == VertexC1P3T4A4A1.VertexTypeIndex;
				if (!isDynamicLighting) continue;

				Texture mainTex = mat.MainTexture.Res;
				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();

				convert.AddResult(mat.Name); // Leave a name string in the result to pick up for the GameObject constructor
				if (mainTex == null || mainTex.AnimFrames == 0)
				{
					LightingSpriteRenderer sprite = convert.Result.OfType<LightingSpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<LightingSpriteRenderer>();
					if (sprite == null) sprite = new LightingSpriteRenderer();
					sprite.SharedMaterial = matRef;
					if (mainTex != null) sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PxWidth, mainTex.PxHeight);
					convert.AddResult(sprite);
				}
				else
				{
					LightingAnimSpriteRenderer sprite = convert.Result.OfType<LightingAnimSpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<LightingAnimSpriteRenderer>();
					if (sprite == null) sprite = new LightingAnimSpriteRenderer();
					sprite.SharedMaterial = matRef;
					sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PxWidth / mainTex.AnimCols, mainTex.PxHeight / mainTex.AnimRows);
					sprite.AnimDuration = 5.0f;
					sprite.AnimFrameCount = mainTex.AnimFrames;
					convert.AddResult(sprite);
				}

				convert.MarkObjectHandled(matRef.Res);
			}

			return false;
		}
	}
}
