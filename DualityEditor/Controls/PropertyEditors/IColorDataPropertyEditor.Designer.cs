namespace DualityEditor.Controls.PropertyEditors
{
	partial class IColorDataPropertyEditor
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
			this.buttonOpenEditor = new System.Windows.Forms.Button();
			this.colorShowBox = new DualityEditor.Controls.ColorShowBox();
			this.tableLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.AutoEllipsis = true;
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.nameLabel.Location = new System.Drawing.Point(0, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Padding = new System.Windows.Forms.Padding(3);
			this.nameLabel.Size = new System.Drawing.Size(50, 26);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "label1";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayout
			// 
			this.tableLayout.ColumnCount = 2;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayout.Controls.Add(this.buttonOpenEditor, 1, 0);
			this.tableLayout.Controls.Add(this.colorShowBox, 0, 0);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(50, 0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 1;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Size = new System.Drawing.Size(147, 26);
			this.tableLayout.TabIndex = 2;
			// 
			// buttonOpenEditor
			// 
			this.buttonOpenEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonOpenEditor.Image = global::DualityEditor.Properties.Resources.icon_color;
			this.buttonOpenEditor.Location = new System.Drawing.Point(121, 0);
			this.buttonOpenEditor.Margin = new System.Windows.Forms.Padding(0);
			this.buttonOpenEditor.Name = "buttonOpenEditor";
			this.buttonOpenEditor.Size = new System.Drawing.Size(26, 26);
			this.buttonOpenEditor.TabIndex = 3;
			this.buttonOpenEditor.Click += new System.EventHandler(this.buttonOpenEditor_Click);
			// 
			// colorShowBox
			// 
			this.colorShowBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.colorShowBox.Color = System.Drawing.Color.Transparent;
			this.colorShowBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colorShowBox.Location = new System.Drawing.Point(0, 1);
			this.colorShowBox.LowerColor = System.Drawing.Color.Transparent;
			this.colorShowBox.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
			this.colorShowBox.Name = "colorShowBox";
			this.colorShowBox.Size = new System.Drawing.Size(120, 24);
			this.colorShowBox.TabIndex = 0;
			this.colorShowBox.UpperColor = System.Drawing.Color.Transparent;
			this.colorShowBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.colorShowBox_DragDrop);
			this.colorShowBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.colorShowBox_DragEnter);
			this.colorShowBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.colorShowBox_MouseDoubleClick);
			this.colorShowBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colorShowBox_MouseDown);
			this.colorShowBox.MouseLeave += new System.EventHandler(this.colorShowBox_MouseLeave);
			this.colorShowBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.colorShowBox_MouseMove);
			this.colorShowBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.colorShowBox_MouseUp);
			// 
			// IColorDataPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "IColorDataPropertyEditor";
			this.Size = new System.Drawing.Size(197, 26);
			this.tableLayout.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Button buttonOpenEditor;
		private ColorShowBox colorShowBox;
	}
}
