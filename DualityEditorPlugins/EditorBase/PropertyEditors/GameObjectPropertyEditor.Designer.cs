namespace EditorBase.PropertyEditors
{
	partial class GameObjectPropertyEditor
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
			this.nameEditor = new System.Windows.Forms.TextBox();
			this.activeEditor = new System.Windows.Forms.CheckBox();
			this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.buttonPrefabLinkDestroy = new System.Windows.Forms.Button();
			this.buttonPrefabLinkShow = new System.Windows.Forms.Button();
			this.buttonPrefabLinkRevert = new System.Windows.Forms.Button();
			this.labelPrefabLink = new System.Windows.Forms.Label();
			this.buttonPrefabLinkApply = new System.Windows.Forms.Button();
			this.tableLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// nameEditor
			// 
			this.nameEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.nameEditor.Location = new System.Drawing.Point(19, 0);
			this.nameEditor.Margin = new System.Windows.Forms.Padding(1);
			this.nameEditor.Name = "nameEditor";
			this.nameEditor.Size = new System.Drawing.Size(247, 20);
			this.nameEditor.TabIndex = 1;
			this.nameEditor.Visible = false;
			this.nameEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameEditor_KeyDown);
			this.nameEditor.Leave += new System.EventHandler(this.nameEditor_Leave);
			// 
			// activeEditor
			// 
			this.activeEditor.AutoSize = true;
			this.activeEditor.BackColor = System.Drawing.Color.Transparent;
			this.activeEditor.Location = new System.Drawing.Point(3, 3);
			this.activeEditor.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.activeEditor.Name = "activeEditor";
			this.activeEditor.Size = new System.Drawing.Size(15, 14);
			this.activeEditor.TabIndex = 2;
			this.activeEditor.UseVisualStyleBackColor = false;
			this.activeEditor.CheckedChanged += new System.EventHandler(this.activeEditor_CheckedChanged);
			// 
			// tableLayout
			// 
			this.tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayout.BackColor = System.Drawing.Color.Transparent;
			this.tableLayout.ColumnCount = 5;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00146F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00146F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99895F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99812F));
			this.tableLayout.Controls.Add(this.buttonPrefabLinkDestroy, 4, 0);
			this.tableLayout.Controls.Add(this.buttonPrefabLinkShow, 1, 0);
			this.tableLayout.Controls.Add(this.buttonPrefabLinkRevert, 2, 0);
			this.tableLayout.Controls.Add(this.labelPrefabLink, 0, 0);
			this.tableLayout.Controls.Add(this.buttonPrefabLinkApply, 3, 0);
			this.tableLayout.Location = new System.Drawing.Point(0, 21);
			this.tableLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 1;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Size = new System.Drawing.Size(266, 21);
			this.tableLayout.TabIndex = 3;
			// 
			// buttonPrefabLinkDestroy
			// 
			this.buttonPrefabLinkDestroy.AutoEllipsis = true;
			this.buttonPrefabLinkDestroy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonPrefabLinkDestroy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.buttonPrefabLinkDestroy.Location = new System.Drawing.Point(214, 0);
			this.buttonPrefabLinkDestroy.Margin = new System.Windows.Forms.Padding(0);
			this.buttonPrefabLinkDestroy.Name = "buttonPrefabLinkDestroy";
			this.buttonPrefabLinkDestroy.Size = new System.Drawing.Size(52, 21);
			this.buttonPrefabLinkDestroy.TabIndex = 4;
			this.buttonPrefabLinkDestroy.Text = "Break";
			this.buttonPrefabLinkDestroy.UseVisualStyleBackColor = true;
			this.buttonPrefabLinkDestroy.Click += new System.EventHandler(this.buttonPrefabLinkDestroy_Click);
			// 
			// buttonPrefabLinkShow
			// 
			this.buttonPrefabLinkShow.AutoEllipsis = true;
			this.buttonPrefabLinkShow.BackColor = System.Drawing.Color.Transparent;
			this.buttonPrefabLinkShow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonPrefabLinkShow.Location = new System.Drawing.Point(64, 0);
			this.buttonPrefabLinkShow.Margin = new System.Windows.Forms.Padding(0);
			this.buttonPrefabLinkShow.Name = "buttonPrefabLinkShow";
			this.buttonPrefabLinkShow.Size = new System.Drawing.Size(50, 21);
			this.buttonPrefabLinkShow.TabIndex = 0;
			this.buttonPrefabLinkShow.Text = "Show";
			this.buttonPrefabLinkShow.UseVisualStyleBackColor = false;
			this.buttonPrefabLinkShow.Click += new System.EventHandler(this.buttonPrefabLinkShow_Click);
			// 
			// buttonPrefabLinkRevert
			// 
			this.buttonPrefabLinkRevert.AutoEllipsis = true;
			this.buttonPrefabLinkRevert.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonPrefabLinkRevert.Location = new System.Drawing.Point(114, 0);
			this.buttonPrefabLinkRevert.Margin = new System.Windows.Forms.Padding(0);
			this.buttonPrefabLinkRevert.Name = "buttonPrefabLinkRevert";
			this.buttonPrefabLinkRevert.Size = new System.Drawing.Size(50, 21);
			this.buttonPrefabLinkRevert.TabIndex = 1;
			this.buttonPrefabLinkRevert.Text = "Revert";
			this.buttonPrefabLinkRevert.UseVisualStyleBackColor = true;
			this.buttonPrefabLinkRevert.Click += new System.EventHandler(this.buttonPrefabLinkRevert_Click);
			// 
			// labelPrefabLink
			// 
			this.labelPrefabLink.AutoSize = true;
			this.labelPrefabLink.BackColor = System.Drawing.Color.Transparent;
			this.labelPrefabLink.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPrefabLink.ForeColor = System.Drawing.Color.Blue;
			this.labelPrefabLink.Location = new System.Drawing.Point(3, 0);
			this.labelPrefabLink.Name = "labelPrefabLink";
			this.labelPrefabLink.Size = new System.Drawing.Size(58, 21);
			this.labelPrefabLink.TabIndex = 2;
			this.labelPrefabLink.Text = "PrefabLink";
			this.labelPrefabLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonPrefabLinkApply
			// 
			this.buttonPrefabLinkApply.AutoEllipsis = true;
			this.buttonPrefabLinkApply.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonPrefabLinkApply.Location = new System.Drawing.Point(164, 0);
			this.buttonPrefabLinkApply.Margin = new System.Windows.Forms.Padding(0);
			this.buttonPrefabLinkApply.Name = "buttonPrefabLinkApply";
			this.buttonPrefabLinkApply.Size = new System.Drawing.Size(50, 21);
			this.buttonPrefabLinkApply.TabIndex = 3;
			this.buttonPrefabLinkApply.Text = "Apply";
			this.buttonPrefabLinkApply.UseVisualStyleBackColor = true;
			this.buttonPrefabLinkApply.Click += new System.EventHandler(this.buttonPrefabLinkApply_Click);
			// 
			// GameObjectPropertyEditor
			// 
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.activeEditor);
			this.Controls.Add(this.nameEditor);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "GameObjectPropertyEditor";
			this.Size = new System.Drawing.Size(266, 44);
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox nameEditor;
		private System.Windows.Forms.CheckBox activeEditor;
		private System.Windows.Forms.TableLayoutPanel tableLayout;
		private System.Windows.Forms.Button buttonPrefabLinkShow;
		private System.Windows.Forms.Button buttonPrefabLinkRevert;
		private System.Windows.Forms.Label labelPrefabLink;
		private System.Windows.Forms.Button buttonPrefabLinkApply;
		private System.Windows.Forms.Button buttonPrefabLinkDestroy;


	}
}
