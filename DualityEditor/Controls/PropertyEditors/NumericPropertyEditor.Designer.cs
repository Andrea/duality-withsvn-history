namespace DualityEditor.Controls.PropertyEditors
{
	partial class NumericPropertyEditor
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
			this.valueEditor = new System.Windows.Forms.NumericUpDown();
			this.nameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.valueEditor)).BeginInit();
			this.SuspendLayout();
			// 
			// valueEditor
			// 
			this.valueEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.valueEditor.Location = new System.Drawing.Point(50, 0);
			this.valueEditor.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
			this.valueEditor.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
			this.valueEditor.Name = "valueEditor";
			this.valueEditor.Size = new System.Drawing.Size(133, 20);
			this.valueEditor.TabIndex = 0;
			this.valueEditor.ValueChanged += new System.EventHandler(this.valueEditor_ValueChanged);
			this.valueEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.valueEditor_KeyDown);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoEllipsis = true;
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.nameLabel.Location = new System.Drawing.Point(0, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(50, 20);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "label1";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// NumericPropertyEditor
			// 
			this.Controls.Add(this.valueEditor);
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "NumericPropertyEditor";
			this.Size = new System.Drawing.Size(183, 20);
			((System.ComponentModel.ISupportInitialize)(this.valueEditor)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown valueEditor;
		private System.Windows.Forms.Label nameLabel;
	}
}
