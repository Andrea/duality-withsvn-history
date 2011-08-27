using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	/// <summary>
	/// Base class for Dualitys binary serializers.
	/// </summary>
	public abstract class BinaryFormatterBase : IDisposable
	{
		/// <summary>
		/// Buffer object for <see cref="Duality.Serialization.ISerializable">custom de/serialization</see>, 
		/// providing read and write functionality.
		/// </summary>
		protected class CustomSerialIO : IDataReader, IDataWriter
		{
			private	Dictionary<string,object>	values;

			/// <summary>
			/// [GET] Enumerates all currently stored <see cref="System.Collections.Generic.KeyValuePair{T,U}">KeyValuePairs</see>.
			/// </summary>
			public IEnumerable<KeyValuePair<string,object>> Values
			{
				get { return this.values; }
			}

			public CustomSerialIO()
			{
				this.values = new Dictionary<string,object>();
			}

			/// <summary>
			/// Writes the contained data to the specified serializer.
			/// </summary>
			/// <param name="formatter">The serializer to write data to.</param>
			public void Serialize(BinaryFormatterBase formatter)
			{
				formatter.WritePrimitive(this.values.Count);
				foreach (var pair in this.values)
				{
					formatter.WriteString(pair.Key);
					formatter.WriteObject(pair.Value);
				}
				this.Clear();
			}
			/// <summary>
			/// Reads data from the specified serializer
			/// </summary>
			/// <param name="formatter">The serializer to read data from.</param>
			public void Deserialize(BinaryFormatterBase formatter)
			{
				this.Clear();
				int count = (int)formatter.ReadPrimitive(DataType.Int);
				for (int i = 0; i < count; i++)
				{
					string key = formatter.ReadString();
					object value = formatter.ReadObject();
					this.values.Add(key, value);
				}
			}
			/// <summary>
			/// Clears all contained data.
			/// </summary>
			public void Clear()
			{
				this.values.Clear();
			}
			
			/// <summary>
			/// Writes the specified name and value.
			/// </summary>
			/// <param name="name">
			/// The name to which the written value is mapped. 
			/// May, for example, be the name of a <see cref="System.Reflection.FieldInfo">Field</see>
			/// to which the written value belongs, but there are no naming restrictions, except that one name can't be used twice.
			/// </param>
			/// <param name="value">The value to write.</param>
			/// <seealso cref="IDataWriter"/>
			public void WriteValue(string name, object value)
			{
				this.values[name] = value;
			}
			/// <summary>
			/// Reads the value that is associated with the specified name.
			/// </summary>
			/// <param name="name">The name that is used for retrieving the value.</param>
			/// <returns>The value that has been read using the given name.</returns>
			/// <seealso cref="IDataReader"/>
			/// <seealso cref="ReadValue{T}(string)"/>
			/// <seealso cref="ReadValue{T}(string, out T)"/>
			public object ReadValue(string name)
			{
				object result;
				if (this.values.TryGetValue(name, out result))
					return result;
				else
					return null;
			}
			/// <summary>
			/// Reads the value that is associated with the specified name.
			/// </summary>
			/// <typeparam name="T">The expected value type.</typeparam>
			/// <param name="name">The name that is used for retrieving the value.</param>
			/// <returns>The value that has been read and cast using the given name and type.</returns>
			/// <seealso cref="IDataReader"/>
			/// <seealso cref="ReadValue(string)"/>
			/// <seealso cref="ReadValue{T}(string, out T)"/>
			public T ReadValue<T>(string name)
			{
				object read = this.ReadValue(name);
				if (read is T)
					return (T)read;
				else
					return (T)Convert.ChangeType(read, typeof(T));
			}
			/// <summary>
			/// Reads the value that is associated with the specified name.
			/// </summary>
			/// <typeparam name="T">The expected value type.</typeparam>
			/// <param name="name">The name that is used for retrieving the value.</param>
			/// <param name="value">The value that has been read and cast using the given name and type.</param>
			/// <seealso cref="IDataReader"/>
			/// <seealso cref="ReadValue(string)"/>
			/// <seealso cref="ReadValue{T}(string)"/>
			public void ReadValue<T>(string name, out T value)
			{
				value = this.ReadValue<T>(name);
			}
		}
		/// <summary>
		/// Operations, the binary serializer is able to perform.
		/// </summary>
		protected enum Operation
		{
			/// <summary>
			/// No operation.
			/// </summary>
			None,

			/// <summary>
			/// Read a dataset / object
			/// </summary>
			Read,
			/// <summary>
			/// Write a dataset / object
			/// </summary>
			Write
		}

		/// <summary>
		/// Binary serialization header id. 
		/// </summary>
		protected	const	string	HeaderId	= "BinaryFormatterHeader";
		/// <summary>
		/// Binary serialization version number.
		/// </summary>
		protected	const	ushort	Version		= 1;


		/// <summary>
		/// The <see cref="BinaryWriter"/> that is used for serialization.
		/// </summary>
		protected	BinaryWriter	writer		= null;
		/// <summary>
		/// The <see cref="BinaryReader"/> that is used for deserialization.
		/// </summary>
		protected	BinaryReader	reader		= null;
		/// <summary>
		/// The de/serialization <see cref="Duality.Log"/>.
		/// </summary>
		protected	Log				log			= Log.Core;
		/// <summary>
		/// A counter for the number of ids that have been used during the current <see cref="Operation"/>.
		/// </summary>
		protected	uint			idCounter	= 0;
		/// <summary>
		/// A dictionary that maps objects to their object ids.
		/// </summary>
		protected	Dictionary<object,uint>	objRefIdMap	= new Dictionary<object,uint>();
		/// <summary>
		/// A dictionary that maps object ids to their objects.
		/// </summary>
		protected	Dictionary<uint,object>	idObjRefMap	= new Dictionary<uint,object>();

		private		bool								disposed			= false;
		private		ushort								dataVersion			= 0;
		private		Operation							lastOperation		= Operation.None;
		private		Stack<long>							offsetStack			= new Stack<long>();
		private		Dictionary<string,TypeDataLayout>	typeDataLayout		= new Dictionary<string,TypeDataLayout>();
		private		Dictionary<string,long>				typeDataLayoutMap	= new Dictionary<string,long>();

		
		/// <summary>
		/// [GET / SET] The <see cref="BinaryWriter"/> that is used for serialization.
		/// </summary>
		public BinaryWriter WriteTarget
		{
			get { return this.writer; }
			set
			{
				if (this.writer == value) return;
				this.writer = value;

				if (this.writer != null && !this.writer.BaseStream.CanSeek) throw new ArgumentException("Cannot use a WriteTarget without seeking capability.");

				// We're switching the stream, so we should discard all stream-specific temporary / cache data
				this.lastOperation = Operation.None;
			}
		}
		/// <summary>
		/// [GET / SET] The <see cref="BinaryReader"/> that is used for deserialization.
		/// </summary>
		public BinaryReader ReadTarget
		{
			get { return this.reader; }
			set
			{
				if (this.reader == value) return;
				this.reader = value;

				if (this.reader != null && !this.reader.BaseStream.CanSeek) throw new ArgumentException("Cannot use a ReadTarget without seeking capability.");

				// We're switching the stream, so we should discard all stream-specific temporary / cache data
				this.lastOperation = Operation.None;
			}
		}
		/// <summary>
		/// [GET / SET] The de/serialization <see cref="Duality.Log"/>.
		/// </summary>
		public Log SerializationLog
		{
			get { return this.log; }
			set { this.log = value ?? new Log(); }
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
		/// <summary>
		/// [GET] Whether this binary serializer has been disposed. A disposed object cannot be used anymore.
		/// </summary>
		public bool Disposed
		{
			get { return this.disposed; }
		}


		public BinaryFormatterBase() : this(null) {}
		public BinaryFormatterBase(Stream stream)
		{
			this.WriteTarget = (stream != null && stream.CanWrite) ? new BinaryWriter(stream) : null;
			this.ReadTarget = (stream != null && stream.CanRead) ? new BinaryReader(stream) : null;
		}
		~BinaryFormatterBase()
		{
			this.Dispose(false);
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}
		protected virtual void Dispose(bool manually)
		{
			if (this.disposed) return;

			if (this.writer != null)
			{
				//this.writer.Dispose();
				this.writer = null;
			}

			if (this.reader != null)
			{
				//this.reader.Dispose();
				this.reader = null;
			}
		}

		
		/// <summary>
		/// Reads an object including all referenced objects using the <see cref="ReadTarget"/>.
		/// </summary>
		/// <returns>The object that has been read.</returns>
		protected object ReadObject()
		{
			if (!this.CanRead) return null;
			if (this.reader.BaseStream.Position == this.reader.BaseStream.Length) throw new EndOfStreamException("No more data to read.");
			if (this.lastOperation != Operation.Read)
			{
				this.ClearStreamSpecificData();
				this.ReadFormatterHeader();
			}
			this.lastOperation = Operation.Read;

			// Not null flag
			bool isNotNull = this.reader.ReadBoolean();
			if (!isNotNull) return this.GetNullObject();

			// Read data type header
			DataType dataType = this.ReadDataType();
			long lastPos = this.reader.BaseStream.Position;
			long offset = this.reader.ReadInt64();

			// Read object
			object result = null;
			try
			{
				// Read the objects body
				result = this.ReadObjectBody(dataType);

				// If we read the object properly and aren't where we're supposed to be, something went wrong
				if (this.reader.BaseStream.Position != lastPos + offset) throw new ApplicationException(string.Format("Wrong dataset offset: '{0}' instead of expected value '{1}'.", this.reader.BaseStream.Position - lastPos, offset));
			}
			catch (Exception e)
			{
				// If anything goes wrong, assure the stream position is valid and points to the next data entry
				this.reader.BaseStream.Seek(lastPos + offset, SeekOrigin.Begin);
				// Log the error
				this.log.WriteError("Error reading object at '{0:X8}'-'{1:X8}': {2}", 
					lastPos,
					lastPos + offset, 
					e is ApplicationException ? e.Message : Log.Exception(e));
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
		/// Returns an object indicating a "null" value.
		/// </summary>
		/// <returns></returns>
		protected virtual object GetNullObject() 
		{
			return null;
		}

		/// <summary>
		/// Returns whether the <see cref="Duality.Serialization.TypeDataLayout"/> for the specified <see cref="System.Type"/> is
		/// already present in the binary serializers internal cache.
		/// </summary>
		/// <param name="t">A string referring to the <see cref="System.Type"/> that is described by the 
		/// <see cref="Duality.Serialization.TypeDataLayout"/> in question.</param>
		/// <returns>True, if the <see cref="Duality.Serialization.TypeDataLayout"/> is already cached, false if not.</returns>
		public bool IsTypeDataLayoutCached(string t)
		{
			return this.typeDataLayout.ContainsKey(t);
		}
		/// <summary>
		/// Reads the <see cref="Duality.Serialization.TypeDataLayout"/> describing the specified <see cref="System.Type"/>.
		/// </summary>
		/// <param name="t">The <see cref="System.Type"/> of which to read the <see cref="Duality.Serialization.TypeDataLayout"/>.</param>
		/// <returns>A <see cref="Duality.Serialization.TypeDataLayout"/> describing the specified <see cref="System.Type"/></returns>
		protected TypeDataLayout ReadTypeDataLayout(Type t)
		{
			return this.ReadTypeDataLayout(ReflectionHelper.GetSerializeType(t).TypeString);
		}
		/// <summary>
		/// Reads the <see cref="Duality.Serialization.TypeDataLayout"/> describing the specified <see cref="System.Type"/>.
		/// </summary>
		/// <param name="t">A string referring to the <see cref="System.Type"/> of which to read the <see cref="Duality.Serialization.TypeDataLayout"/>.</param>
		/// <returns>A <see cref="Duality.Serialization.TypeDataLayout"/> describing the specified <see cref="System.Type"/></returns>
		protected TypeDataLayout ReadTypeDataLayout(string t)
		{
			long backRef = this.reader.ReadInt64();

			TypeDataLayout result = null;
			if (this.typeDataLayout.TryGetValue(t, out result) && backRef != -1L) return result;

			long lastPos = this.reader.BaseStream.Position;
			if (backRef != -1L) this.reader.BaseStream.Seek(backRef, SeekOrigin.Begin);
			result = result ?? new TypeDataLayout(this.reader);
			if (backRef != -1L) this.reader.BaseStream.Seek(lastPos, SeekOrigin.Begin);

			this.typeDataLayout[t] = result;
			return result;
		}
		/// <summary>
		/// Reads the binary serialization header.
		/// </summary>
		protected void ReadFormatterHeader()
		{
			long initialPos = this.reader.BaseStream.Position;
			try
			{
				string headerId = this.reader.ReadString();
				if (headerId != HeaderId) throw new ApplicationException("Header ID does not match.");
				this.dataVersion = this.reader.ReadUInt16();

				// Create "Safe zone" for additional data
				long lastPos = this.reader.BaseStream.Position;
				long offset = this.reader.ReadInt64();
				try
				{
					// --[ Insert reading additional data here ]--

					// If we read the object properly and aren't where we're supposed to be, something went wrong
					if (this.reader.BaseStream.Position != lastPos + offset) throw new ApplicationException(string.Format("Wrong dataset offset: '{0}' instead of expected value '{1}'.", offset, this.reader.BaseStream.Position - lastPos));
				}
				catch (Exception e)
				{
					// If anything goes wrong, assure the stream position is valid and points to the next data entry
					this.reader.BaseStream.Seek(lastPos + offset, SeekOrigin.Begin);
					this.log.WriteError("Error reading header at '{0:X8}'-'{1:X8}': {2}", lastPos, lastPos + offset, Log.Exception(e));
				}
			}
			catch (Exception e) 
			{
				this.reader.BaseStream.Seek(initialPos, SeekOrigin.Begin);
				this.log.WriteError("Error reading header: {0}", Log.Exception(e));
			}
		}
		/// <summary>
		/// Reads a single primitive value, assuming the specified <see cref="Duality.Serialization.DataType"/>.
		/// </summary>
		/// <param name="dataType"></param>
		/// <returns></returns>
		protected object ReadPrimitive(DataType dataType)
		{
			switch (dataType)
			{
				case DataType.Bool:			return this.reader.ReadBoolean();
				case DataType.Byte:			return this.reader.ReadByte();
				case DataType.SByte:		return this.reader.ReadSByte();
				case DataType.Short:		return this.reader.ReadInt16();
				case DataType.UShort:		return this.reader.ReadUInt16();
				case DataType.Int:			return this.reader.ReadInt32();
				case DataType.UInt:			return this.reader.ReadUInt32();
				case DataType.Long:			return this.reader.ReadInt64();
				case DataType.ULong:		return this.reader.ReadUInt64();
				case DataType.Float:		return this.reader.ReadSingle();
				case DataType.Double:		return this.reader.ReadDouble();
				case DataType.Decimal:		return this.reader.ReadDecimal();
				case DataType.Char:			return this.reader.ReadChar();
				default:
					throw new ArgumentException(string.Format("DataType '{0}' is not a primitive.", dataType));
			}
		}
		/// <summary>
		/// Reads a single string value.
		/// </summary>
		/// <returns></returns>
		protected string ReadString()
		{
			return this.reader.ReadString();
		}

		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(bool[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadBoolean();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(byte[] obj)
		{
			this.reader.Read(obj, 0, obj.Length);
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(sbyte[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadSByte();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(short[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadInt16();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(ushort[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadUInt16();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(int[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadInt32();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(uint[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadUInt32();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(long[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadInt64();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(ulong[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadUInt64();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(float[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadSingle();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(double[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadDouble();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(decimal[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadDecimal();
		}
		/// <summary>
		/// Reads a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(char[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadChar();
		}
		/// <summary>
		/// Reads a string array.
		/// </summary>
		/// <param name="obj"></param>
		protected void ReadArrayData(string[] obj)
		{
			for (int l = 0; l < obj.Length; l++)
			{
				bool isNotNull = this.reader.ReadBoolean();
				if (isNotNull)
					obj[l] = this.reader.ReadString();
				else
					obj[l] = null;
			}
		}
		
		/// <summary>
		/// Writes the specified object including all referenced objects using the <see cref="WriteTarget"/>.
		/// </summary>
		/// <param name="obj">The object to write.</param>
		protected void WriteObject(object obj)
		{
			if (!this.CanWrite) return;
			if (this.lastOperation != Operation.Write)
			{
				this.ClearStreamSpecificData();
				this.WriteFormatterHeader();
			}
			this.lastOperation = Operation.Write;

			// NotNull flag
			if (obj == null)
			{
				this.writer.Write(false);
				return;
			}
			else
				this.writer.Write(true);
			
			// Retrieve type data
			SerializeType objSerializeType;
			uint objId;
			DataType dataType;
			this.GetWriteObjectData(obj, out objSerializeType, out dataType, out objId);

			// Write data type header
			this.WriteDataType(dataType);
			this.WritePushOffset();
			try
			{
				// Write object
				this.WriteObjectBody(dataType, obj, objSerializeType, objId);
			}
			finally
			{
				// Write object footer
				this.WritePopOffset();
			}
		}
		/// <summary>
		/// Determines internal data for writing a given object.
		/// </summary>
		/// <param name="obj">The object to write</param>
		/// <param name="objSerializeType">The <see cref="Duality.Serialization.SerializeType"/> that describes the specified object.</param>
		/// <param name="dataType">The <see cref="Duality.Serialization.DataType"/> that is used for writing the specified object.</param>
		/// <param name="objId">An object id that is assigned to the specified object.</param>
		protected abstract void GetWriteObjectData(object obj, out SerializeType objSerializeType, out DataType dataType, out uint objId);
		/// <summary>
		/// Writes the body of a given object.
		/// </summary>
		/// <param name="dataType">The <see cref="Duality.Serialization.DataType"/> as which the object will be written.</param>
		/// <param name="obj">The object to be written.</param>
		/// <param name="objSerializeType">The <see cref="Duality.Serialization.SerializeType"/> that describes the specified object.</param>
		/// <param name="objId">An object id that is assigned to the specified object.</param>
		protected abstract void WriteObjectBody(DataType dataType, object obj, SerializeType objSerializeType, uint objId);
		
		/// <summary>
		/// Writes the binary serialization header.
		/// </summary>
		protected void WriteFormatterHeader()
		{
			this.writer.Write(HeaderId);
			this.writer.Write(Version);
			this.WritePushOffset();

			// --[ Insert writing additional header data here ]--

			this.WritePopOffset();
		}
		/// <summary>
		/// Writes the <see cref="Duality.Serialization.TypeDataLayout"/> describing the specified <see cref="Duality.Serialization.SerializeType"/>.
		/// Note that this method does not write redundant layout data - if the specified TypeDataLayout has already been written withing the same
		/// operation, a back-reference is written instead.
		/// </summary>
		/// <param name="objSerializeType">
		/// The <see cref="Duality.Serialization.SerializeType"/> of which to write the <see cref="Duality.Serialization.TypeDataLayout"/>
		/// </param>
		/// <seealso cref="WriteTypeDataLayout(string)"/>
		/// <seealso cref="WriteTypeDataLayout(TypeDataLayout, string)"/>
		protected void WriteTypeDataLayout(SerializeType objSerializeType)
		{
			if (this.typeDataLayout.ContainsKey(objSerializeType.TypeString))
			{
				long backRef = this.typeDataLayoutMap[objSerializeType.TypeString];
				this.writer.Write(backRef);
				return;
			}

			this.WriteTypeDataLayout(new TypeDataLayout(objSerializeType), objSerializeType.TypeString);
		}
		/// <summary>
		/// Writes the <see cref="Duality.Serialization.TypeDataLayout"/> describing the specified <see cref="Duality.Serialization.SerializeType"/>.
		/// Note that this method does not write redundant layout data - if the specified TypeDataLayout has already been written withing the same
		/// operation, a back-reference is written instead.
		/// </summary>
		/// <param name="typeString">
		/// A string referring to the <see cref="System.Type"/> of which to write the <see cref="Duality.Serialization.TypeDataLayout"/>.
		/// </param>
		/// <seealso cref="WriteTypeDataLayout(SerializeType)"/>
		/// <seealso cref="WriteTypeDataLayout(TypeDataLayout, string)"/>
		protected void WriteTypeDataLayout(string typeString)
		{
			if (this.typeDataLayout.ContainsKey(typeString))
			{
				long backRef = this.typeDataLayoutMap[typeString];
				this.writer.Write(backRef);
				return;
			}

			Type resolved = ReflectionHelper.ResolveType(typeString, false);
			SerializeType cached = resolved != null ? ReflectionHelper.GetSerializeType(resolved) : null;
			TypeDataLayout layout = cached != null ? new TypeDataLayout(cached) : null;
			this.WriteTypeDataLayout(layout, typeString);
		}
		/// <summary>
		/// Writes the specified <see cref="Duality.Serialization.TypeDataLayout"/> describing the <see cref="System.Type"/> referred to by
		/// the given <see cref="ReflectionHelper.GetTypeName">type string</see>. Doesn't care about redundant data, writes always.
		/// </summary>
		/// <param name="layout">The <see cref="Duality.Serialization.TypeDataLayout"/> to write.</param>
		/// <param name="typeString">
		/// The <see cref="ReflectionHelper.GetTypeName">type string</see> that refers to the <see cref="System.Type"/> that
		/// is described by the specified <see cref="Duality.Serialization.TypeDataLayout"/>.
		/// </param>
		/// <seealso cref="WriteTypeDataLayout(string)"/>
		/// <seealso cref="WriteTypeDataLayout(SerializeType)"/>
		protected void WriteTypeDataLayout(TypeDataLayout layout, string typeString)
		{
			this.typeDataLayout[typeString] = layout;
			this.writer.Write(-1L);
			this.typeDataLayoutMap[typeString] = this.writer.BaseStream.Position;
			layout.Write(this.writer);
		}
		/// <summary>
		/// Writes a single primitive value.
		/// </summary>
		/// <param name="obj">The primitive value to write.</param>
		protected void WritePrimitive(object obj)
		{
			if		(obj is bool)		this.writer.Write((bool)obj);
			else if (obj is byte)		this.writer.Write((byte)obj);
			else if (obj is char)		this.writer.Write((char)obj);
			else if (obj is sbyte)		this.writer.Write((sbyte)obj);
			else if (obj is short)		this.writer.Write((short)obj);
			else if (obj is ushort)		this.writer.Write((ushort)obj);
			else if (obj is int)		this.writer.Write((int)obj);
			else if (obj is uint)		this.writer.Write((uint)obj);
			else if (obj is long)		this.writer.Write((long)obj);
			else if (obj is ulong)		this.writer.Write((ulong)obj);
			else if (obj is float)		this.writer.Write((float)obj);
			else if (obj is double)		this.writer.Write((double)obj);
			else if (obj is decimal)	this.writer.Write((decimal)obj);
			else if (obj == null)
				throw new ArgumentNullException("obj");
			else
				throw new ArgumentException(string.Format("Type '{0}' is not a primitive.", obj.GetType()));
		}
		/// <summary>
		/// Writes a single string value.
		/// </summary>
		/// <param name="obj">The string value to write.</param>
		protected void WriteString(string obj)
		{
			this.writer.Write(obj);
		}

		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(bool[] obj)
		{
			for (long l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(byte[] obj)
		{
			this.writer.Write(obj);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(sbyte[] obj)
		{
			for (long l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(short[] obj)
		{
			for (long l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(ushort[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(int[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(uint[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(long[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(ulong[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(float[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(double[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(decimal[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a primitive array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(char[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		/// <summary>
		/// Writes a string array.
		/// </summary>
		/// <param name="obj"></param>
		protected void WriteArrayData(string[] obj)
		{
			for (int l = 0; l < obj.Length; l++)
			{
				if (obj[l] == null)
					this.writer.Write(false);
				else
				{
					this.writer.Write(true);
					this.writer.Write(obj[l]);
				}
			}
		}

		
		/// <summary>
		/// Writes a <see cref="Duality.Serialization.DataType"/>.
		/// </summary>
		/// <param name="dt"></param>
		protected void WriteDataType(DataType dt)
		{
			this.writer.Write((ushort)dt);
		}
		/// <summary>
		/// Reads a <see cref="Duality.Serialization.DataType"/>.
		/// </summary>
		/// <returns></returns>
		protected DataType ReadDataType()
		{
			return (DataType)this.reader.ReadUInt16();
		}
		/// <summary>
		/// Writes the begin of a new "safe zone", usually encapsulating a data set.
		/// If any error occurs within a safe zone, it is guaranteed to not affect any other
		/// safe zone, although the damaged safe zone itsself might be unusable. In general,
		/// a safe zone prevents an error from affecting any of the zones surroundings.
		/// Safe zones may also be nested.
		/// </summary>
		/// <seealso cref="WritePopOffset"/>
		protected void WritePushOffset()
		{
			this.offsetStack.Push(this.writer.BaseStream.Position);
			this.writer.Write(0L);
		}
		/// <summary>
		/// Writes the end of the most recent "safe zone", usually encapsulating a data set.
		/// If any error occurs within a safe zone, it is guaranteed to not affect any other
		/// safe zone, although the damaged safe zone itsself might be unusable. In general,
		/// a safe zone prevents an error from affecting any of the zones surroundings.
		/// Safe zones may also be nested.
		/// </summary>
		/// <seealso cref="WritePushOffset"/>
		protected void WritePopOffset()
		{
			long curPos = this.writer.BaseStream.Position;
			long lastPos = this.offsetStack.Pop();
			long offset = curPos - lastPos;
			
			this.writer.BaseStream.Seek(lastPos, SeekOrigin.Begin);
			this.writer.Write(offset);
			this.writer.BaseStream.Seek(curPos, SeekOrigin.Begin);
		}

		/// <summary>
		/// Clears all <see cref="System.IO.Stream"/>- or <see cref="Operation"/>-specific cache data.
		/// </summary>
		protected void ClearStreamSpecificData()
		{
			this.typeDataLayout.Clear();
			this.typeDataLayoutMap.Clear();
			this.offsetStack.Clear();
			this.objRefIdMap.Clear();
			this.idObjRefMap.Clear();
			this.idCounter = 0;
		}
		/// <summary>
		/// Returns the id that is assigned to the specified object. Assigns one, if
		/// there is none yet.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected uint GetIdFromObject(object obj)
		{
			uint id;
			if (this.objRefIdMap.TryGetValue(obj, out id)) return id;

			id = ++idCounter;
			this.objRefIdMap[obj] = id;
			this.idObjRefMap[id] = obj;

			return id;
		}


		/// <summary>
		/// Logs an error that occured during <see cref="Duality.Serialization.ISerializable">custom serialization</see>.
		/// </summary>
		/// <param name="objId">The object id of the affected object.</param>
		/// <param name="serializeType">The <see cref="System.Type"/> of the affected object.</param>
		/// <param name="e">The <see cref="System.Exception"/> that occured.</param>
		protected void LogCustomSerializationError(uint objId, Type serializeType, Exception e)
		{
			this.log.WriteError(
				"An error occured in custom serialization in object Id {0} of type '{1}': {2}",
				objId,
				Log.Type(serializeType),
				Log.Exception(e));
		}
		/// <summary>
		/// Logs an error that occured during <see cref="Duality.Serialization.ISerializable">custom deserialization</see>.
		/// </summary>
		/// <param name="objId">The object id of the affected object.</param>
		/// <param name="serializeType">The <see cref="System.Type"/> of the affected object.</param>
		/// <param name="e">The <see cref="System.Exception"/> that occured.</param>
		protected void LogCustomDeserializationError(uint objId, Type serializeType, Exception e)
		{
			this.log.WriteError(
				"An error occured in custom deserialization in object Id {0} of type '{1}': {2}",
				objId,
				Log.Type(serializeType),
				Log.Exception(e));
		}
		/// <summary>
		/// Logs an error that occured trying to resolve a <see cref="System.Type"/> by its <see cref="ReflectionHelper.GetTypeName">type string</see>.
		/// </summary>
		/// <param name="objId">The object id of the affected object.</param>
		/// <param name="typeString">The <see cref="ReflectionHelper.GetTypeName">type string</see> that couldn't be resolved.</param>
		protected void LogCantResolveTypeError(uint objId, string typeString)
		{
			this.log.WriteError("Can't resolve Type '{0}' in object Id {1}. Type not found.", typeString, objId);
		}
	}
}
