using System.Globalization;

namespace PouleSimulatie;

public partial class MainForm : Form
{
	private readonly List<Club> _clubs = new();
	
	public MainForm()
	{
		InitializeComponent();
	}
	
	private void MainForm_Load(object sender, EventArgs e)
	{
		ListTeams.Items.Clear();
		var random = new Random();
		for (var c = 0; c < 4; c++)
		{
			var min = c * 25;
			var max = min + 24;
			var club = new Club($"Club {c + 1}", random.Next(min, max), random.Next(min, max), random.Next(min, max));
			ClubCreated(club);
		}
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
		
		var clubName = TxtClubname.Text;
		if (_clubs.Any(c => c.Name == clubName))
		{
			MessageBox.Show("Deze club bestaat al.");
			return;
		}
		
		var attack = (int)NumAtt.Value;
		var midfield = (int)NumMid.Value;
		var defence = (int)NumDef.Value;
		
		var club = new Club(clubName, attack, midfield, defence);
		ClubCreated(club);
		TxtClubname.Text = "";
		NumAtt.Text = "";
		NumMid.Text = "";
		NumDef.Text = "";
	}

	private void ClubCreated(Club club)
	{
		_clubs.Add(club);
		ListTeams.Items.Add($"{club.Name} - A:{club.Attack} M:{club.Midfield} D:{club.Defence}");
	}

	private void BtnDelete_Click(object sender, EventArgs e) 
	{
		if (ListTeams.SelectedItems.Count > 0)
		{
			var rowsToRemove = ListTeams.SelectedItems.Cast<string>().ToList();
			foreach (var row in rowsToRemove)
			{
				var clubName = row.Split(' ').First();
				var club = _clubs.First(c => c.Name == clubName);
				_clubs.Remove(club);
				ListTeams.Items.Remove(row);
			}
		}
		else
		{
			MessageBox.Show("Geen teams geselecteerd om te verwijderen.");
		}
	}
	
	private void BtnStart_Click(object sender, EventArgs e)
	{
		if (_clubs.Count < 2)
		{
			MessageBox.Show("Voeg minimaal 2 teams toe.");
			return;
		}
		
		var form = new PouleForm(_clubs, CheckReturns.Checked);
		form.Show();
	}
	
	private void BtnSimulateThousand_Click(object sender, EventArgs e)
	{
		var startTime = DateTime.Now;
		
		if (_clubs.Count < 2)
		{
			MessageBox.Show("Voeg minimaal 2 teams toe.");
			return;
		}

		var returns = CheckReturns.Checked;
		
		var simulations = 1000000;
		var random = new Random();
		var tasks = new List<Task>();

		var simulationResult = new MassSimulationResult(_clubs);

		for (int p = 0; p < simulations; p++)
		{
			tasks.Add(Task.Run(() =>
			{
				Poule poule = new(_clubs, returns, random);
				poule.Init();
				poule.SimulateAllMatches();
				simulationResult.AddResults(poule);
			}));
		}
		
		Task.WhenAll(tasks).GetAwaiter().GetResult();

		var timeTaken = Math.Round((DateTime.Now - startTime).TotalSeconds, 3);
		
		MessageBox.Show($"Time: {timeTaken}\n{simulationResult.GetResults(simulations)}");
	}
}

public class MassSimulationResult
{
	public List<ClubResult> ClubResults { get; set; }

	public MassSimulationResult(List<Club> clubs)
	{
		ClubResults = clubs.Select(c => new ClubResult(c, clubs.Count)).ToList();
	}

	public void AddResults(Poule poule)
	{
		var orderedStand = poule.GetOrderedStand();
		for(int i = 0; i < orderedStand.Count; i++)
			ClubResults.First(c => c.ClubName == orderedStand[i].Club.Name).AddResult(i + 1, orderedStand[i].GetPoints());
	}

	public string GetResults(int simulations)
	{
		return string.Join("\n", ClubResults.Select(c => c.GetResult(simulations)));
	}
}

public class ClubResult
{
	public string ClubName;
	public readonly Dictionary<int, int> Results;
	public int TotalPoints;
	
	public ClubResult(Club club, int clubsCount)
	{
		ClubName = club.Name;
		Results = new();
		for (int i = 1; i <= clubsCount; i++)
		{
			Results.Add(i, 0);
		}
	}
	
	public void AddResult(int position, int points)
	{
		Results[position]++;
		TotalPoints += points;
	}

	public string GetResult(int simulations)
	{
		return $"{ClubName} - {string.Join(", ", Results.Select(r => $"{r.Key}: {r.Value}"))} - {Math.Round((double)TotalPoints / simulations, 2)} pts avg";
	}
}