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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameObjectPropertyEditor));
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
			resources.ApplyResources(this.nameEditor, "nameEditor");
			this.nameEditor.Name = "nameEditor";
			this.nameEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameEditor_KeyDown);
			this.nameEditor.Leave += new System.EventHandler(this.nameEditor_Leave);
			// 
			// activeEditor
			// 
			resources.ApplyResources(this.activeEditor, "activeEditor");
			this.activeEditor.BackColor = System.Drawing.Color.Transparent;
			this.activeEditor.Name = "activeEditor";
			this.activeEditor.UseVisualStyleBackColor = false;
			this.activeEditor.CheckedChanged += new System.EventHandler(this.activeEditor_CheckedChanged);
			// 
			// tableLayout
			// 
			resources.ApplyResources(this.tableLayout, "tableLayout");
			this.tableLayout.BackColor = System.Drawing.Color.Transparent;
			this.tableLayout.Controls.Add(this.buttonPrefabLinkDestroy, 4, 0);
			this.tableLayout.Controls.Add(this.buttonPrefabLinkShow, 1, 0);
			this.tableLayout.Controls.Add(this.buttonPrefabLinkRevert, 2, 0);
			this.tableLayout.Controls.Add(this.labelPrefabLink, 0, 0);
			this.tableLayout.Controls.Add(this.buttonPrefabLinkApply, 3, 0);
			this.tableLayout.Name = "tableLayout";
			// 
			// buttonPrefabLinkDestroy
			// 
			this.buttonPrefabLinkDestroy.AutoEllipsis = true;
			resources.ApplyResources(this.buttonPrefabLinkDestroy, "buttonPrefabLinkDestroy");
			this.buttonPrefabLinkDestroy.Name = "buttonPrefabLinkDestroy";
			this.buttonPrefabLinkDestroy.UseVisualStyleBackColor = true;
			this.buttonPrefabLinkDestroy.Click += new System.EventHandler(this.buttonPrefabLinkDestroy_Click);
			// 
			// buttonPrefabLinkShow
			// 
			this.buttonPrefabLinkShow.AutoEllipsis = true;
			this.buttonPrefabLinkShow.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.buttonPrefabLinkShow, "buttonPrefabLinkShow");
			this.buttonPrefabLinkShow.Name = "buttonPrefabLinkShow";
			this.buttonPrefabLinkShow.UseVisualStyleBackColor = false;
			this.buttonPrefabLinkShow.Click += new System.EventHandler(this.buttonPrefabLinkShow_Click);
			// 
			// buttonPrefabLinkRevert
			// 
			this.buttonPrefabLinkRevert.AutoEllipsis = true;
			resources.ApplyResources(this.buttonPrefabLinkRevert, "buttonPrefabLinkRevert");
			this.buttonPrefabLinkRevert.Name = "buttonPrefabLinkRevert";
			this.buttonPrefabLinkRevert.UseVisualStyleBackColor = true;
			this.buttonPrefabLinkRevert.Click += new System.EventHandler(this.buttonPrefabLinkRevert_Click);
			// 
			// labelPrefabLink
			// 
			resources.ApplyResources(this.labelPrefabLink, "labelPrefabLink");
			this.labelPrefabLink.BackColor = System.Drawing.Color.Transparent;
			this.labelPrefabLink.ForeColor = System.Drawing.Color.Blue;
			this.labelPrefabLink.Name = "labelPrefabLink";
			// 
			// buttonPrefabLinkApply
			// 
			this.buttonPrefabLinkApply.AutoEllipsis = true;
			resources.ApplyResources(this.buttonPrefabLinkApply, "buttonPrefabLinkApply");
			this.buttonPrefabLinkApply.Name = "buttonPrefabLinkApply";
			this.buttonPrefabLinkApply.UseVisualStyleBackColor = true;
			this.buttonPrefabLinkApply.Click += new System.EventHandler(this.buttonPrefabLinkApply_Click);
			// 
			// GameObjectPropertyEditor
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.activeEditor);
			this.Controls.Add(this.nameEditor);
			this.Name = "GameObjectPropertyEditor";
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
