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
			SuspendLayout();
			// 
			// BtnSluiten
			// 
			BtnSluiten.Location = new Point(12, 12);
			BtnSluiten.Name = "BtnSluiten";
			BtnSluiten.Size = new Size(75, 23);
			BtnSluiten.TabIndex = 0;
			BtnSluiten.Text = "Sluiten";
			BtnSluiten.UseVisualStyleBackColor = true;
			BtnSluiten.Click += BtnSluiten_Click;
			// 
			// MassSimulationResultForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(BtnSluiten);
			Paint += MassSimulationResultForm_Paint;
			Name = "MassSimulationResultForm";
			Text = "MassSimulationResultForm";
			ResumeLayout(false);
		}

		#endregion

		private Button BtnSluiten;
	}
}