namespace CustomPropertyGrid.EditorTemplates
{
	partial class ComboBoxDropDown
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listBox
			// 
			this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBox.IntegralHeight = false;
			this.listBox.Location = new System.Drawing.Point(0, 0);
			this.listBox.Margin = new System.Windows.Forms.Padding(0);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(120, 87);
			this.listBox.TabIndex = 0;
			this.listBox.Click += new System.EventHandler(this.listBox_Click);
			this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
			// 
			// ComboBoxDropDown
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(120, 87);
			this.ControlBox = false;
			this.Controls.Add(this.listBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ComboBoxDropDown";
			this.ShowInTaskbar = false;
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox;
	}
}