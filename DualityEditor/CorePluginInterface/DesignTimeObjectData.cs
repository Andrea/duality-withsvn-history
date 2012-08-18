using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Duality;

namespace DualityEditor.CorePluginInterface
{
	public class DesignTimeObjectData : IEquatable<DesignTimeObjectData>
	{
		private class DataContainer
		{
			public	bool	hidden	= false;
			public	bool	locked	= false;

			public DataContainer() {}
			public DataContainer(DataContainer baseData)
			{
				this.hidden = baseData.hidden;
				this.locked = baseData.locked;
			}
		}

		public static readonly DesignTimeObjectData Default = new DesignTimeObjectData(null);

		private	GameObject		obj		= null;
		private DataContainer	data	= null;
		private	bool			dirty	= false;

		public GameObject ParentObject
		{
			get { return this.obj; }
		}
		public bool IsHidden
		{
			get { return this.data.hidden; }
			set
			{
				if (this.data.hidden != value)
				{
					this.CleanDirty();
					this.data.hidden = value;
				}
			}
		}
		public bool IsLocked
		{
			get { return this.data.locked; }
			set
			{
				if (this.data.locked != value)
				{
					this.CleanDirty();
					this.data.locked = value;
				}
			}
		}


		public DesignTimeObjectData(GameObject parent)
		{
			this.obj = parent;
			this.data = new DataContainer();
		}
		public DesignTimeObjectData(GameObject parent, DesignTimeObjectData baseData)
		{
			this.obj = parent;
			this.data = baseData.data;
			this.dirty = true;
		}

		private void CleanDirty()
		{
			if (!this.dirty) return;
			if (this.data == null)	this.data = new DataContainer();
			else					this.data = new DataContainer(this.data);
			this.dirty = false;
		}

		public static bool operator ==(DesignTimeObjectData a, DesignTimeObjectData b)
		{
			if (object.ReferenceEquals(a, b)) return true;
			if (object.ReferenceEquals(a.data, b.data)) return true;
			return 
				a.data.hidden == b.data.hidden &&
				a.data.locked == b.data.locked;
		}
		public static bool operator !=(DesignTimeObjectData a, DesignTimeObjectData b)
		{
			return !(a == b);
		}
		public override bool Equals(object obj)
		{
			if (obj is DesignTimeObjectData)
				return this.Equals(obj as DesignTimeObjectData);
			else
				return base.Equals(obj);
		}
		public bool Equals(DesignTimeObjectData other)
		{
			return this == other;
		}
	}
}
