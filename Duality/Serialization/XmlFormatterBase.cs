﻿using System;
using System.IO;
using System.Xml;

namespace Duality.Serialization
{
	/// <summary>
	/// Base class for Dualitys xml serializers.
	/// </summary>
	public abstract class XmlFormatterBase : Formatter
	{
		/// <summary>
		/// Buffer object for <see cref="Duality.Serialization.ISerializable">custom de/serialization</see>, 
		/// providing read and write functionality.
		/// </summary>
		protected class CustomSerialIO : CustomSerialIOBase<XmlFormatterBase>
		{
			public const string HeaderElement = "header";
			public const string BodyElement = "body";

			private string elementName = "customSerialIO";

			public CustomSerialIO(string elementName)
			{
				this.elementName = elementName;
			}

			/// <summary>
			/// Writes the contained data to the specified serializer.
			/// </summary>
			/// <param name="formatter">The serializer to write data to.</param>
			public override void Serialize(XmlFormatterBase formatter)
			{
				formatter.writer.WriteStartElement(this.elementName);
				try
				{
					foreach (var pair in this.data)
						formatter.WriteObject(pair.Value, pair.Key);
				}
				finally
				{
					formatter.writer.WriteEndElement();
				}
				this.Clear();
			}
			/// <summary>
			/// Reads data from the specified serializer
			/// </summary>
			/// <param name="formatter">The serializer to read data from.</param>
			public override void Deserialize(XmlFormatterBase formatter)
			{
				this.Clear();

				int elementDepth = formatter.ReadUntilElementStart();

				try
				{
					if (!formatter.reader.IsEmptyElement)
					{
						bool scopeChanged;
						string key;
						object value;
						while (true)
						{
							value = formatter.ReadObject(out key, out scopeChanged);
							if (scopeChanged) break;
							else
							{
								this.data.Add(key, value);
							}
						}
					}
				}
				finally
				{
					formatter.ReadUntilElementEnd(elementDepth);
				}
			}
		}

		
		/// <summary>
		/// The <see cref="XmlWriter"/> that is used for serialization.
		/// </summary>
		protected	XmlWriter	writer	= null;
		/// <summary>
		/// The <see cref="XmlReader"/> that is used for deserialization.
		/// </summary>
		protected	XmlReader	reader	= null;
		
		/// <summary>
		/// [GET / SET] The <see cref="XmlWriter"/> that is used for serialization.
		/// </summary>
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
		/// <summary>
		/// [GET / SET] The <see cref="XmlReader"/> that is used for deserialization.
		/// </summary>
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
		/// <summary>
		/// [GET] Can this binary serializer write data?
		/// </summary>
		public bool CanWrite
		{
			get { return this.writer != null; }
		}
		/// <summary>
		/// [GET] Can this binary serializer read data?
		/// </summary>
		public bool CanRead
		{
			get { return this.reader != null; }
		}

		protected XmlFormatterBase() : this(null) {}
		protected XmlFormatterBase(Stream stream)
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

			base.Dispose(manually);
		}
		

		/// <summary>
		/// Reads an object including all referenced objects using the <see cref="ReadTarget"/>.
		/// </summary>
		/// <returns>The object that has been read.</returns>
		public override object ReadObject()
		{
			string objName;
			bool scopeChanged;
			object obj;

			do
			{
				obj = this.ReadObject(out objName, out scopeChanged);
			} while (scopeChanged);

			return obj;
		}
		protected object ReadObject(out string objName, out bool scopeChanged)
		{
			if (!this.CanRead) throw new InvalidOperationException("Can't read object from a write-only serializer.");
			if (this.reader.ReadState == ReadState.EndOfFile) throw new EndOfStreamException("No more data to read.");
			if (this.reader.ReadState == ReadState.Error) throw new EndOfStreamException("An XML Error occurred.");
			if (this.reader.ReadState == ReadState.Initial)
			{
				this.reader.Read();
				this.reader.MoveToContent();
			}


			int elementDepth = this.ReadUntilElementStart();
			objName = this.GetCodeElementName(this.reader.Name);
			scopeChanged = elementDepth == -1;

			// Moved outside of current scope? Return null
			if (scopeChanged)
			{
				this.ReadUntilElementEnd(elementDepth);
				return this.GetNullObject();
			}

			// Empty element without type data? Return null
			if (this.reader.IsEmptyElement && !this.reader.HasAttributes)
			{
				this.ReadUntilElementEnd(elementDepth);
				return this.GetNullObject();
			}

			// Read data type header
			string dataTypeStr = this.reader.GetAttribute("dataType");
			DataType dataType;
			if (!Enum.TryParse<DataType>(dataTypeStr, out dataType))
			{
				dataType = DataType.Unknown;
				this.SerializationLog.WriteError("Can't resolve DataType: {0}. Returning null reference.", dataTypeStr);
				this.ReadUntilElementEnd(elementDepth);
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
			}
			finally
			{
				this.ReadUntilElementEnd(elementDepth);
			}

