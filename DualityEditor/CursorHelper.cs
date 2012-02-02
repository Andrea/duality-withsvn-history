using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace DualityEditor
{
	public static class CursorHelper
	{
		public static readonly Cursor Arrow;
		public static readonly Cursor ArrowAction;
		public static readonly Cursor ArrowActionMove;
		public static readonly Cursor ArrowActionRotate;
		public static readonly Cursor ArrowActionScale;

		static CursorHelper()
		{
			Arrow				= CreateCursor(EditorRes.GeneralRes.CursorArrow, 0, 0);
			ArrowAction			= CreateCursor(EditorRes.GeneralRes.CursorArrowAction, 0, 0);
			ArrowActionMove		= CreateCursor(EditorRes.GeneralRes.CursorArrowActionMove, 0, 0);
			ArrowActionRotate	= CreateCursor(EditorRes.GeneralRes.CursorArrowActionRotate, 0, 0);
			ArrowActionScale	= CreateCursor(EditorRes.GeneralRes.CursorArrowActionScale, 0, 0);
		}

		public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
		{
			IntPtr ptr = bmp.GetHicon();
			NativeMethods.IconInfo tmp = new NativeMethods.IconInfo();
			NativeMethods.GetIconInfo(ptr, ref tmp);
			tmp.xHotspot = xHotSpot;
			tmp.yHotspot = yHotSpot;
			tmp.fIcon = false;
			ptr = NativeMethods.CreateIconIndirect(ref tmp);
			return new Cursor(ptr);
		}
	}
}
