using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Reflection;

using Duality.Serialization.MetaFormat;

namespace Duality.Serialization
{
	public class XmlMetaFormatter : XmlFormatterBase
	{
		public XmlMetaFormatter() {}
		public XmlMetaFormatter(Stream stream) : base(stream) {}
		
		protected override void GetWriteObjectData(object obj, out SerializeType objSerializeType, out DataType dataType, out uint objId)
		{
			DataNode node = obj as DataNode;
			if (node == null) throw new InvalidOperationException("The XmlMetaFormatter can't serialize objects that do not derive from DataNode");

			objSerializeType = null;
			objId = 0;
			dataType = node.NodeType;

			if		(node is ObjectNode)	objId = (node as ObjectNode).ObjId;
			else if (node is ObjectRefNode) objId = (node as ObjectRefNode).ObjRefId;
		}
		protected override void WriteObjectBody(DataType dataType, object obj, SerializeType objSerializeType, uint objId)
		{
			
		}
		
		protected override object GetNullObject()
		{
			return new PrimitiveNode(DataType.Unknown, null);
		}
		protected override object ReadObjectBody(DataType dataType)
		{
			return null;
		}
	}
}
