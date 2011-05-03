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
			this.tableLayout = new DualityEditor.Controls.DBTableLayoutPanel(this.components);
			this.labelAngleVel = new System.Windows.Forms.Label();
			this.labelVel = new System.Windows.Forms.Label();
			this.labelAngle = new System.Windows.Forms.Label();
			this.labelScale = new System.Windows.Forms.Label();
			this.labelPos = new System.Windows.Forms.Label();
			this.separatorPanel = new System.Windows.Forms.Panel();
			this.labelPosX = new System.Windows.Forms.Label();
			this.editorPosX = new System.Windows.Forms.NumericUpDown();
			this.labelPosY = new System.Windows.Forms.Label();
			this.editorPosY = new System.Windows.Forms.NumericUpDown();
			this.labelPosZ = new System.Windows.Forms.Label();
			this.editorPosZ = new System.Windows.Forms.NumericUpDown();
			this.labelScaleX = new System.Windows.Forms.Label();
			this.editorScaleX = new System.Windows.Forms.NumericUpDown();
			this.labelScaleY = new System.Windows.Forms.Label();
			this.editorScaleY = new System.Windows.Forms.NumericUpDown();
			this.labelScaleZ = new System.Windows.Forms.Label();
			this.editorScaleZ = new System.Windows.Forms.NumericUpDown();
			this.labelAngleDeg = new System.Windows.Forms.Label();
			this.editorAngleDeg = new System.Windows.Forms.NumericUpDown();
			this.labelAngleRad = new System.Windows.Forms.Label();
			this.editorAngleRad = new System.Windows.Forms.NumericUpDown();
			this.labelVelX = new System.Windows.Forms.Label();
			this.editorVelX = new System.Windows.Forms.NumericUpDown();
			this.labelVelY = new System.Windows.Forms.Label();
			this.editorVelY = new System.Windows.Forms.NumericUpDown();
			this.labelVelZ = new System.Windows.Forms.Label();
			this.editorVelZ = new System.Windows.Forms.NumericUpDown();
			this.labelAngleVelDeg = new System.Windows.Forms.Label();
			this.editorAngleVelDeg = new System.Windows.Forms.NumericUpDown();
			this.labelAngleVelRad = new System.Windows.Forms.Label();
			this.editorAngleVelRad = new System.Windows.Forms.NumericUpDown();
			this.tableLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorPosX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleDeg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleRad)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelZ)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelDeg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelRad)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayout
			// 
			this.tableLayout.BackColor = System.Drawing.Color.Transparent;
			this.tableLayout.ColumnCount = 7;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
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
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 0);
			this.tableLayout.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 6;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayout.Size = new System.Drawing.Size(295, 118);
			this.tableLayout.TabIndex = 0;
			// 
			// labelAngleVel
			// 
			this.labelAngleVel.AutoSize = true;
			this.labelAngleVel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAngleVel.Location = new System.Drawing.Point(3, 95);
			this.labelAngleVel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.labelAngleVel.Name = "labelAngleVel";
			this.labelAngleVel.Size = new System.Drawing.Size(49, 23);
			this.labelAngleVel.TabIndex = 4;
			this.labelAngleVel.Text = "AngleVel";
			this.labelAngleVel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelVel
			// 
			this.labelVel.AutoSize = true;
			this.labelVel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVel.Location = new System.Drawing.Point(3, 73);
			this.labelVel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.labelVel.Name = "labelVel";
			this.labelVel.Size = new System.Drawing.Size(49, 22);
			this.labelVel.TabIndex = 3;
			this.labelVel.Text = "Vel";
			this.labelVel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelAngle
			// 
			this.labelAngle.AutoSize = true;
			this.labelAngle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAngle.Location = new System.Drawing.Point(3, 44);
			this.labelAngle.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.labelAngle.Name = "labelAngle";
			this.labelAngle.Size = new System.Drawing.Size(49, 22);
			this.labelAngle.TabIndex = 2;
			this.labelAngle.Text = "Angle";
			this.labelAngle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelScale
			// 
			this.labelScale.AutoSize = true;
			this.labelScale.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelScale.Location = new System.Drawing.Point(3, 22);
			this.labelScale.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.labelScale.Name = "labelScale";
			this.labelScale.Size = new System.Drawing.Size(49, 22);
			this.labelScale.TabIndex = 1;
			this.labelScale.Text = "Scale";
			this.labelScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPos
			// 
			this.labelPos.AutoSize = true;
			this.labelPos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPos.Location = new System.Drawing.Point(3, 0);
			this.labelPos.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.labelPos.Name = "labelPos";
			this.labelPos.Size = new System.Drawing.Size(49, 22);
			this.labelPos.TabIndex = 0;
			this.labelPos.Text = "Pos";
			this.labelPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// separatorPanel
			// 
			this.separatorPanel.BackColor = System.Drawing.Color.DarkGray;
			this.tableLayout.SetColumnSpan(this.separatorPanel, 7);
			this.separatorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.separatorPanel.Location = new System.Drawing.Point(3, 69);
			this.separatorPanel.Name = "separatorPanel";
			this.separatorPanel.Size = new System.Drawing.Size(289, 1);
			this.separatorPanel.TabIndex = 5;
			// 
			// labelPosX
			// 
			this.labelPosX.AutoSize = true;
			this.labelPosX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPosX.Location = new System.Drawing.Point(52, 0);
			this.labelPosX.Margin = new System.Windows.Forms.Padding(0);
			this.labelPosX.Name = "labelPosX";
			this.labelPosX.Size = new System.Drawing.Size(15, 22);
			this.labelPosX.TabIndex = 6;
			this.labelPosX.Text = "X";
			this.labelPosX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorPosX
			// 
			this.editorPosX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorPosX.Location = new System.Drawing.Point(68, 1);
			this.editorPosX.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorPosX.Size = new System.Drawing.Size(64, 20);
			this.editorPosX.TabIndex = 7;
			// 
			// labelPosY
			// 
			this.labelPosY.AutoSize = true;
			this.labelPosY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPosY.Location = new System.Drawing.Point(133, 0);
			this.labelPosY.Margin = new System.Windows.Forms.Padding(0);
			this.labelPosY.Name = "labelPosY";
			this.labelPosY.Size = new System.Drawing.Size(15, 22);
			this.labelPosY.TabIndex = 8;
			this.labelPosY.Text = "Y";
			this.labelPosY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorPosY
			// 
			this.editorPosY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorPosY.Location = new System.Drawing.Point(149, 1);
			this.editorPosY.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorPosY.Size = new System.Drawing.Size(64, 20);
			this.editorPosY.TabIndex = 9;
			// 
			// labelPosZ
			// 
			this.labelPosZ.AutoSize = true;
			this.labelPosZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPosZ.Location = new System.Drawing.Point(214, 0);
			this.labelPosZ.Margin = new System.Windows.Forms.Padding(0);
			this.labelPosZ.Name = "labelPosZ";
			this.labelPosZ.Size = new System.Drawing.Size(14, 22);
			this.labelPosZ.TabIndex = 10;
			this.labelPosZ.Text = "Z";
			this.labelPosZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorPosZ
			// 
			this.editorPosZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorPosZ.Location = new System.Drawing.Point(229, 1);
			this.editorPosZ.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorPosZ.Size = new System.Drawing.Size(65, 20);
			this.editorPosZ.TabIndex = 11;
			// 
			// labelScaleX
			// 
			this.labelScaleX.AutoSize = true;
			this.labelScaleX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelScaleX.Location = new System.Drawing.Point(52, 22);
			this.labelScaleX.Margin = new System.Windows.Forms.Padding(0);
			this.labelScaleX.Name = "labelScaleX";
			this.labelScaleX.Size = new System.Drawing.Size(15, 22);
			this.labelScaleX.TabIndex = 12;
			this.labelScaleX.Text = "X";
			this.labelScaleX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorScaleX
			// 
			this.editorScaleX.DecimalPlaces = 2;
			this.editorScaleX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorScaleX.Location = new System.Drawing.Point(68, 23);
			this.editorScaleX.Margin = new System.Windows.Forms.Padding(1);
			this.editorScaleX.Name = "editorScaleX";
			this.editorScaleX.Size = new System.Drawing.Size(64, 20);
			this.editorScaleX.TabIndex = 13;
			// 
			// labelScaleY
			// 
			this.labelScaleY.AutoSize = true;
			this.labelScaleY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelScaleY.Location = new System.Drawing.Point(133, 22);
			this.labelScaleY.Margin = new System.Windows.Forms.Padding(0);
			this.labelScaleY.Name = "labelScaleY";
			this.labelScaleY.Size = new System.Drawing.Size(15, 22);
			this.labelScaleY.TabIndex = 14;
			this.labelScaleY.Text = "Y";
			this.labelScaleY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorScaleY
			// 
			this.editorScaleY.DecimalPlaces = 2;
			this.editorScaleY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorScaleY.Location = new System.Drawing.Point(149, 23);
			this.editorScaleY.Margin = new System.Windows.Forms.Padding(1);
			this.editorScaleY.Name = "editorScaleY";
			this.editorScaleY.Size = new System.Drawing.Size(64, 20);
			this.editorScaleY.TabIndex = 15;
			// 
			// labelScaleZ
			// 
			this.labelScaleZ.AutoSize = true;
			this.labelScaleZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelScaleZ.Location = new System.Drawing.Point(214, 22);
			this.labelScaleZ.Margin = new System.Windows.Forms.Padding(0);
			this.labelScaleZ.Name = "labelScaleZ";
			this.labelScaleZ.Size = new System.Drawing.Size(14, 22);
			this.labelScaleZ.TabIndex = 16;
			this.labelScaleZ.Text = "Z";
			this.labelScaleZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorScaleZ
			// 
			this.editorScaleZ.DecimalPlaces = 2;
			this.editorScaleZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorScaleZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorScaleZ.Location = new System.Drawing.Point(229, 23);
			this.editorScaleZ.Margin = new System.Windows.Forms.Padding(1);
			this.editorScaleZ.Name = "editorScaleZ";
			this.editorScaleZ.Size = new System.Drawing.Size(65, 20);
			this.editorScaleZ.TabIndex = 17;
			// 
			// labelAngleDeg
			// 
			this.labelAngleDeg.AutoSize = true;
			this.labelAngleDeg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAngleDeg.Location = new System.Drawing.Point(52, 44);
			this.labelAngleDeg.Margin = new System.Windows.Forms.Padding(0);
			this.labelAngleDeg.Name = "labelAngleDeg";
			this.labelAngleDeg.Size = new System.Drawing.Size(15, 22);
			this.labelAngleDeg.TabIndex = 18;
			this.labelAngleDeg.Text = "D";
			this.labelAngleDeg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorAngleDeg
			// 
			this.editorAngleDeg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorAngleDeg.Location = new System.Drawing.Point(68, 45);
			this.editorAngleDeg.Margin = new System.Windows.Forms.Padding(1);
			this.editorAngleDeg.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.editorAngleDeg.Name = "editorAngleDeg";
			this.editorAngleDeg.Size = new System.Drawing.Size(64, 20);
			this.editorAngleDeg.TabIndex = 19;
			// 
			// labelAngleRad
			// 
			this.labelAngleRad.AutoSize = true;
			this.labelAngleRad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAngleRad.Location = new System.Drawing.Point(133, 44);
			this.labelAngleRad.Margin = new System.Windows.Forms.Padding(0);
			this.labelAngleRad.Name = "labelAngleRad";
			this.labelAngleRad.Size = new System.Drawing.Size(15, 22);
			this.labelAngleRad.TabIndex = 20;
			this.labelAngleRad.Text = "R";
			this.labelAngleRad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorAngleRad
			// 
			this.editorAngleRad.DecimalPlaces = 2;
			this.editorAngleRad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorAngleRad.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
			this.editorAngleRad.Location = new System.Drawing.Point(149, 45);
			this.editorAngleRad.Margin = new System.Windows.Forms.Padding(1);
			this.editorAngleRad.Maximum = new decimal(new int[] {
            628,
            0,
            0,
            131072});
			this.editorAngleRad.Name = "editorAngleRad";
			this.editorAngleRad.Size = new System.Drawing.Size(64, 20);
			this.editorAngleRad.TabIndex = 21;
			// 
			// labelVelX
			// 
			this.labelVelX.AutoSize = true;
			this.labelVelX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVelX.Location = new System.Drawing.Point(52, 73);
			this.labelVelX.Margin = new System.Windows.Forms.Padding(0);
			this.labelVelX.Name = "labelVelX";
			this.labelVelX.Size = new System.Drawing.Size(15, 22);
			this.labelVelX.TabIndex = 24;
			this.labelVelX.Text = "X";
			this.labelVelX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorVelX
			// 
			this.editorVelX.DecimalPlaces = 2;
			this.editorVelX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorVelX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorVelX.Location = new System.Drawing.Point(68, 74);
			this.editorVelX.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorVelX.Size = new System.Drawing.Size(64, 20);
			this.editorVelX.TabIndex = 25;
			// 
			// labelVelY
			// 
			this.labelVelY.AutoSize = true;
			this.labelVelY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVelY.Location = new System.Drawing.Point(133, 73);
			this.labelVelY.Margin = new System.Windows.Forms.Padding(0);
			this.labelVelY.Name = "labelVelY";
			this.labelVelY.Size = new System.Drawing.Size(15, 22);
			this.labelVelY.TabIndex = 26;
			this.labelVelY.Text = "Y";
			this.labelVelY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorVelY
			// 
			this.editorVelY.DecimalPlaces = 2;
			this.editorVelY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorVelY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorVelY.Location = new System.Drawing.Point(149, 74);
			this.editorVelY.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorVelY.Size = new System.Drawing.Size(64, 20);
			this.editorVelY.TabIndex = 27;
			// 
			// labelVelZ
			// 
			this.labelVelZ.AutoSize = true;
			this.labelVelZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVelZ.Location = new System.Drawing.Point(214, 73);
			this.labelVelZ.Margin = new System.Windows.Forms.Padding(0);
			this.labelVelZ.Name = "labelVelZ";
			this.labelVelZ.Size = new System.Drawing.Size(14, 22);
			this.labelVelZ.TabIndex = 28;
			this.labelVelZ.Text = "Z";
			this.labelVelZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorVelZ
			// 
			this.editorVelZ.DecimalPlaces = 2;
			this.editorVelZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorVelZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorVelZ.Location = new System.Drawing.Point(229, 74);
			this.editorVelZ.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorVelZ.Size = new System.Drawing.Size(65, 20);
			this.editorVelZ.TabIndex = 29;
			// 
			// labelAngleVelDeg
			// 
			this.labelAngleVelDeg.AutoSize = true;
			this.labelAngleVelDeg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAngleVelDeg.Location = new System.Drawing.Point(52, 95);
			this.labelAngleVelDeg.Margin = new System.Windows.Forms.Padding(0);
			this.labelAngleVelDeg.Name = "labelAngleVelDeg";
			this.labelAngleVelDeg.Size = new System.Drawing.Size(15, 23);
			this.labelAngleVelDeg.TabIndex = 30;
			this.labelAngleVelDeg.Text = "D";
			this.labelAngleVelDeg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorAngleVelDeg
			// 
			this.editorAngleVelDeg.DecimalPlaces = 2;
			this.editorAngleVelDeg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorAngleVelDeg.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorAngleVelDeg.Location = new System.Drawing.Point(68, 96);
			this.editorAngleVelDeg.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorAngleVelDeg.Size = new System.Drawing.Size(64, 20);
			this.editorAngleVelDeg.TabIndex = 31;
			// 
			// labelAngleVelRad
			// 
			this.labelAngleVelRad.AutoSize = true;
			this.labelAngleVelRad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAngleVelRad.Location = new System.Drawing.Point(133, 95);
			this.labelAngleVelRad.Margin = new System.Windows.Forms.Padding(0);
			this.labelAngleVelRad.Name = "labelAngleVelRad";
			this.labelAngleVelRad.Size = new System.Drawing.Size(15, 23);
			this.labelAngleVelRad.TabIndex = 32;
			this.labelAngleVelRad.Text = "R";
			this.labelAngleVelRad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// editorAngleVelRad
			// 
			this.editorAngleVelRad.DecimalPlaces = 2;
			this.editorAngleVelRad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorAngleVelRad.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.editorAngleVelRad.Location = new System.Drawing.Point(149, 96);
			this.editorAngleVelRad.Margin = new System.Windows.Forms.Padding(1);
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
			this.editorAngleVelRad.Size = new System.Drawing.Size(64, 20);
			this.editorAngleVelRad.TabIndex = 33;
			// 
			// TransformPropertyEditor
			// 
			this.Controls.Add(this.tableLayout);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TransformPropertyEditor";
			this.Size = new System.Drawing.Size(295, 118);
			this.tableLayout.ResumeLayout(false);
			this.tableLayout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.editorPosX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorPosZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorScaleZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleDeg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleRad)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorVelZ)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelDeg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.editorAngleVelRad)).EndInit();
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





	}
}
