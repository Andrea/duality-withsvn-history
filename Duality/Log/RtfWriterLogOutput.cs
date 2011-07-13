using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DW.RtfWriter;

using Duality.ColorFormat;

namespace Duality
{
	public class RtfDocWriterLogOutput : ILogOutput
	{
		private	string			prefix	= null;
		private	RtfDocument		rtfDoc	= null;
		private	LogOutputFormat	format	= null;
		
		private	FontDescriptor	font		= null;
		private	ColorDescriptor	clrBg		= null;
		private	ColorDescriptor	clrNormal	= null;
		private	ColorDescriptor	clrWarning	= null;
		private	ColorDescriptor	clrError	= null;

		public RtfDocWriterLogOutput(RtfDocument bufferDoc, string prefix, ColorRGBA bgColor, LogOutputFormat formatHolder)
		{
			if (formatHolder == null) formatHolder = new LogOutputFormat();

			this.prefix		= prefix;
			this.rtfDoc		= bufferDoc;
			this.format		= formatHolder;

			this.font		= this.rtfDoc.createFont("Courier New");
			this.clrNormal	= this.rtfDoc.createColor(new DW.RtfWriter.Color(0, 0, 0));
			this.clrWarning	= this.rtfDoc.createColor(new DW.RtfWriter.Color(206, 141, 0));
			this.clrError	= this.rtfDoc.createColor(new DW.RtfWriter.Color(220, 0, 0));
			this.clrBg		= this.rtfDoc.createColor(new DW.RtfWriter.Color(bgColor.r, bgColor.g, bgColor.b));
		}
		public RtfDocWriterLogOutput(RtfDocument bufferDoc, string prefix, ColorRGBA bgColor) : this(bufferDoc, prefix, bgColor, null) {}
		public RtfDocWriterLogOutput(RtfDocument bufferDoc, string prefix) : this(bufferDoc, prefix, ColorRGBA.White, null) {}
		public RtfDocWriterLogOutput(RtfDocument bufferDoc) : this(bufferDoc, null, ColorRGBA.White, null) {}

		public void PushIndent()
		{
			this.format.Indent++;
		}
		public void PopIndent()
		{
			this.format.Indent--;
		}

		public void Write(LogMessageType type, string msg)
		{
			string parText = "";
			string[] lines = msg.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < lines.Length; i++)
			{
				if (i == 0)
				{
					switch (type)
					{
						case LogMessageType.Message:
							lines[i] = this.prefix + "Info:    " + new string(' ', this.format.Indent * 4) + lines[i];
							break;
						case LogMessageType.Warning:
							lines[i] = this.prefix + "Warning: " + new string(' ', this.format.Indent * 4) + lines[i];
							break;
						case LogMessageType.Error:
							lines[i] = this.prefix + "ERROR:   " + new string(' ', this.format.Indent * 4) + lines[i];
							break;
					}
				}
				else
				{
					lines[i] = this.prefix + "         " + new string(' ', this.format.Indent * 4) + lines[i];
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