			return result ?? this.GetNullObject();
		}
		/// <summary>
		/// Reads the body of an object.
		/// </summary>
		/// <param name="dataType">The <see cref="Duality.Serialization.DataType"/> that is assumed.</param>
		/// <returns>The object that has been read.</returns>
		protected abstract object ReadObjectBody(DataType dataType);
		/// <summary>
		/// Reads a single primitive value, assuming the specified <see cref="Duality.Serialization.DataType"/>.
		/// </summary>
		/// <param name="dataType"></param>
		/// <returns></returns>
		protected object ReadPrimitive(DataType dataType)
		{
			string val = this.reader.ReadString();
			switch (dataType)
			{
				case DataType.Bool:			return XmlConvert.ToBoolean(val);
				case DataType.Byte:			return XmlConvert.ToByte(val);
				case DataType.SByte:		return XmlConvert.ToSByte(val);
				case DataType.Short:		return XmlConvert.ToInt16(val);
				case DataType.UShort:		return XmlConvert.ToUInt16(val);
				case DataType.Int:			return XmlConvert.ToInt32(val);
				case DataType.UInt:			return XmlConvert.ToUInt32(val);
				case DataType.Long:			return XmlConvert.ToInt64(val);
				case DataType.ULong:		return XmlConvert.ToUInt64(val);
				case DataType.Float:		return XmlConvert.ToSingle(val);
				case DataType.Double:		return XmlConvert.ToDouble(val);
				case DataType.Decimal:		return XmlConvert.ToDecimal(val);
				case DataType.Char:			return XmlConvert.ToChar(val);
				default:
					throw new ArgumentException(string.Format("DataType '{0}' is not a primitive.", dataType));
			}
		}

		protected int ReadUntilElementStart()
		{
			int oldDepth = this.reader.Depth;
			while (this.reader.Read())
			{
				if (this.reader.Depth < oldDepth) return -1;
				if (this.reader.NodeType == XmlNodeType.Element) return this.reader.Depth;
			}
			return -1;
		}
		protected void ReadUntilElementEnd(int depth)
		{
			if (depth == -1) return;
			if (this.reader.IsEmptyElement && this.reader.Depth == depth) return;
			do
			{
				if (this.reader.NodeType == XmlNodeType.EndElement && this.reader.Depth == depth) return;
			} while (this.reader.Read());
		}
		

		/// <summary>
		/// Writes the specified object including all referenced objects using the <see cref="WriteTarget"/>.
		/// </summary>
		/// <param name="obj">The object to write.</param>
		public override void WriteObject(object obj)
		{
			this.WriteObject(obj, "object");
		}
		protected void WriteObject(object obj, string elementName)
		{
			if (!this.CanWrite) throw new InvalidOperationException("Can't write object to a read-only serializer");
			if (this.writer.WriteState == WriteState.Start) this.writer.WriteStartElement("root");
			elementName = this.GetXmlElementName(elementName);
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
			
			// Unknown DataType? Empty Element.
			if (dataType == DataType.Unknown)
			{
				this.writer.WriteEndElement();
				return;
			}

			// Write data type header
			this.writer.WriteAttributeString("dataType", dataType.ToString());

			// Write object
			try 
			{
				this.idManager.PushIdLevel();
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
				this.idManager.PopIdLevel();
			}
		}
		/// <summary>
		/// Writes the body of a given object.
		/// </summary>
		/// <param name="dataType">The <see cref="Duality.Serialization.DataType"/> as which the object will be written.</param>
		/// <param name="obj">The object to be written.</param>
		/// <param name="objSerializeType">The <see cref="Duality.Serialization.SerializeType"/> that describes the specified object.</param>
		/// <param name="objId">An object id that is assigned to the specified object.</param>
		protected abstract void WriteObjectBody(DataType dataType, object obj, SerializeType objSerializeType, uint objId);
		/// <summary>
		/// Writes a single primitive value.
		/// </summary>
		/// <param name="obj">The primitive value to write.</param>
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


		/// <summary>
		/// Returns whether the specified stream is an XML stream. The check requires a stream that is both readable and seekable.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		[System.Diagnostics.DebuggerStepThrough]
		public static bool IsXmlStream(Stream stream)
		{
			if (!stream.CanRead) throw new InvalidOperationException("The specified stream is not readable.");
			if (!stream.CanSeek) throw new InvalidOperationException("The specified stream is not seekable. XML check aborted to maintain stream state.");
			if (stream.Length == 0) throw new InvalidOperationException("The specified stream is empty.");
			long oldPos = stream.Position;

			bool isXml = true;
			var xmlSettings = new XmlReaderSettings();
			xmlSettings.CloseInput = false;
			xmlSettings.IgnoreComments = true;
			xmlSettings.IgnoreWhitespace = true;
			xmlSettings.IgnoreProcessingInstructions = true;
			try
			{
				using (XmlReader xmlRead = XmlReader.Create(stream, xmlSettings))
				{
					xmlRead.Read();
				}
			} catch (Exception) { isXml = false; }
			stream.Seek(oldPos, SeekOrigin.Begin);

			return isXml;
		}

		protected byte[] StringToByteArray(string str)
		{
			return Convert.FromBase64String(str);
		}
		protected string ByteArrayToString(byte[] arr)
		{
			return Convert.ToBase64String(arr, Base64FormattingOptions.None);
		}

		protected string GetXmlElementName(string codeName)
		{
			return codeName.Replace("<", "__sbo__").Replace(">", "__sbc__");
		}
		protected string GetCodeElementName(string xmlName)
		{
			return xmlName.Replace("__sbo__", "<").Replace("__sbc__", ">");
		}
	}
}
