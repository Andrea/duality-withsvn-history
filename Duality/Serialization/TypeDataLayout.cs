using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Duality.Serialization
{
	public class TypeDataLayout
	{
		public struct FieldDataInfo
		{
			public	string	name;
			public	string	typeString;

			public FieldDataInfo(string name, string typeString)
			{
				this.name = name;
				this.typeString = typeString;
			}
		}

		private	FieldDataInfo[]	fields;

		public FieldDataInfo[] Fields
		{
			get { return this.fields; }
			set { this.fields = value; }
		}

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
		public TypeDataLayout(TypeDataLayout t)
		{
			this.fields = t.fields != null ? t.fields.Clone() as FieldDataInfo[] : null;
		}
		public TypeDataLayout(SerializeType t)
		{
			this.fields = new FieldDataInfo[t.Fields.Length];
			for (int i = 0; i < t.Fields.Length; i++)
			{
				this.fields[i].name = t.Fields[i].Name;
				this.fields[i].typeString = ReflectionHelper.GetTypeName(t.Fields[i].FieldType, TypeNameFormat.FullNameWithoutAssembly);
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
}
