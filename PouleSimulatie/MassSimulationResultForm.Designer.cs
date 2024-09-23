namespace PouleSimulatie
{
	partial class MassSimulationResultForm
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
			BtnSluiten = new Button();
			LblTime = new Label();
			SuspendLayout();
			// 
			// BtnSluiten
			// 
			BtnSluiten.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
			BtnSluiten.Location = new Point(12, 12);
			BtnSluiten.Name = "BtnSluiten";
			BtnSluiten.Size = new Size(104, 41);
			BtnSluiten.TabIndex = 0;
			BtnSluiten.Text = "Sluiten";
			BtnSluiten.UseVisualStyleBackColor = true;
			BtnSluiten.Click += BtnSluiten_Click;
			// 
			// LblTime
			// 
			LblTime.AutoSize = true;
			LblTime.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
			LblTime.Location = new Point(162, 18);
			LblTime.Name = "LblTime";
			LblTime.Size = new Size(80, 28);
			LblTime.TabIndex = 1;
			LblTime.Text = "LblTime";
			// 
			// MassSimulationResultForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(LblTime);
			Controls.Add(BtnSluiten);
			Name = "MassSimulationResultForm";
			Text = "MassSimulationResultForm";
			Paint += MassSimulationResultForm_Paint;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button BtnSluiten;
		private Label LblTime;
	}
}