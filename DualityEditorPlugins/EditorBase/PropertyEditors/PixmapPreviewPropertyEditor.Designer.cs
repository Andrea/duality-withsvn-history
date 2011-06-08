namespace EditorBase.PropertyEditors
{
	partial class PixmapPreviewPropertyEditor
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tableLayout = new DualityEditor.Controls.DBTableLayoutPanel(this.components);
			this.labelPath = new System.Windows.Forms.Label();
			this.labelSizeValue = new System.Windows.Forms.Label();
			this.labelSize = new System.Windows.Forms.Label();
			this.previewBox = new System.Windows.Forms.Panel();
			this.tableLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.BackColor = System.Drawing.Color.Transparent;
			this.tableLayout.ColumnCount = 3;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Controls.Add(this.labelPath, 2, 0);
			this.tableLayout.Controls.Add(this.labelSizeValue, 1, 0);
			this.tableLayout.Controls.Add(this.labelSize, 0, 0);
			this.tableLayout.Controls.Add(this.previewBox, 0, 1);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 0);
			this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 2;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Size = new System.Drawing.Size(166, 100);
			this.tableLayout.TabIndex = 2;
			// 
			// labelPath
			// 
			this.labelPath.AutoSize = true;
			this.labelPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.labelPath.Location = new System.Drawing.Point(54, 0);
			this.labelPath.Margin = new System.Windows.Forms.Padding(0);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(112, 32);
			this.labelPath.TabIndex = 2;
			this.labelPath.Text = "Pixmap Path";
			this.labelPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelSizeValue
			// 
			this.labelSizeValue.AutoSize = true;
			this.labelSizeValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelSizeValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.labelSizeValue.Location = new System.Drawing.Point(41, 0);
			this.labelSizeValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelSizeValue.Name = "labelSizeValue";
			this.labelSizeValue.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.labelSizeValue.Size = new System.Drawing.Size(13, 32);
			this.labelSizeValue.TabIndex = 1;
			this.labelSizeValue.Text = "0\r\n0";
			// 
			// labelSize
			// 
			this.labelSize.AutoSize = true;
			this.labelSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelSize.Location = new System.Drawing.Point(0, 0);
			this.labelSize.Margin = new System.Windows.Forms.Padding(0);
			this.labelSize.Name = "labelSize";
			this.labelSize.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.labelSize.Size = new System.Drawing.Size(41, 32);
			this.labelSize.TabIndex = 0;
			this.labelSize.Text = "Width:\r\nHeight:";
			// 
			// previewBox
			// 
			this.previewBox.BackColor = System.Drawing.Color.Silver;
			this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayout.SetColumnSpan(this.previewBox, 3);
			this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.previewBox.Location = new System.Drawing.Point(0, 32);
			this.previewBox.Margin = new System.Windows.Forms.Padding(0);
			this.previewBox.Name = "previewBox";
			this.previewBox.Size = new System.Drawing.Size(166, 68);
			this.previewBox.TabIndex = 3;
			this.previewBox.DoubleClick += new System.EventHandler(this.previewBox_DoubleClick);
			// 
			// PixmapPreviewPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "PixmapPreviewPropertyEditor";
			this.Size = new System.Drawing.Size(166, 100);
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DualityEditor.Controls.DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Label labelPath;
		private System.Windows.Forms.Label labelSizeValue;
		private System.Windows.Forms.Label labelSize;
		private System.Windows.Forms.Panel previewBox;
	}
}
