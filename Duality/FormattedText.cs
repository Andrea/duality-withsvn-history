using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality.Resources;
using Duality.VertexFormat;
using Duality.ColorFormat;

using OpenTK;


namespace Duality
{
	[Serializable]
	public class FormattedText
	{
		public	const	string	FormatSlash			= "//";
		public	const	string	FormatElement		= "/e";
		public	const	string	FormatColor			= "/c";
		public	const	string	FormatFont			= "/f";
		public	const	string	FormatIcon			= "/i";
		public	const	string	FormatAlignLeft		= "/al";
		public	const	string	FormatAlignRight	= "/ar";
		public	const	string	FormatAlignCenter	= "/ac";
		public	const	string	FormatNewline		= "/n";


		[Serializable] public abstract class Element {}
		[Serializable] public class TextElement : Element
		{
			private	string	text;
			public string Text
			{
				get { return this.text; }
			}
			public TextElement(string text)
			{
				this.text = text;
			}
		}
		[Serializable] public class IconElement : Element
		{
			private	int	iconIndex;
			public int IconIndex
			{
				get { return this.iconIndex; }
			}
			public IconElement(int icon)
			{
				this.iconIndex = icon;
			}
		}
		[Serializable] public class NewLineElement : Element
		{
			public NewLineElement() {}
		}
		[Serializable] public class FontChangeElement : Element
		{
			private	int	fontIndex;
			public int FontIndex
			{
				get { return this.fontIndex; }
			}
			public FontChangeElement(int font)
			{
				this.fontIndex = font;
			}
		}
		[Serializable] public class ColorChangeElement : Element
		{
			private ColorRGBA color;
			public ColorRGBA Color
			{
				get { return this.color; }
			}
			public ColorChangeElement(ColorRGBA color)
			{
				this.color = color;
			}
		}
		[Serializable] public class AlignChangeElement : Element
		{
			private	Alignment align;
			public Alignment Align
			{
				get { return this.align; }
			}
			public AlignChangeElement(Alignment align)
			{
				this.align = align;
			}
		}

		[Serializable] public struct Icon
		{
			public	Rect	uvRect;
			public	Vector2	size;

			public Icon(Rect uvRect, Vector2 size)
			{
				this.uvRect = uvRect;
				this.size = size;
			}
		}
		[Serializable] public struct FlowArea
		{
			public	int		width;
			public	int		height;
			public	int		y;
			public	bool	alignRight;

			public FlowArea(int y, int height, int width, bool alignRight)
			{
				this.y = y;
				this.height = height;
				this.width = width;
				this.alignRight = alignRight;
			}
		}
		public class Metrics
		{
			private	Vector2		size;
			private	Rect[]		lineBounds;
			private	Rect[]		elementBounds;

			public Vector2 Size 
			{
				get { return this.size; }
			}
			public int LineCount
			{
				get { return this.lineBounds.Length; }
			}
			public Rect[] LineBounds
			{
				get { return this.lineBounds; }
			}
			public Rect[] ElementBounds
			{
				get { return this.elementBounds; }
			}

			public Metrics(Vector2 size, IEnumerable<Rect> lineBounds, IEnumerable<Rect> elementBounds)
			{
				this.size = size;
				this.lineBounds = lineBounds.ToArray();
				this.elementBounds = elementBounds.ToArray();
			}
		}
		public enum WrapMode
		{
			Glyph,
			Word,
			Element
		}

		private class RenderState
		{
			// General state data
			private	FormattedText	parent;
			private	int[]			vertTextIndex;
			private	int				vertIconIndex;
			private	int				elemIndex;
			private	int				lineIndex;
			private	Vector2			offset;
			// Format state
			private	int				fontIndex;
			private	Font			font;
			private	ColorRGBA		color;
			// Line stats
			private	float			lineBeginX;
			private	float			lineAvailWidth;
			private	float			lineWidth;
			private	float			lineHeight;
			private	int				lineBaseLine;
			private	Alignment		lineAlign;
			// Current element data. Current == just 'been processed in NextElement()
			private	Vector2			curElemOffset;
			private	int				curElemVertTextIndex;
			private	int				curElemVertIconIndex;
			private	int				curElemWrapIndex;
			private	string			curElemText;


