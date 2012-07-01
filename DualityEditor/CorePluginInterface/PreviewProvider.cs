using System;
using System.Linq;
using System.Drawing;

using Duality;

namespace DualityEditor.CorePluginInterface
{
	public enum PreviewSizeMode
	{
		FixedNone,
		FixedWidth,
		FixedHeight,
		FixedBoth
	}

	public static class PreviewProvider
	{
		public static Bitmap GetPreviewImage(object obj, int desiredWidth, int desiredHeight, PreviewSizeMode mode = PreviewSizeMode.FixedNone)
		{
			if (DualityApp.ExecContext == DualityApp.ExecutionContext.Terminated) return null;
			if (obj == null) return null;
			Bitmap result = null;
			PreviewSettings settings = new PreviewSettings(desiredWidth, desiredHeight, mode);
			
			//System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
			//System.Diagnostics.Stopwatch w2 = new System.Diagnostics.Stopwatch();
			//w.Restart();

			var generators = CorePluginHelper.RequestPreviewGenerators().ToArray();
			generators.StableSort((g1, g2) => g2.Priority - g1.Priority);

			ConvertOperation convert = new ConvertOperation(new[] { obj }, ConvertOperation.Operation.Convert);
			foreach (IPreviewGenerator gen in generators)
			{
				if (convert.CanPerform(gen.ObjectType))
				{
					object genObj = convert.Perform(gen.ObjectType).FirstOrDefault();

					if (genObj == null) continue;
					if (!gen.CanPerformOn(genObj, settings)) continue;
					//w2.Restart();
					result = gen.Perform(genObj, settings);
					//w2.Stop();

					break;
				}
			}

			//Log.Editor.Write("Generating preview for {0} took {1} and {2} ms", obj, w.ElapsedMilliseconds, w2.ElapsedMilliseconds);

			return result;
		}
	}

	public class PreviewSettings
	{
		public int DesiredWidth { get; private set; }
		public int DesiredHeight { get; private set; }
		public PreviewSizeMode SizeMode { get; private set; }

		public PreviewSettings(int desiredWidth, int desiredHeight, PreviewSizeMode mode)
		{
			this.DesiredWidth = desiredWidth;
			this.DesiredHeight = desiredHeight;
			this.SizeMode = mode;
		}
	}
	
	public interface IPreviewGenerator
	{
		int Priority { get; }
		Type ObjectType { get; }

		Bitmap Perform(object obj, PreviewSettings settings);
		bool CanPerformOn(object obj, PreviewSettings settings);
	}
	public abstract class PreviewGenerator<T> : IPreviewGenerator
	{
		public virtual int Priority
		{
			get { return CorePluginHelper.Priority_General; }
		}
		public Type ObjectType
		{
			get { return typeof(T); }
		}

		public abstract Bitmap Perform(T obj, PreviewSettings settings);
		public abstract bool CanPerformOn(T obj, PreviewSettings settings);

		Bitmap IPreviewGenerator.Perform(object obj, PreviewSettings settings)
		{
			return this.Perform((T)obj, settings);
		}
		bool IPreviewGenerator.CanPerformOn(object obj, PreviewSettings settings)
		{
			if (obj == null) return false;
			return this.CanPerformOn((T)obj, settings);
		}
	}
}
