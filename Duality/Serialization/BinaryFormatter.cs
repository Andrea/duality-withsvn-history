using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	public class BinaryFormatter : BinaryFormatterBase
	{
		public BinaryFormatter() : base() {}
		public BinaryFormatter(Stream stream) : base(stream) {}
		
		public new void WriteObject(object obj)
		{
			base.WriteObject(obj);
		}
		public new object ReadObject()
		{
			return base.ReadObject();
		}

		protected override void GetWriteObjectData(object obj, out CachedType objCachedType, out DataType dataType, out uint objId)
		{
			Type objType = obj.GetType();
			objCachedType = ReflectionHelper.GetCachedType(objType);
			objId = 0;
			dataType = objCachedType.DataType;
			
			// Check whether it's going to be an ObjectRef or not
			if (dataType == DataType.Array || dataType == DataType.Class || dataType == DataType.Delegate || dataType.IsMemberInfoType())
			{
				objId = this.GetIdFromObject(obj);

				// If its not a new id, write a reference
				if (objId < idCounter) dataType = DataType.ObjectRef;
			}
		}
		protected override void WriteObjectBody(DataType dataType, object obj, CachedType objCachedType, uint objId)
		{
			if (dataType.IsPrimitiveType())				this.WritePrimitive(obj);
			else if (dataType == DataType.String)		this.writer.Write(obj as string);
			else if (dataType == DataType.Struct)		this.WriteStruct(obj, objCachedType);
			else if (dataType == DataType.ObjectRef)	this.writer.Write(objId);
			else if	(dataType == DataType.Array)		this.WriteArray(obj, objCachedType, objId);
			else if (dataType == DataType.Class)		this.WriteStruct(obj, objCachedType, objId);
			else if (dataType == DataType.Delegate)		this.WriteDelegate(obj, objCachedType, objId);
			else if (dataType.IsMemberInfoType())		this.WriteMemberInfo(obj, objId);
		}
		protected void WriteMemberInfo(object obj, uint id = 0)
		{
			this.writer.Write(id);
			if (obj is Type)
			{
				Type type = obj as Type;
				CachedType cachedType = ReflectionHelper.GetCachedType(type);

				this.writer.Write(cachedType.TypeString);
			}
			else if (obj is FieldInfo)
			{
				FieldInfo field = obj as FieldInfo;
				CachedType declaringType = ReflectionHelper.GetCachedType(field.DeclaringType);

				this.writer.Write(field.IsStatic);
				this.writer.Write(declaringType.TypeString);
				this.writer.Write(field.Name);
			}
			else if (obj is PropertyInfo)
			{
				PropertyInfo property = obj as PropertyInfo;
				ParameterInfo[] indexParams = property.GetIndexParameters();

				CachedType declaringType = ReflectionHelper.GetCachedType(property.DeclaringType);
				CachedType propertyType = ReflectionHelper.GetCachedType(property.PropertyType);

				CachedType[] paramTypes = new CachedType[indexParams.Length];
				for (int i = 0; i < indexParams.Length; i++)
					paramTypes[i] = ReflectionHelper.GetCachedType(indexParams[i].ParameterType);

				this.writer.Write(property.GetAccessors(true)[0].IsStatic);
				this.writer.Write(declaringType.TypeString);
				this.writer.Write(property.Name);
				this.writer.Write(propertyType.TypeString);
				this.writer.Write(paramTypes.Length);
				for (int i = 0; i < paramTypes.Length; i++)
					this.writer.Write(paramTypes[i].TypeString);
			}
			else if (obj is MethodInfo)
			{
				MethodInfo method = obj as MethodInfo;
				ParameterInfo[] parameters = method.GetParameters();

				CachedType declaringType = ReflectionHelper.GetCachedType(method.DeclaringType);

				CachedType[] paramTypes = new CachedType[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
					paramTypes[i] = ReflectionHelper.GetCachedType(parameters[i].ParameterType);

				this.writer.Write(method.IsStatic);
				this.writer.Write(declaringType.TypeString);
				this.writer.Write(method.Name);
				this.writer.Write(paramTypes.Length);
				for (int i = 0; i < paramTypes.Length; i++)
					this.writer.Write(paramTypes[i].TypeString);
			}
			else if (obj is ConstructorInfo)
			{
				ConstructorInfo method = obj as ConstructorInfo;
				ParameterInfo[] parameters = method.GetParameters();

				CachedType declaringType = ReflectionHelper.GetCachedType(method.DeclaringType);

				CachedType[] paramTypes = new CachedType[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
					paramTypes[i] = ReflectionHelper.GetCachedType(parameters[i].ParameterType);

				this.writer.Write(method.IsStatic);
				this.writer.Write(declaringType.TypeString);
				this.writer.Write(paramTypes.Length);
				for (int i = 0; i < paramTypes.Length; i++)
					this.writer.Write(paramTypes[i].TypeString);
			}
			else if (obj is EventInfo)
			{
				EventInfo e = obj as EventInfo;
				CachedType declaringType = ReflectionHelper.GetCachedType(e.DeclaringType);

				this.writer.Write(e.GetRaiseMethod().IsStatic);
				this.writer.Write(declaringType.TypeString);
				this.writer.Write(e.Name);
			}
			else if (obj == null)
				throw new ArgumentNullException("obj");
			else
				throw new ArgumentException(string.Format("Type '{0}' is not a supported MemberInfo.", obj.GetType()));
		}
		protected void WriteArray(object obj, CachedType objCachedType, uint id = 0)
		{
			Array objAsArray = obj as Array;

			if (objAsArray.Rank != 1) throw new ArgumentException("Non single-Rank arrays are not supported");
			if (objAsArray.GetLowerBound(0) != 0) throw new ArgumentException("Non zero-based arrays are not supported");

			this.writer.Write(objCachedType.TypeString);
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
			this.writer.Write(objCachedType.TypeString);
			this.writer.Write(id);

			// Assure the type data layout has bee written (only once per file)
			this.WriteTypeDataLayout(objCachedType);

			// Write the structs fields
			foreach (FieldInfo field in objCachedType.Fields)
			{
				this.WriteObject(field.GetValue(obj));
			}
		}
		protected void WriteDelegate(object obj, CachedType objCachedType, uint id = 0)
		{
			bool multi = obj is MulticastDelegate;

			// Write the delegates type
			this.writer.Write(objCachedType.TypeString);
			this.writer.Write(id);
			this.writer.Write(multi);

			if (!multi)
			{
				Delegate objAsDelegate = obj as Delegate;
				this.WriteObject(objAsDelegate.Method);
				this.WriteObject(objAsDelegate.Target);
			}
			else
			{
				MulticastDelegate objAsDelegate = obj as MulticastDelegate;
				this.WriteObject(objAsDelegate.Method);
				this.WriteObject(objAsDelegate.Target);
				this.WriteObject(objAsDelegate.GetInvocationList());
			}
		}

		protected override object ReadObjectBody(DataType dataType)
		{
			object result = null;

			if (dataType.IsPrimitiveType())				result = this.ReadPrimitive(dataType);
			else if (dataType == DataType.String)		result = this.reader.ReadString();
			else if (dataType == DataType.Struct)		result = this.ReadStruct();
			else if (dataType == DataType.ObjectRef)	result = this.ReadObjectRef();
			else if (dataType == DataType.Array)		result = this.ReadArray();
			else if (dataType == DataType.Class)		result = this.ReadStruct();
			else if (dataType == DataType.Delegate)		result = this.ReadDelegate();
			else if (dataType.IsMemberInfoType())		result = this.ReadMemberInfo(dataType);

			return result;
		}
		protected Array ReadArray()
		{
			string	arrTypeString	= this.reader.ReadString();
			uint	objId			= this.reader.ReadUInt32();
			int		arrRang			= this.reader.ReadInt32();
			long	arrLength		= this.reader.ReadInt32();
			Type	arrType			= ReflectionHelper.ResolveType(arrTypeString);

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
			Type	objType			= ReflectionHelper.ResolveType(objTypeString);

			// Construct object
			object obj = ReflectionHelper.CreateInstanceOf(objType);
			// If no appropriate default constructor is available, just force creating it without constructor
			if (obj == null) obj = ReflectionHelper.CreateInstanceOf(objType, true);
			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[obj] = objId;
				this.idObjRefMap[objId] = obj;
			}

			// Determine data layout
			TypeDataLayout layout	= this.ReadTypeDataLayout(objType);

			// Read fields
			for (int i = 0; i < layout.Fields.Length; i++)
			{
				FieldInfo field = objType.GetField(layout.Fields[i].name, ReflectionHelper.BindInstanceAll);
				Type fieldType = ReflectionHelper.ResolveType(layout.Fields[i].typeString);
				object fieldValue = this.ReadObject();

				if (field == null)
					Log.Core.WriteWarning("Field '{0}' not found. Discarding value '{1}'", layout.Fields[i].name, fieldValue);
				else if (field.FieldType != fieldType)
					Log.Core.WriteWarning("Data layout Type '{0}' of field '{1}' does not match reflected Type '{2}'. Discarding value '{3}'", layout.Fields[i].typeString, layout.Fields[i].name, objTypeString, fieldValue);
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
		protected MemberInfo ReadMemberInfo(DataType dataType)
		{
			uint objId = this.reader.ReadUInt32();
			MemberInfo result;

			if (dataType == DataType.Type)
			{
				string typeString = this.reader.ReadString();
				Type type = ReflectionHelper.ResolveType(typeString);
				result = type;
			}
			else if (dataType == DataType.FieldInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string fieldName = this.reader.ReadString();

				Type declaringType = ReflectionHelper.ResolveType(declaringTypeString);
				FieldInfo field = declaringType.GetField(fieldName, isStatic ? ReflectionHelper.BindStaticAll : ReflectionHelper.BindInstanceAll);
				result = field;
			}
			else if (dataType == DataType.PropertyInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string propertyName = this.reader.ReadString();
				string propertyTypeString = this.reader.ReadString();

				int paramCount = this.reader.ReadInt32();
				string[] paramTypeStrings = new string[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypeStrings[i] = this.reader.ReadString();

				Type declaringType = ReflectionHelper.ResolveType(declaringTypeString);
				Type propertyType = ReflectionHelper.ResolveType(propertyTypeString);
				Type[] paramTypes = new Type[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypes[i] = ReflectionHelper.ResolveType(paramTypeStrings[i]);

				PropertyInfo property = declaringType.GetProperty(
					propertyName, 
					isStatic ? ReflectionHelper.BindStaticAll : ReflectionHelper.BindInstanceAll, 
					null, 
					propertyType, 
					paramTypes, 
					null);

				result = property;
			}
			else if (dataType == DataType.MethodInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string methodName = this.reader.ReadString();

				int paramCount = this.reader.ReadInt32();
				string[] paramTypeStrings = new string[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypeStrings[i] = this.reader.ReadString();

				Type declaringType = ReflectionHelper.ResolveType(declaringTypeString);
				Type[] paramTypes = new Type[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypes[i] = ReflectionHelper.ResolveType(paramTypeStrings[i]);

				MethodInfo method = declaringType.GetMethod(methodName, isStatic ? ReflectionHelper.BindStaticAll : ReflectionHelper.BindInstanceAll, null, paramTypes, null);
				result = method;
			}
			else if (dataType == DataType.ConstructorInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();

				int paramCount = this.reader.ReadInt32();
				string[] paramTypeStrings = new string[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypeStrings[i] = this.reader.ReadString();

				Type declaringType = ReflectionHelper.ResolveType(declaringTypeString);
				Type[] paramTypes = new Type[paramCount];
				for (int i = 0; i < paramCount; i++)
					paramTypes[i] = ReflectionHelper.ResolveType(paramTypeStrings[i]);

				ConstructorInfo method = declaringType.GetConstructor(isStatic ? ReflectionHelper.BindStaticAll : ReflectionHelper.BindInstanceAll, null, paramTypes, null);
				result = method;
			}
			else if (dataType == DataType.EventInfo)
			{
				bool isStatic = this.reader.ReadBoolean();
				string declaringTypeString = this.reader.ReadString();
				string eventName = this.reader.ReadString();

				Type declaringType = ReflectionHelper.ResolveType(declaringTypeString);
				EventInfo e = declaringType.GetEvent(eventName, isStatic ? ReflectionHelper.BindStaticAll : ReflectionHelper.BindInstanceAll);
				result = e;
			}
			else
				throw new ApplicationException(string.Format("Invalid DataType '{0}' in ReadMemberInfo method.", dataType));

			
			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[result] = objId;
				this.idObjRefMap[objId] = result;
			}

			return result;
		}
		protected Delegate ReadDelegate()
		{
			string		delegateTypeString	= this.reader.ReadString();
			uint		objId				= this.reader.ReadUInt32();
			bool		multi				= this.reader.ReadBoolean();
			Type		delType				= ReflectionHelper.ResolveType(delegateTypeString);

			MethodInfo	method	= this.ReadObject() as MethodInfo;
			object		target	= this.ReadObject();
			Delegate	del		= Delegate.CreateDelegate(delType, target, method);

			// Prepare object reference
			if (objId != 0)
			{
				this.objRefIdMap[del] = objId;
				this.idObjRefMap[objId] = del;
			}

			// Combine multicast delegates
			if (multi)
			{
				Delegate[] invokeList = this.ReadObject() as Delegate[];
				del = Delegate.Combine(invokeList);
			}

			return del;
		}
	}
}
