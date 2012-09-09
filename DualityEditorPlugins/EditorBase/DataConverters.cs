using System;
using System.Collections.Generic;
using System.Linq;

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
			get { return CorePluginRegistry.Priority_Specialized; }
		}

		public override bool CanConvertFrom(ConvertOperation convert)
		{
			return 
				convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateObj) && 
				convert.Data.ContainsContentRefs<Prefab>();
		}
		public override bool Convert(ConvertOperation convert)
		{
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
					}
				}
			}
			// Don't finish convert operation - other converters miht contribute to the new GameObjects!
			return false; 
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
			List<Component> availData = convert.Perform<Component>().ToList();

			// Generate objects
			foreach (Component cmp in availData)
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
					else if (availData.OfType<SpriteRenderer>().Any()) // Try to use a Material name
						gameObj.Name = availData.OfType<SpriteRenderer>().First().SharedMaterial.Name;
					else if (availData.OfType<SoundEmitter>().Any() && availData.OfType<SoundEmitter>().First().Sources.Any()) // Try to use a Sound name
						gameObj.Name = availData.OfType<SoundEmitter>().First().Sources[0].Sound.Name;
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
			List<Sound> availData = convert.Perform<Sound>().ToList();

			// Generate objects
			foreach (Sound snd in availData)
			{
				if (convert.IsObjectHandled(snd)) continue;

				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();
				SoundEmitter emitter = convert.Result.OfType<SoundEmitter>().FirstOrDefault();
				if (emitter == null && gameobj != null) emitter = gameobj.GetComponent<SoundEmitter>();
				if (emitter == null) emitter = new SoundEmitter();
				convert.AddResult(snd.Name); // Leave a name string in the result to pick up for the GameObject constructor
					
				SoundEmitter.Source source = new SoundEmitter.Source(snd);
				source.Paused = false;
				emitter.Sources.Add(source);

				convert.AddResult(emitter);
				convert.MarkObjectHandled(snd);
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
			List<Material> availData = convert.Perform<Material>().ToList();

			// Generate objects
			foreach (Material mat in availData)
			{
				if (convert.IsObjectHandled(mat)) continue;
				Texture mainTex = mat.MainTexture.Res;
				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();

				convert.AddResult(mat.Name); // Leave a name string in the result to pick up for the GameObject constructor
				if (mainTex == null || mainTex.AnimFrames == 0)
				{
					SpriteRenderer sprite = convert.Result.OfType<SpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<SpriteRenderer>();
					if (sprite == null) sprite = new SpriteRenderer();
					sprite.SharedMaterial = mat;
					if (mainTex != null) sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PxWidth, mainTex.PxHeight);
					convert.AddResult(sprite);
				}
				else
				{
					AnimSpriteRenderer sprite = convert.Result.OfType<AnimSpriteRenderer>().FirstOrDefault();
					if (sprite == null && gameobj != null) sprite = gameobj.GetComponent<AnimSpriteRenderer>();
					if (sprite == null) sprite = new AnimSpriteRenderer();
					sprite.SharedMaterial = mat;
					sprite.Rect = Rect.AlignCenter(0.0f, 0.0f, mainTex.PxWidth / mainTex.AnimCols, mainTex.PxHeight / mainTex.AnimRows);
					sprite.AnimDuration = 5.0f;
					sprite.AnimFrameCount = mainTex.AnimFrames;
					convert.AddResult(sprite);
				}

				convert.MarkObjectHandled(mat);
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
			List<Font> availData = convert.Perform<Font>().ToList();

			// Generate objects
			foreach (Font font in availData)
			{
				if (convert.IsObjectHandled(font)) continue;

				GameObject gameobj = convert.Result.OfType<GameObject>().FirstOrDefault();
				TextRenderer renderer = convert.Result.OfType<TextRenderer>().FirstOrDefault();
				if (renderer == null && gameobj != null) renderer = gameobj.GetComponent<TextRenderer>();
				if (renderer == null) renderer = new TextRenderer();
				convert.AddResult(font.Name); // Leave a name string in the result to pick up for the GameObject constructor
					
				if (!renderer.Text.Fonts.Contains(font))
				{
					var fonts = renderer.Text.Fonts.ToList();
					if (fonts[0] == Font.GenericMonospace10) fonts.RemoveAt(0);
					fonts.Add(font);
					renderer.Text.Fonts = fonts.ToArray();
					renderer.Text.ApplySource();
					renderer.UpdateText();
				}

				convert.AddResult(renderer);
				convert.MarkObjectHandled(font);
			}

			return false;
		}
	}
	public class PrefabFromGameObject : DataConverter
	{
		public override int Priority
		{
			get { return CorePluginRegistry.Priority_Specialized; }
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

			List<Material> availData = convert.Perform<Material>().ToList();

			// Append objects
			foreach (Material mat in availData)
			{
				if (convert.IsObjectHandled(mat)) continue;

				convert.AddResult(mat.Info);
				finishConvertOp = true;
				convert.MarkObjectHandled(mat);
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
			List<BatchInfo> availData = convert.Perform<BatchInfo>().ToList();

			// Generate objects
			foreach (BatchInfo info in availData)
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
			if (convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
			{
				return convert.CanPerform<Texture>();
			}
			
			if (convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert))
			{
				List<Texture> availTex = convert.Perform<Texture>(ConvertOperation.Operation.Convert).ToList();
				return availTex.Any(t => this.FindMatch(t) != null);
			}

			return false;
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;
			List<Texture> availData = convert.Perform<Texture>().ToList();

			// Generate objects
			foreach (Texture baseRes in availData)
			{
				if (convert.IsObjectHandled(baseRes)) continue;

				// Find target Resource matching the source - or create one.
				Material targetRes = this.FindMatch(baseRes);
				if (targetRes == null && convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
				{
					targetRes = Material.CreateFromTexture(baseRes).Res;
				}

				if (targetRes == null) continue;
				convert.AddResult(targetRes);
				finishConvertOp = true;
				convert.MarkObjectHandled(baseRes);
			}

			return finishConvertOp;
		}
		private Material FindMatch(Texture baseRes)
		{
			if (baseRes == null)
			{
				return null;
			}
			else if (baseRes.IsDefaultContent)
			{
				var defaultContent = ContentProvider.GetAllDefaultContent();
				return defaultContent.FirstOrDefault(r => r.Is<Material>() && (r.Res as Material).MainTexture == baseRes).As<Material>().Res;
			}
			else
			{
				string targetPath = baseRes.FullName + Material.FileExt;
				return ContentProvider.RequestContent<Material>(targetPath).Res;
			}
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

			List<Material> availData = convert.Perform<Material>().ToList();

			// Append objects
			foreach (Material baseRes in availData)
			{
				if (convert.IsObjectHandled(baseRes)) continue;
				if (!baseRes.MainTexture.IsAvailable) continue;

				convert.AddResult(baseRes.MainTexture.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(baseRes);
			}

			return finishConvertOp;
		}
	}
	public class TextureFromPixmap : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			if (convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
			{
				return convert.CanPerform<Pixmap>();
			}
			
			if (convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert))
			{
				List<Pixmap> availData = convert.Perform<Pixmap>(ConvertOperation.Operation.Convert).ToList();
				return availData.Any(t => this.FindMatch(t) != null);
			}

			return false;
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;
			List<Pixmap> availData = convert.Perform<Pixmap>().ToList();

			// Generate objects
			foreach (Pixmap baseRes in availData)
			{
				if (convert.IsObjectHandled(baseRes)) continue;

				// Find target Resource matching the source - or create one.
				Texture targetRes = this.FindMatch(baseRes);
				if (targetRes == null && convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
				{
					targetRes = Texture.CreateFromPixmap(baseRes).Res;
				}

				if (targetRes == null) continue;
				convert.AddResult(targetRes);
				finishConvertOp = true;
				convert.MarkObjectHandled(baseRes);
			}

			return finishConvertOp;
		}
		private Texture FindMatch(Pixmap baseRes)
		{
			if (baseRes == null)
			{
				return null;
			}
			else if (baseRes.IsDefaultContent)
			{
				var defaultContent = ContentProvider.GetAllDefaultContent();
				return defaultContent.FirstOrDefault(r => r.Is<Texture>() && (r.Res as Texture).BasePixmap == baseRes).As<Texture>().Res;
			}
			else
			{
				string targetPath = baseRes.FullName + Texture.FileExt;
				return ContentProvider.RequestContent<Texture>(targetPath).Res;
			}
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

			List<Texture> availData = convert.Perform<Texture>().ToList();

			// Append objects
			foreach (Texture baseRes in availData)
			{
				if (convert.IsObjectHandled(baseRes)) continue;
				if (!baseRes.BasePixmap.IsAvailable) continue;

				convert.AddResult(baseRes.BasePixmap.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(baseRes);
			}

			return finishConvertOp;
		}
	}
	public class SoundFromAudioData : DataConverter
	{
		public override bool CanConvertFrom(ConvertOperation convert)
		{
			if (convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
			{
				return convert.CanPerform<AudioData>();
			}
			
			if (convert.AllowedOperations.HasFlag(ConvertOperation.Operation.Convert))
			{
				List<AudioData> availData = convert.Perform<AudioData>(ConvertOperation.Operation.Convert).ToList();
				return availData.Any(t => this.FindMatch(t) != null);
			}

			return false;
		}
		public override bool Convert(ConvertOperation convert)
		{
			bool finishConvertOp = false;
			List<AudioData> availData = convert.Perform<AudioData>().ToList();

			// Generate objects
			foreach (AudioData baseRes in availData)
			{
				if (convert.IsObjectHandled(baseRes)) continue;

				// Find target Resource matching the source - or create one.
				Sound targetRes = this.FindMatch(baseRes);
				if (targetRes == null && convert.AllowedOperations.HasFlag(ConvertOperation.Operation.CreateRes))
				{
					targetRes = Sound.CreateFromAudioData(baseRes).Res;
				}

				if (targetRes == null) continue;
				convert.AddResult(targetRes);
				finishConvertOp = true;
				convert.MarkObjectHandled(baseRes);
			}

			return finishConvertOp;
		}
		private Sound FindMatch(AudioData baseRes)
		{
			if (baseRes == null)
			{
				return null;
			}
			else if (baseRes.IsDefaultContent)
			{
				var defaultContent = ContentProvider.GetAllDefaultContent();
				return defaultContent.FirstOrDefault(r => r.Is<Sound>() && (r.Res as Sound).Data == baseRes).As<Sound>().Res;
			}
			else
			{
				string targetPath = baseRes.FullName + Texture.FileExt;
				return ContentProvider.RequestContent<Sound>(targetPath).Res;
			}
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

			List<Sound> availData = convert.Perform<Sound>().ToList();

			// Append objects
			foreach (Sound baseRes in availData)
			{
				if (convert.IsObjectHandled(baseRes)) continue;
				if (!baseRes.Data.IsAvailable) continue;

				convert.AddResult(baseRes.Data.Res);
				finishConvertOp = true;
				convert.MarkObjectHandled(baseRes);
			}

			return finishConvertOp;
		}
	}
}
