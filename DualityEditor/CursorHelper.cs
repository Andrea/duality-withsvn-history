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
		public struct IconInfo
		{
		  public bool fIcon;
		  public int xHotspot;
		  public int yHotspot;
		  public IntPtr hbmMask;
		  public IntPtr hbmColor;
		}

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
		  IconInfo tmp = new IconInfo();
		  GetIconInfo(ptr, ref tmp);
		  tmp.xHotspot = xHotSpot;
		  tmp.yHotspot = yHotSpot;
		  tmp.fIcon = false;
		  ptr = CreateIconIndirect(ref tmp);
		  return new Cursor(ptr);
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
		[DllImport("user32.dll")]
		private static extern IntPtr CreateIconIndirect(ref IconInfo icon);
	}
}
