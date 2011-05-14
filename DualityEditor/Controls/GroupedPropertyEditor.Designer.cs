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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupedPropertyEditor));
			this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.header = new DualityEditor.Controls.GroupedPropertyEditorHeader();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			resources.ApplyResources(this.tableLayout, "tableLayout");
			this.tableLayout.Name = "tableLayout";
			// 
			// header
			// 
			resources.ApplyResources(this.header, "header");
			this.header.Name = "header";
			// 
			// GroupedPropertyEditor
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.header);
			this.Name = "GroupedPropertyEditor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayout;
		private GroupedPropertyEditorHeader header;
	}
}