			public int FontIndex
			{
				get { return this.fontIndex; }
			}
			public Font Font
			{
				get { return this.font; }
			}
			public ColorRGBA Color
			{
				get { return this.color; }
			}
			public int LineBaseLine
			{
				get { return this.lineBaseLine; }
			}
			public int CurrentElemIndex
			{
				get { return this.elemIndex - 1; }
			}
			public int CurrentLineIndex
			{
				get { return this.lineIndex; }
			}
			public Vector2 CurrentElemOffset
			{
				get { return this.curElemOffset; }
			}
			public int CurrentElemTextVertexIndex
			{
				get { return this.curElemVertTextIndex; }
			}
			public int CurrentElemIconVertexIndex
			{
				get { return this.curElemVertIconIndex; }
			}
			public string CurrentElemText
			{
				get { return this.curElemText; }
			}


			public RenderState(FormattedText parent)
			{
				this.parent = parent;
				this.vertTextIndex = new int[this.parent.fonts != null ? this.parent.fonts.Length : 0];
				this.font = (this.parent.fonts != null && this.parent.fonts.Length > 0) ? this.parent.fonts[0].Res : null;
				this.color = ColorRGBA.White;
				this.lineAlign = Alignment.Left;

				this.PeekLineStats();
				this.offset.X = this.lineBeginX;
			}
			public RenderState(RenderState other)
			{
				this.parent = other.parent;
				this.vertTextIndex = other.vertTextIndex.Clone() as int[];
				this.vertIconIndex = other.vertIconIndex;
				this.offset = other.offset;
				this.elemIndex = other.elemIndex;
				this.lineIndex = other.lineIndex;

				this.fontIndex = other.fontIndex;
				this.font = other.font;
				this.color = other.color;

				this.lineBeginX = other.lineBeginX;
				this.lineWidth = other.lineWidth;
				this.lineAvailWidth = other.lineAvailWidth;
				this.lineHeight = other.lineHeight;
				this.lineBaseLine = other.lineBaseLine;
				this.lineAlign = other.lineAlign;

				this.curElemOffset = other.curElemOffset;
				this.curElemVertTextIndex = other.curElemVertTextIndex;
				this.curElemVertIconIndex = other.curElemVertIconIndex;
				this.curElemWrapIndex = other.curElemWrapIndex;
				this.curElemText = other.curElemText;
			}
			public RenderState Clone()
			{
				return new RenderState(this);
			}

