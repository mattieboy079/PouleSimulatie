namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly Poule _poule;
	private int _currentRound = 1;
	private int _teamsAdvancing = 2;

	public PouleForm(List<Club> clubs, bool returns)
	{
		DoubleBuffered = true;
		_poule = new Poule(clubs, returns, new Random());
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
		DrawStand(graphics);
	}
	
	private void RefreshScreen(object? sender, EventArgs e)
	{
		BtnExit.Location = new Point(Size.Width - 162, 12);
		Refresh();
	}

	private void DrawPlayRound(Graphics graphics)
	{
		LblPlayround.Text = $"{_currentRound}/{_poule.TotalRondes}";
		List<Match> matches = _poule.GetMatches(_currentRound);
		for(var index = 0; index < matches.Count; index++)
		{
			var match = matches[index];
			var homeClub = match.HomeClub;
			var awayClub = match.AwayClub;
			var scoreString = match.IsPlayed ? $"{match.HomeGoals} - {match.AwayGoals}" : "";
			var matchString = $"{homeClub.Name} ({homeClub.GetRating()}) - {awayClub.Name} ({awayClub.GetRating()})";
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
		graphics.DrawString(scoreString, font, brush, 300, startHeight + heightInc * index);
	}

	private void DrawStand(Graphics graphics)
	{
		var startHeight = 110;
		var heightInc = 30;
		var font = new Font("Arial", 16);
		var brush = new SolidBrush(Color.Black);
		var pen = new Pen(Color.Black, 1);

		var format = new StringFormat
		{
			LineAlignment = StringAlignment.Center
		};
		
		var numberWidth = 0.07;
		var stringWidth = 0.265;

		var tableRect = new Rectangle(380, startHeight, Size.Width - 410, heightInc);
		
		graphics.DrawRectangle(pen, tableRect.X, tableRect.Y, tableRect.Width, heightInc);
        graphics.DrawString("#", font, brush, new Rectangle(tableRect.X, tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("Club", font, brush, new Rectangle(tableRect.X + (int)(numberWidth * tableRect.Width), tableRect.Y, (int)(stringWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("Pts", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("P", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 2) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("W", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 3) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("D", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 4) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("L", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 5) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("+/-", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 6) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("+", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 7) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("-", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 8) * tableRect.Width), tableRect.Y, (int)(numberWidth * tableRect.Width), heightInc), format);
        graphics.DrawString("Rating", font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 9) * tableRect.Width), tableRect.Y, (int)(numberWidth * 1.5 * tableRect.Width), heightInc), format);
        
        var stand = _poule.GetOrderedStand();
        for (var i = 0; i < stand.Count; i++)
		{
			var startheight = tableRect.Y + heightInc * (i + 1);
			graphics.DrawRectangle(pen, tableRect.X, startheight, tableRect.Width, heightInc);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, GetBackgroundColor(i + 1))), new Rectangle(tableRect.X, startheight, tableRect.Width, heightInc));
	        graphics.DrawString((i + 1).ToString(), font, brush, new Rectangle(tableRect.X, startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].Club.Name, font, brush, new Rectangle(tableRect.X + (int)(numberWidth * tableRect.Width), startheight, (int)(stringWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].GetPoints().ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].GetPlayed().ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 2) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].Won.ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 3) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].Drawn.ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 4) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].Lost.ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 5) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].GetGoalDiff().ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 6) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].GoalsFor.ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 7) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].GoalsAgainst.ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 8) * tableRect.Width), startheight, (int)(numberWidth * tableRect.Width), heightInc), format);
	        graphics.DrawString(stand[i].Club.GetRating().ToString(), font, brush, new Rectangle(tableRect.X + (int)((stringWidth + numberWidth * 9) * tableRect.Width), startheight, (int)(numberWidth * 1.5 * tableRect.Width), heightInc), format);
	    }
	}

	private Color GetBackgroundColor(int pos)
	{
		return pos <= _teamsAdvancing 
			? Color.LimeGreen 
			: Color.White;
	}

	private void BtnPlayOne_Click(object sender, EventArgs e)
	{
		var nextRound = _poule.GetNextMatchRound();
		if (nextRound == null)
		{
			MessageBox.Show("Alle wedstrijden zijn al gespeeld");
			return;
		}

		_currentRound = nextRound.Value;
		_poule.SimulateNextMatch();
		Refresh();
	}

	private void BtnSimulateAll_Click(object sender, EventArgs e)
	{
		_poule.SimulateAllMatches();
		Refresh();
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