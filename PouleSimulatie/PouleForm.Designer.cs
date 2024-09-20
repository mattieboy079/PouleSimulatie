namespace PouleSimulatie
{
	partial class PouleForm
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
			BtnExit = new Button();
			BtnPlayOne = new Button();
			BtnSimulateAll = new Button();
			BtnPrevious = new Button();
			BtnNext = new Button();
			LblPlayround = new Label();
			SuspendLayout();
			// 
			// BtnExit
			// 
			BtnExit.Location = new Point(1056, 12);
			BtnExit.Name = "BtnExit";
			BtnExit.Size = new Size(132, 23);
			BtnExit.TabIndex = 0;
			BtnExit.Text = "Sluiten";
			BtnExit.UseVisualStyleBackColor = true;
			BtnExit.Click += BtnExit_Click;
			// 
			// BtnPlayOne
			// 
			BtnPlayOne.Location = new Point(12, 12);
			BtnPlayOne.Name = "BtnPlayOne";
			BtnPlayOne.Size = new Size(132, 23);
			BtnPlayOne.TabIndex = 1;
			BtnPlayOne.Text = "Speel 1 wedstrijd";
			BtnPlayOne.UseVisualStyleBackColor = true;
			BtnPlayOne.Click += BtnPlayOne_Click;
			// 
			// BtnSimulateAll
			// 
			BtnSimulateAll.Location = new Point(12, 41);
			BtnSimulateAll.Name = "BtnSimulateAll";
			BtnSimulateAll.Size = new Size(132, 23);
			BtnSimulateAll.TabIndex = 2;
			BtnSimulateAll.Text = "Simuleer alles";
			BtnSimulateAll.UseVisualStyleBackColor = true;
			BtnSimulateAll.Click += BtnSimulateAll_Click;
			// 
			// BtnPrevious
			// 
			BtnPrevious.Location = new Point(12, 70);
			BtnPrevious.Name = "BtnPrevious";
			BtnPrevious.Size = new Size(132, 23);
			BtnPrevious.TabIndex = 3;
			BtnPrevious.Text = "Vorige speelronde";
			BtnPrevious.UseVisualStyleBackColor = true;
			BtnPrevious.Click += BtnPrevious_Click;
			// 
			// BtnNext
			// 
			BtnNext.Location = new Point(236, 70);
			BtnNext.Name = "BtnNext";
			BtnNext.Size = new Size(132, 23);
			BtnNext.TabIndex = 4;
			BtnNext.Text = "Volgende speelronde";
			BtnNext.UseVisualStyleBackColor = true;
			BtnNext.Click += BtnNext_Click;
			// 
			// LblPlayround
			// 
			LblPlayround.AutoSize = true;
			LblPlayround.Location = new Point(170, 74);
			LblPlayround.Name = "LblPlayround";
			LblPlayround.Size = new Size(36, 15);
			LblPlayround.TabIndex = 5;
			LblPlayround.Text = "12/12";
			// 
			// PouleForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1200, 450);
			Controls.Add(LblPlayround);
			Controls.Add(BtnNext);
			Controls.Add(BtnPrevious);
			Controls.Add(BtnSimulateAll);
			Controls.Add(BtnPlayOne);
			Controls.Add(BtnExit);
			Name = "PouleForm";
			Text = "PouleForm";
			Paint += OnPaintHandler;
			SizeChanged += SizeChangedHandler;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button BtnExit;
		private Button BtnPlayOne;
		private Button BtnSimulateAll;
		private Button BtnPrevious;
		private Button BtnNext;
		private Label LblPlayround;
	}
}