			public Element NextElement(bool stopAtLineBreak = false)
			{
				if (this.elemIndex >= this.parent.elements.Length) return null;
				Element elem = this.parent.elements[this.elemIndex];

				if (elem is TextElement && this.font != null)
				{
					TextElement textElem = elem as TextElement;

					string textToDisplay;
					string fittingText;
					// Word wrap by glyph / word
					if (this.parent.maxWidth > 0 && this.parent.wrapMode != WrapMode.Element)
					{
						textToDisplay = textElem.Text.Substring(this.curElemWrapIndex, textElem.Text.Length - this.curElemWrapIndex);
						fittingText = this.font.FitText(textToDisplay, this.lineAvailWidth - (this.offset.X - this.lineBeginX), this.parent.wrapMode == WrapMode.Word);

						// If by-word results in instant line break: Do it by glyph instead
						if (this.offset.X == this.lineBeginX && fittingText.Length == 0 && this.parent.wrapMode == WrapMode.Word) 
							fittingText = this.font.FitText(textToDisplay, this.lineAvailWidth - (this.offset.X - this.lineBeginX), false);

						// If doing it by glyph results in an instant line break: Use at least one glyph anyway
						if (this.lineAvailWidth == this.parent.maxWidth && 
							this.offset.X == this.lineBeginX && 
							this.parent.maxHeight == 0 &&
							fittingText.Length == 0) fittingText = textToDisplay.Substring(0, 1);
					}
					// No word wrap (or by whole element)
					else
					{
						textToDisplay = textElem.Text;
						fittingText = textElem.Text;
					}
					Vector2 textElemSize = this.font.MeasureText(fittingText);

					// Perform word wrap by whole Element
					if (this.parent.maxWidth > 0 && this.parent.wrapMode == WrapMode.Element)
					{
						if ((this.lineAvailWidth < this.parent.maxWidth || this.offset.X > this.lineBeginX || this.parent.maxHeight > 0) && 
							this.offset.X + textElemSize.X > this.lineAvailWidth)
						{
							if (stopAtLineBreak)	return null;
							else					this.PerformNewLine();
							if (this.offset.Y + this.lineHeight > this.parent.maxHeight) return null;
						}
					}

					this.curElemText = fittingText;
					this.curElemVertTextIndex = this.vertTextIndex[this.fontIndex];
					this.curElemOffset = this.offset;

					// If it all fits: Stop wrap mode, proceed with next element
					if (fittingText.Length == textToDisplay.Length)
					{
						this.curElemWrapIndex = 0;
						this.elemIndex++;
					}
					// If only some part fits: Move wrap index & return
					else if (fittingText.Length > 0)
					{
						this.curElemWrapIndex += fittingText.Length;
					}
					// If nothing fits: Begin a new line & return
					else
					{
						if (stopAtLineBreak)	return null;
						else					this.PerformNewLine();
						if (this.offset.Y + this.lineHeight > this.parent.maxHeight) return null;
					}

					this.vertTextIndex[this.fontIndex] += fittingText.Length * 4;
					this.offset.X += textElemSize.X;
					this.lineWidth += textElemSize.X;
					this.lineHeight = Math.Max(this.lineHeight, this.font.Height);
				}
				else if (elem is TextElement && this.font == null)
				{
					this.elemIndex++;
				}
				else if (elem is IconElement)
				{
					IconElement iconElem = elem as IconElement;
					Icon icon = iconElem.IconIndex >= 0 && iconElem.IconIndex < this.parent.icons.Length ? this.parent.icons[iconElem.IconIndex] : new Icon();

					// Word Wrap
					if (this.parent.maxWidth > 0)
					{
						while ((this.lineAvailWidth < this.parent.maxWidth || this.offset.X > this.lineBeginX || this.parent.maxHeight > 0) && 
							this.offset.X - this.lineBeginX + icon.size.X > this.lineAvailWidth)
						{
							if (stopAtLineBreak)	return null;
							else					this.PerformNewLine();
							if (this.offset.Y + this.lineHeight > this.parent.maxHeight) return null;
						}
					}

					this.curElemVertIconIndex = this.vertIconIndex;
					this.curElemOffset = this.offset;

					this.vertIconIndex += 4;
					this.offset.X += icon.size.X;
					this.lineWidth += icon.size.X;
					this.lineHeight = Math.Max(this.lineHeight, icon.size.Y);
					this.lineBaseLine = Math.Max(this.lineBaseLine, (int)Math.Round(icon.size.Y));
					this.elemIndex++;
				}
				else if (elem is FontChangeElement)
				{
					FontChangeElement fontChangeElem = elem as FontChangeElement;
					ContentRef<Font> font = this.fontIndex >= 0 && this.fontIndex < this.parent.fonts.Length ? this.parent.fonts[this.fontIndex] : ContentRef<Font>.Null;
					this.fontIndex = fontChangeElem.FontIndex;
					this.font = font.Res;
					if (font.IsAvailable) this.lineBaseLine = Math.Max(this.lineBaseLine, font.Res.BaseLine);
					this.elemIndex++;
				}
				else if (elem is ColorChangeElement)
				{
					ColorChangeElement colorChangeElem = elem as ColorChangeElement;
					this.color = colorChangeElem.Color;
					this.elemIndex++;
				}
				else if (elem is AlignChangeElement)
				{
					AlignChangeElement alignChangeElem = elem as AlignChangeElement;
					this.lineAlign = alignChangeElem.Align;
					this.elemIndex++;
				}
				else if (elem is NewLineElement)
				{
					this.elemIndex++;
					if (stopAtLineBreak)	return null;
					else					this.PerformNewLine();
					if (this.offset.Y + this.lineHeight > this.parent.maxHeight) return null;
				}

				return elem;
			}

