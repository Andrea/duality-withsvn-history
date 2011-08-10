namespace ResourceHacker
{
	partial class RenameTypeDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameTypeDialog));
			this.labelSearchFor = new System.Windows.Forms.Label();
			this.labelReplaceWith = new System.Windows.Forms.Label();
			this.textBoxReplaceWith = new System.Windows.Forms.TextBox();
			this.buttonAbort = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.comboBoxSearchFor = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelSearchFor
			// 
			resources.ApplyResources(this.labelSearchFor, "labelSearchFor");
			this.labelSearchFor.Name = "labelSearchFor";
			// 
			// labelReplaceWith
			// 
			resources.ApplyResources(this.labelReplaceWith, "labelReplaceWith");
			this.labelReplaceWith.Name = "labelReplaceWith";
			// 
			// textBoxReplaceWith
			// 
			resources.ApplyResources(this.textBoxReplaceWith, "textBoxReplaceWith");
			this.textBoxReplaceWith.Name = "textBoxReplaceWith";
			this.textBoxReplaceWith.TextChanged += new System.EventHandler(this.textBoxReplaceWith_TextChanged);
			// 
			// buttonAbort
			// 
			resources.ApplyResources(this.buttonAbort, "buttonAbort");
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.UseVisualStyleBackColor = true;
			this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
			// 
			// buttonOk
			// 
			resources.ApplyResources(this.buttonOk, "buttonOk");
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// comboBoxSearchFor
			// 
			resources.ApplyResources(this.comboBoxSearchFor, "comboBoxSearchFor");
			this.comboBoxSearchFor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comboBoxSearchFor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxSearchFor.Name = "comboBoxSearchFor";
			this.comboBoxSearchFor.TextChanged += new System.EventHandler(this.comboBoxSearchFor_TextChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label1.Name = "label1";
			// 
			// RenameTypeDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxSearchFor);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonAbort);
			this.Controls.Add(this.textBoxReplaceWith);
			this.Controls.Add(this.labelReplaceWith);
			this.Controls.Add(this.labelSearchFor);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RenameTypeDialog";
			this.ShowIcon = false;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelSearchFor;
		private System.Windows.Forms.Label labelReplaceWith;
		private System.Windows.Forms.TextBox textBoxReplaceWith;
		private System.Windows.Forms.Button buttonAbort;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.ComboBox comboBoxSearchFor;
		private System.Windows.Forms.Label label1;
	}
}