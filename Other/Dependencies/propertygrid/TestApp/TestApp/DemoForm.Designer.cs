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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
			this.propertyGrid1.Size = new System.Drawing.Size(318, 235);
			this.propertyGrid1.TabIndex = 0;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown1.DecimalPlaces = 2;
			this.numericUpDown1.Location = new System.Drawing.Point(12, 276);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(250, 20);
			this.numericUpDown1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(456, 276);
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
			this.radioEnabled.Location = new System.Drawing.Point(12, 253);
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
			this.radioDisabled.Location = new System.Drawing.Point(160, 253);
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
			this.radioReadOnly.Location = new System.Drawing.Point(82, 253);
			this.radioReadOnly.Name = "radioReadOnly";
			this.radioReadOnly.Size = new System.Drawing.Size(72, 17);
			this.radioReadOnly.TabIndex = 5;
			this.radioReadOnly.TabStop = true;
			this.radioReadOnly.Text = "ReadOnly";
			this.radioReadOnly.UseVisualStyleBackColor = true;
			this.radioReadOnly.CheckedChanged += new System.EventHandler(this.radioReadOnly_CheckedChanged);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(12, 305);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(250, 20);
			this.textBox1.TabIndex = 6;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.Location = new System.Drawing.Point(268, 305);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(263, 20);
			this.textBox2.TabIndex = 7;
			// 
			// propertyGrid2
			// 
			this.propertyGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid2.Location = new System.Drawing.Point(336, 12);
			this.propertyGrid2.Name = "propertyGrid2";
			this.propertyGrid2.Size = new System.Drawing.Size(195, 235);
			this.propertyGrid2.TabIndex = 8;
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "One",
            "Two",
            "Three",
            "Apples",
            "Four"});
			this.comboBox1.Location = new System.Drawing.Point(268, 275);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(182, 21);
			this.comboBox1.TabIndex = 9;
			// 
			// DemoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(543, 337);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.propertyGrid2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
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
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.PropertyGrid propertyGrid2;
		private System.Windows.Forms.ComboBox comboBox1;
	}
}