			private int GetFlowAreaMinXAt(int y, int h)
			{
				if (this.parent.flowAreas == null) return 0;
				int minX = 0;
				for (int i = 0; i < this.parent.flowAreas.Length; i++)
				{
					if (this.parent.flowAreas[i].alignRight) continue;
					if (y + h < this.parent.flowAreas[i].y || y > this.parent.flowAreas[i].y + this.parent.flowAreas[i].height) continue;
					minX = Math.Max(minX, this.parent.flowAreas[i].width);
				}
				return minX;
			}
			private int GetFlowAreaMaxXAt(int y, int h)
			{
				if (this.parent.flowAreas == null) return this.parent.maxWidth;
				int maxX = this.parent.maxWidth;
				for (int i = 0; i < this.parent.flowAreas.Length; i++)
				{
					if (!this.parent.flowAreas[i].alignRight) continue;
					if (y + h < this.parent.flowAreas[i].y || y > this.parent.flowAreas[i].y + this.parent.flowAreas[i].height) continue;
					maxX = Math.Min(maxX, this.parent.maxWidth - this.parent.flowAreas[i].width);
				}
				return maxX;
			}
			private void PerformNewLine()
			{
				// Advance to new line
				this.offset.Y += this.lineHeight;
				this.lineIndex++;
				// Init new line
				this.PeekLineStats();
				this.offset.X = this.lineBeginX;
			}
			private void PeekLineStats()
			{
				// First pass: Determine line width, height & base line
				this.lineBaseLine = this.font != null ? this.font.BaseLine : 0;
				this.lineHeight = this.font != null ? this.font.Height : 0;
				this.lineBeginX = 0.0f;
				this.lineWidth = 0.0f;
				this.lineAvailWidth = this.parent.maxWidth;
				this.offset.X = this.lineBeginX;

				RenderState peekState = this.Clone();
				while (peekState.NextElement(true) != null);

				this.lineBaseLine = peekState.lineBaseLine;
				this.lineWidth = peekState.lineWidth;
				this.lineHeight = peekState.lineHeight;
				this.lineAlign = peekState.lineAlign;

				// Second pass: Obey flow areas
				if (this.parent.flowAreas != null && this.parent.flowAreas.Length > 0)
				{
					this.lineBeginX = this.GetFlowAreaMinXAt((int)this.offset.Y, (int)this.lineHeight);
					this.lineAvailWidth = this.GetFlowAreaMaxXAt((int)this.offset.Y, (int)this.lineHeight) - this.lineBeginX;
					this.lineBaseLine = this.font != null ? this.font.BaseLine : 0;
					this.lineHeight = this.font != null ? this.font.Height : 0;
					this.lineWidth = 0.0f;
					this.offset.X = this.lineBeginX;

					peekState = this.Clone();
					while (peekState.NextElement(true) != null);

					this.lineBaseLine = peekState.lineBaseLine;
					this.lineWidth = peekState.lineWidth;
					this.lineHeight = peekState.lineHeight;
					this.lineAlign = peekState.lineAlign;
				}

				// Apply alignment
				if (this.parent.maxWidth != 0)
				{
					if (this.lineAlign == Alignment.Right)
						this.lineBeginX += this.lineAvailWidth - this.lineWidth;
					else if (this.lineAlign == Alignment.Center)
						this.lineBeginX += (this.lineAvailWidth - this.lineWidth) / 2;

					this.lineBeginX = MathF.Round(this.lineBeginX);
				}
			}
		}


		private	string				sourceText		= null;
		private	Icon[]				icons			= null;
		private	FlowArea[]			flowAreas		= null;
		private	ContentRef<Font>[]	fonts			= null;
		private	int					maxWidth		= 0;
		private	int					maxHeight		= 0;
		private	WrapMode			wrapMode		= WrapMode.Word;

		private	string				displayedText	= null;
		private	int[]				fontGlyphCount	= null;
		private	int					iconCount		= 0;
		private	Element[]			elements		= null;


