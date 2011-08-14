namespace EditorBase.PropertyEditors
{
	partial class ContentRefPropertyEditor
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
			this.buttonReload = new System.Windows.Forms.Button();
			this.buttonSetNull = new System.Windows.Forms.Button();
			this.labelLinkedTo = new System.Windows.Forms.Label();
			this.buttonShow = new System.Windows.Forms.Button();
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
			this.nameLabel.Size = new System.Drawing.Size(50, 54);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "label1";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayout
			// 
			this.tableLayout.ColumnCount = 3;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Controls.Add(this.buttonReload, 2, 1);
			this.tableLayout.Controls.Add(this.buttonSetNull, 1, 1);
			this.tableLayout.Controls.Add(this.labelLinkedTo, 0, 0);
			this.tableLayout.Controls.Add(this.buttonShow, 0, 1);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(50, 0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 2;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.Size = new System.Drawing.Size(145, 54);
			this.tableLayout.TabIndex = 2;
			// 
			// buttonReload
			// 
			this.buttonReload.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonReload.Image = global::EditorBase.Properties.Resources.arrow_refresh;
			this.buttonReload.Location = new System.Drawing.Point(123, 33);
			this.buttonReload.Margin = new System.Windows.Forms.Padding(0);
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.Size = new System.Drawing.Size(22, 21);
			this.buttonReload.TabIndex = 3;
			this.buttonReload.UseVisualStyleBackColor = true;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// buttonSetNull
			// 
			this.buttonSetNull.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonSetNull.Image = global::EditorBase.Properties.Resources.cross;
			this.buttonSetNull.Location = new System.Drawing.Point(45, 33);
			this.buttonSetNull.Margin = new System.Windows.Forms.Padding(0);
			this.buttonSetNull.Name = "buttonSetNull";
			this.buttonSetNull.Size = new System.Drawing.Size(22, 21);
			this.buttonSetNull.TabIndex = 2;
			this.buttonSetNull.UseVisualStyleBackColor = true;
			this.buttonSetNull.Click += new System.EventHandler(this.buttonSetNull_Click);
			// 
			// labelLinkedTo
			// 
			this.labelLinkedTo.AutoEllipsis = true;
			this.labelLinkedTo.BackColor = System.Drawing.SystemColors.Window;
			this.labelLinkedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayout.SetColumnSpan(this.labelLinkedTo, 3);
			this.labelLinkedTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelLinkedTo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.labelLinkedTo.Location = new System.Drawing.Point(0, 0);
			this.labelLinkedTo.Margin = new System.Windows.Forms.Padding(0);
			this.labelLinkedTo.Name = "labelLinkedTo";
			this.labelLinkedTo.Size = new System.Drawing.Size(145, 33);
			this.labelLinkedTo.TabIndex = 0;
			this.labelLinkedTo.Text = "linked to";
			this.labelLinkedTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelLinkedTo.DragDrop += new System.Windows.Forms.DragEventHandler(this.labelLinkedTo_DragDrop);
			this.labelLinkedTo.DragEnter += new System.Windows.Forms.DragEventHandler(this.labelLinkedTo_DragEnter);
			this.labelLinkedTo.DragLeave += new System.EventHandler(this.labelLinkedTo_DragLeave);
			this.labelLinkedTo.DoubleClick += new System.EventHandler(this.labelLinkedTo_DoubleClick);
			this.labelLinkedTo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelLinkedTo_MouseDown);
			this.labelLinkedTo.MouseLeave += new System.EventHandler(this.labelLinkedTo_MouseLeave);
			this.labelLinkedTo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelLinkedTo_MouseMove);
			this.labelLinkedTo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelLinkedTo_MouseUp);
			// 
			// buttonShow
			// 
			this.buttonShow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonShow.Location = new System.Drawing.Point(0, 33);
			this.buttonShow.Margin = new System.Windows.Forms.Padding(0);
			this.buttonShow.Name = "buttonShow";
			this.buttonShow.Size = new System.Drawing.Size(45, 21);
			this.buttonShow.TabIndex = 1;
			this.buttonShow.Text = "Show";
			this.buttonShow.UseVisualStyleBackColor = true;
			this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
			// 
			// ContentRefPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ContentRefPropertyEditor";
			this.Size = new System.Drawing.Size(195, 54);
			this.tableLayout.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private DualityEditor.Controls.DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Label labelLinkedTo;
		private System.Windows.Forms.Button buttonReload;
		private System.Windows.Forms.Button buttonSetNull;
		private System.Windows.Forms.Button buttonShow;
	}
}
