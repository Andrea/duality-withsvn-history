using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;

using Duality;
using Duality.Components;
using Duality.Components.Renderers;
using Duality.Resources;

using DualityEditor;
using DualityEditor.CorePluginInterface;


namespace EditorBase.DataConverters
{
	public class GameObjFromPrefab : DataConverter
	{
		public override int Priority
		{
			get { return CorePluginHelper.Priority_Specialized; }
		}

		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.Data.ContainsContentRefs<Prefab>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;
			if (convert.Data.ContainsContentRefs<Prefab>())
			{
				ContentRef<Prefab>[] dropdata = convert.Data.GetContentRefs<Prefab>();

				// Instantiate Prefabs
				foreach (ContentRef<Prefab> pRef in dropdata)
				{
					if (convert.IsObjectHandled(pRef.Res)) continue;
					if (!pRef.IsAvailable) continue;
					GameObject newObj = pRef.Res.Instantiate();
					if (newObj != null)
					{
						convert.AddResult(newObj);
						convert.MarkObjectHandled(pRef.Res);
						finishConvertOp = true;
					}
				}
			}
			return finishConvertOp;
		}
	}
	public class GameObjFromComponents : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.CanPerform<Component>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			List<Component> dropdata = new List<Component>();
			var matSelectionQuery = convert.Perform<Component>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery);

			// Generate objects
			foreach (Component cmp in dropdata)
			{
				if (convert.IsObjectHandled(cmp)) continue;

				// Create GameObject
				GameObject gameObj = convert.Result.OfType<GameObject>().FirstOrDefault();
				if (gameObj == null)
				{
					gameObj = new GameObject();
					// Come up with a suitable name
					if (convert.Result.OfType<string>().Any()) // Somebody left a string in the results? Perfect.
						gameObj.Name = convert.Result.OfType<string>().First();
					else if (dropdata.OfType<SpriteRenderer>().Any()) // Try to use a Material name
						gameObj.Name = dropdata.OfType<SpriteRenderer>().First().SharedMaterial.Name;
					else if (dropdata.OfType<SoundEmitter>().Any() && dropdata.OfType<SoundEmitter>().First().Sources.Any()) // Try to use a Sound name
						gameObj.Name = dropdata.OfType<SoundEmitter>().First().Sources[0].Sound.Name;
					else // Use default name
						gameObj.Name = "GameObject";
				}

				// Make sure all requirements are met
				foreach (Type t in cmp.GetRequiredComponents())
					gameObj.AddComponent(t);

				// Add Component
				gameObj.AddComponent(cmp);

				convert.AddResult(gameObj);
				convert.MarkObjectHandled(cmp);
			}

			return false;
		}
	}
	public class ComponentFromSound : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.CanPerform<Sound>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			List<ContentRef<Sound>> dropdata = new List<ContentRef<Sound>>();
			var matSelectionQuery = convert.Perform<Sound>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Generate objects
			foreach (ContentRef<Sound> sndRef in dropdata)
			{
				if (convert.IsObjectHandled(sndRef.Res)) continue;
				if (!sndRef.IsAvailable) continue;
				Sound snd = sndRef.Res;

				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();
				SoundEmitter emitter = convert.Result.OfType<SoundEmitter>().FirstOrDefault();
				if (emitter == null && gameobj != null) emitter = gameobj.GetComponent<SoundEmitter>();
				if (emitter == null) emitter = new SoundEmitter();
				convert.AddResult(snd.Name); // Leave a name string in the result to pick up for the GameObject constructor
					
				SoundEmitter.Source source = new SoundEmitter.Source(snd);
				source.Paused = false;
				emitter.Sources.Add(source);

				convert.AddResult(emitter);
				convert.MarkObjectHandled(sndRef.Res);
			}

			return false;
		}
	}
	public class ComponentFromMaterial : DataConverter
	{
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
				Texture mainTex = mat.MainTexture.Res;
				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();

				convert.AddResult(mat.Name); // Leave a name string in the result to pick up for the GameObject constructor
				if (mainTex == null || mainTex.AnimFrames == 0)
				{
					SpriteRenderer sprite = convert.Result.OfType<SpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<SpriteRenderer>();
					if (sprite == null) sprite = new SpriteRenderer();
					sprite.SharedMaterial = matRef;
					if (mainTex != null) sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PxWidth, mainTex.PxHeight);
					convert.AddResult(sprite);
				}
				else
				{
					AnimSpriteRenderer sprite = convert.Result.OfType<AnimSpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<AnimSpriteRenderer>();
					if (sprite == null) sprite = new AnimSpriteRenderer();
					sprite.SharedMaterial = matRef;
					sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PxWidth / mainTex.AnimCols, mainTex.PxHeight / mainTex.AnimRows);
					convert.AddResult(sprite);
				}

				convert.MarkObjectHandled(matRef.Res);
			}

			return false;
		}
	}
	public class ComponentFromFont : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.CanPerform<Font>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			List<ContentRef<Font>> dropdata = new List<ContentRef<Font>>();
			var matSelectionQuery = convert.Perform<Font>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Generate objects
			foreach (ContentRef<Font> fontRef in dropdata)
			{
				if (convert.IsObjectHandled(fontRef.Res)) continue;
				if (!fontRef.IsAvailable) continue;
				Font font = fontRef.Res;

				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();
				TextRenderer renderer = convert.Result.OfType<TextRenderer>().FirstOrDefault();
				if (renderer == null && gameobj != null) renderer = gameobj.GetComponent<TextRenderer>();
				if (renderer == null) renderer = new TextRenderer();
				convert.AddResult(font.Name); // Leave a name string in the result to pick up for the GameObject constructor
					
				if (!renderer.Text.Fonts.Contains(fontRef))
				{
					var fonts = renderer.Text.Fonts.ToList();
					if (fonts[0] == Font.GenericMonospace10) fonts.RemoveAt(0);
					fonts.Add(fontRef);
					renderer.Text.Fonts = fonts.ToArray();
					renderer.Text.ApplySource();
					renderer.UpdateMetrics();
				}

				convert.AddResult(renderer);
				convert.MarkObjectHandled(fontRef.Res);
			}

			return false;
		}
	}
	public class PrefabFromGameObject : DataConverter
	{
		public override int Priority
		{
			get { return CorePluginHelper.Priority_Specialized; }
		}

		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes) && 
				convert.Data.ContainsGameObjectRefs();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			if (convert.Data.ContainsGameObjectRefs())
			{
				GameObject[] draggedObjArray = convert.Data.GetGameObjectRefs();

				// Filter out GameObjects that are children of others
				draggedObjArray = draggedObjArray.Where(o => !draggedObjArray.Any(o2 => o.IsChildOf(o2))).ToArray();

				// Generate Prefabs
				foreach (GameObject draggedObj in draggedObjArray)
				{
					if (convert.IsObjectHandled(draggedObj)) continue;
					// Create Prefab
					Prefab prefab = new Prefab(draggedObj);
					prefab.SourcePath = draggedObj.Name; // Dummy "source path" that may be used as indicator where to save the Resource later.
					convert.MarkObjectHandled(draggedObj);						
					convert.AddResult(prefab);
					finishConvertOp = true;
				}
			}

			return finishConvertOp;
		}
	}
	public class BatchInfoFromMaterial : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<Material>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<Material>> dropdata = new List<ContentRef<Material>>();
			var matSelectionQuery = convert.Perform<Material>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Append objects
			foreach (ContentRef<Material> matRef in dropdata)
			{
				if (convert.IsObjectHandled(matRef.Res)) continue;
				if (!matRef.IsAvailable) continue;

				convert.AddResult(matRef.Res.Info);
				finishConvertOp = true;
				convert.MarkObjectHandled(matRef.Res);
			}

			return finishConvertOp;
		}
	}
	public class MaterialFromBatchInfo : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes) && 
				convert.CanPerform<BatchInfo>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<BatchInfo> dropdata = new List<BatchInfo>();
			var matSelectionQuery = convert.Perform<BatchInfo>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery);

			// Generate objects
			foreach (BatchInfo info in dropdata)
			{
				if (convert.IsObjectHandled(info)) continue;

				// Auto-Generate Material
				string matName = "Material";
				if (!info.MainTexture.IsExplicitNull) matName = info.MainTexture.FullName;
				string matPath = PathHelper.GetFreePath(matName, Material.FileExt);
				Material mat = new Material(info);
				mat.Save(matPath);

				convert.AddResult(mat);
				finishConvertOp = true;
				convert.MarkObjectHandled(info);
			}

			return finishConvertOp;
		}
	}
	public class MaterialFromTexture : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<Texture>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<Texture>> dropdata = new List<ContentRef<Texture>>();
			var matSelectionQuery = convert.Perform<Texture>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Generate objects
			foreach (ContentRef<Texture> texRef in dropdata)
			{
				if (convert.IsObjectHandled(texRef.Res)) continue;
				if (!texRef.IsAvailable) continue;
				Texture tex = texRef.Res;

				// Find Material matching Texture
				ContentRef<Material> matRef = ContentRef<Material>.Null;
				if (texRef.IsDefaultContent)
				{
					var defaultContent = ContentProvider.GetAllDefaultContent();
					matRef = defaultContent.Where(r => r.Is<Material>() && (r.Res as Material).MainTexture == tex).FirstOrDefault().As<Material>();
				}
				else
				{
					string matPath = tex.FullName + Material.FileExt;
					matRef = ContentProvider.RequestContent<Material>(matPath);
					if (!matRef.IsAvailable && convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
					{
						// Auto-Generate Material
						matRef = Material.CreateFromTexture(tex);
					}
				}

				if (!matRef.IsAvailable) continue;
				convert.AddResult(matRef.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(texRef.Res);
			}

			return finishConvertOp;
		}
	}
	public class TextureFromMaterial : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<Material>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<Material>> dropdata = new List<ContentRef<Material>>();
			var matSelectionQuery = convert.Perform<Material>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Append objects
			foreach (ContentRef<Material> matRef in dropdata)
			{
				if (convert.IsObjectHandled(matRef.Res)) continue;
				if (!matRef.IsAvailable) continue;

				if (!matRef.Res.MainTexture.IsAvailable) continue;
				convert.AddResult(matRef.Res.MainTexture.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(matRef.Res);
			}

			return finishConvertOp;
		}
	}
	public class TextureFromPixmap : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<Pixmap>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<Pixmap>> dropdata = new List<ContentRef<Pixmap>>();
			var matSelectionQuery = convert.Perform<Pixmap>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Generate objects
			foreach (ContentRef<Pixmap> pixRef in dropdata)
			{
				if (convert.IsObjectHandled(pixRef.Res)) continue;
				if (!pixRef.IsAvailable) continue;
				Pixmap pix = pixRef.Res;

				// Find Material matching Texture
				ContentRef<Texture> texRef = ContentRef<Texture>.Null;
				if (pixRef.IsDefaultContent)
				{
					var defaultContent = ContentProvider.GetAllDefaultContent();
					texRef = defaultContent.Where(r => r.Is<Texture>() && (r.Res as Texture).BasePixmap == pix).FirstOrDefault().As<Texture>();
				}
				else
				{
					string texPath = pix.FullName + Texture.FileExt;
					texRef = ContentProvider.RequestContent<Texture>(texPath);
					if (!texRef.IsAvailable && convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
					{
						// Auto-Generate Texture
						texRef = Texture.CreateFromPixmap(pix);
					}
				}

				if (!texRef.IsAvailable) continue;
				convert.AddResult(texRef.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(pixRef.Res);
			}

			return finishConvertOp;
		}
	}
	public class PixmapFromTexture : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<Texture>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<Texture>> dropdata = new List<ContentRef<Texture>>();
			var matSelectionQuery = convert.Perform<Texture>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Append objects
			foreach (ContentRef<Texture> texRef in dropdata)
			{
				if (convert.IsObjectHandled(texRef.Res)) continue;
				if (!texRef.IsAvailable) continue;

				if (!texRef.Res.BasePixmap.IsAvailable) continue;
				convert.AddResult(texRef.Res.BasePixmap.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(texRef.Res);
			}

			return finishConvertOp;
		}
	}
	public class SoundFromAudioData : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<AudioData>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<AudioData>> dropdata = new List<ContentRef<AudioData>>();
			var matSelectionQuery = convert.Perform<AudioData>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Generate objects
			foreach (ContentRef<AudioData> audRef in dropdata)
			{
				if (convert.IsObjectHandled(audRef.Res)) continue;
				if (!audRef.IsAvailable) continue;
				AudioData aud = audRef.Res;

				// Find Material matching Texture
				ContentRef<Sound> sndRef = ContentRef<Sound>.Null;
				if (audRef.IsDefaultContent)
				{
					var defaultContent = ContentProvider.GetAllDefaultContent();
					sndRef = defaultContent.Where(r => r.Is<Sound>() && (r.Res as Sound).Data.Res == aud).FirstOrDefault().As<Sound>();
				}
				else
				{
					string sndPath = aud.FullName + Sound.FileExt;
					sndRef = ContentProvider.RequestContent<Sound>(sndPath);
					if (!sndRef.IsAvailable && convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
					{
						// Auto-Generate Material
						sndRef = Sound.CreateFromAudioData(aud);
					}
				}

				if (!sndRef.IsAvailable) continue;
				convert.AddResult(sndRef.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(audRef.Res);
			}

			return finishConvertOp;
		}
	}
	public class AudioDataFromSound : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert) && 
				convert.CanPerform<Sound>();
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;

			List<ContentRef<Sound>> dropdata = new List<ContentRef<Sound>>();
			var matSelectionQuery = convert.Perform<Sound>();
			if (matSelectionQuery != null) dropdata.AddRange(matSelectionQuery.Ref());

			// Append objects
			foreach (ContentRef<Sound> sndRef in dropdata)
			{
				if (convert.IsObjectHandled(sndRef.Res)) continue;
				if (!sndRef.IsAvailable) continue;

				if (!sndRef.Res.Data.IsAvailable) continue;
				convert.AddResult(sndRef.Res.Data.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(sndRef.Res);
			}

			return finishConvertOp;
		}
	}
}
