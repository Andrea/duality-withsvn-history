﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duality;
using DualityEditor;

namespace EditorBase
{
	public class LogEntryList : UserControl
	{
		[Flags]
		public enum MessageFilter
		{
			None			= 0x0,

			SourceCore		= 0x01,
			SourceEditor	= 0x02,
			SourceGame		= 0x04,

			TypeMessage		= 0x08,
			TypeWarning		= 0x10,
			TypeError		= 0x20,

			SourceAll		= SourceCore | SourceEditor | SourceGame,
			TypeAll			= TypeMessage | TypeWarning | TypeError,
			All				= SourceAll | TypeAll
		}
		public class ViewEntry
		{
			private	DataLogOutput.LogEntry	log	= null;
			
			public DataLogOutput.LogEntry LogEntry
			{
				get { return this.log; }
			}
			public int Height
			{
				get { return 20; }
			}
			public Image TypeIcon
			{
				get
				{
					if (this.log.Type == LogMessageType.Error) return Properties.Resources.log_error;
					if (this.log.Type == LogMessageType.Warning) return Properties.Resources.log_warning;
					return Properties.Resources.log_message;
				}
			}
			public Image SourceIcon
			{
				get
				{
					if (this.log.Source == Log.Game) return Properties.Resources.log_game;
					if (this.log.Source == Log.Editor) return Properties.Resources.log_editor;
					return Properties.Resources.log_core;
				}
			}

			public ViewEntry(DataLogOutput.LogEntry log)
			{
				this.log = log;
			}

			public bool Matches(DateTime minTime, MessageFilter filter)
			{
				if (this.log.Timestamp < minTime) return false;
				if (this.log.Type == LogMessageType.Message && (filter & MessageFilter.TypeMessage) == MessageFilter.None) return false;
				if (this.log.Type == LogMessageType.Warning && (filter & MessageFilter.TypeWarning) == MessageFilter.None) return false;
				if (this.log.Type == LogMessageType.Error && (filter & MessageFilter.TypeError) == MessageFilter.None) return false;
				if (this.log.Source == Log.Core && (filter & MessageFilter.SourceCore) == MessageFilter.None) return false;
				if (this.log.Source == Log.Editor && (filter & MessageFilter.SourceEditor) == MessageFilter.None) return false;
				if (this.log.Source == Log.Game && (filter & MessageFilter.SourceGame) == MessageFilter.None) return false;
				return true;
			}
		}


		private	List<ViewEntry>	entryList		= new List<ViewEntry>();
		private	MessageFilter	displayFilter	= MessageFilter.All;
		private	DateTime		displayMinTime	= DateTime.MinValue;
		private	DataLogOutput	boundOutput		= null;
		private	Color			baseColor		= SystemColors.Control;
		private	bool			scrolledToEnd	= true;


		public IEnumerable<ViewEntry> Entries
		{
			get { return this.entryList; }
		}
		public IEnumerable<ViewEntry> DisplayedEntries
		{
			get { return this.entryList.Where(e => e.Matches(this.displayMinTime, this.displayFilter)); }
		}

		public int ScrollOffset
		{
			get { return -this.AutoScrollPosition.Y; }
			set { this.AutoScrollPosition = new Point(0, Math.Min(value, this.MaxScrollOffset)); }
		}
		public int MaxScrollOffset
		{
			get { return this.AutoScrollMinSize.Height - this.ClientRectangle.Height; }
		}
		public int ContentHeight
		{
			get { return this.AutoScrollMinSize.Height; }
		}
		public MessageFilter DisplayFilter
		{
			get { return this.displayFilter; }
			set 
			{
				ViewEntry lastEntry = this.GetEntryAt(this.ScrollOffset);
				int entryOff = this.ScrollOffset - this.GetEntryOffset(lastEntry);

				this.displayFilter = value;
				this.OnContentChanged();

				this.ScrollToEntry(lastEntry, entryOff);
			}
		}
		public DateTime DisplayMinTime
		{
			get { return this.displayMinTime; }
			set
			{
				ViewEntry lastEntry = this.GetEntryAt(this.ScrollOffset);
				int entryOff = this.ScrollOffset - this.GetEntryOffset(lastEntry);

				this.displayMinTime = value;
				this.OnContentChanged();

				this.ScrollToEntry(lastEntry, entryOff);
			}
		}
		public Color BaseColor
		{
			get { return this.baseColor; }
			set { this.baseColor = value; this.Invalidate(); }
		}
		public bool IsScrolledToEnd
		{
			get { return this.scrolledToEnd; }
		}


