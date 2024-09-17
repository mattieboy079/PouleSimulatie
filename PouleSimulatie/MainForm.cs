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
		
		var form = new PouleForm(_clubs);
		form.Show();
	}
	
	private void BtnSimulateThousand_Click(object sender, EventArgs e)
	{
		if (_clubs.Count < 2)
		{
			MessageBox.Show("Voeg minimaal 2 teams toe.");
			return;
		}
	}
}