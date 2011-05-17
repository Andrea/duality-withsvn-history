namespace EditorBase.PropertyEditors
{
	partial class TransformPropertyEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformPropertyEditor));
			this.tableLayout = new DualityEditor.Controls.DBTableLayoutPanel(this.components);
			this.editorAngleVelRad = new System.Windows.Forms.NumericUpDown();
			this.labelAngleVelRad = new System.Windows.Forms.Label();
			this.editorAngleVelDeg = new System.Windows.Forms.NumericUpDown();
			this.labelAngleVelDeg = new System.Windows.Forms.Label();
			this.editorVelZ = new System.Windows.Forms.NumericUpDown();
			this.labelVelZ = new System.Windows.Forms.Label();
			this.editorVelY = new System.Windows.Forms.NumericUpDown();
			this.labelVelY = new System.Windows.Forms.Label();
			this.editorVelX = new System.Windows.Forms.NumericUpDown();
			this.labelVelX = new System.Windows.Forms.Label();
			this.editorAngleRad = new System.Windows.Forms.NumericUpDown();
			this.labelAngleRad = new System.Windows.Forms.Label();
			this.editorAngleDeg = new System.Windows.Forms.NumericUpDown();
			this.labelAngleDeg = new System.Windows.Forms.Label();
			this.editorScaleZ = new System.Windows.Forms.NumericUpDown();
			this.labelScaleZ = new System.Windows.Forms.Label();
			this.editorScaleY = new System.Windows.Forms.NumericUpDown();
			this.labelScaleY = new System.Windows.Forms.Label();
			this.editorScaleX = new System.Windows.Forms.NumericUpDown();
			this.labelScaleX = new System.Windows.Forms.Label();
			this.editorPosZ = new System.Windows.Forms.NumericUpDown();
			this.labelPosZ = new System.Windows.Forms.Label();
			this.editorPosY = new System.Windows.Forms.NumericUpDown();
			this.labelPosY = new System.Windows.Forms.Label();
			this.labelAngleVel = new System.Windows.Forms.Label();
			this.labelVel = new System.Windows.Forms.Label();
			this.labelAngle = new System.Windows.Forms.Label();
			this.labelScale = new System.Windows.Forms.Label();
			this.labelPos = new System.Windows.Forms.Label();
			this.separatorPanel = new System.Windows.Forms.Panel();
			this.labelPosX = new System.Windows.Forms.Label();
			this.editorPosX = new System.Windows.Forms.NumericUpDown();
			this.relativeValues = new System.Windows.Forms.CheckBox();
			this.separatorPanel2 = new System.Windows.Forms.Panel();
			this.tableLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelRad)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelDeg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleRad)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleDeg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosX)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.tableLayout, "tableLayout");
			this.tableLayout.Controls.Add(this.editorAngleVelRad, 4, 5);
			this.tableLayout.Controls.Add(this.labelAngleVelRad, 3, 5);
			this.tableLayout.Controls.Add(this.editorAngleVelDeg, 2, 5);
			this.tableLayout.Controls.Add(this.labelAngleVelDeg, 1, 5);
			this.tableLayout.Controls.Add(this.editorVelZ, 6, 4);
			this.tableLayout.Controls.Add(this.labelVelZ, 5, 4);
			this.tableLayout.Controls.Add(this.editorVelY, 4, 4);
			this.tableLayout.Controls.Add(this.labelVelY, 3, 4);
			this.tableLayout.Controls.Add(this.editorVelX, 2, 4);
			this.tableLayout.Controls.Add(this.labelVelX, 1, 4);
			this.tableLayout.Controls.Add(this.editorAngleRad, 4, 2);
			this.tableLayout.Controls.Add(this.labelAngleRad, 3, 2);
			this.tableLayout.Controls.Add(this.editorAngleDeg, 2, 2);
			this.tableLayout.Controls.Add(this.labelAngleDeg, 1, 2);
			this.tableLayout.Controls.Add(this.editorScaleZ, 6, 1);
			this.tableLayout.Controls.Add(this.labelScaleZ, 5, 1);
			this.tableLayout.Controls.Add(this.editorScaleY, 4, 1);
			this.tableLayout.Controls.Add(this.labelScaleY, 3, 1);
			this.tableLayout.Controls.Add(this.editorScaleX, 2, 1);
			this.tableLayout.Controls.Add(this.labelScaleX, 1, 1);
			this.tableLayout.Controls.Add(this.editorPosZ, 6, 0);
			this.tableLayout.Controls.Add(this.labelPosZ, 5, 0);
			this.tableLayout.Controls.Add(this.editorPosY, 4, 0);
			this.tableLayout.Controls.Add(this.labelPosY, 3, 0);
			this.tableLayout.Controls.Add(this.labelAngleVel, 0, 5);
			this.tableLayout.Controls.Add(this.labelVel, 0, 4);
			this.tableLayout.Controls.Add(this.labelAngle, 0, 2);
			this.tableLayout.Controls.Add(this.labelScale, 0, 1);
			this.tableLayout.Controls.Add(this.labelPos, 0, 0);
			this.tableLayout.Controls.Add(this.separatorPanel, 0, 3);
			this.tableLayout.Controls.Add(this.labelPosX, 1, 0);
			this.tableLayout.Controls.Add(this.editorPosX, 2, 0);
			this.tableLayout.Controls.Add(this.relativeValues, 0, 7);
			this.tableLayout.Controls.Add(this.separatorPanel2, 0, 7);
			this.tableLayout.Name = "tableLayout";
			// 
			// editorAngleVelRad
			// 
			this.editorAngleVelRad.DecimalPlaces = 2;
			resources.ApplyResources(this.editorAngleVelRad, "editorAngleVelRad");
			this.editorAngleVelRad.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorAngleVelRad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.editorAngleVelRad.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.editorAngleVelRad.Name = "editorAngleVelRad";
			this.editorAngleVelRad.ValueChanged += new System.EventHandler(this.editorAngleVelRad_ValueChanged);
			// 
			// labelAngleVelRad
			// 
			resources.ApplyResources(this.labelAngleVelRad, "labelAngleVelRad");
			this.labelAngleVelRad.Name = "labelAngleVelRad";
			// 
			// editorAngleVelDeg
			// 
			this.editorAngleVelDeg.DecimalPlaces = 2;
			resources.ApplyResources(this.editorAngleVelDeg, "editorAngleVelDeg");
			this.editorAngleVelDeg.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorAngleVelDeg.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.editorAngleVelDeg.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.editorAngleVelDeg.Name = "editorAngleVelDeg";
			this.editorAngleVelDeg.ValueChanged += new System.EventHandler(this.editorAngleVelDeg_ValueChanged);
			// 
			// labelAngleVelDeg
			// 
			resources.ApplyResources(this.labelAngleVelDeg, "labelAngleVelDeg");
			this.labelAngleVelDeg.Name = "labelAngleVelDeg";
			// 
			// editorVelZ
			// 
			this.editorVelZ.DecimalPlaces = 2;
			resources.ApplyResources(this.editorVelZ, "editorVelZ");
			this.editorVelZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorVelZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.editorVelZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.editorVelZ.Name = "editorVelZ";
			this.editorVelZ.ValueChanged += new System.EventHandler(this.editorVelZ_ValueChanged);
			// 
			// labelVelZ
			// 
			resources.ApplyResources(this.labelVelZ, "labelVelZ");
			this.labelVelZ.Name = "labelVelZ";
			// 
			// editorVelY
			// 
			this.editorVelY.DecimalPlaces = 2;
			resources.ApplyResources(this.editorVelY, "editorVelY");
			this.editorVelY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorVelY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.editorVelY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.editorVelY.Name = "editorVelY";
			this.editorVelY.ValueChanged += new System.EventHandler(this.editorVelY_ValueChanged);
			// 
			// labelVelY
			// 
			resources.ApplyResources(this.labelVelY, "labelVelY");
			this.labelVelY.Name = "labelVelY";
			// 
			// editorVelX
			// 
			this.editorVelX.DecimalPlaces = 2;
			resources.ApplyResources(this.editorVelX, "editorVelX");
			this.editorVelX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorVelX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.editorVelX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.editorVelX.Name = "editorVelX";
			this.editorVelX.ValueChanged += new System.EventHandler(this.editorVelX_ValueChanged);
			// 
			// labelVelX
			// 
			resources.ApplyResources(this.labelVelX, "labelVelX");
			this.labelVelX.Name = "labelVelX";
			// 
			// editorAngleRad
			// 
			this.editorAngleRad.DecimalPlaces = 2;
			resources.ApplyResources(this.editorAngleRad, "editorAngleRad");
			this.editorAngleRad.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
			this.editorAngleRad.Maximum = new decimal(new int[] {
            62832,
            0,
            0,
            262144});
			this.editorAngleRad.Name = "editorAngleRad";
			this.editorAngleRad.ValueChanged += new System.EventHandler(this.editorAngleRad_ValueChanged);
			// 
			// labelAngleRad
			// 
			resources.ApplyResources(this.labelAngleRad, "labelAngleRad");
			this.labelAngleRad.Name = "labelAngleRad";
			// 
			// editorAngleDeg
			// 
			resources.ApplyResources(this.editorAngleDeg, "editorAngleDeg");
			this.editorAngleDeg.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.editorAngleDeg.Name = "editorAngleDeg";
			this.editorAngleDeg.ValueChanged += new System.EventHandler(this.editorAngleDeg_ValueChanged);
			// 
			// labelAngleDeg
			// 
			resources.ApplyResources(this.labelAngleDeg, "labelAngleDeg");
			this.labelAngleDeg.Name = "labelAngleDeg";
			// 
			// editorScaleZ
			// 
			this.editorScaleZ.DecimalPlaces = 2;
			resources.ApplyResources(this.editorScaleZ, "editorScaleZ");
			this.editorScaleZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorScaleZ.Name = "editorScaleZ";
			this.editorScaleZ.ValueChanged += new System.EventHandler(this.editorScaleZ_ValueChanged);
			// 
			// labelScaleZ
			// 
			resources.ApplyResources(this.labelScaleZ, "labelScaleZ");
			this.labelScaleZ.Name = "labelScaleZ";
			// 
			// editorScaleY
			// 
			this.editorScaleY.DecimalPlaces = 2;
			resources.ApplyResources(this.editorScaleY, "editorScaleY");
			this.editorScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorScaleY.Name = "editorScaleY";
			this.editorScaleY.ValueChanged += new System.EventHandler(this.editorScaleY_ValueChanged);
			// 
			// labelScaleY
			// 
			resources.ApplyResources(this.labelScaleY, "labelScaleY");
			this.labelScaleY.Name = "labelScaleY";
			// 
			// editorScaleX
			// 
			this.editorScaleX.DecimalPlaces = 2;
			resources.ApplyResources(this.editorScaleX, "editorScaleX");
			this.editorScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorScaleX.Name = "editorScaleX";
			this.editorScaleX.ValueChanged += new System.EventHandler(this.editorScaleX_ValueChanged);
			// 
			// labelScaleX
			// 
			resources.ApplyResources(this.labelScaleX, "labelScaleX");
			this.labelScaleX.Name = "labelScaleX";
			// 
			// editorPosZ
			// 
			resources.ApplyResources(this.editorPosZ, "editorPosZ");
			this.editorPosZ.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.editorPosZ.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
			this.editorPosZ.Name = "editorPosZ";
			this.editorPosZ.ValueChanged += new System.EventHandler(this.editorPosZ_ValueChanged);
			// 
			// labelPosZ
			// 
			resources.ApplyResources(this.labelPosZ, "labelPosZ");
			this.labelPosZ.Name = "labelPosZ";
			// 
			// editorPosY
			// 
			resources.ApplyResources(this.editorPosY, "editorPosY");
			this.editorPosY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.editorPosY.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
			this.editorPosY.Name = "editorPosY";
			this.editorPosY.ValueChanged += new System.EventHandler(this.editorPosY_ValueChanged);
			// 
			// labelPosY
			// 
			resources.ApplyResources(this.labelPosY, "labelPosY");
			this.labelPosY.Name = "labelPosY";
			// 
			// labelAngleVel
			// 
			resources.ApplyResources(this.labelAngleVel, "labelAngleVel");
			this.labelAngleVel.Name = "labelAngleVel";
			// 
			// labelVel
			// 
			resources.ApplyResources(this.labelVel, "labelVel");
			this.labelVel.Name = "labelVel";
			// 
			// labelAngle
			// 
			resources.ApplyResources(this.labelAngle, "labelAngle");
			this.labelAngle.Name = "labelAngle";
			// 
			// labelScale
			// 
			resources.ApplyResources(this.labelScale, "labelScale");
			this.labelScale.Name = "labelScale";
			// 
			// labelPos
			// 
			resources.ApplyResources(this.labelPos, "labelPos");
			this.labelPos.Name = "labelPos";
			// 
			// separatorPanel
			// 
			this.separatorPanel.BackColor = System.Drawing.Color.DarkGray;
			this.tableLayout.SetColumnSpan(this.separatorPanel, 7);
			resources.ApplyResources(this.separatorPanel, "separatorPanel");
			this.separatorPanel.Name = "separatorPanel";
			// 
			// labelPosX
			// 
			resources.ApplyResources(this.labelPosX, "labelPosX");
			this.labelPosX.Name = "labelPosX";
			// 
			// editorPosX
			// 
			resources.ApplyResources(this.editorPosX, "editorPosX");
			this.editorPosX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.editorPosX.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
			this.editorPosX.Name = "editorPosX";
			this.editorPosX.ValueChanged += new System.EventHandler(this.editorPosX_ValueChanged);
			// 
			// relativeValues
			// 
			resources.ApplyResources(this.relativeValues, "relativeValues");
			this.tableLayout.SetColumnSpan(this.relativeValues, 7);
			this.relativeValues.Name = "relativeValues";
			this.relativeValues.UseVisualStyleBackColor = true;
			this.relativeValues.CheckedChanged += new System.EventHandler(this.relativeValues_CheckedChanged);
			// 
			// separatorPanel2
			// 
			this.separatorPanel2.BackColor = System.Drawing.Color.DarkGray;
			this.tableLayout.SetColumnSpan(this.separatorPanel2, 7);
			resources.ApplyResources(this.separatorPanel2, "separatorPanel2");
			this.separatorPanel2.Name = "separatorPanel2";
			// 
			// TransformPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			resources.ApplyResources(this, "$this");
			this.Name = "TransformPropertyEditor";
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelRad)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelDeg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleRad)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleDeg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosX)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DualityEditor.Controls.DBTableLayoutPanel tableLayout;
		private System.Windows.Forms.Label labelAngleVel;
		private System.Windows.Forms.Label labelVel;
		private System.Windows.Forms.Label labelAngle;
		private System.Windows.Forms.Label labelScale;
		private System.Windows.Forms.Label labelPos;
		private System.Windows.Forms.Panel separatorPanel;
		private System.Windows.Forms.NumericUpDown editorPosZ;
		private System.Windows.Forms.Label labelPosZ;
		private System.Windows.Forms.NumericUpDown editorPosY;
		private System.Windows.Forms.Label labelPosY;
		private System.Windows.Forms.Label labelPosX;
		private System.Windows.Forms.NumericUpDown editorPosX;
		private System.Windows.Forms.NumericUpDown editorScaleX;
		private System.Windows.Forms.Label labelScaleX;
		private System.Windows.Forms.NumericUpDown editorScaleZ;
		private System.Windows.Forms.Label labelScaleZ;
		private System.Windows.Forms.NumericUpDown editorScaleY;
		private System.Windows.Forms.Label labelScaleY;
		private System.Windows.Forms.NumericUpDown editorAngleVelRad;
		private System.Windows.Forms.Label labelAngleVelRad;
		private System.Windows.Forms.NumericUpDown editorAngleVelDeg;
		private System.Windows.Forms.Label labelAngleVelDeg;
		private System.Windows.Forms.NumericUpDown editorVelZ;
		private System.Windows.Forms.Label labelVelZ;
		private System.Windows.Forms.NumericUpDown editorVelY;
		private System.Windows.Forms.Label labelVelY;
		private System.Windows.Forms.NumericUpDown editorVelX;
		private System.Windows.Forms.Label labelVelX;
		private System.Windows.Forms.NumericUpDown editorAngleRad;
		private System.Windows.Forms.Label labelAngleRad;
		private System.Windows.Forms.NumericUpDown editorAngleDeg;
		private System.Windows.Forms.Label labelAngleDeg;
		private System.Windows.Forms.CheckBox relativeValues;
		private System.Windows.Forms.Panel separatorPanel2;





	}
}
