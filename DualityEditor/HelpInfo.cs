using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Drawing;

using Duality;

namespace DualityEditor
{
	public interface IHelpProvider
	{
		HelpInfo ProvideHoverHelp(Point localPos);
	}

	public class HelpInfo
	{
		private	string	topic;
		private	string	desc;

		public string Topic
		{
			get { return this.topic; }
		}
		public string Description
		{
			get { return this.desc; }
		}

		public HelpInfo(string topic, string desc)
		{
			this.topic = topic;
			this.desc = desc;
		}
		public HelpInfo(MemberInfo member)
		{
			XmlCodeDoc.Entry doc = CorePluginHelper.RequestXmlCodeDoc(member);

			this.topic = member.Name;
			this.desc = "";

			if (doc != null)
			{
				if (doc.Summary != null) this.desc += doc.Summary + "\n\n";
				if (doc.Remarks != null) this.desc += doc.Remarks;
			}
			else
				this.desc = "Unknown Member: " + member.GetMemberId();
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

		public HelpStack() {}

		public void Push(IHelpProvider sender, HelpInfo info)
		{
			HelpInfo lastActiveHelp = this.ActiveHelp;

			stack.Add(new KeyValuePair<IHelpProvider,HelpInfo>(sender, info));
			
			if (lastActiveHelp != this.ActiveHelp)
				this.OnActiveHelpChanged(lastActiveHelp, this.ActiveHelp);
		}
		public void Pop(IHelpProvider sender)
		{
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

		private void OnActiveHelpChanged(HelpInfo last, HelpInfo current)
		{
			if (this.ActiveHelpChanged != null)
				this.ActiveHelpChanged(this, new HelpStackChangedEventArgs(last, current));
		}
	}
}
