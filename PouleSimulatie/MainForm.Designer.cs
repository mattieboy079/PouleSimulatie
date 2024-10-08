﻿namespace PouleSimulatie
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			helpProvider1 = new HelpProvider();
			ListTeams = new ListBox();
			BtnAdd = new Button();
			BtnStart = new Button();
			BtnSimulateThousand = new Button();
			TxtClubname = new TextBox();
			LblClubname = new Label();
			NumAtt = new NumericUpDown();
			LblAtt = new Label();
			LblRatings = new Label();
			LblMid = new Label();
			NumMid = new NumericUpDown();
			LblDef = new Label();
			NumDef = new NumericUpDown();
			LblTeams = new Label();
			BtnDelete = new Button();
			CheckReturns = new CheckBox();
			NumAdvancingTeams = new NumericUpDown();
			LblAdvancingTeams = new Label();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			pbSimulateThousand = new ProgressBar();
			((System.ComponentModel.ISupportInitialize)NumAtt).BeginInit();
			((System.ComponentModel.ISupportInitialize)NumMid).BeginInit();
			((System.ComponentModel.ISupportInitialize)NumDef).BeginInit();
			((System.ComponentModel.ISupportInitialize)NumAdvancingTeams).BeginInit();
			SuspendLayout();
			// 
			// ListTeams
			// 
			ListTeams.FormattingEnabled = true;
			ListTeams.ItemHeight = 15;
			ListTeams.Location = new Point(288, 66);
			ListTeams.Name = "ListTeams";
			ListTeams.Size = new Size(200, 109);
			ListTeams.TabIndex = 10;
			// 
			// BtnAdd
			// 
			BtnAdd.Location = new Point(12, 182);
			BtnAdd.Name = "BtnAdd";
			BtnAdd.Size = new Size(241, 23);
			BtnAdd.TabIndex = 4;
			BtnAdd.Text = "Voeg team toe aan poule";
			BtnAdd.UseVisualStyleBackColor = true;
			BtnAdd.Click += BtnAdd_Click;
			// 
			// BtnStart
			// 
			BtnStart.Location = new Point(12, 240);
			BtnStart.Name = "BtnStart";
			BtnStart.Size = new Size(241, 23);
			BtnStart.TabIndex = 5;
			BtnStart.Text = "Start poule";
			BtnStart.UseVisualStyleBackColor = true;
			BtnStart.Click += BtnStart_Click;
			// 
			// BtnSimulateThousand
			// 
			BtnSimulateThousand.Location = new Point(12, 270);
			BtnSimulateThousand.Name = "BtnSimulateThousand";
			BtnSimulateThousand.Size = new Size(241, 23);
			BtnSimulateThousand.TabIndex = 6;
			BtnSimulateThousand.Text = "Simuleer 1.000.000x";
			BtnSimulateThousand.UseVisualStyleBackColor = true;
			BtnSimulateThousand.Click += BtnSimulateThousand_Click;
			// 
			// TxtClubname
			// 
			TxtClubname.Location = new Point(106, 29);
			TxtClubname.Name = "TxtClubname";
			TxtClubname.Size = new Size(147, 23);
			TxtClubname.TabIndex = 0;
			// 
			// LblClubname
			// 
			LblClubname.AutoSize = true;
			LblClubname.Location = new Point(12, 32);
			LblClubname.Name = "LblClubname";
			LblClubname.Size = new Size(62, 15);
			LblClubname.TabIndex = 21;
			LblClubname.Text = "Clubnaam";
			// 
			// NumAtt
			// 
			NumAtt.Location = new Point(69, 95);
			NumAtt.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			NumAtt.Name = "NumAtt";
			NumAtt.Size = new Size(41, 23);
			NumAtt.TabIndex = 1;
			NumAtt.Value = new decimal(new int[] { 100, 0, 0, 0 });
			// 
			// LblAtt
			// 
			LblAtt.AutoSize = true;
			LblAtt.Location = new Point(12, 97);
			LblAtt.Name = "LblAtt";
			LblAtt.Size = new Size(41, 15);
			LblAtt.TabIndex = 25;
			LblAtt.Text = "Attack";
			// 
			// LblRatings
			// 
			LblRatings.AutoSize = true;
			LblRatings.Location = new Point(12, 66);
			LblRatings.Name = "LblRatings";
			LblRatings.Size = new Size(86, 15);
			LblRatings.TabIndex = 26;
			LblRatings.Text = "Ratings (1-100)";
			// 
			// LblMid
			// 
			LblMid.AutoSize = true;
			LblMid.Location = new Point(12, 126);
			LblMid.Name = "LblMid";
			LblMid.Size = new Size(51, 15);
			LblMid.TabIndex = 28;
			LblMid.Text = "Midfield";
			// 
			// NumMid
			// 
			NumMid.Location = new Point(69, 124);
			NumMid.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			NumMid.Name = "NumMid";
			NumMid.Size = new Size(41, 23);
			NumMid.TabIndex = 2;
			NumMid.Value = new decimal(new int[] { 100, 0, 0, 0 });
			// 
			// LblDef
			// 
			LblDef.AutoSize = true;
			LblDef.Location = new Point(12, 155);
			LblDef.Name = "LblDef";
			LblDef.Size = new Size(50, 15);
			LblDef.TabIndex = 30;
			LblDef.Text = "Defence";
			// 
			// NumDef
			// 
			NumDef.Location = new Point(69, 153);
			NumDef.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			NumDef.Name = "NumDef";
			NumDef.Size = new Size(41, 23);
			NumDef.TabIndex = 3;
			NumDef.Value = new decimal(new int[] { 100, 0, 0, 0 });
			// 
			// LblTeams
			// 
			LblTeams.AutoSize = true;
			LblTeams.Location = new Point(288, 32);
			LblTeams.Name = "LblTeams";
			LblTeams.Size = new Size(40, 15);
			LblTeams.TabIndex = 20;
			LblTeams.Text = "Teams";
			// 
			// BtnDelete
			// 
			BtnDelete.Location = new Point(288, 182);
			BtnDelete.Name = "BtnDelete";
			BtnDelete.Size = new Size(120, 23);
			BtnDelete.TabIndex = 11;
			BtnDelete.Text = "Verwijder team";
			BtnDelete.UseVisualStyleBackColor = true;
			BtnDelete.Click += BtnDelete_Click;
			// 
			// CheckReturns
			// 
			CheckReturns.AutoSize = true;
			CheckReturns.Location = new Point(12, 215);
			CheckReturns.Name = "CheckReturns";
			CheckReturns.Size = new Size(66, 19);
			CheckReturns.TabIndex = 31;
			CheckReturns.Text = "Returns";
			CheckReturns.UseVisualStyleBackColor = true;
			// 
			// NumAdvancingTeams
			// 
			NumAdvancingTeams.Location = new Point(106, 211);
			NumAdvancingTeams.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
			NumAdvancingTeams.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			NumAdvancingTeams.Name = "NumAdvancingTeams";
			NumAdvancingTeams.Size = new Size(43, 23);
			NumAdvancingTeams.TabIndex = 32;
			NumAdvancingTeams.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// LblAdvancingTeams
			// 
			LblAdvancingTeams.AutoSize = true;
			LblAdvancingTeams.Location = new Point(155, 216);
			LblAdvancingTeams.Name = "LblAdvancingTeams";
			LblAdvancingTeams.Size = new Size(98, 15);
			LblAdvancingTeams.TabIndex = 33;
			LblAdvancingTeams.Text = "Teams advancing";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(116, 97);
			label1.Name = "label1";
			label1.Size = new Size(122, 15);
			label1.TabIndex = 34;
			label1.Text = "Increase score chance";
			// 
			// label2
			// 
			label2.Location = new Point(116, 149);
			label2.Name = "label2";
			label2.Size = new Size(132, 34);
			label2.TabIndex = 35;
			label2.Text = "Decrease opponent score chance";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(116, 126);
			label3.Name = "label3";
			label3.Size = new Size(110, 15);
			label3.TabIndex = 36;
			label3.Text = "Increase att and def";
			// 
			// pbSimulateThousand
			// 
			pbSimulateThousand.Location = new Point(12, 299);
			pbSimulateThousand.Maximum = 1;
			pbSimulateThousand.Name = "pbSimulateThousand";
			pbSimulateThousand.Size = new Size(241, 23);
			pbSimulateThousand.Step = 1;
			pbSimulateThousand.TabIndex = 37;
			pbSimulateThousand.Visible = false;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(pbSimulateThousand);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(LblAdvancingTeams);
			Controls.Add(NumAdvancingTeams);
			Controls.Add(CheckReturns);
			Controls.Add(BtnDelete);
			Controls.Add(LblTeams);
			Controls.Add(LblDef);
			Controls.Add(NumDef);
			Controls.Add(LblMid);
			Controls.Add(NumMid);
			Controls.Add(LblRatings);
			Controls.Add(LblAtt);
			Controls.Add(NumAtt);
			Controls.Add(LblClubname);
			Controls.Add(TxtClubname);
			Controls.Add(BtnAdd);
			Controls.Add(BtnStart);
			Controls.Add(BtnSimulateThousand);
			Controls.Add(ListTeams);
			Name = "MainForm";
			Text = "MainForm";
			Load += MainForm_Load;
			((System.ComponentModel.ISupportInitialize)NumAtt).EndInit();
			((System.ComponentModel.ISupportInitialize)NumMid).EndInit();
			((System.ComponentModel.ISupportInitialize)NumDef).EndInit();
			((System.ComponentModel.ISupportInitialize)NumAdvancingTeams).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private HelpProvider helpProvider1;
		private ListBox ListTeams;
		private Button BtnAdd;
		private Button BtnStart;
		private Button BtnSimulateThousand;
		private TextBox TxtClubname;
		private Label LblClubname;
		private NumericUpDown NumAtt;
		private Label LblAtt;
		private Label LblRatings;
		private Label LblMid;
		private NumericUpDown NumMid;
		private Label LblDef;
		private NumericUpDown NumDef;
		private Label LblTeams;
		private Button BtnDelete;
		private CheckBox CheckReturns;
		private NumericUpDown NumAdvancingTeams;
		private Label LblAdvancingTeams;
		private Label label1;
		private Label label2;
		private Label label3;
		private ProgressBar pbSimulateThousand;
	}
}
