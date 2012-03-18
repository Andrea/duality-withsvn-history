using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

using Duality;

namespace DualityEditor.CorePluginInterface
{
	public class ConversionData : IDataObject
	{
		private	IDataObject		data		= null;
		private	DataObject		dataCache	= new DataObject();

		public ConversionData(IDataObject data)
		{
			this.data = data;
		}

		public object GetData(Type format)
		{
			if (!this.dataCache.GetDataPresent(format) && this.data.GetDataPresent(format))
				this.dataCache.SetData(format, this.data.GetData(format));
			return this.dataCache.GetData(format);
		}
		public object GetData(string format)
		{
			if (!this.dataCache.GetDataPresent(format) && this.data.GetDataPresent(format))
				this.dataCache.SetData(format, this.data.GetData(format));
			return this.dataCache.GetData(format);
		}
		public object GetData(string format, bool autoConvert)
		{
			if (!this.dataCache.GetDataPresent(format) && this.data.GetDataPresent(format, autoConvert))
				this.dataCache.SetData(format, this.data.GetData(format, autoConvert));
			return this.dataCache.GetData(format);
		}

		public bool GetDataPresent(Type format)
		{
			return this.data.GetDataPresent(format);
		}
		public bool GetDataPresent(string format)
		{
			return this.data.GetDataPresent(format);
		}
		public bool GetDataPresent(string format, bool autoConvert)
		{
			return this.data.GetDataPresent(format, autoConvert);
		}

		string[] IDataObject.GetFormats()
		{
			return this.data.GetFormats();
		}
		string[] IDataObject.GetFormats(bool autoConvert)
		{
			return this.data.GetFormats(autoConvert);
		}

		void IDataObject.SetData(object data)
		{
			throw new NotImplementedException();
		}
		void IDataObject.SetData(Type format, object data)
		{
			throw new NotImplementedException();
		}
		void IDataObject.SetData(string format, object data)
		{
			throw new NotImplementedException();
		}
		void IDataObject.SetData(string format, bool autoConvert, object data)
		{
			throw new NotImplementedException();
		}
	}
	public class ConvertOperation
	{
		private struct ConvComplexityEntry
		{
			public DataConverter Converter;
			public int Complexity;

			public ConvComplexityEntry(DataConverter converter, int complexity)
			{
				this.Converter = converter;
				this.Complexity = complexity;
			}
		}

		[Flags]
		public enum Operation
		{
			None		= 0x0,

			/// <summary>
			/// A simple conversion operation that does not affect any data.
			/// Example: Retrieving Texture from Material.
			/// </summary>
			Convert		= 0x1,
			/// <summary>
			/// A conversion that might create new resource data.
			/// Example: Creating Material from Texture.
			/// </summary>
			CreateRes	= 0x2,
			/// <summary>
			/// A conversion that might create new object data.
			/// Example: Construct a GameObject out of a set of Resources.
			/// </summary>
			CreateObj	= 0x4,

			All		= Convert | CreateRes | CreateObj
		}

		private	Operation		allowedOp	= Operation.All;
		private	ConversionData	data		= null;
		private	List<object>	result		= new List<object>();
		private	HashSet<object>	handledObj	= new HashSet<object>();
		// For "converter pathfinding":
		private	HashSet<DataConverter>	usedConverters	= new HashSet<DataConverter>();
		private	HashSet<Type>			checkedTypes	= new HashSet<Type>();
		private	int						curComplexity	= 0;
		private	int						maxComplexity	= 0;


		public ConversionData Data
		{
			get { return this.data; }
		}
		public IEnumerable<object> Result
		{
			get { return this.result; }
		}
		public Operation AllowedOperations
		{
			get { return this.allowedOp; }
		}


		public ConvertOperation(IDataObject data, Operation allowedOp)
		{
			allowedOp &= data.GetAllowedConvertOp();

			this.data = new ConversionData(data);
			this.allowedOp = allowedOp;
		}
		public ConvertOperation(IEnumerable<object> data, Operation allowedOp)
		{
			DataObject dataObj = new DataObject();
			foreach (object obj in data)
			{
				if (obj == null) continue;
				dataObj.SetData(obj.GetType(), obj);
			}
			this.data = new ConversionData(dataObj);
			this.allowedOp = allowedOp;
		}

		public void Reset()
		{
			this.handledObj.Clear();
			this.result.Clear();
			this.usedConverters.Clear();
			this.checkedTypes.Clear();
			this.curComplexity = 0;
			this.maxComplexity = 0;
		}
			
		public bool IsObjectHandled(object data)
		{
			if (data == null) return false;
			if (data is IContentRef) data = (data as IContentRef).Res;
			return this.handledObj.Contains(data);
		}
		public void MarkObjectHandled(object data)
		{
			if (data == null) return;
			if (data is IContentRef) data = (data as IContentRef).Res;
			this.handledObj.Add(data);
			//Log.Editor.Write("handled: {0} {1}", data != null ? data.GetType().Name : "", data);
		}
		public void AddResult(object obj)
		{
			if (obj == null) return;
			if (this.result.Contains(obj)) return;
			this.result.Add(obj);
			//Log.Editor.Write("addresult: {0} {1}", obj != null ? obj.GetType().Name : "", obj);
		}

