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

		public XmlFormatterBase() : this(null) {}
		public XmlFormatterBase(Stream stream)
		{
			XmlWriterSettings writerSettings = new XmlWriterSettings();
			writerSettings.Indent = true;
			XmlReaderSettings readerSettings = new XmlReaderSettings();
			readerSettings.IgnoreWhitespace = true;
			readerSettings.IgnoreComments = true;
			readerSettings.IgnoreProcessingInstructions = true;

			this.WriteTarget = (stream != null && stream.CanWrite) ? XmlTextWriter.Create(stream, writerSettings) : null;
			this.ReadTarget = (stream != null && stream.CanRead) ? XmlTextReader.Create(stream, readerSettings) : null;
		}
		protected override void Dispose(bool manually)
		{
			if (this.Disposed) return;

			if (this.writer != null)
			{
				if (this.writer.WriteState != WriteState.Start) this.writer.WriteEndDocument();
				this.writer.Flush();
				this.writer = null;
			}

			if (this.reader != null)
			{
				this.reader = null;
			}
		}
		
		public override object ReadObject()
		{
			string objName;
			return this.ReadObject(out objName);
		}
		protected object ReadObject(out string objName)
		{
			// DEBUG: How to use XmlReader
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						string[] attributes;
						if (reader.HasAttributes)
						{
							attributes = new string[reader.AttributeCount];
							for (int i = 0; i < reader.AttributeCount; i++)
							{
								reader.MoveToAttribute(i);
								attributes[i] = string.Format("{0}=\"{1}\"", reader.Name, reader.Value);
							}
							reader.MoveToElement();
						}
						else attributes = new string[0];

						if (reader.IsEmptyElement)
						{
							Log.Core.Write("<{0} {1} />", reader.Name, attributes.ToString(", "));
						}
						else
						{
							Log.Core.Write("<{0} {1}>", reader.Name, attributes.ToString(", "));
							Log.Core.PushIndent();
						}
					}
					else if (reader.NodeType == XmlNodeType.EndElement)
					{
						Log.Core.PopIndent();
						Log.Core.Write("</{0}>", reader.Name);
					}
					else
					{
						Log.Core.Write("({2}) {0} => {1}", reader.Name, reader.Value, reader.NodeType);
					}
				}
				throw new NotImplementedException("This is debug code");
			}
			// ALL THAT FOLLOWS IS OLD, EXPERIMENTAL AND DOESNT WORK. REWRITE USING ABOVE CODE

			if (!this.CanRead) throw new InvalidOperationException("Can't read object from a write-only serializer");
			if (this.reader.ReadState == ReadState.EndOfFile) throw new EndOfStreamException("No more data to read.");
			if (this.reader.ReadState == ReadState.Initial)
			{
				this.reader.Read();
				this.reader.MoveToContent();
			}


			this.reader.ReadStartElement();
			objName = this.reader.Name;

			// Empty element? Return null
			if (this.reader.IsEmptyElement)
			{
				this.reader.ReadEndElement();
				return this.GetNullObject();
			}

			// Read data type header
			string dataTypeStr = this.reader.GetAttribute("dataType");
			DataType dataType;
			if (!Enum.TryParse<DataType>(dataTypeStr, out dataType))
			{
				dataType = DataType.Unknown;
				this.SerializationLog.WriteError("Can't resolve DataType: {0}. Returning null reference.", dataTypeStr);
				this.reader.Skip();
				return this.GetNullObject();
			}

			// Read object
			object result = null;
			try
			{
				// Read the objects body
				result = this.ReadObjectBody(dataType);
			}
			catch (Exception e)
			{
				this.SerializationLog.WriteError("Error reading object: {0}", e is ApplicationException ? e.Message : Log.Exception(e));
				this.reader.Skip();
			}

			return result ?? this.GetNullObject();
		}
		protected abstract object ReadObjectBody(DataType dataType);

		public override void WriteObject(object obj)
		{
			this.WriteObject(obj, "object");
		}
		protected void WriteObject(object obj, string elementName)
		{
			if (!this.CanWrite) throw new InvalidOperationException("Can't write object to a read-only serializer");
			if (this.writer.WriteState == WriteState.Start) this.writer.WriteStartElement("root");
			this.writer.WriteStartElement(elementName);

			// Null? Empty Element.
			if (obj == null)
			{
				this.writer.WriteEndElement();
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
			catch (Exception e)
			{
				// Log the error
				this.SerializationLog.WriteError("Error writing object: {0}", e is ApplicationException ? e.Message : Log.Exception(e));
			}
			finally
			{
				this.writer.WriteEndElement();
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
	}
}
