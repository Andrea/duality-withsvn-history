using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DualityEditor
{
	public class MouseEventMessageFilter : IMessageFilter
	{
		public event EventHandler MouseMove;
		public event EventHandler MouseLeave;

		private enum MouseMessages
		{
			WM_LBUTTONDOWN	= 0x0201,
			WM_LBUTTONUP	= 0x0202,
			WM_MOUSEMOVE	= 0x0200,
			WM_MOUSEWHEEL	= 0x020A,
			WM_RBUTTONDOWN	= 0x0204,
			WM_RBUTTONUP	= 0x0205,
			WM_MOUSELEAVE	= 0x02A3
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == (int)MouseMessages.WM_MOUSEMOVE)
			{
				if (this.MouseMove != null) this.MouseMove(this, EventArgs.Empty);
			}
			else if (m.Msg == (int)MouseMessages.WM_MOUSELEAVE)
			{
				if (this.MouseLeave != null) this.MouseLeave(this, EventArgs.Empty);
			}
			return false;
		}
	}
}
