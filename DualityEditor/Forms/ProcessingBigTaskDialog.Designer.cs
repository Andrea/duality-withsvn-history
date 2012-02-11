﻿namespace DualityEditor.Forms
{
	partial class ProcessingBigTaskDialog
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
			this.components = new System.ComponentModel.Container();
			this.descLabel = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.progressTimer = new System.Windows.Forms.Timer(this.components);
			this.stateDescLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// descLabel
			// 
			this.descLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.descLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.descLabel.Location = new System.Drawing.Point(-1, -1);
			this.descLabel.Margin = new System.Windows.Forms.Padding(0);
			this.descLabel.Name = "descLabel";
			this.descLabel.Padding = new System.Windows.Forms.Padding(5, 6, 5, 5);
			this.descLabel.Size = new System.Drawing.Size(332, 53);
			this.descLabel.TabIndex = 0;
			this.descLabel.Text = "Description";
			this.descLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.descLabel.UseWaitCursor = true;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(12, 95);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(308, 23);
			this.progressBar.TabIndex = 1;
			this.progressBar.UseWaitCursor = true;
			// 
			// progressTimer
			// 
			this.progressTimer.Interval = 50;
			this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
			// 
			// stateDescLabel
			// 
			this.stateDescLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stateDescLabel.AutoEllipsis = true;
			this.stateDescLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.stateDescLabel.Location = new System.Drawing.Point(9, 52);
			this.stateDescLabel.Margin = new System.Windows.Forms.Padding(0);
			this.stateDescLabel.Name = "stateDescLabel";
			this.stateDescLabel.Padding = new System.Windows.Forms.Padding(3);
			this.stateDescLabel.Size = new System.Drawing.Size(311, 40);
			this.stateDescLabel.TabIndex = 2;
			this.stateDescLabel.Text = "Current State";
			this.stateDescLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.stateDescLabel.UseWaitCursor = true;
			// 
			// ProcessingBigTaskDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(329, 130);
			this.ControlBox = false;
			this.Controls.Add(this.stateDescLabel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.descLabel);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProcessingBigTaskDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Caption";
			this.UseWaitCursor = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label descLabel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Timer progressTimer;
		private System.Windows.Forms.Label stateDescLabel;
	}
}