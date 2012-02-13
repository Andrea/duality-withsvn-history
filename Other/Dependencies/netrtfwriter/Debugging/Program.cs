using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using DW.RtfWriter;

namespace Debugging
{
	class Program
	{
		static void Main(string[] args)
		{
			/// Create document by specifying paper size and orientation, 
			/// and default language.
			RtfDocument doc = new RtfDocument(PaperSize.A4, PaperOrientation.Portrait,
				Lcid.TraditionalChinese);
			/// Create fonts and colors for later use
			FontDescriptor times = doc.createFont("Times New Roman");
			FontDescriptor courier = doc.createFont("Courier New");
			ColorDescriptor red = doc.createColor(new Color("ff0000"));
			ColorDescriptor blue = doc.createColor(new Color(0, 0, 255));

			/// Don't instantiate RtfTable, RtfParagraph, and RtfImage objects by using
			/// ``new'' keyword. Instead, use add* method in objects derived from 
			/// RtfBlockList class. (See Demos.)
			RtfTable table;
			RtfParagraph par;
			RtfImage img;
			/// Don't instantiate RtfCharFormat by using ``new'' keyword, either. 
			/// An addCharFormat method are provided by RtfParagraph objects.
			RtfCharFormat fmt;
			

			/// ==========================================================================
			/// Demo 1: Font Setting
			/// ==========================================================================
			/// If you want to use Latin characters only, it is as simple as assigning
			/// ``Font'' property of RtfCharFormat objects. If you want to render Far East 
			/// characters with some font, and Latin characters with another, you may 
			/// assign the Far East font to ``Font'' property and the Latin font to 
			/// ``AnsiFont'' property. This Demo contains Traditional Chinese characters.
			/// (Note: non-Latin characters are unicoded so you don't have to be worried.)
			par = doc.addParagraph();
			par.DefaultCharFormat.Font = doc.createFont("Tahoma");
			par.DefaultCharFormat.AnsiFont = courier;
			par.DefaultCharFormat.FgColor = blue;
			par.DefaultCharFormat.BgColor = red;
			par.Text = "Demo1: Some Text";


			/// ==========================================================================
			/// Demo 2: Character Formatting
			/// ==========================================================================
			par = doc.addParagraph();
			par.DefaultCharFormat.Font = times;
			par.Text = "Demo2: Character Formatting";
			/// Besides setting default character formats of a paragraph, you can specify
			/// a range of characters to which formatting is applied. For convenience,
			/// let's call it range formatting. The following section sets formatting 
			/// for the 4th, 5th, ..., 8th characters in the paragraph. (Note: the first
			/// character has an index of 0)
			fmt = par.addCharFormat(4, 8);
			fmt.FgColor = blue;
			fmt.BgColor = red;
			fmt.FontSize = 18;
			/// Sets another range formatting. Note that when range formatting overlaps, 
			/// the latter formatting will overwrite the former ones. In the following, 
			/// formatting for the 8th chacacter is overwritten.
			fmt = par.addCharFormat(8, 10);
			fmt.FontStyle.addStyle(FontStyleFlag.Bold);
			fmt.FontStyle.addStyle(FontStyleFlag.Underline);
			fmt.Font = courier;
			

			/// ==========================================================================
			/// Demo 3: Footnote
			/// ==========================================================================
			par = doc.addParagraph();
			par.Text = "Demo3: Footnote";
			/// In this example, the footnote is inserted just after the 7th character in
			/// the paragraph.
			par.addFootnote(7).addParagraph().Text = "Footnote details here.";


			/// ==========================================================================
			/// Demo 4: Header and Footer
			/// ==========================================================================
			/// You may use ``Header'' and ``Footer'' properties of RtfDocument objects to
			/// specify information to be displayed in the header and footer of every page,
			/// respectively.
			par = doc.Footer.addParagraph();
			par.Text = "Demo4: Page: / Date: Time:";
			par.Alignment = Align.Center;
			par.DefaultCharFormat.FontSize = 15;
			/// You may insert control words, including page number, total pages, date and
			/// time, into the header and/or the footer. 
			par.addControlWord(12, RtfFieldControlWord.FieldType.Page);
			par.addControlWord(13, RtfFieldControlWord.FieldType.NumPages);
			par.addControlWord(19, RtfFieldControlWord.FieldType.Date);
			par.addControlWord(25, RtfFieldControlWord.FieldType.Time);
			/// Here we also add some text in header.
			par = doc.Header.addParagraph();
			par.Text = "Demo4: Header";
			

			/// ==========================================================================
			/// Demo 5: Image
			/// ==========================================================================
			img = doc.addImage("../../demo5.jpg", ImageFileType.Jpg);
			/// You may set the width only, and let the height be automatically adjusted 
			/// to keep aspect ratio.
			img.Width = 130;
			/// Place the image on a new page. The ``StartNewPage'' property is also supported
			/// by paragraphs and tables.
			img.StartNewPage = true;


			/// ==========================================================================
			/// demo 6: ���
			/// ==========================================================================
			/// Please be careful when dealing with tables, as most crashes come from them.
			/// If you follow steps below, the resulting RTF is not likely to crash your 
			/// MS Word.
			/// 
			/// Step 1. Plan and draw the table you want on a scratch paper.
			/// Step 2. Start with a MxN regular table.
			table = doc.addTable(5, 4);
			table.Margins[Direction.Bottom] = 20;
			/// Step 3. (Optional) Set text alignment for each cell, row height, column width,
			///			border style, etc.
			for (int i = 0; i < table.RowCount; i++) {
				for (int j = 0; j < table.ColCount; j++) {
					table.cell(i, j).Alignment = Align.Right;
					table.cell(i, j).AlignmentVertical = AlignVertical.Middle;
					table.cell(i, j).addParagraph().Text = "CELL " + i.ToString() + "," + j.ToString();
				}
			}
			table.setInnerBorder(BorderStyle.Dotted, 1.5f);
			table.setOuterBorder(BorderStyle.Double, 3f);
			/// Step 4. Merge cells so that the resulting table would look like the one you drew
			///			on paper. One cell cannot be merged twice. In this way, we can construct
			///			almost all kinds of tables we need.
			table.merge(1, 0, 4, 1);
			/// Step 5. You may start inserting content for each cell. Actually, it is adviced
			///			that the only thing you do after merging cell is inserting content.
			table.cell(1, 0).addParagraph().Text = "Demo6: Table";


			/// ==========================================================================
			/// Demo 7: ``Two in one'' format
			/// ==========================================================================
			/// This format is provisioned for Far East languages. This demo uses Traditional
			/// Chinese as an example.
			par = doc.addParagraph();
			par.Text = "Demo7: aaa�ñƤ�raaa";
			fmt = par.addCharFormat(10, 13);
			fmt.TwoInOneStyle = TwoInOneStyle.Braces;
			fmt.FontSize = 16;
			

			/// ==========================================================================
			/// Save
			/// ==========================================================================
			/// You may also retrieve RTF code string by calling to render() method of 
			/// RtfDocument objects.
			doc.save("Demo.rtf");


			/// ==========================================================================
			/// Open the RTF file we just saved
			/// ==========================================================================
			Process p = new Process();
			p.StartInfo.FileName = "Demo.rtf";
			p.Start();
		}
	}
}
