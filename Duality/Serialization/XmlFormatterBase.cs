using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;

using CultureInfo = System.Globalization.CultureInfo;

namespace Duality.Serialization
{
	public abstract class XmlFormatterBase : FormatterBase
	{
		protected class CustomSerialIO : CustomSerialIOBase<XmlFormatterBase>
		{
			public override void Serialize(XmlFormatterBase formatter)
			{
				foreach (var pair in this.data)
					formatter.WriteObject(pair.Value, pair.Key);
				this.Clear();
			}
			public override void Deserialize(XmlFormatterBase formatter)
			{
				this.Clear();
				throw new NotImplementedException();
				//int count = (int)formatter.ReadPrimitive(DataType.Int);
				//for (int i = 0; i < count; i++)
				//{
				//    string key = formatter.ReadString();
				//    object value = formatter.ReadObject();
				//    this.values.Add(key, value);
				//}
			}
		}


		protected	XmlWriter	writer	= null;
		protected	XmlReader	reader	= null;

		public XmlWriter WriteTarget
		{
			get { return this.writer; }
			set
			{
				if (this.writer != value)
				{
					this.writer = value;
				}
			}
		}
		public XmlReader ReadTarget
		{
			get { return this.reader; }
			set
			{
				if (this.reader != value)
				{
					this.reader = value;
				}
			}
		}
		public bool CanWrite
		{
			get { return this.writer != null; }
		}
		public bool CanRead
		{
			get { return this.reader != null; }
		}

		public XmlFormatterBase(XmlWriter writer)
		{
			this.WriteTarget = writer;
		}

		public override object ReadObject()
		{
			if (!this.CanRead) return this.GetNullObject();
			throw new NotImplementedException();
			return this.GetNullObject();
		}
		protected abstract object ReadObjectBody(DataType dataType);

		public override void WriteObject(object obj)
		{
			if (!this.CanWrite) return;
			this.WriteObject(obj, "object");
		}
		protected void WriteObject(object obj, string elementName)
		{
			this.PushObjectElement(elementName);

			// Null? Empty Element.
			if (obj == null)
			{
				this.PopObjectElement();
				return;
			}
			
			// Retrieve type data
			SerializeType objSerializeType;
			uint objId;
			DataType dataType;
			this.GetWriteObjectData(obj, out objSerializeType, out dataType, out objId);

			// Write data type header
			this.writer.WriteAttributeString("dataType", dataType.ToString());

			// Write object
			try 
			{
				this.WriteObjectBody(dataType, obj, objSerializeType, objId);
			}
			finally
			{
				this.PopObjectElement();
			}
		}
		protected abstract void WriteObjectBody(DataType dataType, object obj, SerializeType objSerializeType, uint objId);
		
		protected void WritePrimitive(object obj)
		{
			if		(obj is bool)		this.writer.WriteValue((bool)obj);
			else if (obj is byte)		this.writer.WriteValue((byte)obj);
			else if (obj is char)		this.writer.WriteValue((char)obj);
			else if (obj is sbyte)		this.writer.WriteValue((sbyte)obj);
			else if (obj is short)		this.writer.WriteValue((short)obj);
			else if (obj is ushort)		this.writer.WriteValue((ushort)obj);
			else if (obj is int)		this.writer.WriteValue((int)obj);
			else if (obj is uint)		this.writer.WriteValue((uint)obj);
			else if (obj is long)		this.writer.WriteValue((long)obj);
			else if (obj is ulong)		this.writer.WriteValue((decimal)(ulong)obj);
			else if (obj is float)		this.writer.WriteValue((float)obj);
			else if (obj is double)		this.writer.WriteValue((double)obj);
			else if (obj is decimal)	this.writer.WriteValue((decimal)obj);
			else if (obj == null)
				throw new ArgumentNullException("obj");
			else
				throw new ArgumentException(string.Format("Type '{0}' is not a primitive.", obj.GetType()));
		}

		private void InitRoot()
		{
			if (this.writer.WriteState == WriteState.Start)
				this.writer.WriteStartElement("root");
		}
		private void PushObjectElement(string elementName)
		{
			this.InitRoot();
			this.writer.WriteStartElement(elementName);
		}
		private void PopObjectElement()
		{
			this.writer.WriteEndElement();
		}
	}
}
