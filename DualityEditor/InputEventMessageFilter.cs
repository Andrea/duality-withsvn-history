using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DualityEditor
{
	public class InputEventMessageFilter : IMessageFilter
	{
		public event EventHandler MouseMove;
		public event EventHandler MouseLeave;
		public event EventHandler<KeyEventArgs> KeyDown;

		private enum WindowsMessages : int
		{
			WM_LBUTTONDOWN	= 0x0201,
			WM_LBUTTONUP	= 0x0202,
			WM_MOUSEMOVE	= 0x0200,
			WM_MOUSEWHEEL	= 0x020A,
			WM_RBUTTONDOWN	= 0x0204,
			WM_RBUTTONUP	= 0x0205,
			WM_MOUSELEAVE	= 0x02A3,

			WM_KEYDOWN		= 0x0100
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == (int)WindowsMessages.WM_MOUSEMOVE)
			{
				if (this.MouseMove != null) this.MouseMove(this, EventArgs.Empty);
			}
			else if (m.Msg == (int)WindowsMessages.WM_MOUSELEAVE)
			{
				if (this.MouseLeave != null) this.MouseLeave(this, EventArgs.Empty);
			}
			else if (m.Msg == (int)WindowsMessages.WM_KEYDOWN)
			{
				if (this.KeyDown != null)
				{
					KeyEventArgs args = new KeyEventArgs((Keys)m.WParam.ToInt32());
					this.KeyDown(this, args);
					return args.Handled;
				}
			}
			return false;
		}
	}
}
