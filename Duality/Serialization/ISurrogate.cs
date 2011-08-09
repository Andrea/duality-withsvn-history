using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality.Serialization
{
	public interface ISurrogate
	{
		object RealObject { get; set; }
		ISerializable SurrogateObject { get; }
		int Priority { get; }

		bool MatchesType(Type t);
		void WriteConstructorData(IDataWriter writer);
		object ConstructObject(IDataReader reader, Type objType);
	}
	public abstract class Surrogate<T> : ISurrogate, ISerializable where T : class
	{
		private T realObj;
		
		object ISurrogate.RealObject
		{
			get { return this.realObj; }
			set { this.realObj = (T)value; }
		}
		ISerializable ISurrogate.SurrogateObject
		{
			get { return this; }
		}

		protected T RealObject
		{
			get { return this.realObj; }
		}
		public virtual int Priority
		{
			get { return 0; }
		}

		public virtual bool MatchesType(Type t)
		{
			return typeof(T) == t;
		}

		public virtual void WriteConstructorData(IDataWriter writer) {}
		public abstract void WriteData(IDataWriter writer);

		public virtual object ConstructObject(IDataReader reader, Type objType)
		{
			return ReflectionHelper.CreateInstanceOf(objType) ?? ReflectionHelper.CreateInstanceOf(objType, true);
		}
		public abstract void ReadData(IDataReader reader);
	}
}
