﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Duality.Serialization
{
	public class TypeDataLayout
	{
		public struct FieldDataInfo
		{
			public	string	name;
			public	string	typeString;
		}

		private	FieldDataInfo[]	fields;

		public FieldDataInfo[] Fields
		{
			get { return this.fields; }
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
		public TypeDataLayout(CachedType t)
		{
			this.fields = new FieldDataInfo[t.Fields.Length];
			for (int i = 0; i < t.Fields.Length; i++)
			{
				this.fields[i].name = t.Fields[i].Name;
				this.fields[i].typeString = ReflectionHelper.GetTypeString(t.Fields[i].FieldType, ReflectionHelper.TypeStringAttrib.CSCodeIdent);
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
