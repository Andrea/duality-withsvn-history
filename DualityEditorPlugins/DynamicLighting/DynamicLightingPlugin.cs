﻿using System.Collections.Generic;
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
			CorePluginRegistry.RegisterTypeImage(typeof(LightingTechnique),				DynLightResCache.IconResLightingTechnique);
			CorePluginRegistry.RegisterTypeImage(typeof(LightingSpriteRenderer),		DynLightResCache.IconCmpLightingSpriteRenderer);
			CorePluginRegistry.RegisterTypeImage(typeof(LightingAnimSpriteRenderer),	DynLightResCache.IconCmpLightingSpriteRenderer);
			CorePluginRegistry.RegisterTypeImage(typeof(Light),							DynLightResCache.IconLight);

			CorePluginRegistry.RegisterTypeCategory(typeof(LightingTechnique),			GeneralRes.Category_Graphics);
			CorePluginRegistry.RegisterTypeCategory(typeof(LightingSpriteRenderer),		GeneralRes.Category_Graphics);
			CorePluginRegistry.RegisterTypeCategory(typeof(LightingAnimSpriteRenderer), GeneralRes.Category_Graphics);
			CorePluginRegistry.RegisterTypeCategory(typeof(Light),						GeneralRes.Category_Graphics);

			CorePluginRegistry.RegisterDataConverter<Component>(new LightingRendererFromMaterial());
		}
	}

	public class LightingRendererFromMaterial : DataConverter
	{
		public override int Priority
		{
			get { return CorePluginRegistry.Priority_Specialized; }
		}
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.CanPerform<Material>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			List<object> results = new List<object>();
			List<Material> availData = convert.Perform<Material>().ToList();

			// Generate objects
			foreach (Material mat in availData)
			{
				if (convert.IsObjectHandled(mat)) continue;

				DrawTechnique tech = mat.Technique.Res;
				LightingTechnique lightTech = tech as LightingTechnique;
				if (tech == null) continue;

				bool isDynamicLighting = lightTech != null ||
					tech.PreferredVertexFormat == VertexC1P3T2A4.VertexTypeIndex ||
					tech.PreferredVertexFormat == VertexC1P3T4A4A1.VertexTypeIndex;
				if (!isDynamicLighting) continue;

				Texture mainTex = mat.MainTexture.Res;
				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();

				if (mainTex == null || mainTex.AnimFrames == 0)
				{
					LightingSpriteRenderer sprite = convert.Result.OfType<LightingSpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<LightingSpriteRenderer>();
					if (sprite == null) sprite = new LightingSpriteRenderer();
					sprite.SharedMaterial = mat;
					if (mainTex != null) sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PixelWidth, mainTex.PixelHeight);
					convert.SuggestResultName(sprite, mat.Name);
					results.Add(sprite);
				}
				else
				{
					LightingAnimSpriteRenderer sprite = convert.Result.OfType<LightingAnimSpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<LightingAnimSpriteRenderer>();
					if (sprite == null) sprite = new LightingAnimSpriteRenderer();
					sprite.SharedMaterial = mat;
					sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PixelWidth / mainTex.AnimCols, mainTex.PixelHeight / mainTex.AnimRows);
					sprite.AnimDuration = 5.0f;
					sprite.AnimFrameCount = mainTex.AnimFrames;
					convert.SuggestResultName(sprite, mat.Name);
					results.Add(sprite);
				}
				
				convert.MarkObjectHandled(mat);
			}

			convert.AddResult(results);
			return false;
		}
	}
}
