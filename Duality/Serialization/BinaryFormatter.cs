using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	public class BinaryFormatter : IDisposable
	{
		protected enum DataType : byte
		{
			Unknown,

			Bool,
			Byte,
			SByte,
			Short,
			UShort,
			Int,
			UInt,
			Long,
			ULong,
			Float,
			Double,
			Decimal,
			Char,

			String,
			Array,
			Class,
			Struct,

			ObjectRef
		}
		protected class CachedType
		{
			public	Type		type;
			public	FieldInfo[]	fields;
			public	string		typeString;
			public	DataType	dataType;

			public CachedType(Type t)
			{
				this.type = t;
				this.fields = this.type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				this.typeString = ReflectionHelper.GetTypeString(this.type, ReflectionHelper.TypeStringAttrib.CSCodeIdent);
				this.dataType = BinaryFormatter.GetDataType(this.type);
			}
		}
		protected class TypeDataLayout
		{
			public struct FieldDataInfo
			{
				public	string	name;
				public	string	typeString;
			}

			public	FieldDataInfo[]	fields;

			public TypeDataLayout(BinaryReader r)
			{
				int count = r.ReadInt32();
				this.fields = new FieldDataInfo[count];

				for (int i = 0; i < count; i++)
				{
					this.fields[i].name = r.ReadString();
					this.fields[i].typeString = r.ReadString();
				}
			}
			public TypeDataLayout(CachedType t)
			{
				this.fields = new FieldDataInfo[t.fields.Length];
				for (int i = 0; i < t.fields.Length; i++)
				{
					this.fields[i].name = t.fields[i].Name;
					this.fields[i].typeString = ReflectionHelper.GetTypeString(t.fields[i].FieldType, ReflectionHelper.TypeStringAttrib.CSCodeIdent);
				}
			}

			public void Write(BinaryWriter w)
			{
				w.Write(this.fields.Length);
				for (int i = 0; i < this.fields.Length; i++)				
				{
					w.Write(this.fields[i].name);
					w.Write(this.fields[i].typeString);
				}
			}
		}


		// General fields
		protected	BinaryWriter	writer;
		protected	BinaryReader	reader;
		protected	bool			disposed;
		// Reflection data caching
		protected	Dictionary<Type,CachedType>	typeCache			= new Dictionary<Type,CachedType>();
		protected	Dictionary<string,Type>		typeResolveCache	= new Dictionary<string,Type>();
		// Temporary, "stream operation"-specific data
		protected	bool							lastWritten		= false;
		protected	Stack<long>						offsetStack		= new Stack<long>();
		protected	Dictionary<Type,TypeDataLayout>	typeDataLayout	= new Dictionary<Type,TypeDataLayout>();
		protected	Dictionary<object,uint>			objRefIdMap		= new Dictionary<object,uint>();
		protected	Dictionary<uint,object>			idObjRefMap		= new Dictionary<uint,object>();
		protected	uint							idCounter		= 0;


		public BinaryWriter WriteTarget
		{
			get { return this.writer; }
			set
			{
				if (this.writer == value) return;
				this.writer = value;

				if (!this.writer.BaseStream.CanSeek) throw new ArgumentException("Cannot use a WriteTarget without seeking capability.");

				// We're switching the stream, so we should discard all stream-specific temporary / cache data
				this.ClearStreamSpecificData();
			}
		}
		public BinaryReader ReadTarget
		{
			get { return this.reader; }
			set
			{
				if (this.reader == value) return;
				this.reader = value;

				if (!this.reader.BaseStream.CanSeek) throw new ArgumentException("Cannot use a ReadTarget without seeking capability.");

				// We're switching the stream, so we should discard all stream-specific temporary / cache data
				this.ClearStreamSpecificData();
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


		public BinaryFormatter() : this(null) {}
		public BinaryFormatter(Stream stream)
		{
			this.WriteTarget = new BinaryWriter(stream);
			this.ReadTarget = new BinaryReader(stream);
		}
		~BinaryFormatter()
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
				if (this.writer.BaseStream.CanWrite) this.writer.Flush();
				this.writer.Dispose();
				this.writer = null;
			}

			if (this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		public void WriteObject(object obj)
		{
			if (!this.CanWrite) return;
			if (!this.lastWritten) this.ClearStreamSpecificData();
			this.lastWritten = true;

			// NotNull flag
			if (obj == null)
			{
				this.writer.Write(false);
				return;
			}
			else
				this.writer.Write(true);

			// Retrieve type data
			Type objType = obj.GetType();
			CachedType objCachedType = this.GetCachedType(objType);
			uint objId = 0;
			DataType dataType = objCachedType.dataType;

			// Check whether it's going to be an ObjectRef or not
			if (!IsPrimitiveDataType(dataType) && dataType != DataType.String && dataType != DataType.Struct)
			{
				objId = this.GetIdFromObject(obj);

				// If its not a new id, write a reference
				if (objId < idCounter) dataType = DataType.ObjectRef;
			}

			// Write data type header
			this.WriteDataType(dataType);

			// Write object
			this.WritePushOffset();
			try
			{
				if (IsPrimitiveDataType(dataType))			this.WritePrimitive(obj);
				else if (dataType == DataType.String)		this.writer.Write(obj as string);
				else if (dataType == DataType.Struct)		this.WriteStruct(obj, objCachedType);
				else if	(dataType == DataType.Array)		this.WriteArray(obj, objCachedType, objId);
				else if (dataType == DataType.Class)		this.WriteStruct(obj, objCachedType, objId);
				else if (dataType == DataType.ObjectRef)	this.writer.Write(objId);
			}
			finally
			{
				this.WritePopOffset();
			}
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
		protected void WriteArray(object obj, CachedType objCachedType, uint id = 0)
		{
			Array objAsArray = obj as Array;

			if (objAsArray.Rank != 1) throw new ArgumentException("Non single-Rank arrays are not supported");
			if (objAsArray.GetLowerBound(0) != 0) throw new ArgumentException("Non zero-based arrays are not supported");

			this.writer.Write(objCachedType.typeString);
			this.writer.Write(id);
			this.writer.Write(objAsArray.Rank);
			this.writer.Write(objAsArray.Length);

			if		(objAsArray is bool[])		this.WriteArrayData(objAsArray as bool[]);
			else if (objAsArray is byte[])		this.WriteArrayData(objAsArray as byte[]);
			else if (objAsArray is sbyte[])		this.WriteArrayData(objAsArray as sbyte[]);
			else if (objAsArray is short[])		this.WriteArrayData(objAsArray as short[]);
			else if (objAsArray is ushort[])	this.WriteArrayData(objAsArray as ushort[]);
			else if (objAsArray is int[])		this.WriteArrayData(objAsArray as int[]);
			else if (objAsArray is uint[])		this.WriteArrayData(objAsArray as uint[]);
			else if (objAsArray is long[])		this.WriteArrayData(objAsArray as long[]);
			else if (objAsArray is ulong[])		this.WriteArrayData(objAsArray as ulong[]);
			else if (objAsArray is float[])		this.WriteArrayData(objAsArray as float[]);
			else if (objAsArray is double[])	this.WriteArrayData(objAsArray as double[]);
			else if (objAsArray is decimal[])	this.WriteArrayData(objAsArray as decimal[]);
			else if (objAsArray is char[])		this.WriteArrayData(objAsArray as char[]);
			else if (objAsArray is string[])	this.WriteArrayData(objAsArray as string[]);
			else
			{
				for (long l = 0; l < objAsArray.Length; l++)
					this.WriteObject(objAsArray.GetValue(l));
			}
		}
		protected void WriteStruct(object obj, CachedType objCachedType, uint id = 0)
		{
			// Write the structs data type
			this.writer.Write(objCachedType.typeString);
			this.writer.Write(id);

			// Assure the type data layout has bee written (only once per file)
			this.WriteTypeDataLayout(objCachedType);

			// Write the structs fields
			foreach (FieldInfo field in objCachedType.fields)
			{
				this.WriteObject(field.GetValue(obj));
			}
		}
		protected void WriteTypeDataLayout(CachedType objCachedType)
		{
			if (this.typeDataLayout.ContainsKey(objCachedType.type)) return;

			TypeDataLayout layout = new TypeDataLayout(objCachedType);
			this.typeDataLayout[objCachedType.type] = layout;

			layout.Write(this.writer);
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


		public object ReadObject()
		{
			if (!this.CanRead) return null;
			if (this.lastWritten) this.ClearStreamSpecificData();
			this.lastWritten = false;

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
				if (IsPrimitiveDataType(dataType))			result = this.ReadPrimitive(dataType);
				else if (dataType == DataType.String)		result = this.reader.ReadString();
				else if (dataType == DataType.Array)		result = this.ReadArray();
				else if (dataType == DataType.Struct)		result = this.ReadStruct();
				else if (dataType == DataType.Class)		result = this.ReadStruct();
				else if (dataType == DataType.ObjectRef)	result = this.ReadObjectRef();

				// If we read the object properly and aren't where we're supposed to be, something went wrong
				if (this.reader.BaseStream.Position != lastPos + offset) throw new ApplicationException(string.Format("Wrong dataset offset: '{0}' instead of expected value '{1}'.", offset, this.reader.BaseStream.Position - lastPos));
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
		protected Array ReadArray()
		{
			string	arrTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();
			int		arrRang			= this.reader.ReadInt32();
			long	arrLength		= this.reader.ReadInt32();
			Type	arrType			= this.ResolveType(arrTypeString);

			Array arrObj = Array.CreateInstance(arrType.GetElementType(), arrLength);
			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[arrObj] = objId;
				this.idObjRefMap[objId] = arrObj;
			}

			if		(arrObj is bool[])		this.ReadArrayData(arrObj as bool[]);
			else if (arrObj is byte[])		this.ReadArrayData(arrObj as byte[]);
			else if (arrObj is sbyte[])		this.ReadArrayData(arrObj as sbyte[]);
			else if (arrObj is short[])		this.ReadArrayData(arrObj as short[]);
			else if (arrObj is ushort[])	this.ReadArrayData(arrObj as ushort[]);
			else if (arrObj is int[])		this.ReadArrayData(arrObj as int[]);
			else if (arrObj is uint[])		this.ReadArrayData(arrObj as uint[]);
			else if (arrObj is long[])		this.ReadArrayData(arrObj as long[]);
			else if (arrObj is ulong[])		this.ReadArrayData(arrObj as ulong[]);
			else if (arrObj is float[])		this.ReadArrayData(arrObj as float[]);
			else if (arrObj is double[])	this.ReadArrayData(arrObj as double[]);
			else if (arrObj is decimal[])	this.ReadArrayData(arrObj as decimal[]);
			else if (arrObj is char[])		this.ReadArrayData(arrObj as char[]);
			else if (arrObj is string[])	this.ReadArrayData(arrObj as string[]);
			else
			{
				for (long l = 0; l < arrObj.Length; l++)
					arrObj.SetValue(this.ReadObject(), l);
			}

			return arrObj;
		}
		protected object ReadStruct()
		{
			// Read struct type
			string	objTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();
			Type	objType			= this.ResolveType(objTypeString);

			// Construct object
			object obj = ReflectionHelper.CreateInstanceOf(objType);
			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[obj] = objId;
				this.idObjRefMap[objId] = obj;
			}

			// Determine data layout
			TypeDataLayout layout	= this.ReadTypeDataLayout(objType);

			// Read fields
			for (int i = 0; i < layout.fields.Length; i++)
			{
				FieldInfo field = objType.GetField(layout.fields[i].name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				Type fieldType = this.ResolveType(layout.fields[i].typeString);
				object fieldValue = this.ReadObject();

				if (field == null)
					Log.Core.WriteWarning("Field '{0}' not found. Discarding value '{1}'", layout.fields[i].name, fieldValue);
				else if (field.FieldType != fieldType)
					Log.Core.WriteWarning("Data layout Type '{0}' of field '{1}' does not match reflected Type '{2}'. Discarding value '{3}'", layout.fields[i].typeString, layout.fields[i].name, objTypeString, fieldValue);
				else
					field.SetValue(obj, fieldValue);
			}

			return obj;
		}
		protected object ReadObjectRef()
		{
			object obj;
			uint objId = this.reader.ReadUInt32();

			if (!this.idObjRefMap.TryGetValue(objId, out obj)) throw new ApplicationException(string.Format("Cannot resolve object reference '{0}'.", objId));

			return obj;
		}
		protected TypeDataLayout ReadTypeDataLayout(Type t)
		{
			TypeDataLayout result;
			if (this.typeDataLayout.TryGetValue(t, out result)) return result;

			result = new TypeDataLayout(this.reader);
			this.typeDataLayout[t] = result;
			return result;
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

		
		protected void WriteDataType(DataType dt)
		{
			this.writer.Write((byte)dt);
		}
		protected DataType ReadDataType()
		{
			return (DataType)this.reader.ReadByte();
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

		public void ClearCache()
		{
			this.typeCache.Clear();
			this.typeResolveCache.Clear();
			this.typeDataLayout.Clear();
		}
		protected void ClearStreamSpecificData()
		{
			this.typeDataLayout.Clear();
			this.offsetStack.Clear();
			this.objRefIdMap.Clear();
			this.idObjRefMap.Clear();
			this.idCounter = 0;
		}
		protected Type ResolveType(string typeString)
		{
			Type result;
			if (this.typeResolveCache.TryGetValue(typeString, out result)) return result;

			Assembly[] searchAsm = AppDomain.CurrentDomain.GetAssemblies().Except(DualityApp.DisposedPlugins).ToArray();
			result = ReflectionHelper.FindType(typeString, searchAsm);
			this.typeResolveCache[typeString] = result;

			if (result == null) throw new ApplicationException(string.Format("Cannot resolve Type '{0}'. Type not found", typeString));
			return result;
		}
		protected CachedType GetCachedType(Type t)
		{
			CachedType result;
			if (this.typeCache.TryGetValue(t, out result)) return result;

			result = new CachedType(t);
			this.typeCache[t] = result;
			return result;
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

		protected static bool IsPrimitiveDataType(DataType dt)
		{
			return (byte)dt <= (byte)DataType.Char;
		}
		protected static DataType GetDataType(Type t)
		{
			if (t.IsPrimitive)
			{
				if		(t == typeof(bool))		return DataType.Bool;
				else if (t == typeof(byte))		return DataType.Byte;
				else if (t == typeof(char))		return DataType.Char;
				else if (t == typeof(sbyte))	return DataType.SByte;
				else if (t == typeof(short))	return DataType.Short;
				else if (t == typeof(ushort))	return DataType.UShort;
				else if (t == typeof(int))		return DataType.Int;
				else if (t == typeof(uint))		return DataType.UInt;
				else if (t == typeof(long))		return DataType.Long;
				else if (t == typeof(ulong))	return DataType.ULong;
				else if (t == typeof(float))	return DataType.Float;
				else if (t == typeof(double))	return DataType.Double;
				else if (t == typeof(decimal))	return DataType.Decimal;
			}
			else if (t == typeof(string))
				return DataType.String;
			else if (t.IsArray)
				return DataType.Array;
			else if (t.IsClass)
				return DataType.Class;
			else if (t.IsValueType)
				return DataType.Struct;

			// Should never happen in theory
			return DataType.Unknown;
		}




		public static void __Debug__Test()
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			using (FileStream fileStream = File.Open("test.dat", FileMode.Create, FileAccess.ReadWrite))
			{
				BinaryFormatter formatter = new BinaryFormatter(fileStream);

				List<string> tempList = new List<string>{ "One", "Two", "Three" };

				formatter.WriteObject(new KeyValuePair<string,ColorFormat.ColorRGBA[]>("Test", new ColorFormat.ColorRGBA[] { ColorFormat.ColorRGBA.Green }));
				formatter.WriteObject(42);
				formatter.WriteObject(new[] { 1, 2, 3 });
				formatter.WriteObject("Hello World");
				formatter.WriteObject(tempList);
				formatter.WriteObject(tempList);
				formatter.WriteObject(new Action(__Debug__Test));
				formatter.WriteObject(new Action(tempList.Clear));
				formatter.WriteObject(typeof(int));

				fileStream.Seek(0, SeekOrigin.Begin);

				object[] readObj = new object[9];
				readObj[0] = formatter.ReadObject();
				readObj[1] = formatter.ReadObject();
				readObj[2] = formatter.ReadObject();
				readObj[3] = formatter.ReadObject();
				readObj[4] = formatter.ReadObject();
				readObj[5] = formatter.ReadObject();
				readObj[6] = formatter.ReadObject();
				readObj[7] = formatter.ReadObject();
				readObj[8] = formatter.ReadObject();

				List<string> tempList2 = readObj[5] as List<string>;
				tempList2.Clear();
			}
		}
	}
}
