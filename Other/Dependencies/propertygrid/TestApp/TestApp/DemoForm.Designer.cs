namespace AdamsLair.PropertyGrid
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
			this.propertyGrid1 = new AdamsLair.PropertyGrid.PropertyGrid();
			this.radioEnabled = new System.Windows.Forms.RadioButton();
			this.radioDisabled = new System.Windows.Forms.RadioButton();
			this.radioReadOnly = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.AllowDrop = true;
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.AutoScroll = true;
			this.propertyGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.ReadOnly = false;
			this.propertyGrid1.Size = new System.Drawing.Size(248, 291);
			this.propertyGrid1.TabIndex = 0;
			// 
			// radioEnabled
			// 
			this.radioEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioEnabled.AutoSize = true;
			this.radioEnabled.Location = new System.Drawing.Point(12, 309);
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
			this.radioDisabled.Location = new System.Drawing.Point(160, 309);
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
			this.radioReadOnly.Location = new System.Drawing.Point(82, 309);
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
			this.ClientSize = new System.Drawing.Size(272, 338);
			this.Controls.Add(this.radioReadOnly);
			this.Controls.Add(this.radioDisabled);
			this.Controls.Add(this.radioEnabled);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "DemoForm";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PropertyGrid propertyGrid1;
		private System.Windows.Forms.RadioButton radioEnabled;
		private System.Windows.Forms.RadioButton radioDisabled;
		private System.Windows.Forms.RadioButton radioReadOnly;
	}
}