		public LogEntryList()
		{
			this.AutoScroll = true;

			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		public void Clear()
		{
			this.entryList.Clear();
			this.OnContentChanged();
		}
		public ViewEntry GetViewEntry(DataLogOutput.LogEntry entry)
		{
			return this.entryList.FirstOrDefault(e => e.LogEntry == entry);
		}
		public ViewEntry AddEntry(DataLogOutput.LogEntry entry)
		{
			ViewEntry viewEntry = new ViewEntry(entry);
			this.entryList.Add(viewEntry);
			this.OnContentChanged();
			return viewEntry;
		}
		public void AddEntry(IEnumerable<DataLogOutput.LogEntry> entries)
		{
			foreach (var entry in entries)
				this.entryList.Add(new ViewEntry(entry));
			this.OnContentChanged();
		}
		public void UpdateFromLog(DataLogOutput dualityLog)
		{
			if (dualityLog == null)
			{
				this.Clear();
				return;
			}

			this.entryList.Clear();
			foreach (var entry in dualityLog.Data)
				this.entryList.Add(new ViewEntry(entry));

			this.OnContentChanged();
		}
		public void BindToOutput(DataLogOutput dualityLog)
		{
			if (this.boundOutput == dualityLog) return;

			if (this.boundOutput != null)
				this.boundOutput.NewEntry -= this.boundOutput_NewEntry;

			this.boundOutput = dualityLog;
			this.UpdateFromLog(this.boundOutput);

			if (this.boundOutput != null)
				this.boundOutput.NewEntry += this.boundOutput_NewEntry;
		}
		
		public void SetFilterFlag(MessageFilter flag, bool isSet)
		{
			if (isSet)
				this.DisplayFilter |= flag;
			else
				this.DisplayFilter &= ~flag;
		}

		public void ScrollToEnd()
		{
			this.ScrollOffset = this.MaxScrollOffset;
		}
		public void ScrollToEntry(ViewEntry entry, int offsetY)
		{
			this.ScrollOffset = this.GetEntryOffset(entry) + offsetY;
		}
		public int GetEntryOffset(ViewEntry entry)
		{
			int totalHeight = 0;
			foreach (ViewEntry e in this.DisplayedEntries)
			{
				if (e == entry) break;
				totalHeight += e.Height;
			}
			return totalHeight;
		}
		public ViewEntry GetEntryAt(int offsetY)
		{
			int totalHeight = 0;
			foreach (ViewEntry entry in this.DisplayedEntries)
			{
				totalHeight += entry.Height;
				if (totalHeight >= offsetY) return entry;
			}
			return null;
		}

		private void UpdateContentSize()
		{
			this.AutoScrollMinSize = new Size(0, this.GetEntryOffset(null));
		}
		private void OnContentChanged()
		{
			this.UpdateContentSize();
			this.Invalidate();
		}
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			this.scrolledToEnd = -this.AutoScrollPosition.Y + this.ClientRectangle.Height >= this.AutoScrollMinSize.Height - 5;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

			Pen foregroundPen = new Pen(this.ForeColor);
			Brush foregroundBrush = new SolidBrush(this.ForeColor);
			Brush foregroundBrushAlpha = new SolidBrush(Color.FromArgb(128, this.ForeColor));
			Brush baseBrush = new SolidBrush(this.baseColor);
			Brush backgroundBrush = new SolidBrush(this.BackColor);
			Brush backgroundBrushAlt = new SolidBrush(Color.FromArgb(
				Math.Max(0, this.BackColor.R - 10), 
				Math.Max(0, this.BackColor.G - 10), 
				Math.Max(0, this.BackColor.B - 10)));
			StringFormat messageFormat = StringFormat.GenericDefault;
			messageFormat.Alignment = StringAlignment.Near;
			messageFormat.LineAlignment = StringAlignment.Center;
			messageFormat.Trimming = StringTrimming.EllipsisCharacter;
			messageFormat.FormatFlags = 0;
			StringFormat messageFormatTimestamp = new StringFormat(messageFormat);
			messageFormatTimestamp.Alignment = StringAlignment.Far;

			e.Graphics.FillRectangle(baseBrush, this.ClientRectangle);

			int offsetY = 0;
			bool evenEntry = false;
			bool showTimestamp = this.ClientRectangle.Width >= 350;
			bool showFramestamp = this.ClientRectangle.Width >= 400;
			Size textMargin = new Size(10, 2);
			foreach (ViewEntry entry in this.DisplayedEntries)
			{
				int entryHeight = entry.Height;

				if (offsetY + entryHeight >= -this.AutoScrollPosition.Y)
				{
					int textIndent = entry.LogEntry.Indent * 20;
					Rectangle entryRect = new Rectangle(this.ClientRectangle.X, offsetY, this.ClientRectangle.Width, entryHeight);
					Rectangle typeIconRect = new Rectangle(
						entryRect.X + textMargin.Width / 2, 
						entryRect.Y, 
						20, 
						entryRect.Height);
					Rectangle sourceIconRect = new Rectangle(
						typeIconRect.Right, 
						entryRect.Y, 
						20, 
						entryRect.Height);
					Rectangle timeTextRect = new Rectangle(
						entryRect.Width - textMargin.Width - (showTimestamp ? 60 : 0) - (showFramestamp ? 50 : 0), 
						entryRect.Y + textMargin.Height, 
						(showTimestamp ? 60 : 0) + (showFramestamp ? 50 : 0), 
						entryRect.Height - textMargin.Height * 2);
					Rectangle textRect = new Rectangle(
						sourceIconRect.Right + textMargin.Width / 2 + textIndent, 
						entryRect.Y + textMargin.Height, 
						Math.Max(1, entryRect.Width - sourceIconRect.Right - textMargin.Width / 2 - textIndent - timeTextRect.Width), 
						entryRect.Height - textMargin.Height * 2);
					Image typeIcon = entry.TypeIcon;
					Image sourceIcon = entry.SourceIcon;

					{
						int newTextHeight;
						newTextHeight = this.Font.Height * (textRect.Height / this.Font.Height);
						textRect.Y = textRect.Y + textRect.Height / 2 - newTextHeight / 2;
						textRect.Height = newTextHeight;
						timeTextRect.Y = textRect.Y;
						timeTextRect.Height = textRect.Height;
					}

					e.Graphics.FillRectangle(evenEntry ? backgroundBrushAlt : backgroundBrush, entryRect);
					if (entry.LogEntry.Type == LogMessageType.Warning)
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 245, 200, 85)), entryRect);
					else if (entry.LogEntry.Type == LogMessageType.Error)
						e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 230, 105, 90)), entryRect);

					e.Graphics.DrawImage(typeIcon, 
						typeIconRect.X + typeIconRect.Width / 2 - typeIcon.Width / 2,
						typeIconRect.Y + typeIconRect.Height / 2 - typeIcon.Height / 2);
					e.Graphics.DrawImage(sourceIcon, 
						sourceIconRect.X + sourceIconRect.Width / 2 - sourceIcon.Width / 2,
						sourceIconRect.Y + sourceIconRect.Height / 2 - sourceIcon.Height / 2);
					e.Graphics.DrawString(entry.LogEntry.Message, this.Font, foregroundBrush, textRect, messageFormat);
					if (showTimestamp)
					{
						e.Graphics.DrawString(
							string.Format("{0:00}:{1:00}:{2:00}", 
								entry.LogEntry.Timestamp.Hour, 
								entry.LogEntry.Timestamp.Minute,
								entry.LogEntry.Timestamp.Second), 
							this.Font, foregroundBrushAlpha, 
							new Rectangle(timeTextRect.Right - 50, timeTextRect.Y, 50, timeTextRect.Height), 
							messageFormatTimestamp);
					}
					if (showFramestamp)
					{
						e.Graphics.DrawString(
							string.Format("#{0}", entry.LogEntry.FrameIndex), 
							this.Font, foregroundBrushAlpha, 
							new Rectangle(timeTextRect.X + 5, timeTextRect.Y, timeTextRect.Width - (showTimestamp ? 50 + 10 : 0), timeTextRect.Height), 
							messageFormatTimestamp);
					}
				}

				offsetY += entryHeight;
				evenEntry = !evenEntry;
				if (offsetY > this.ClientRectangle.Height + (-this.AutoScrollPosition.Y)) break;
			}
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.OnContentChanged();
		}

		private void boundOutput_NewEntry(object sender, DataLogOutput.LogEntryEventArgs e)
		{
			bool wasAtEnd = this.IsScrolledToEnd;
			this.AddEntry(e.Entry);
			if (wasAtEnd) this.ScrollToEnd();
		}
	}
}
