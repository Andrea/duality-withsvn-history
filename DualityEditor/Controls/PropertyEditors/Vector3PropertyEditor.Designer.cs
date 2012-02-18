namespace DualityEditor.Controls.PropertyEditors
{
	partial class Vector3PropertyEditor
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
			this.editorZ = new System.Windows.Forms.NumericUpDown();
			this.labelZ = new System.Windows.Forms.Label();
			this.editorY = new System.Windows.Forms.NumericUpDown();
			this.labelY = new System.Windows.Forms.Label();
			this.labelX = new System.Windows.Forms.Label();
			this.editorX = new System.Windows.Forms.NumericUpDown();
			this.tableLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorZ)).BeginInit();
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
			this.nameLabel.Size = new System.Drawing.Size(50, 58);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "label1";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayout
			// 
			this.tableLayout.ColumnCount = 2;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayout.Controls.Add(this.editorZ, 1, 2);
			this.tableLayout.Controls.Add(this.labelZ, 0, 2);
			this.tableLayout.Controls.Add(this.editorY, 1, 1);
			this.tableLayout.Controls.Add(this.labelY, 0, 1);
			this.tableLayout.Controls.Add(this.labelX, 0, 0);
			this.tableLayout.Controls.Add(this.editorX, 1, 0);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(50, 0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 3;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayout.Size = new System.Drawing.Size(109, 58);
			this.tableLayout.TabIndex = 2;
			// 
			// editorZ
			// 
			this.editorZ.DecimalPlaces = 2;
			this.editorZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorZ.Location = new System.Drawing.Point(17, 38);
			this.editorZ.Margin = new System.Windows.Forms.Padding(0);
			this.editorZ.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.editorZ.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
			this.editorZ.Name = "editorZ";
			this.editorZ.Size = new System.Drawing.Size(92, 20);
			this.editorZ.TabIndex = 7;
			this.editorZ.ValueChanged += new System.EventHandler(this.editorZ_ValueChanged);
			// 
			// labelZ
			// 
			this.labelZ.AutoSize = true;
			this.labelZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelZ.Location = new System.Drawing.Point(0, 38);
			this.labelZ.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelZ.Name = "labelZ";
			this.labelZ.Size = new System.Drawing.Size(14, 20);
			this.labelZ.TabIndex = 6;
			this.labelZ.Text = "Z";
			this.labelZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// editorY
			// 
			this.editorY.DecimalPlaces = 2;
			this.editorY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorY.Location = new System.Drawing.Point(17, 19);
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
			this.editorY.Size = new System.Drawing.Size(92, 20);
			this.editorY.TabIndex = 5;
			this.editorY.ValueChanged += new System.EventHandler(this.editorY_ValueChanged);
			// 
			// labelY
			// 
			this.labelY.AutoSize = true;
			this.labelY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelY.Location = new System.Drawing.Point(0, 19);
			this.labelY.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.labelY.Name = "labelY";
			this.labelY.Size = new System.Drawing.Size(14, 19);
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
			this.labelX.Size = new System.Drawing.Size(14, 19);
			this.labelX.TabIndex = 0;
			this.labelX.Text = "X";
			this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// editorX
			// 
			this.editorX.DecimalPlaces = 2;
			this.editorX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorX.Location = new System.Drawing.Point(17, 0);
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
			this.editorX.Size = new System.Drawing.Size(92, 20);
			this.editorX.TabIndex = 4;
			this.editorX.ValueChanged += new System.EventHandler(this.editorX_ValueChanged);
			// 
			// Vector3PropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "Vector3PropertyEditor";
			this.Size = new System.Drawing.Size(159, 58);
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorX)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Label labelY;
		private System.Windows.Forms.Label labelX;
		private System.Windows.Forms.NumericUpDown editorX;
		private System.Windows.Forms.NumericUpDown editorY;
		private System.Windows.Forms.NumericUpDown editorZ;
		private System.Windows.Forms.Label labelZ;
	}
}