		public string SourceText
		{
			get { return this.sourceText; }
			set { this.ApplySource(value); }
		}
		public Icon[] Icons
		{
			get { return this.icons; }
			set { this.SetIcons(value); }
		}
		public FlowArea[] FlowAreas
		{
			get { return this.flowAreas; }
			set { this.SetFlowAreas(value); }
		}
		public ContentRef<Font>[] Fonts
		{
			get { return this.fonts; }
			set { this.SetFonts(value); }
		}
		public int MaxWidth
		{
			get { return this.maxWidth; }
			set { this.maxWidth = value; }
		}
		public int MaxHeight
		{
			get { return this.maxHeight; }
			set { this.maxHeight = value; }
		}
		public WrapMode WordWrap
		{
			get { return this.wrapMode; }
			set { this.wrapMode = value; }
		}

		public string DisplayedText
		{
			get { return this.displayedText; }
		}
		public Element[] Elements
		{
			get { return this.elements; }
		}



		public FormattedText(string text)
		{
			this.ApplySource(text);
		}
		public FormattedText(FormattedText other)
		{
			this.sourceText = other.sourceText;
			this.icons		= other.icons != null ? (Icon[])other.icons.Clone() : null;
			this.flowAreas	= other.flowAreas != null ? (FlowArea[])other.flowAreas.Clone() : null;
			this.fonts		= other.fonts != null ? (ContentRef<Font>[])other.fonts.Clone() : null;
			this.maxWidth	= other.maxWidth;
			this.maxHeight	= other.maxHeight;
			this.wrapMode	= other.wrapMode;

			this.ApplySource(this.sourceText);
		}
		public FormattedText Clone()
		{
			return new FormattedText(this);
		}

		public void ApplySource(string text = null)
		{
			if (text == null) text = this.sourceText;

			this.sourceText = text;
			this.iconCount = 0;

			List<int> fontGlyphCounter = new List<int>();
			List<Element> elemList = new List<Element>();
			StringBuilder displayedTextBuilder = new StringBuilder();
			StringBuilder curTextElemTextBuilder = new StringBuilder();
			int curTextElemBegin = 0;
			int curTextElemLen = 0;
			int curFontIndex = 0;
			for (int i = 0; i < this.sourceText.Length; i++)
			{
				if (this.sourceText[i] == '/' && i + 1 < this.sourceText.Length)
				{
					i++;

					curTextElemTextBuilder.Append(this.sourceText, curTextElemBegin, curTextElemLen);

					if (this.sourceText[i] == '/')
					{
						curTextElemTextBuilder.Append('/');
					}
					else
					{
						if (curTextElemTextBuilder.Length > 0)
						{
							string textElem = curTextElemTextBuilder.ToString();
							elemList.Add(new TextElement(textElem));
							displayedTextBuilder.Append(textElem);
							curTextElemTextBuilder.Clear();

							while (fontGlyphCounter.Count <= curFontIndex) fontGlyphCounter.Add(0);
							fontGlyphCounter[curFontIndex] += textElem.Length;
						}

						if (this.sourceText[i] == 'c')
						{
							if (this.sourceText.Length > i + 8)
							{
								uint	clr;
								string	clrString = new StringBuilder().Append(this.sourceText, i + 1, 8).ToString();
								if (uint.TryParse(clrString, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.InvariantInfo, out clr))
									elemList.Add(new ColorChangeElement(ColorRGBA.FromIntRgba(clr)));
								else
									elemList.Add(new ColorChangeElement(ColorRGBA.White));

								i += 8;
							}
						}
						if (this.sourceText[i] == 'e')
						{
							// Just separates elements
						}
						else if (this.sourceText[i] == 'a')
						{
							if (this.sourceText.Length > i + 1)
							{
								string alignString = new StringBuilder().Append(this.sourceText, i + 1, 1).ToString();
								if (alignString == "l")
									elemList.Add(new AlignChangeElement(Alignment.Left));
								else if (alignString == "r")
									elemList.Add(new AlignChangeElement(Alignment.Right));
								else
									elemList.Add(new AlignChangeElement(Alignment.Center));

								i += 1;
							}
						}
						else if (this.sourceText[i] == 'f')
						{
							int indexOfClose = this.sourceText.IndexOf(']', i + 1);
							if (indexOfClose != -1)
							{
								string numStr = this.sourceText.Substring(i + 2, indexOfClose - (i + 2));
								int num;
								if (int.TryParse(numStr, out num))
									elemList.Add(new FontChangeElement(num));
								else
									elemList.Add(new FontChangeElement(0));

								curFontIndex = num;
								i += 2 + numStr.Length;
							}
						}
						else if (this.sourceText[i] == 'i')
						{
							int indexOfClose = this.sourceText.IndexOf(']', i + 1);
							string numStr = this.sourceText.Substring(i + 2, indexOfClose - (i + 2));
							int num;
							if (int.TryParse(numStr, out num))
								elemList.Add(new IconElement(num));
							else
								elemList.Add(new IconElement(0));

							this.iconCount++;
							i += 2 + numStr.Length;
						}
						else if (this.sourceText[i] == 'n')
						{
							elemList.Add(new NewLineElement());
						}

					}

					curTextElemLen = 0;
					curTextElemBegin = i + 1;
				}
				else
				{
					curTextElemLen++;
				}
			}

			if (curTextElemLen > 0) curTextElemTextBuilder.Append(this.sourceText, curTextElemBegin, curTextElemLen);
			if (curTextElemTextBuilder.Length > 0)
			{
				string textElem = curTextElemTextBuilder.ToString();
				elemList.Add(new TextElement(textElem));
				displayedTextBuilder.Append(textElem);

				while (fontGlyphCounter.Count <= curFontIndex) fontGlyphCounter.Add(0);
				fontGlyphCounter[curFontIndex] += textElem.Length;
			}

			this.fontGlyphCount = fontGlyphCounter.ToArray();
			this.displayedText = displayedTextBuilder.ToString();
			this.elements = elemList.ToArray();
		}
		public void SetIcons(params Icon[] icons)
		{
			this.icons = icons;
		}
		public void SetFlowAreas(params FlowArea[] flowAreas)
		{
			this.flowAreas = flowAreas;
		}
		public void SetFonts(params ContentRef<Font>[] fonts)
		{
			this.fonts = fonts;
		}

