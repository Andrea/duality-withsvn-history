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
	public static class TextHelper
	{
		public	const	string	FormatColor		= "/c";
		public	const	string	FormatFont		= "/f";
		public	const	string	FormatIcon		= "/i";
		public	const	string	FormatNewline	= "/n";

		[Serializable]
		public class FormattedText
		{
			[Serializable]
			public abstract class Element 
			{
				private	ColorRGBA	color;

				// ToDo
			}
			[Serializable]
			public class TextElement : Element
			{
				private	string				text;
				private	ContentRef<Font>	font;

				// ToDo
			}
			[Serializable]
			public class IconElement : Element
			{
				private	Rect	iconUV;

				// ToDo
			}

			private	string		originalText	= null;
			private	string		displayedText	= null;
			private	Element[]	elements		= null;

			public FormattedText(string text)
			{
				this.SetText(text);
			}

			public void SetText(string text)
			{
				this.originalText = text;
				this.displayedText = this.originalText;
			}

			// ToDo
		}

		public static void DrawTextFormatted(FormattedText text, ref VertexC4P3T2[] textVertices, ref VertexC4P3T2[] iconVertices)
		{
			// ToDo
		}
	}
}
