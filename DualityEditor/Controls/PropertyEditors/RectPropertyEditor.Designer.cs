namespace DualityEditor.Controls.PropertyEditors
{
	partial class RectPropertyEditor
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
			this.nameLabel = new System.Windows.Forms.Label();
			this.tableLayout = new DualityEditor.Controls.DBTableLayoutPanel(this.components);
			this.editorH = new System.Windows.Forms.NumericUpDown();
			this.editorW = new System.Windows.Forms.NumericUpDown();
			this.editorY = new System.Windows.Forms.NumericUpDown();
			this.labelH = new System.Windows.Forms.Label();
			this.labelW = new System.Windows.Forms.Label();
			this.labelY = new System.Windows.Forms.Label();
			this.labelX = new System.Windows.Forms.Label();
			this.editorX = new System.Windows.Forms.NumericUpDown();
			this.tableLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorX)).BeginInit();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.AutoEllipsis = true;
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.nameLabel.Location = new System.Drawing.Point(0, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(50, 39);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "label1";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayout
			// 
			this.tableLayout.ColumnCount = 4;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayout.Controls.Add(this.editorH, 3, 1);
			this.tableLayout.Controls.Add(this.editorW, 1, 1);
			this.tableLayout.Controls.Add(this.editorY, 3, 0);
			this.tableLayout.Controls.Add(this.labelH, 2, 1);
			this.tableLayout.Controls.Add(this.labelW, 0, 1);
			this.tableLayout.Controls.Add(this.labelY, 2, 0);
			this.tableLayout.Controls.Add(this.labelX, 0, 0);
			this.tableLayout.Controls.Add(this.editorX, 1, 0);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(50, 0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 2;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayout.Size = new System.Drawing.Size(131, 39);
			this.tableLayout.TabIndex = 2;
			// 
			// editorH
			// 
			this.editorH.DecimalPlaces = 2;
			this.editorH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorH.Location = new System.Drawing.Point(86, 19);
			this.editorH.Margin = new System.Windows.Forms.Padding(0);
			this.editorH.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.editorH.Name = "editorH";
			this.editorH.Size = new System.Drawing.Size(45, 20);
			this.editorH.TabIndex = 7;
			this.editorH.ValueChanged += new System.EventHandler(this.editorH_ValueChanged);
			// 
			// editorW
			// 
			this.editorW.DecimalPlaces = 2;
			this.editorW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorW.Location = new System.Drawing.Point(21, 19);
			this.editorW.Margin = new System.Windows.Forms.Padding(0);
			this.editorW.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.editorW.Name = "editorW";
			this.editorW.Size = new System.Drawing.Size(44, 20);
			this.editorW.TabIndex = 6;
			this.editorW.ValueChanged += new System.EventHandler(this.editorW_ValueChanged);
			// 
			// editorY
			// 
			this.editorY.DecimalPlaces = 2;
			this.editorY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorY.Location = new System.Drawing.Point(86, 0);
			this.editorY.Margin = new System.Windows.Forms.Padding(0);
			this.editorY.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.editorY.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.editorY.Name = "editorY";
			this.editorY.Size = new System.Drawing.Size(45, 20);
			this.editorY.TabIndex = 5;
			this.editorY.ValueChanged += new System.EventHandler(this.editorY_ValueChanged);
			// 
			// labelH
			// 
			this.labelH.AutoSize = true;
			this.labelH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelH.Location = new System.Drawing.Point(68, 19);
			this.labelH.Name = "labelH";
			this.labelH.Size = new System.Drawing.Size(15, 20);
			this.labelH.TabIndex = 3;
			this.labelH.Text = "H";
			this.labelH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelW
			// 
			this.labelW.AutoSize = true;
			this.labelW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelW.Location = new System.Drawing.Point(0, 19);
			this.labelW.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelW.Name = "labelW";
			this.labelW.Size = new System.Drawing.Size(18, 20);
			this.labelW.TabIndex = 2;
			this.labelW.Text = "W";
			this.labelW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelY
			// 
			this.labelY.AutoSize = true;
			this.labelY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelY.Location = new System.Drawing.Point(68, 0);
			this.labelY.Name = "labelY";
			this.labelY.Size = new System.Drawing.Size(15, 19);
			this.labelY.TabIndex = 1;
			this.labelY.Text = "Y";
			this.labelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelX
			// 
			this.labelX.AutoSize = true;
			this.labelX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelX.Location = new System.Drawing.Point(0, 0);
			this.labelX.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelX.Name = "labelX";
			this.labelX.Size = new System.Drawing.Size(18, 19);
			this.labelX.TabIndex = 0;
			this.labelX.Text = "X";
			this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// editorX
			// 
			this.editorX.DecimalPlaces = 2;
			this.editorX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorX.Location = new System.Drawing.Point(21, 0);
			this.editorX.Margin = new System.Windows.Forms.Padding(0);
			this.editorX.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.editorX.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.editorX.Name = "editorX";
			this.editorX.Size = new System.Drawing.Size(44, 20);
			this.editorX.TabIndex = 4;
			this.editorX.ValueChanged += new System.EventHandler(this.editorX_ValueChanged);
			// 
			// RectPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "RectPropertyEditor";
			this.Size = new System.Drawing.Size(181, 39);
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorX)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Label labelH;
		private System.Windows.Forms.Label labelW;
		private System.Windows.Forms.Label labelY;
		private System.Windows.Forms.Label labelX;
		private System.Windows.Forms.NumericUpDown editorX;
		private System.Windows.Forms.NumericUpDown editorH;
		private System.Windows.Forms.NumericUpDown editorW;
		private System.Windows.Forms.NumericUpDown editorY;
	}
}