		public void EmitVertices(ref VertexC4P3T2[][] vertText, ref VertexC4P3T2[] vertIcons, float x, float y, float z = 0.0f)
		{
			this.EmitVertices(ref vertText, ref vertIcons, x, y, z, ColorRGBA.White);
		}
		public void EmitVertices(ref VertexC4P3T2[][] vertText, ref VertexC4P3T2[] vertIcons, float x, float y, ColorRGBA clr)
		{
			this.EmitVertices(ref vertText, ref vertIcons);
			
			Vector3 offset = new Vector3(x, y, 0);

			for (int i = 0; i < vertText.Length; i++)
			{
				for (int j = 0; j < vertText[i].Length; j++)
				{
					Vector3 vertex = vertText[i][j].pos;
					vertex += offset;
					vertText[i][j].pos = vertex;
					vertText[i][j].clr *= clr;
				}
			}
			for (int i = 0; i < vertIcons.Length; i++)
			{
				Vector3 vertex = vertIcons[i].pos;
				vertex += offset;
				vertIcons[i].pos = vertex;
				vertIcons[i].clr *= clr;
			}
		}
		public void EmitVertices(ref VertexC4P3T2[][] vertText, ref VertexC4P3T2[] vertIcons, float x, float y, float z, ColorRGBA clr, float angle = 0.0f, float scale = 1.0f)
		{
			Vector2 xDot, yDot;
			MathF.GetTransformDotVec(angle, scale, out xDot, out yDot);
			this.EmitVertices(ref vertText, ref vertIcons, x, y, z, clr, xDot, yDot);
		}
		public void EmitVertices(ref VertexC4P3T2[][] vertText, ref VertexC4P3T2[] vertIcons, float x, float y, float z, ColorRGBA clr, Vector2 xDot, Vector2 yDot)
		{
			this.EmitVertices(ref vertText, ref vertIcons);
			
			Vector3 offset = new Vector3(x, y, z);
			for (int i = 0; i < vertText.Length; i++)
			{
				for (int j = 0; j < vertText[i].Length; j++)
				{
					Vector3 vertex = vertText[i][j].pos;

					MathF.TransdormDotVec(ref vertex, ref xDot, ref yDot);
					vertex += offset;

					vertText[i][j].pos = vertex;
					vertText[i][j].clr *= clr;
				}
			}
			for (int i = 0; i < vertIcons.Length; i++)
			{
				Vector3 vertex = vertIcons[i].pos;

				MathF.TransdormDotVec(ref vertex, ref xDot, ref yDot);
				vertex += offset;

				vertIcons[i].pos = vertex;
				vertIcons[i].clr *= clr;
			}
		}
		public void EmitVertices(ref VertexC4P3T2[][] vertText, ref VertexC4P3T2[] vertIcons)
		{
			int fontNum = this.fonts != null ? this.fonts.Length : 0;

			// Setting up vertex buffers
			if (vertIcons == null || vertIcons.Length != this.iconCount * 4) vertIcons = new VertexC4P3T2[this.iconCount * 4];
			if (vertText == null || vertText.Length != fontNum) vertText = new VertexC4P3T2[fontNum][];
			for (int i = 0; i < vertText.Length; i++)
				if (vertText[i] == null || vertText[i].Length != (this.fontGlyphCount.Length > i ? this.fontGlyphCount[i] * 4 : 0)) 
					vertText[i] = new VertexC4P3T2[this.fontGlyphCount.Length > i ? this.fontGlyphCount[i] * 4 : 0];

			// Rendering
			RenderState state = new RenderState(this);
			Element elem;
			int[] vertTextLen = new int[fontNum];
			int vertIconLen = 0;
			while ((elem = state.NextElement()) != null)
			{
				if (elem is TextElement && state.Font != null)
				{
					TextElement textElem = elem as TextElement;
					VertexC4P3T2[] textElemVert = null;
					state.Font.EmitTextVertices(
						state.CurrentElemText, 
						ref textElemVert, 
						state.CurrentElemOffset.X, 
						state.CurrentElemOffset.Y + state.LineBaseLine - state.Font.BaseLine, 
						state.Color);
					Array.Copy(textElemVert, 0, vertText[state.FontIndex], state.CurrentElemTextVertexIndex, textElemVert.Length);
					vertTextLen[state.FontIndex] = state.CurrentElemTextVertexIndex + textElemVert.Length;
				}
				else if (elem is IconElement)
				{
					IconElement iconElem = elem as IconElement;
					Icon icon = iconElem.IconIndex > 0 && iconElem.IconIndex < this.icons.Length ? this.icons[iconElem.IconIndex] : new Icon();
					Vector2 iconSize = icon.size;
					Rect iconUvRect = icon.uvRect;

					vertIcons[state.CurrentElemIconVertexIndex + 0].pos.X = state.CurrentElemOffset.X;
					vertIcons[state.CurrentElemIconVertexIndex + 0].pos.Y = state.CurrentElemOffset.Y + state.LineBaseLine - iconSize.Y;
					vertIcons[state.CurrentElemIconVertexIndex + 0].pos.Z = 0;
					vertIcons[state.CurrentElemIconVertexIndex + 0].clr = state.Color;
					vertIcons[state.CurrentElemIconVertexIndex + 0].texCoord = iconUvRect.TopLeft;

					vertIcons[state.CurrentElemIconVertexIndex + 1].pos.X = state.CurrentElemOffset.X + iconSize.X;
					vertIcons[state.CurrentElemIconVertexIndex + 1].pos.Y = state.CurrentElemOffset.Y + state.LineBaseLine - iconSize.Y;
					vertIcons[state.CurrentElemIconVertexIndex + 1].pos.Z = 0;
					vertIcons[state.CurrentElemIconVertexIndex + 1].clr = state.Color;
					vertIcons[state.CurrentElemIconVertexIndex + 1].texCoord = iconUvRect.TopRight;

					vertIcons[state.CurrentElemIconVertexIndex + 2].pos.X = state.CurrentElemOffset.X + iconSize.X;
					vertIcons[state.CurrentElemIconVertexIndex + 2].pos.Y = state.CurrentElemOffset.Y + state.LineBaseLine;
					vertIcons[state.CurrentElemIconVertexIndex + 2].pos.Z = 0;
					vertIcons[state.CurrentElemIconVertexIndex + 2].clr = state.Color;
					vertIcons[state.CurrentElemIconVertexIndex + 2].texCoord = iconUvRect.BottomRight;

					vertIcons[state.CurrentElemIconVertexIndex + 3].pos.X = state.CurrentElemOffset.X;
					vertIcons[state.CurrentElemIconVertexIndex + 3].pos.Y = state.CurrentElemOffset.Y + state.LineBaseLine;
					vertIcons[state.CurrentElemIconVertexIndex + 3].pos.Z = 0;
					vertIcons[state.CurrentElemIconVertexIndex + 3].clr = state.Color;
					vertIcons[state.CurrentElemIconVertexIndex + 3].texCoord = iconUvRect.BottomLeft;

					vertIconLen = state.CurrentElemIconVertexIndex + 4;
				}
			}

			for (int i = 0; i < fontNum; i++)
			{
				if (vertText[i].Length > vertTextLen[i])
					Array.Resize(ref vertText[i], vertTextLen[i]);
			}
			if (vertIcons.Length > vertIconLen)
				Array.Resize(ref vertIcons, vertIconLen);
		}