		public bool CanPerform<T>()
		{
			return this.CanPerform(typeof(T));
		}
		public IEnumerable<T> Perform<T>()
		{
			IEnumerable<object> result = this.Perform(typeof(T)); 
			IEnumerable<T> castResult = result.OfType<T>();
			return castResult;
		}
		public bool CanPerform(Type target)
		{
			// Convert ContentRef requests to their respective Resource-requests
			target = this.ResTypeFromRefType(target);

			if (this.checkedTypes.Contains(target)) return false;
			this.curComplexity++;
			this.checkedTypes.Add(target);
			
			bool result = false;
			if (!result && this.data.GetDataPresent(target)) result = true;
			if (!result && this.data.GetDataPresent(target.MakeArrayType())) result = true;
			if (!result && this.data.ContainsContentRefs(target)) result = true;
			if (!result)
			{
				result = CorePluginHelper.RequestDataConverters(target)
					.Where(s => !this.usedConverters.Contains(s) && s.CanConvertFrom(this))
					.Any();
			}

			if (result) this.checkedTypes.Remove(target);
			this.maxComplexity = Math.Max(this.maxComplexity, this.curComplexity);
			this.curComplexity--;

			return result;
		}
		public IEnumerable<object> Perform(Type target)
		{
			// Convert ContentRef requests to their respective Resource-requests
			Type originalType = target;
			target = this.ResTypeFromRefType(target);

			//Log.Editor.Write("Convert to {0}", target.Name);
			int resultCountBefore = this.result.Count;

			// Check if there already is fitting data available
			IEnumerable<object> fittingData = null;
			if (fittingData == null)
			{
				// Single object
				if (this.data.GetDataPresent(target)) fittingData = new object[] { this.data.GetData(target) };
			}
			if (fittingData == null)
			{
				// Object array
				Type arrType = target.MakeArrayType();
				if (this.data.GetDataPresent(arrType)) fittingData = this.data.GetData(arrType) as IEnumerable<object>;
			}
			if (fittingData == null)
			{
				// ContentRefs
				if (this.data.ContainsContentRefs(target)) fittingData = this.data.GetContentRefs(target).Res();
			}
			
			// If something fitting was found, directly add it to the operation results
			if (fittingData != null)
			{
				foreach (object obj in fittingData)
					this.AddResult(obj);
			}

			// No result yet? Search suitable converters
			if (this.result.Count == resultCountBefore)
			{
				var converterQuery = CorePluginHelper.RequestDataConverters(target);
				List<ConvComplexityEntry> converters = new List<ConvComplexityEntry>();
				foreach (var c in converterQuery)
				{
					this.maxComplexity = 0;
					if (this.usedConverters.Contains(c)) continue;
					if (!c.CanConvertFrom(this)) continue;
					converters.Add(new ConvComplexityEntry(c, this.maxComplexity));
				}

				// Perform conversion
				converters.StableSort((c1, c2) => (c2.Converter.Priority - c1.Converter.Priority) * 10000 + (c1.Complexity - c2.Complexity));
				foreach (var c in converters)
				{
					//Log.Editor.Write("using {0}", s.GetType().Name);
					//Log.Editor.PushIndent();
					//Log.Editor.Write("before: {0}", this.Result.ToString(o => string.Format("{0} {1}", o.GetType().Name, o), ", "));
					this.usedConverters.Add(c.Converter);
					bool handled = c.Converter.Convert(this);
					this.usedConverters.Remove(c.Converter);
					//Log.Editor.Write("after: {0}", this.Result.ToString(o => string.Format("{0} {1}", o.GetType().Name, o), ", "));
					//Log.Editor.PopIndent();
					if (handled) break;
				}
			}

			IEnumerable<object> returnValue = this.result;

			// Convert back to Resource requests
			if (typeof(IContentRef).IsAssignableFrom(originalType))
				returnValue = result.OfType<Resource>().Select(r => r.GetContentRef());

			returnValue = returnValue.Where(o => originalType.IsAssignableFrom(o.GetType()));
			return returnValue ?? (IEnumerable<object>)Array.CreateInstance(originalType, 0);
		}

		private Type ResTypeFromRefType(Type type)
		{
			if (typeof(IContentRef).IsAssignableFrom(type))
			{
				if (type.IsGenericType)
					type = type.GetGenericArguments()[0];
				else
					type = typeof(Resource);
			}

			return type;
		}
	}
	public abstract class DataConverter
	{
		public virtual int Priority
		{
			get { return CorePluginHelper.Priority_General; }
		}

		public abstract bool CanConvertFrom(ConvertOperation convert);
		public abstract bool Convert(ConvertOperation convert);
	}
}
