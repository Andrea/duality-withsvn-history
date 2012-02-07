using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Reflection;

using CultureInfo = System.Globalization.CultureInfo;

namespace Duality.Serialization
{
	public class XmlFormatter : XmlFormatterBase
	{
		public XmlFormatter(XmlWriter writer) : base(writer) {}

		protected override void WriteObjectBody(DataType dataType, object obj, SerializeType objSerializeType, uint objId)
		{
			if (dataType.IsPrimitiveType())				this.WritePrimitive(obj);
			else if (dataType == DataType.Enum)			this.WriteEnum(obj as Enum, objSerializeType);
			else if (dataType == DataType.String)		this.writer.WriteString(obj as string);
			else if (dataType == DataType.Struct)		this.WriteStruct(obj, objSerializeType);
			else if (dataType == DataType.ObjectRef)	this.writer.WriteValue(objId);
			else if	(dataType == DataType.Array)		this.WriteArray(obj, objSerializeType, objId);
			else if (dataType == DataType.Class)		this.WriteStruct(obj, objSerializeType, objId);
			else if (dataType == DataType.Delegate)		this.WriteDelegate(obj, objSerializeType, objId);
			else if (dataType.IsMemberInfoType())		this.WriteMemberInfo(obj, objId);
		}
		protected void WriteMemberInfo(object obj, uint id = 0)
		{
			if (id != 0) this.writer.WriteAttributeString("id", id.ToString(CultureInfo.InvariantCulture));

			if (obj is Type)
			{
				Type type = obj as Type;
				SerializeType cachedType = ReflectionHelper.GetSerializeType(type);
				this.writer.WriteAttributeString("value", cachedType.TypeString);
			}
			else if (obj is MemberInfo)
			{
				MemberInfo member = obj as MemberInfo;
				this.writer.WriteAttributeString("value", member.GetMemberId());
			}
			else if (obj == null)
				throw new ArgumentNullException("obj");
			else
				throw new ArgumentException(string.Format("Type '{0}' is not a supported MemberInfo.", obj.GetType()));
		}
		protected void WriteArray(object obj, SerializeType objSerializeType, uint id = 0)
		{
			Array objAsArray = obj as Array;

			if (objAsArray.Rank != 1) throw new ArgumentException("Non single-Rank arrays are not supported");
			if (objAsArray.GetLowerBound(0) != 0) throw new ArgumentException("Non zero-based arrays are not supported");

			this.writer.WriteAttributeString("type", objSerializeType.TypeString);
			if (id != 0) this.writer.WriteAttributeString("id", id.ToString(CultureInfo.InvariantCulture));
			if (objAsArray.Rank != 1) this.writer.WriteAttributeString("rank", objAsArray.Rank.ToString(CultureInfo.InvariantCulture));

			if (objAsArray is byte[])
			{
				this.writer.WriteAttributeString("length", objAsArray.Length.ToString(CultureInfo.InvariantCulture));
				this.writer.WriteBinHex(objAsArray as byte[], 0, objAsArray.Length);
			}
			else
			{
				for (long l = 0; l < objAsArray.Length; l++)
					this.WriteObject(objAsArray.GetValue(l));
			}
		}
		protected void WriteStruct(object obj, SerializeType objSerializeType, uint id = 0)
		{
			ISerializable objAsCustom = obj as ISerializable;
			ISurrogate objSurrogate = this.GetSurrogateFor(objSerializeType.Type);

			// Write the structs data type
			this.writer.WriteAttributeString("type", objSerializeType.TypeString);
			if (id != 0) this.writer.WriteAttributeString("id", id.ToString(CultureInfo.InvariantCulture));
			if (objAsCustom != null) this.writer.WriteAttributeString("custom", true.ToString(CultureInfo.InvariantCulture));
			if (objSurrogate != null) this.writer.WriteAttributeString("surrogate", true.ToString(CultureInfo.InvariantCulture));

			if (objSurrogate != null)
			{
				objSurrogate.RealObject = obj;
				objAsCustom = objSurrogate.SurrogateObject;

				CustomSerialIO customIO = new CustomSerialIO();
				try { objSurrogate.WriteConstructorData(customIO); }
				catch (Exception e) { this.LogCustomSerializationError(id, objSerializeType.Type, e); }
				customIO.Serialize(this);
			}

			if (objAsCustom != null)
			{
				CustomSerialIO customIO = new CustomSerialIO();
				try { objAsCustom.WriteData(customIO); }
				catch (Exception e) { this.LogCustomSerializationError(id, objSerializeType.Type, e); }
				customIO.Serialize(this);
			}
			else
			{
				// Write the structs fields
				foreach (FieldInfo field in objSerializeType.Fields)
				{
					object val = field.GetValue(obj);

					if (val != null && this.IsFieldBlocked(field))
						val = ReflectionHelper.GetDefaultInstanceOf(field.FieldType);

					this.WriteObject(val, field.Name);
				}
			}
		}
		protected void WriteDelegate(object obj, SerializeType objSerializeType, uint id = 0)
		{
			bool multi = obj is MulticastDelegate;

			// Write the delegates type
			this.writer.WriteAttributeString("type", objSerializeType.TypeString);
			if (id != 0) this.writer.WriteAttributeString("id", id.ToString(CultureInfo.InvariantCulture));
			if (multi) this.writer.WriteAttributeString("multi", multi.ToString(CultureInfo.InvariantCulture));

			if (!multi)
			{
				Delegate objAsDelegate = obj as Delegate;
				this.WriteObject(objAsDelegate.Method);
				this.WriteObject(objAsDelegate.Target);
			}
			else
			{
				MulticastDelegate objAsDelegate = obj as MulticastDelegate;
				Delegate[] invokeList = objAsDelegate.GetInvocationList();
				this.WriteObject(objAsDelegate.Method);
				this.WriteObject(objAsDelegate.Target);
				this.WriteObject(invokeList);
			}
		}
		protected void WriteEnum(Enum obj, SerializeType objSerializeType)
		{
			this.writer.WriteAttributeString("type", objSerializeType.TypeString);
			this.writer.WriteAttributeString("name", obj.ToString());
			this.writer.WriteAttributeString("value", Convert.ToInt64(obj).ToString(CultureInfo.InvariantCulture));
		}
		
		protected override object ReadObjectBody(DataType dataType)
		{
			throw new NotImplementedException();
			return null;
		}
	}
}
