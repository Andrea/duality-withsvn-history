using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;
using System.IO;

using Duality;

namespace DualityEditor
{
	public interface IHelpProvider
	{
		HelpInfo ProvideHoverHelp(Point localPos, ref bool captured);
		bool PerformHelpAction(HelpInfo info);
	}
	public static class ExtMethodsIHelpProvider
	{
		public static bool DefaultPerformHelpAction(this IHelpProvider provider, HelpInfo info)
		{
			MemberInfo member = !string.IsNullOrEmpty(info.Id) ? ReflectionHelper.ResolveMember(info.Id, false) : null;
			if (member != null)
			{
				string memberHtmlName;
				if (member is FieldInfo && member.DeclaringType.IsEnum)
					memberHtmlName = member.DeclaringType.GetMemberId();
				else
					memberHtmlName = info.Id;
				memberHtmlName = memberHtmlName.Replace('.', '_').Replace(':', '_').Replace('+', '_');
				
				string ddocPath = Path.GetFullPath("DDoc.chm");
				string cmdLine = string.Format("{0}::/html/{1}.htm", ddocPath, memberHtmlName);

				Process[] proc = Process.GetProcessesByName("hh");
				if (proc.Length > 0) proc[0].CloseMainWindow();
				Process.Start("HH.exe", cmdLine);
				return true;
			}
			
			return false;
		}
	}

	public class HelpInfo
	{
		private	string	id;
		private	string	topic;
		private	string	desc;
		
		public string Id
		{
			get { return this.id; }
		}
		public string Topic
		{
			get { return this.topic; }
		}
		public string Description
		{
			get { return this.desc; }
		}

		private HelpInfo() {}

		public static HelpInfo FromText(string topic, string desc, string id = null)
		{
			HelpInfo info = new HelpInfo();
			
			info.id = id;
			info.topic = topic;
			info.desc = desc;

			return info;
		}
		public static HelpInfo FromMember(MemberInfo member)
		{
			XmlCodeDoc.Entry doc = CorePluginHelper.RequestXmlCodeDoc(member);

			if (doc != null)
			{
				HelpInfo info = new HelpInfo();

				info.id = member.GetMemberId();
				info.topic = member.Name;
				info.desc = "";

				if (doc.Summary != null) info.desc += doc.Summary + "\n\n";
				if (doc.Remarks != null) info.desc += doc.Remarks;

				return info;
			}

			return FromText(member.Name, DualityEditor.EditorRes.GeneralRes.HelpInfo_NotAvailable_Desc);
		}
		public static HelpInfo FromResource(ContentRef<Resource> res)
		{
			return FromMember(res.ResType);
		}
		public static HelpInfo FromGameObject(GameObject obj)
		{
			if (obj == null) return null;
			HelpInfo info = FromMember(typeof(GameObject));

			if (info == null) info = new HelpInfo();
			info.topic = obj.FullName;
			info.desc = obj.GetComponents<Component>().ToString(c => c.GetType().GetTypeKeyword(), "\n");

			return info;
		}
		public static HelpInfo FromComponent(Component cmp)
		{
			if (cmp == null) return null;
			HelpInfo info = FromMember(cmp.GetType());

			if (info == null) info = new HelpInfo();
			info.topic = cmp.ToString();

			return info;
		}
		public static HelpInfo FromSelection(ObjectSelection sel)
		{
			if (sel == null || sel.GameObjectCount != 1) return null;
			return FromGameObject(sel.GameObjects.First());

			// ToDo: Probably improve later
		}
	}

	public class HelpStack
	{
		private	List<KeyValuePair<IHelpProvider,HelpInfo>>	stack	= new List<KeyValuePair<IHelpProvider,HelpInfo>>();

		public event EventHandler<HelpStackChangedEventArgs> ActiveHelpChanged = null;

		public HelpInfo ActiveHelp
		{
			get { return this.stack.Count > 0 ? this.stack[this.stack.Count - 1].Value : null; }
		}
		public IHelpProvider ActiveHelpProvider
		{
			get { return this.stack.Count > 0 ? this.stack[this.stack.Count - 1].Key : null; }
		}

		public HelpStack() {}

		public void Push(IHelpProvider sender, HelpInfo info)
		{
			if (sender == null) throw new ArgumentNullException("sender");
			if (info == null) throw new ArgumentNullException("info");
			HelpInfo lastActiveHelp = this.ActiveHelp;

			stack.Add(new KeyValuePair<IHelpProvider,HelpInfo>(sender, info));
			
			if (lastActiveHelp != this.ActiveHelp)
				this.OnActiveHelpChanged(lastActiveHelp, this.ActiveHelp);
		}
		public void Pop(IHelpProvider sender)
		{
			if (sender == null) throw new ArgumentNullException("sender");
			HelpInfo lastActiveHelp = this.ActiveHelp;

			for (int i = stack.Count - 1; i >= 0; i--)
			{
				if (stack[i].Key == sender)
				{
					stack.RemoveAt(i);
					break;
				}
			}

			if (lastActiveHelp != this.ActiveHelp)
				this.OnActiveHelpChanged(lastActiveHelp, this.ActiveHelp);
		}
		public void Switch(IHelpProvider sender, HelpInfo newInfo)
		{
			if (sender == null) throw new ArgumentNullException("sender");
			if (newInfo == null) throw new ArgumentNullException("newInfo");
			HelpInfo lastActiveHelp = this.ActiveHelp;

			for (int i = stack.Count - 1; i >= 0; i--)
			{
				if (stack[i].Key == sender)
				{
					stack[i] = new KeyValuePair<IHelpProvider,HelpInfo>(sender, newInfo);
					break;
				}
			}

			if (lastActiveHelp != this.ActiveHelp)
				this.OnActiveHelpChanged(lastActiveHelp, this.ActiveHelp);
		}
		public void UpdateFromProvider(IHelpProvider oldProvider, IHelpProvider newProvider, HelpInfo info)
		{
			if (oldProvider != null) this.Pop(oldProvider);
			if (info != null) this.Push(newProvider, info);
		}
		public void UpdateFromProvider(IHelpProvider provider, HelpInfo info)
		{
			if (this.ActiveHelpProvider == provider)
			{
				if (info != null)
					this.Switch(provider, info);
				else
					this.Pop(provider);
			}
			else if (info != null)
				this.Push(provider, info);
		}

		private void OnActiveHelpChanged(HelpInfo last, HelpInfo current)
		{
			if (this.ActiveHelpChanged != null)
				this.ActiveHelpChanged(this, new HelpStackChangedEventArgs(last, current));
		}
	}
}
