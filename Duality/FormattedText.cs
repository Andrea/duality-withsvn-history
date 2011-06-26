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
		public	const	string	FormatSlash		= "//";
		public	const	string	FormatColor		= "/c";
		public	const	string	FormatFont		= "/f";
		public	const	string	FormatIcon		= "/i";
		public	const	string	FormatNewline	= "/n";


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


		private	string				sourceText		= null;
		private	string				displayedText	= null;
		private	Element[]			elements		= null;
		private	Rect[]				icons			= null;
		private	ContentRef<Font>[]	fonts			= null;


		public string SourceText
		{
			get { return this.sourceText; }
			set { this.SetSource(value); }
		}
		public Rect[] Icons
		{
			get { return this.icons; }
			set { this.icons = value; }
		}
		public ContentRef<Font>[] Fonts
		{
			get { return this.fonts; }
			set { this.fonts = value; }
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
			this.SetSource(text);
		}

		public void SetSource(string text)
		{
			this.sourceText = text;

			List<Element> elemList = new List<Element>();
			StringBuilder displayedTextBuilder = new StringBuilder();
			StringBuilder curTextElemTextBuilder = new StringBuilder();
			int curTextElemBegin = 0;
			int curTextElemLen = 0;
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
						}

						if (this.sourceText[i] == 'c')
						{
							uint	clr;
							string	clrString = new StringBuilder().Append(this.sourceText, i + 1, 8).ToString();
							if (uint.TryParse(clrString, System.Globalization.NumberStyles.HexNumber, System.Globalization.NumberFormatInfo.InvariantInfo, out clr))
								elemList.Add(new ColorChangeElement(ColorRGBA.FromIntRgba(clr)));
							else
								elemList.Add(new ColorChangeElement(ColorRGBA.White));

							i += 8;
						}
						else if (this.sourceText[i] == 'f')
						{
							int indexOfClose = this.sourceText.IndexOf(']', i + 1);
							string numStr = this.sourceText.Substring(i + 2, indexOfClose - (i + 2));
							int num;
							if (int.TryParse(numStr, out num))
								elemList.Add(new FontChangeElement(num));
							else
								elemList.Add(new FontChangeElement(0));

							i += 2 + numStr.Length;
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
			}

			this.displayedText = displayedTextBuilder.ToString();
			this.elements = elemList.ToArray();
		}
	}
}
