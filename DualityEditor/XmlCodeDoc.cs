using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

using Duality;

namespace DualityEditor
{
	public class XmlCodeDoc
	{
		private readonly char[] MemberNameSep = "{[(".ToCharArray();

		public enum EntryType
		{
			Unknown,

			Type,
			Field,
			Property,
			Event,
			Method
		}
		public class Entry
		{
			private	EntryType	type;
			private	string		typeName;
			private	string		memberName;
			private	string		summary;

			public EntryType Type
			{
				get { return this.type; }
			}
			public string TypeName
			{
				get { return this.typeName; }
			}
			public string MemberName
			{
				get { return this.memberName; }
			}

			private Entry(EntryType type, string typeName, string memberName)
			{
				this.type = type;
				this.typeName = typeName;
				this.memberName = memberName;
			}

			public static Entry Create(EntryType entryType, Type type, MemberInfo member)
			{
				if (entryType == EntryType.Type)
					return new Entry(entryType, type.GetTypeName(TypeNameFormat.FullNameWithoutAssembly), null);
				else if (member != null)
					return new Entry(entryType, type.GetTypeName(TypeNameFormat.FullNameWithoutAssembly), member.Name);
				else
					return null;
			}
		}

		public XmlCodeDoc()
		{

		}
		public XmlCodeDoc(Stream str)
		{
			this.LoadFromStream(str);
		}
		public XmlCodeDoc(string file)
		{
			this.LoadFromFile(file);
		}

		public void LoadFromFile(string file)
		{
			using (FileStream str = File.OpenRead(file))
			{
				this.LoadFromStream(str);
			}
		}
		public void LoadFromStream(Stream str)
		{
			StreamReader reader = new StreamReader(str);
			this.LoadFromXml(reader.ReadToEnd());
		}
		public void LoadFromXml(string xml)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);
			
			XmlNode assemblyNode = xmlDoc.DocumentElement["assembly"];
			XmlNode assemblyNameNode = assemblyNode != null ? assemblyNode["name"] : null;
			string assemblyName = assemblyNameNode.InnerText;

			XmlNode memberNode = xmlDoc.DocumentElement["members"];
			foreach (XmlNode child in memberNode)
			{
				if (child is XmlComment) continue;
				XmlAttribute memberNameAttrib = child.Attributes["name"];
				if (memberNameAttrib == null) continue;

				// Determine entry type
				EntryType memberEntryType;
				switch (memberNameAttrib.Value[0])
				{
					case 'M':	memberEntryType = EntryType.Method;		break;
					case 'T':	memberEntryType = EntryType.Type;		break;
					case 'F':	memberEntryType = EntryType.Field;		break;
					case 'P':	memberEntryType = EntryType.Property;	break;
					case 'E':	memberEntryType = EntryType.Event;		break;
					default:	memberEntryType = EntryType.Unknown;	break;
				}

				// Determine member name and its (Declaring) type name
				string memberName;
				string memberTypeName;
				if (memberEntryType == EntryType.Type)
				{
					memberName = memberTypeName = memberNameAttrib.Value.Remove(0, 2);
				}
				else
				{
					int memberNameSepIndex = memberNameAttrib.Value.IndexOfAny(MemberNameSep);
					int lastDotIndex = memberNameSepIndex != -1 ? 
						memberNameAttrib.Value.LastIndexOf('.', memberNameSepIndex) :
						memberNameAttrib.Value.LastIndexOf('.');
					memberTypeName = memberNameAttrib.Value.Substring(2, lastDotIndex - 2);
					memberName = memberNameAttrib.Value.Substring(lastDotIndex + 1, memberNameAttrib.Value.Length - lastDotIndex - 1);
				}

				// Determine the members (declaring) type
				Type memberType = ResolveDocStyleType(memberTypeName);

				// Determine the member info
				MemberInfo member = null;
				if (memberEntryType == EntryType.Field)
					member = memberType.GetField(memberName, ReflectionHelper.BindAll);
				else if (memberEntryType == EntryType.Event)
					member = memberType.GetEvent(memberName, ReflectionHelper.BindAll);
				else if (memberEntryType == EntryType.Property)
				{
					string methodName;
					Type[] paramTypes;
					int paramIndex = memberName.IndexOf('(');
					if (paramIndex != -1)
					{
						methodName = memberName.Substring(0, paramIndex);
						string[] paramTypeNames = memberName.Substring(paramIndex + 1, memberName.Length - paramIndex - 2).Split(',');
						paramTypes = paramTypeNames.Select(ResolveDocStyleType).ToArray();
					}
					else
					{
						methodName = memberName;
						paramTypes = Type.EmptyTypes;
					}
					member = memberType.GetProperty(methodName, ReflectionHelper.BindAll, null, null, paramTypes, null);
				}
				else if (memberEntryType == EntryType.Method)
				{
					// ToDo: Basically same as for Property, but supporting generic methods.
				}

				// Create a member entry based on the determined data.
				Entry memberEntry = Entry.Create(memberEntryType, memberType, member);
				//if (memberEntry != null) Console.WriteLine("{0},\t{1},\t{2}", memberEntry.Type, memberEntry.TypeName, memberEntry.MemberName);
			}
		}

		public void Append(XmlCodeDoc other)
		{

		}

		private static Type ResolveDocStyleType(string typeString)
		{
			Type result = null;
			StringBuilder memberTypeNameBuilder = null;
			do
			{
				result = ReflectionHelper.ResolveType(typeString, false);
				if (result != null) break;

				if (memberTypeNameBuilder == null) memberTypeNameBuilder = new StringBuilder(typeString);
				int lastDotIndex = typeString.LastIndexOf('.');
				if (lastDotIndex == -1) break;

				memberTypeNameBuilder[lastDotIndex] = '+';
				typeString = memberTypeNameBuilder.ToString();
			} while (true);
			return result;
		}
	}
}
