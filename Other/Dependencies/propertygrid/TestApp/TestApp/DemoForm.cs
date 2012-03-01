﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomPropertyGrid
{
	public partial class DemoForm : Form
	{
		#region Some Test / Demo classes
		[Flags]
		private enum FlaggedEnumTest
		{
			One	= 0x1,
			Two	= 0x2,
			Three = 0x4,

			OneAndThree = One | Three,
			None = 0x0,
			All = One | Two | Three
		}
		private enum EnumTest
		{
			One,
			Two,
			Three
		}
		private class Test
		{
			private int i;
			private int i2;
			private float f;
			private byte b;
			private int[] i3;
			private string t;
			private Test2 subclass;
			public List<string> sl;
			public FlaggedEnumTest enum1;
			public EnumTest enum2;

			public int IPropWithAVeryLongName
			{
				get { return this.i; }
				set { this.i = value; }
			}
			public int SomeInt
			{
				get { return this.i2; }
				set { this.i2 = value * 2; }
			}
			public float SomeFloat
			{
				get { return this.f; }
				set { this.f = value; }
			}
			public byte SomeByte
			{
				get { return this.b; }
				set { this.b = value; }
			}
			public int[] SomeIntArray
			{
				get { return this.i3; }
				set { this.i3 = value; }
			}
			public string SomeString
			{
				get { return this.t; }
				set { this.t = value; }
			}
			public Test2 Subclass
			{
				get { return this.subclass; }
				set { this.subclass = value; }
			}
		}
		private struct Test2
		{
			private int yoink;

			public int Yoink
			{
				get { return this.yoink; }
				set { this.yoink = value; }
			}

			public Test2(int val)
			{
				this.yoink = val;
			}
		}
		#endregion

		public DemoForm()
		{
			this.InitializeComponent();

			// Generate some test / demo objects
			Test testObj = new Test();
			testObj.IPropWithAVeryLongName = 42;
			testObj.SomeString = "Blubdiwupp";
			testObj.SomeIntArray = new int[] { 3, 4, 5 };
			testObj.Subclass = new Test2(42);
			testObj.sl = new List<string>() { "hallo", "welt" };
			Test testObj2 = new Test();
			testObj2.IPropWithAVeryLongName = 447;
			testObj2.SomeString = "Blubdiwupp";
			testObj2.Subclass = new Test2();
			testObj2.enum2 = EnumTest.Three;

			// Case A: Singleselect
			this.propertyGrid1.SelectObjects(new object[] { true });
			// Case B: Multiselect
			//this.propertyGrid1.SelectObjects(new object[] { testObj, testObj2 });
			// Case C: Select this very form
			//this.propertyGrid1.SelectObjects(new object[] { this });
		}
	}
}
