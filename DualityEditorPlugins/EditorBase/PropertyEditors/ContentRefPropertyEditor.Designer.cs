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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentRefPropertyEditor));
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
			resources.ApplyResources(this.nameLabel, "nameLabel");
			this.nameLabel.Name = "nameLabel";
			// 
			// tableLayout
			// 
			resources.ApplyResources(this.tableLayout, "tableLayout");
			this.tableLayout.Controls.Add(this.buttonReload, 2, 1);
			this.tableLayout.Controls.Add(this.buttonSetNull, 1, 1);
			this.tableLayout.Controls.Add(this.labelLinkedTo, 0, 0);
			this.tableLayout.Controls.Add(this.buttonShow, 0, 1);
			this.tableLayout.Name = "tableLayout";
			// 
			// buttonReload
			// 
			resources.ApplyResources(this.buttonReload, "buttonReload");
			this.buttonReload.Image = global::EditorBase.Properties.Resources.arrow_refresh;
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.UseVisualStyleBackColor = true;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// buttonSetNull
			// 
			resources.ApplyResources(this.buttonSetNull, "buttonSetNull");
			this.buttonSetNull.Image = global::EditorBase.Properties.Resources.cross;
			this.buttonSetNull.Name = "buttonSetNull";
			this.buttonSetNull.UseVisualStyleBackColor = true;
			this.buttonSetNull.Click += new System.EventHandler(this.buttonSetNull_Click);
			// 
			// labelLinkedTo
			// 
			this.labelLinkedTo.AutoEllipsis = true;
			this.labelLinkedTo.BackColor = System.Drawing.SystemColors.Window;
			this.labelLinkedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayout.SetColumnSpan(this.labelLinkedTo, 3);
			resources.ApplyResources(this.labelLinkedTo, "labelLinkedTo");
			this.labelLinkedTo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.labelLinkedTo.Name = "labelLinkedTo";
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
			resources.ApplyResources(this.buttonShow, "buttonShow");
			this.buttonShow.Name = "buttonShow";
			this.buttonShow.UseVisualStyleBackColor = true;
			this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
			// 
			// ContentRefPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.nameLabel);
			resources.ApplyResources(this, "$this");
			this.Name = "ContentRefPropertyEditor";
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
