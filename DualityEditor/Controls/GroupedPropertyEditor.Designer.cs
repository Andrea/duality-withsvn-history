namespace DualityEditor.Controls
{
	partial class GroupedPropertyEditor
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
			this.header = new GroupedPropertyEditorHeader();
			this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.AutoSize = true;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 20);
			this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 1;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.Size = new System.Drawing.Size(161, 0);
			this.tableLayout.TabIndex = 0;
			this.tableLayout.Visible = false;
			// 
			// header
			// 
			this.header.ActiveEnabled = true;
			this.header.ActiveVisible = false;
			this.header.BaseColor = System.Drawing.SystemColors.Control;
			this.header.Dock = System.Windows.Forms.DockStyle.Top;
			this.header.ExpandEnabled = true;
			this.header.ExpandVisible = true;
			this.header.Icon = null;
			this.header.Location = new System.Drawing.Point(0, 0);
			this.header.Name = "header";
			this.header.ResetEnabled = true;
			this.header.ResetIsInit = false;
			this.header.ResetVisible = true;
			this.header.Size = new System.Drawing.Size(161, 20);
			this.header.Style = DualityEditor.Controls.GroupedPropertyEditorHeader.HeaderStyle.Flat;
			this.header.TabIndex = 0;
			this.header.Text = "header";
			this.header.ValueText = null;
			// 
			// GroupedPropertyEditor
			// 
			this.AutoSize = true;
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.header);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "GroupedPropertyEditor";
			this.Size = new System.Drawing.Size(161, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayout;
		private GroupedPropertyEditorHeader header;
	}
}
