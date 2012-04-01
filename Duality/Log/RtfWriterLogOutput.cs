using System;
using DW.RtfWriter;

using Duality.ColorFormat;

namespace Duality
{
	/// <summary>
	/// A <see cref="ILogOutput">Log output</see> that uses a RTF document as message destination.
	/// </summary>
	public class RtfDocWriterLogOutput : ILogOutput
	{
		private	RtfDocument		rtfDoc	= null;
		
		private	FontDescriptor	font		= null;
		private	ColorDescriptor	clrBg		= null;
		private	ColorDescriptor	clrNormal	= null;
		private	ColorDescriptor	clrWarning	= null;
		private	ColorDescriptor	clrError	= null;

		public RtfDocWriterLogOutput(RtfDocument bufferDoc, ColorRgba bgColor)
		{
			this.rtfDoc		= bufferDoc;

			this.font		= this.rtfDoc.createFont("Courier New");
			this.clrNormal	= this.rtfDoc.createColor(new DW.RtfWriter.Color(0, 0, 0));
			this.clrWarning	= this.rtfDoc.createColor(new DW.RtfWriter.Color(206, 141, 0));
			this.clrError	= this.rtfDoc.createColor(new DW.RtfWriter.Color(220, 0, 0));
			this.clrBg		= this.rtfDoc.createColor(new DW.RtfWriter.Color(bgColor.r, bgColor.g, bgColor.b));
		}
		public RtfDocWriterLogOutput(RtfDocument bufferDoc) : this(bufferDoc, ColorRgba.White) {}
		
		/// <summary>
		/// Writes a single message to the output.
		/// </summary>
		/// <param name="source">The <see cref="Log"/> from which the message originates.</param>
		/// <param name="type">The type of the log message.</param>
		/// <param name="msg">The message to write.</param>
		/// <param name="indent">The messages indent value.</param>
		public void Write(Log source, LogMessageType type, string msg)
		{
			int indent = source.Indent;
			string parText = "";
			string[] lines = msg.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < lines.Length; i++)
			{
				if (i == 0)
				{
					switch (type)
					{
						case LogMessageType.Message:
							lines[i] = source.Prefix + "Info:    " + new string(' ', indent * 4) + lines[i];
							break;
						case LogMessageType.Warning:
							lines[i] = source.Prefix + "Warning: " + new string(' ', indent * 4) + lines[i];
							break;
						case LogMessageType.Error:
							lines[i] = source.Prefix + "ERROR:   " + new string(' ', indent * 4) + lines[i];
							break;
					}
				}
				else
				{
					lines[i] = source.Prefix + "         " + new string(' ', indent * 4) + lines[i];
				}
				if (parText.Length > 0) parText += '\n';
				parText += lines[i];
			}

			RtfParagraph par = this.rtfDoc.addParagraph();
			par.DefaultCharFormat.Font = this.font;
			par.DefaultCharFormat.BgColor = this.clrBg;
			par.DefaultCharFormat.FgColor = (type == LogMessageType.Error) ? 
				this.clrError : (type == LogMessageType.Warning ? 
				this.clrWarning : this.clrNormal);
			if (type == LogMessageType.Error) 
				par.DefaultCharFormat.FontStyle.addStyle(FontStyleFlag.Bold);
			if (type == LogMessageType.Warning) 
				par.DefaultCharFormat.FontStyle.addStyle(FontStyleFlag.Italic);
			par.Text = parText;
		}
	}
}