		public Metrics Measure()
		{
			Vector2 size = Vector2.Zero;
			List<Rect> lineBounds = new List<Rect>();
			List<Rect> elementBounds = new List<Rect>();

			RenderState state = new RenderState(this);
			Element elem;
			Vector2 elemSize;
			Vector2 elemOffset;
			int lastElemIndex = -1;
			int lastLineIndex = 0;
			bool elemIndexChanged = true;
			bool lineChanged = true;
			bool hasBounds;
			while ((elem = state.NextElement()) != null)
			{
				if (elem is TextElement && state.Font != null)
				{
					TextElement textElem = elem as TextElement;
					elemSize = state.Font.MeasureText(state.CurrentElemText);
					elemOffset = new Vector2(state.CurrentElemOffset.X, state.CurrentElemOffset.Y + state.LineBaseLine - state.Font.Ascent);
					hasBounds = elemSize != Vector2.Zero;
				}
				else if (elem is IconElement)
				{
					IconElement iconElem = elem as IconElement;
					elemSize = this.icons[iconElem.IconIndex].size;
					elemOffset = new Vector2(state.CurrentElemOffset.X, state.CurrentElemOffset.Y + state.LineBaseLine - elemSize.Y);
					hasBounds = true;
				}
				else
				{
					elemSize = Vector2.Zero;
					elemOffset = Vector2.Zero;
					hasBounds = false;
				}

				if (elemIndexChanged) elementBounds.Add(Rect.Empty);
				if (hasBounds && elementBounds[elementBounds.Count - 1] == Rect.Empty)
					elementBounds[elementBounds.Count - 1] = new Rect(elemOffset.X, elemOffset.Y, elemSize.X, elemSize.Y);
				else if (hasBounds)
					elementBounds[elementBounds.Count - 1] = elementBounds[elementBounds.Count - 1].ExpandToContain(elemOffset.X, elemOffset.Y, elemSize.X, elemSize.Y);
				
				if (lineChanged) lineBounds.Add(Rect.Empty);
				if (hasBounds && lineBounds[lineBounds.Count - 1] == Rect.Empty)
					lineBounds[lineBounds.Count - 1] = new Rect(elemOffset.X, elemOffset.Y, elemSize.X, elemSize.Y);
				else if (hasBounds)
					lineBounds[lineBounds.Count - 1] = lineBounds[lineBounds.Count - 1].ExpandToContain(elemOffset.X, elemOffset.Y, elemSize.X, elemSize.Y);

				size.X = Math.Max(size.X, elemOffset.X + elemSize.X);
				size.Y = Math.Max(size.Y, elemOffset.Y + elemSize.Y);

				elemIndexChanged = lastElemIndex != state.CurrentElemIndex;
				lineChanged = lastLineIndex != state.CurrentLineIndex;
				lastElemIndex = state.CurrentElemIndex;
				lastLineIndex = state.CurrentLineIndex;
			}

			return new Metrics(size, lineBounds, elementBounds);
		}
	}
}
