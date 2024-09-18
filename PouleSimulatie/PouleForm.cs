namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly Poule _poule;
	private int _currentRound = 1;

	public PouleForm(List<Club> clubs)
	{
		DoubleBuffered = true;
		_poule = new Poule(clubs, false);
		InitializeComponent();
		Init();
	}

	private void Init()
	{
		_poule.Init();
		LblPlayround.Text = $"1/{_poule.TotalRondes}";
		Refresh();
	}

	private void Draw(object? sender, PaintEventArgs e)
	{
		var graphics = e.Graphics;
		DrawPlayRound(graphics);
		//DrawStand(graphics);
	}

	private void DrawPlayRound(Graphics graphics)
	{
		List<Match> matches = _poule.GetMatches(_currentRound);
		for(var index = 0; index < matches.Count; index++)
		{
			var match = matches[index];
			var homeClub = match.HomeClub;
			var awayClub = match.AwayClub;
			var scoreString = match.IsPlayed ? $"{match.HomeGoals} - {match.AwayGoals}" : "";
			var matchString = $"{homeClub.Name} - {awayClub.Name}";
			DrawMatch(graphics, index, matchString, scoreString);
		}
	}

	private void DrawMatch(Graphics graphics, int index, string matchString, string scoreString)
	{
		var startHeight = 110;
		var heightInc = 30;
		
		var font = new Font("Arial", 16);
		var brush = new SolidBrush(Color.Black);
		
		graphics.DrawString(matchString, font, brush, 12, startHeight + heightInc * index);
	}

	private void DrawStand(Graphics graphics)
	{
		throw new NotImplementedException();
	}

	private void BtnPlayOne_Click(object sender, EventArgs e)
	{
		
	}

	private void BtnSimulateAll_Click(object sender, EventArgs e)
	{
		
	}

	private void BtnPrevious_Click(object sender, EventArgs e)
	{
		if(_currentRound > 1)
			_currentRound--;
		
		Refresh();
	}

	private void BtnNext_Click(object sender, EventArgs e)
	{
		if(_currentRound < _poule.TotalRondes)
			_currentRound++;
		
		Refresh();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}
}