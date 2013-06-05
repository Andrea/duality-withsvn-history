using System;
using System.Configuration;
using System.Text;

namespace DW.RtfWriter
{
	/// <summary>
	/// Summary description for RtfTableCell
	/// </summary>
	public class RtfTableCell : RtfBlockList
	{
		private float _width;
		private Align _halign;
		private AlignVertical _valign;
		private Borders _borders;
		private CellMergeInfo _mergeInfo;
		private int _rowIndex;
		private int _colIndex;
		
		internal RtfTableCell(float width, int rowIndex, int colIndex)
			: base(true, true, false, true, false)
		{
			_width = width;
			_halign = Align.None;
			_valign = AlignVertical.Top;
			_borders = new Borders();
			_mergeInfo = null;
			_rowIndex = rowIndex;
			_colIndex = colIndex;
		}
		
		internal bool IsBeginOfColSpan
		{
			get
			{
				if (_mergeInfo == null) {
					return false;
				}
				return (_mergeInfo.ColIndex == 0);
			}
		}

		internal bool IsBeginOfRowSpan
		{
			get
			{
				if (_mergeInfo == null) {
					return false;
				}
				return (_mergeInfo.RowIndex == 0);
			}
		}

		internal bool IsMerged
		{
			get
			{
				if (_mergeInfo == null) {
					return false;
				}
				return true;
			}
		}

		internal CellMergeInfo MergeInfo
		{
			get
			{
				return _mergeInfo;
			}
			set
			{
				_mergeInfo = value;
			}
		}

		public float Width
		{
			get
			{
				return _width;
			}
			set
			{
				_width = value;
			}
		}

		public Borders Borders
		{
			get
			{
				return _borders;
			}
		}
		
		public Align Alignment
		{
			get
			{
				return _halign;
			}
			set
			{
				_halign = value;
			}
		}
		
		public AlignVertical AlignmentVertical
		{
			get
			{
				return _valign;
			}
			set
			{
				_valign = value;
			}
		}
		
		internal int RowIndex
		{
			get
			{
				return _rowIndex;
			}
		}
		
		internal int ColIndex
		{
			get
			{
				return _colIndex;
			}
		}
		
		internal new string render()
		{
			StringBuilder result = new StringBuilder();
			string align = "";
						
			switch (_halign) {
			case Align.Left:
				align = @"\ql";
				break;
			case Align.Right:
				align = @"\qr";
				break;
			case Align.Center:
				align = @"\qc";
				break;
			case Align.FullyJustify:
				align = @"\qj";
				break;
			case Align.Distributed:
				align = @"\qd";
				break;
			}
			
			//result.Append(@"{\intbl");
			//result.AppendLine();
			
			if (base._blocks.Count <= 0) {
				result.AppendLine(@"\pard\intbl");
			} else {
				for (int i = 0; i < base._blocks.Count; i++) {
					RtfBlock block = (RtfBlock) base._blocks[i];
					if (_defaultCharFormat != null && block.DefaultCharFormat != null) {
						block.DefaultCharFormat.copyFrom(_defaultCharFormat);
					}
					if (block.Margins[Direction.Top] < 0) {
						block.Margins[Direction.Top] = 0;
					}
					if (block.Margins[Direction.Right] < 0) {
						block.Margins[Direction.Right] = 0;
					}
					if (block.Margins[Direction.Bottom] < 0) {
						block.Margins[Direction.Bottom] = 0;
					}
					if (block.Margins[Direction.Left] < 0) {
						block.Margins[Direction.Left] = 0;
					}
					if (i == 0) {
						//block.BlockHead = @"{\pard\intbl" + align;
						block.BlockHead = @"\pard\intbl" + align;
					} else {
						block.BlockHead = @"\par" + align;
					}
					block.BlockTail = "";
					result.AppendLine(block.render());
				}
			}
			
			result.AppendLine(@"\cell");
			return result.ToString();
		}
	}
}