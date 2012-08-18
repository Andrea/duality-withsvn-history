using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Duality;

namespace DualityEditor.CorePluginInterface
{
	public class DesignTimeObjectData
	{
		private class DataContainer : IEquatable<DataContainer>
		{
			public	bool	hidden	= false;
			public	bool	locked	= false;
			public	Dictionary<Type,object>	custom	= null;

			public DataContainer() {}
			public DataContainer(DataContainer baseData)
			{
				this.hidden = baseData.hidden;
				this.locked = baseData.locked;
				this.custom = baseData.custom != null ? new Dictionary<Type,object>(baseData.custom) : null;
			}

			public static bool operator ==(DataContainer a, DataContainer b)
			{
				if (object.ReferenceEquals(a, b)) return true;
				if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null)) return false;

				if (a.hidden != b.hidden) return false;
				if (a.locked != b.locked) return false;

				if (a.custom != b.custom)
				{
					if (a.custom == null || b.custom == null) return false;
					if (a.custom.Count != b.custom.Count) return false;
					foreach (var pair in a.custom)
					{
						object valB;
						if (!b.custom.TryGetValue(pair.Key, out valB)) return false;
						if (!object.Equals(pair.Value, valB)) return false;
					}
				}

				return true;
			}
			public static bool operator !=(DataContainer a, DataContainer b)
			{
				return !(a == b);
			}
			public override bool Equals(object obj)
			{
				if (obj is DataContainer)
					return this.Equals(obj as DataContainer);
				else
					return base.Equals(obj);
			}
			public override int GetHashCode()
			{
				int hash = 17;
				hash = hash * 23 + this.hidden.GetHashCode();
				hash = hash * 23 + this.locked.GetHashCode();
				if (this.custom != null)
				{
					foreach (var pair in this.custom)
						hash = hash * 23 + pair.Value.GetHashCode();
				}
				return hash;
			}
			public bool Equals(DataContainer other)
			{
				return this == other;
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
		public bool IsDefault
		{
			get
			{
				if (object.ReferenceEquals(this, Default)) return true;
				return this.data == Default.data;
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

		public T RequestCustomData<T>() where T : new()
		{
			this.CleanDirty();

			if (this.data.custom == null) this.data.custom = new Dictionary<Type,object>();

			object val;
			if (!this.data.custom.TryGetValue(typeof(T), out val))
			{
				T newVal = new T();
				this.data.custom[typeof(T)] = newVal;
				return newVal;
			}
			else
			{
				return (T)val;
			}
		}
		public void RemoveCustomData<T>()
		{
			this.CleanDirty();

			if (this.data.custom == null) return;
			this.data.custom.Remove(typeof(T));
		}

		private void CleanDirty()
		{
			if (!this.dirty) return;
			if (this.data == null)	this.data = new DataContainer();
			else					this.data = new DataContainer(this.data);
			this.dirty = false;
		}
	}
}
