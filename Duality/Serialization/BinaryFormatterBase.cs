using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	public abstract class BinaryFormatterBase : IDisposable
	{
		protected enum Operation
		{
			None,

			Read,
			Write
		}

		protected	const	string	HeaderId	= "BinaryFormatterHeader";
		protected	const	ushort	Version		= 1;

		// General fields
		protected	BinaryWriter	writer;
		protected	BinaryReader	reader;
		protected	bool			disposed;
		// Temporary, "stream operation"-specific data
		protected	ushort								dataVersion			= 0;
		protected	Operation							lastOperation		= Operation.None;
		protected	Stack<long>							offsetStack			= new Stack<long>();
		protected	Dictionary<string,TypeDataLayout>	typeDataLayout		= new Dictionary<string,TypeDataLayout>();
		protected	Dictionary<string,long>				typeDataLayoutMap	= new Dictionary<string,long>();
		protected	Dictionary<object,uint>				objRefIdMap			= new Dictionary<object,uint>();
		protected	Dictionary<uint,object>				idObjRefMap			= new Dictionary<uint,object>();
		protected	uint								idCounter			= 0;


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
		public bool CanWrite
		{
			get { return this.writer != null; }
		}
		public bool CanRead
		{
			get { return this.reader != null; }
		}
		public bool Disposed
		{
			get { return this.disposed; }
		}


		public BinaryFormatterBase() 
		{
			this.WriteTarget = null;
			this.ReadTarget = null;
		}
		public BinaryFormatterBase(Stream stream)
		{
			this.WriteTarget = stream.CanWrite ? new BinaryWriter(stream) : null;
			this.ReadTarget = stream.CanRead ? new BinaryReader(stream) : null;
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
				this.writer.Dispose();
				this.writer = null;
			}

			if (this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		
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
			if (!isNotNull) return null;

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
				Log.Core.WriteError("Error reading object at '{0:X8}'-'{1:X8}':\n{2}", lastPos, lastPos + offset, e.ToString());
			}

			return result;
		}
		protected abstract object ReadObjectBody(DataType dataType);

		public bool IsTypeDataLayoutCached(string t)
		{
			return this.typeDataLayout.ContainsKey(t);
		}
		protected TypeDataLayout ReadTypeDataLayout(Type t)
		{
			return this.ReadTypeDataLayout(ReflectionHelper.GetCachedType(t).TypeString);
		}
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
					Log.Core.WriteError("Error reading header at '{0:X8}'-'{1:X8}':\n{2}", lastPos, lastPos + offset, e.ToString());
				}
			}
			catch (Exception e) 
			{
				this.reader.BaseStream.Seek(initialPos, SeekOrigin.Begin);
				Log.Core.WriteError("Error reading header: {0}", e);
			}
		}
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

		protected void ReadArrayData(bool[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadBoolean();
		}
		protected void ReadArrayData(byte[] obj)
		{
			this.reader.Read(obj, 0, obj.Length);
		}
		protected void ReadArrayData(sbyte[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadSByte();
		}
		protected void ReadArrayData(short[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadInt16();
		}
		protected void ReadArrayData(ushort[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadUInt16();
		}
		protected void ReadArrayData(int[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadInt32();
		}
		protected void ReadArrayData(uint[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadUInt32();
		}
		protected void ReadArrayData(long[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadInt64();
		}
		protected void ReadArrayData(ulong[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadUInt64();
		}
		protected void ReadArrayData(float[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadSingle();
		}
		protected void ReadArrayData(double[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadDouble();
		}
		protected void ReadArrayData(decimal[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadDecimal();
		}
		protected void ReadArrayData(char[] obj)
		{
			for (int l = 0; l < obj.Length; l++) obj[l] = this.reader.ReadChar();
		}
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
			CachedType objCachedType;
			uint objId;
			DataType dataType;
			this.GetWriteObjectData(obj, out objCachedType, out dataType, out objId);

			// Write data type header
			this.WriteDataType(dataType);
			this.WritePushOffset();
			try
			{
				// Write object
				this.WriteObjectBody(dataType, obj, objCachedType, objId);
			}
			finally
			{
				// Write object footer
				this.WritePopOffset();
			}
		}
		protected abstract void GetWriteObjectData(object obj, out CachedType objCachedType, out DataType dataType, out uint objId);
		protected abstract void WriteObjectBody(DataType dataType, object obj, CachedType objCachedType, uint objId);

		protected void WriteFormatterHeader()
		{
			this.writer.Write(HeaderId);
			this.writer.Write(Version);
			this.WritePushOffset();

			// --[ Insert writing additional header data here ]--

			this.WritePopOffset();
		}
		protected void WriteTypeDataLayout(CachedType objCachedType)
		{
			if (this.typeDataLayout.ContainsKey(objCachedType.TypeString))
			{
				long backRef = this.typeDataLayoutMap[objCachedType.TypeString];
				this.writer.Write(backRef);
				return;
			}

			this.WriteTypeDataLayout(new TypeDataLayout(objCachedType), objCachedType.TypeString);
		}
		protected void WriteTypeDataLayout(string typeString)
		{
			if (this.typeDataLayout.ContainsKey(typeString))
			{
				long backRef = this.typeDataLayoutMap[typeString];
				this.writer.Write(backRef);
				return;
			}

			Type resolved = ReflectionHelper.ResolveType(typeString, false);
			CachedType cached = resolved != null ? ReflectionHelper.GetCachedType(resolved) : null;
			TypeDataLayout layout = cached != null ? new TypeDataLayout(cached) : null;
			this.WriteTypeDataLayout(layout, typeString);
		}
		protected void WriteTypeDataLayout(TypeDataLayout layout, string typeString)
		{
			this.typeDataLayout[typeString] = layout;
			this.writer.Write(-1L);
			this.typeDataLayoutMap[typeString] = this.writer.BaseStream.Position;
			layout.Write(this.writer);
		}
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

		protected void WriteArrayData(bool[] obj)
		{
			for (long l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(byte[] obj)
		{
			this.writer.Write(obj);
		}
		protected void WriteArrayData(sbyte[] obj)
		{
			for (long l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(short[] obj)
		{
			for (long l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(ushort[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(int[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(uint[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(long[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(ulong[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(float[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(double[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(decimal[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
		protected void WriteArrayData(char[] obj)
		{
			for (int l = 0; l < obj.Length; l++) this.writer.Write(obj[l]);
		}
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

		
		protected void WriteDataType(DataType dt)
		{
			this.writer.Write((ushort)dt);
		}
		protected DataType ReadDataType()
		{
			return (DataType)this.reader.ReadUInt16();
		}
		protected void WritePushOffset()
		{
			this.offsetStack.Push(this.writer.BaseStream.Position);
			this.writer.Write(0L);
		}
		protected void WritePopOffset()
		{
			long curPos = this.writer.BaseStream.Position;
			long lastPos = this.offsetStack.Pop();
			long offset = curPos - lastPos;
			
			this.writer.BaseStream.Seek(lastPos, SeekOrigin.Begin);
			this.writer.Write(offset);
			this.writer.BaseStream.Seek(curPos, SeekOrigin.Begin);
		}

		protected void ClearStreamSpecificData()
		{
			this.typeDataLayout.Clear();
			this.typeDataLayoutMap.Clear();
			this.offsetStack.Clear();
			this.objRefIdMap.Clear();
			this.idObjRefMap.Clear();
			this.idCounter = 0;
		}
		protected uint GetIdFromObject(object obj)
		{
			uint id;
			if (this.objRefIdMap.TryGetValue(obj, out id)) return id;

			id = ++idCounter;
			this.objRefIdMap[obj] = id;
			this.idObjRefMap[id] = obj;

			return id;
		}
	}
}
