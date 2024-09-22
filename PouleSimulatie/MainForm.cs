using System.Data;
using PouleSimulatie.Interfaces;
using PouleSimulatie.Services;

namespace PouleSimulatie;

public partial class MainForm : Form
{
	private readonly IClubService _clubService;
	private readonly ISimulationService _simulationService;
	
	public MainForm(IClubService clubService, ISimulationService simulationService)
	{
		InitializeComponent();
		_clubService = clubService;
		_simulationService = simulationService;
	}
	
	/// <summary>
	/// Edit the components when a club is created
	/// </summary>
	/// <param name="club">The created club</param>
	private void ClubCreated(Club club)
	{
		ListTeams.Items.Add(club.ToString());
		if(ListTeams.Items.Count > 1)
			NumAdvancingTeams.Maximum = ListTeams.Items.Count - 1;
	}

	#region EventHandlers

	private void MainForm_Load(object sender, EventArgs e)
	{
		ListTeams.Items.Clear();
		foreach (var club in _clubService.CreateRandomClubs(4))
			ListTeams.Items.Add(club.ToString());
		NumAdvancingTeams.Maximum = 3;
		NumAdvancingTeams.Value = 2;
		NumAtt.Text = "";
		NumMid.Text = "";
		NumDef.Text = "";
	}
	
	private void BtnAdd_Click(object sender, EventArgs e)
	{
		if (TxtClubname.Text == "")
		{
			MessageBox.Show("Vul een clubnaam in.");
			return;
		}
		if(NumAtt.Text == "" || NumMid.Text == "" || NumDef.Text == "")
		{
			MessageBox.Show("Vul alle ratings in.");
			return;
		}

		try
		{
			var newClub = _clubService.TryCreatingClub(TxtClubname.Text, NumAtt.Value, NumMid.Value, NumDef.Value);
			ClubCreated(newClub);
			TxtClubname.Text = "";
			NumAtt.Text = "";
			NumMid.Text = "";
			NumDef.Text = "";
		}
		catch (DuplicateNameException ex)
		{
			MessageBox.Show(ex.Message);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Er is iets fout gegaan bij het toevoegen van de club.");
		}
	}

	private void BtnDelete_Click(object sender, EventArgs e) 
	{
		if (ListTeams.SelectedItems.Count > 0)
		{
			var rowsToRemove = ListTeams.SelectedItems.Cast<string>().ToList();
			foreach (var row in rowsToRemove)
			{
				var clubName = row.Split(" - ").First();
				if (!_clubService.RemoveClub(clubName)) continue;
				
				ListTeams.Items.Remove(row);
				if (ListTeams.Items.Count < 2) continue;
				
				NumAdvancingTeams.Maximum--;
				if (NumAdvancingTeams.Value > NumAdvancingTeams.Maximum)
					NumAdvancingTeams.Value = NumAdvancingTeams.Maximum;
			}
		}
		else
		{
			MessageBox.Show("Geen teams geselecteerd om te verwijderen.");
		}
	}
	
	private void BtnStart_Click(object sender, EventArgs e)
	{
		if (_clubService.Clubs.Count < 2)
		{
			MessageBox.Show("Voeg minimaal 2 teams toe.");
			return;
		}
		
		var form = new PouleForm(_clubService.Clubs, CheckReturns.Checked, (int)NumAdvancingTeams.Value, new DataTableRendererService());
		form.Show();
	}
	
	private void BtnSimulateThousand_Click(object sender, EventArgs e)
	{
		var simulations = 1000000;
		pbSimulateThousand.Visible = true;
		pbSimulateThousand.Value = 0;
		pbSimulateThousand.Maximum = simulations;
		var task = _simulationService.Simulate(simulations, _clubService.Clubs, CheckReturns.Checked, (int)NumAdvancingTeams.Value);

		while (!task.IsCompleted)
		{
			Task.Delay(15).GetAwaiter().GetResult();
			var progress = _simulationService.GetProgress();
			pbSimulateThousand.Value = progress;
			Application.DoEvents();
		}
		
		var result = task.Result;
		
		if (result == null)
			return;
		
		MessageBox.Show($"Time: {result.TimeTaken}\n{result.GetResults(simulations)}");
		pbSimulateThousand.Visible = false;
	}

	#endregion
}