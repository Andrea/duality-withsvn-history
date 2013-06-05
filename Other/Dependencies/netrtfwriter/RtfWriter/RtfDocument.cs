using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DW.RtfWriter
{
	/// <summary>
	/// Summary description for RtfDocument
	/// </summary>
	public class RtfDocument : RtfBlockList
	{
		private PaperSize _paper;
		private PaperOrientation _orientation;
		private Margins _margins;
		private Lcid _lcid;
		private List<string> _fontTable;
		private List<Color> _colorTable;
		private RtfHeaderFooter _header;
		private RtfHeaderFooter _footer;
		
		public RtfDocument(PaperSize paper, PaperOrientation orientation, Lcid lcid)
		{
			_paper = paper;
			_orientation = orientation;
			_margins = new Margins();
			if (_orientation == PaperOrientation.Portrait) {
				_margins[Direction.Top] = DefaultValue.MarginSmall;
				_margins[Direction.Right] = DefaultValue.MarginLarge;
				_margins[Direction.Bottom] = DefaultValue.MarginSmall;
				_margins[Direction.Left] = DefaultValue.MarginLarge;
			} else { // landscape
				_margins[Direction.Top] = DefaultValue.MarginLarge;
				_margins[Direction.Right] = DefaultValue.MarginSmall;
				_margins[Direction.Bottom] = DefaultValue.MarginLarge;
				_margins[Direction.Left] = DefaultValue.MarginSmall;
			}
			_lcid = lcid;
			_fontTable = new List<string>();
			_fontTable.Add(DefaultValue.Font);		// default font
			_colorTable = new List<Color>();
			_colorTable.Add(new Color());			// default color
			_header = null;
			_footer = null;
		}
		
		public Margins Margins
		{
			get
			{
				return _margins;
			}
		}

		public RtfHeaderFooter Header
		{
			get
			{
				if (_header == null) {
					_header = new RtfHeaderFooter(HeaderFooterType.Header);
				}
				return _header;
			}
		}
		
		public RtfHeaderFooter Footer
		{
			get
			{
				if (_footer == null) {
					_footer = new RtfHeaderFooter(HeaderFooterType.Footer);
				}
				return _footer;
			}
		}
		
		public ColorDescriptor DefaultColor
		{
			get
			{
				return  new ColorDescriptor(0);
			}
		}
		
		public FontDescriptor DefaultFont
		{
			get
			{
				return new FontDescriptor(0);
			}
		} 

		public FontDescriptor createFont(string fontName)
		{
			if (_fontTable.Contains(fontName)) {
				return new FontDescriptor(_fontTable.IndexOf(fontName));
			}
			_fontTable.Add(fontName);
			return new FontDescriptor(_fontTable.IndexOf(fontName));
		}
		
		public ColorDescriptor createColor(Color color)
		{
			if (_colorTable.Contains(color)) {
				return new ColorDescriptor(_colorTable.IndexOf(color));
			}
			_colorTable.Add(color);
			return new ColorDescriptor(_colorTable.IndexOf(color));
		}
		
		public RtfTable addTable(int rowCount, int colCount)
		{
			float horizontalWidth;

			if (_orientation == PaperOrientation.Portrait) {
				horizontalWidth = RtfUtility.paperWidthInPt(_paper, _orientation) 
					- _margins[Direction.Left] - _margins[Direction.Right];
			} else {
				horizontalWidth = RtfUtility.paperHeightInPt(_paper, _orientation)
					- _margins[Direction.Left] - _margins[Direction.Right];
			}
			return base.addTable(rowCount, colCount, horizontalWidth);
		}
		
		public new string render()
		{
			StringBuilder rtf = new StringBuilder();
			
			// ---------------------------------------------------
			// Prologue
			// ---------------------------------------------------
			rtf.AppendLine(@"{\rtf1\ansi\deff0");
			rtf.AppendLine();

			// ---------------------------------------------------
			// Insert font table
			// ---------------------------------------------------
			rtf.AppendLine(@"{\fonttbl");
			for (int i = 0; i < _fontTable.Count; i++) {
				rtf.AppendLine(@"{\f" + i + " " + RtfUtility.unicodeEncode(_fontTable[i].ToString()) + ";}");
			}
			rtf.AppendLine("}");
			rtf.AppendLine();

			// ---------------------------------------------------
			// Insert color table
			// ---------------------------------------------------
			rtf.AppendLine(@"{\colortbl");
			rtf.AppendLine(";");
			for (int i = 1; i < _colorTable.Count; i++) {
				Color c = _colorTable[i];
				rtf.AppendLine(@"\red" + c.Red + @"\green" + c.Green + @"\blue" + c.Blue + ";");
			}
			rtf.AppendLine("}");
			rtf.AppendLine();

			// ---------------------------------------------------
			// Preliminary
			// ---------------------------------------------------
			rtf.AppendLine(@"\deflang" + (int)_lcid + @"\plain\fs" 
				+ RtfUtility.pt2HalfPt(DefaultValue.FontSize) + @"\widowctrl\hyphauto\ftnbj");
			// page size
			rtf.AppendLine(@"\paperw" + RtfUtility.paperWidthInTwip(_paper, _orientation) 
				+ @"\paperh" + RtfUtility.paperHeightInTwip(_paper, _orientation));
			// page margin
			rtf.AppendLine(@"\margt" + RtfUtility.pt2Twip(_margins[Direction.Top]));
			rtf.AppendLine(@"\margr" + RtfUtility.pt2Twip(_margins[Direction.Right]));
			rtf.AppendLine(@"\margb" + RtfUtility.pt2Twip(_margins[Direction.Bottom]));
			rtf.AppendLine(@"\margl" + RtfUtility.pt2Twip(_margins[Direction.Left]));
			// orientation
			if (_orientation == PaperOrientation.Landscape) {
				rtf.AppendLine(@"\landscape");
			}
			// header/footer
			if (_header != null) {
				rtf.Append(_header.render());
			}
			if (_footer != null) {
				rtf.Append(_footer.render());
			}
			rtf.AppendLine();

			// ---------------------------------------------------
			// Document body
			// ---------------------------------------------------
			rtf.Append(base.render());
			
			// ---------------------------------------------------
			// Ending
			// ---------------------------------------------------
			rtf.AppendLine("}");
			
			return rtf.ToString();
		}
		
		public void save(string fname)
		{
			StreamWriter w = new StreamWriter(fname);
			w.Write(render());
			w.Close();
		}
	}
}