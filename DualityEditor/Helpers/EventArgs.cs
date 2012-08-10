﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Duality;

namespace DualityEditor
{
	public class HelpStackChangedEventArgs : EventArgs
	{
		private	HelpInfo	last;
		private	HelpInfo	current;

		public HelpInfo LastHelp
		{
			get { return this.last; }
		}
		public HelpInfo CurrentHelp
		{
			get { return this.current; }
		}

		public HelpStackChangedEventArgs(HelpInfo last, HelpInfo current)
		{
			this.last = last;
			this.current = current;
		}
	}

	public class SelectionChangedEventArgs : EventArgs
	{
		private	ObjectSelection				current;
		private	ObjectSelection				previous;
		private ObjectSelection.Category	diffCat;
		private ObjectSelection				added;
		private ObjectSelection				removed;

		public ObjectSelection Current
		{
			get { return this.current; }
		}
		public ObjectSelection Previous
		{
			get { return this.previous; }
		}
		public ObjectSelection.Category AffectedCategories
		{
			get { return this.diffCat; }
		}
		public ObjectSelection Added
		{
			get { return this.added; }
		}
		public ObjectSelection Removed
		{
			get { return this.removed; }
		}
		public bool SameObjects
		{
			get { return this.added.Empty && this.removed.Empty; }
		}

		public SelectionChangedEventArgs(ObjectSelection current, ObjectSelection previous, ObjectSelection.Category changedCategoryFallback)
		{
			this.current = current;
			this.previous = previous;

			this.diffCat = ObjectSelection.GetAffectedCategories(this.previous, this.current);
			if (this.diffCat == ObjectSelection.Category.None) this.diffCat = changedCategoryFallback;
			this.added = this.current - this.previous;
			this.removed = this.previous - this.current;
		}
	}

	public class ObjectPropertyChangedEventArgs : EventArgs
	{
		private	ObjectSelection		obj;
		private	List<PropertyInfo>	propInfos;
		private	List<string>		propNames;
		private	bool				prefabApplied;

		public ObjectSelection Objects
		{
			get { return this.obj; }
		}
		public IEnumerable<PropertyInfo> PropInfos
		{
			get { return this.propInfos; }
		}
		public IEnumerable<string> PropNames
		{
			get { return this.propNames; }
		}
		public bool PrefabApplied
		{
			get { return this.prefabApplied; }
		}

		public ObjectPropertyChangedEventArgs(ObjectSelection obj, IEnumerable<PropertyInfo> infos, bool prefabApplied)
		{
			this.obj = obj;
			this.propInfos = new List<PropertyInfo>(infos);
			this.propNames = new List<string>(infos.Select(i => i.Name));
			this.prefabApplied = prefabApplied;

			if (this.prefabApplied) 
				this.obj = this.obj.HierarchyExpand();
		}

		public bool HasProperty(PropertyInfo info)
		{
			return this.prefabApplied || this.propInfos.Any(i => ReflectionHelper.MemberInfoEquals(i, info));
		}
		public bool HasProperty(string name)
		{
			return this.prefabApplied || this.propNames.Contains(name);
		}

		public bool HasAnyProperty(params PropertyInfo[] info)
		{
			return info.Any(this.HasProperty);
		}
		public bool HasAnyProperty(params string[] name)
		{
			return name.Any(this.HasProperty);
		}
	}

	public class ResourceRenamedEventArgs : ResourceEventArgs
	{
		private	string	oldPath;

		public string OldPath
		{
			get { return this.oldPath; }
		}
		public ContentRef<Resource> OldContent
		{
			get { return this.IsDirectory ? ContentRef<Resource>.Null : new ContentRef<Resource>(null, this.oldPath); }
		}

		public ResourceRenamedEventArgs(string path, string oldPath) : base(path)
		{
			this.oldPath = oldPath;
		}
	}
}