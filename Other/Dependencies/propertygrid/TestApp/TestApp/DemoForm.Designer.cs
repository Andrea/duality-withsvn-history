namespace CustomPropertyGrid
{
	partial class DemoForm
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

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid1 = new CustomPropertyGrid.PropertyGrid();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.radioEnabled = new System.Windows.Forms.RadioButton();
			this.radioDisabled = new System.Windows.Forms.RadioButton();
			this.radioReadOnly = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.AutoScroll = true;
			this.propertyGrid1.AutoScrollMinSize = new System.Drawing.Size(0, 283);
			this.propertyGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.ReadOnly = false;
			this.propertyGrid1.Size = new System.Drawing.Size(264, 224);
			this.propertyGrid1.TabIndex = 0;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown1.Location = new System.Drawing.Point(12, 265);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(183, 20);
			this.numericUpDown1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(201, 265);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// radioEnabled
			// 
			this.radioEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioEnabled.AutoSize = true;
			this.radioEnabled.Location = new System.Drawing.Point(12, 242);
			this.radioEnabled.Name = "radioEnabled";
			this.radioEnabled.Size = new System.Drawing.Size(64, 17);
			this.radioEnabled.TabIndex = 3;
			this.radioEnabled.TabStop = true;
			this.radioEnabled.Text = "Enabled";
			this.radioEnabled.UseVisualStyleBackColor = true;
			this.radioEnabled.CheckedChanged += new System.EventHandler(this.radioEnabled_CheckedChanged);
			// 
			// radioDisabled
			// 
			this.radioDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioDisabled.AutoSize = true;
			this.radioDisabled.Location = new System.Drawing.Point(160, 242);
			this.radioDisabled.Name = "radioDisabled";
			this.radioDisabled.Size = new System.Drawing.Size(66, 17);
			this.radioDisabled.TabIndex = 4;
			this.radioDisabled.TabStop = true;
			this.radioDisabled.Text = "Disabled";
			this.radioDisabled.UseVisualStyleBackColor = true;
			this.radioDisabled.CheckedChanged += new System.EventHandler(this.radioDisabled_CheckedChanged);
			// 
			// radioReadOnly
			// 
			this.radioReadOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioReadOnly.AutoSize = true;
			this.radioReadOnly.Location = new System.Drawing.Point(82, 242);
			this.radioReadOnly.Name = "radioReadOnly";
			this.radioReadOnly.Size = new System.Drawing.Size(72, 17);
			this.radioReadOnly.TabIndex = 5;
			this.radioReadOnly.TabStop = true;
			this.radioReadOnly.Text = "ReadOnly";
			this.radioReadOnly.UseVisualStyleBackColor = true;
			this.radioReadOnly.CheckedChanged += new System.EventHandler(this.radioReadOnly_CheckedChanged);
			// 
			// DemoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(288, 297);
			this.Controls.Add(this.radioReadOnly);
			this.Controls.Add(this.radioDisabled);
			this.Controls.Add(this.radioEnabled);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "DemoForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PropertyGrid propertyGrid1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RadioButton radioEnabled;
		private System.Windows.Forms.RadioButton radioDisabled;
		private System.Windows.Forms.RadioButton radioReadOnly;
	}
}

