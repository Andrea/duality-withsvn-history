namespace EditorBase.PropertyEditors
{
	partial class TexturePreviewPropertyEditor
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
			this.labelFrameValue = new System.Windows.Forms.Label();
			this.labelFrame = new System.Windows.Forms.Label();
			this.labelOglSizeValue = new System.Windows.Forms.Label();
			this.labelOglSize = new System.Windows.Forms.Label();
			this.labelPath = new System.Windows.Forms.Label();
			this.labelSizeValue = new System.Windows.Forms.Label();
			this.labelSize = new System.Windows.Forms.Label();
			this.previewBox = new System.Windows.Forms.Label();
			this.scrollAtlas = new System.Windows.Forms.VScrollBar();
			this.tableLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.BackColor = System.Drawing.Color.Transparent;
			this.tableLayout.ColumnCount = 6;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.Controls.Add(this.labelFrameValue, 1, 2);
			this.tableLayout.Controls.Add(this.labelFrame, 0, 2);
			this.tableLayout.Controls.Add(this.labelOglSizeValue, 3, 0);
			this.tableLayout.Controls.Add(this.labelOglSize, 2, 0);
			this.tableLayout.Controls.Add(this.labelPath, 4, 0);
			this.tableLayout.Controls.Add(this.labelSizeValue, 1, 0);
			this.tableLayout.Controls.Add(this.labelSize, 0, 0);
			this.tableLayout.Controls.Add(this.previewBox, 0, 1);
			this.tableLayout.Controls.Add(this.scrollAtlas, 5, 1);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 0);
			this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 3;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.Size = new System.Drawing.Size(337, 125);
			this.tableLayout.TabIndex = 2;
			// 
			// labelFrameValue
			// 
			this.labelFrameValue.AutoSize = true;
			this.labelFrameValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelFrameValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.labelFrameValue.Location = new System.Drawing.Point(41, 106);
			this.labelFrameValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelFrameValue.Name = "labelFrameValue";
			this.labelFrameValue.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.labelFrameValue.Size = new System.Drawing.Size(13, 19);
			this.labelFrameValue.TabIndex = 8;
			this.labelFrameValue.Text = "0";
			// 
			// labelFrame
			// 
			this.labelFrame.AutoSize = true;
			this.labelFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelFrame.Location = new System.Drawing.Point(0, 106);
			this.labelFrame.Margin = new System.Windows.Forms.Padding(0);
			this.labelFrame.Name = "labelFrame";
			this.labelFrame.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.labelFrame.Size = new System.Drawing.Size(41, 19);
			this.labelFrame.TabIndex = 7;
			this.labelFrame.Text = "Frame:\r\n";
			this.labelFrame.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelOglSizeValue
			// 
			this.labelOglSizeValue.AutoSize = true;
			this.labelOglSizeValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelOglSizeValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.labelOglSizeValue.Location = new System.Drawing.Point(114, 0);
			this.labelOglSizeValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelOglSizeValue.Name = "labelOglSizeValue";
			this.labelOglSizeValue.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.labelOglSizeValue.Size = new System.Drawing.Size(13, 32);
			this.labelOglSizeValue.TabIndex = 5;
			this.labelOglSizeValue.Text = "0\r\n0";
			// 
			// labelOglSize
			// 
			this.labelOglSize.AutoSize = true;
			this.labelOglSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelOglSize.Location = new System.Drawing.Point(54, 0);
			this.labelOglSize.Margin = new System.Windows.Forms.Padding(0);
			this.labelOglSize.Name = "labelOglSize";
			this.labelOglSize.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.labelOglSize.Size = new System.Drawing.Size(60, 32);
			this.labelOglSize.TabIndex = 4;
			this.labelOglSize.Text = "OglWidth:\r\nOglHeight:";
			this.labelOglSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPath
			// 
			this.labelPath.AutoEllipsis = true;
			this.labelPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.labelPath.Location = new System.Drawing.Point(127, 0);
			this.labelPath.Margin = new System.Windows.Forms.Padding(0);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(190, 32);
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
			this.labelSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// previewBox
			// 
			this.previewBox.BackColor = System.Drawing.Color.Silver;
			this.previewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayout.SetColumnSpan(this.previewBox, 5);
			this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.previewBox.ForeColor = System.Drawing.Color.White;
			this.previewBox.Location = new System.Drawing.Point(0, 32);
			this.previewBox.Margin = new System.Windows.Forms.Padding(0);
			this.previewBox.Name = "previewBox";
			this.previewBox.Size = new System.Drawing.Size(317, 74);
			this.previewBox.TabIndex = 3;
			this.previewBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.previewBox.DoubleClick += new System.EventHandler(this.previewBox_DoubleClick);
			// 
			// scrollAtlas
			// 
			this.scrollAtlas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrollAtlas.Enabled = false;
			this.scrollAtlas.LargeChange = 1;
			this.scrollAtlas.Location = new System.Drawing.Point(317, 32);
			this.scrollAtlas.Maximum = 0;
			this.scrollAtlas.Name = "scrollAtlas";
			this.scrollAtlas.Size = new System.Drawing.Size(20, 74);
			this.scrollAtlas.TabIndex = 6;
			this.scrollAtlas.ValueChanged += new System.EventHandler(this.scrollAtlas_ValueChanged);
			// 
			// TexturePreviewPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TexturePreviewPropertyEditor";
			this.Size = new System.Drawing.Size(337, 125);
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DualityEditor.Controls.DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Label labelPath;
		private System.Windows.Forms.Label labelSizeValue;
		private System.Windows.Forms.Label labelSize;
		private System.Windows.Forms.Label previewBox;
		private System.Windows.Forms.Label labelOglSizeValue;
		private System.Windows.Forms.Label labelOglSize;
		private System.Windows.Forms.VScrollBar scrollAtlas;
		private System.Windows.Forms.Label labelFrameValue;
		private System.Windows.Forms.Label labelFrame;
	}
}
