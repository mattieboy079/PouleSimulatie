namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly Poule _poule;
	private int _currentRound = 1;
	private readonly int _teamsAdvancing;
	private bool _isOrdering;
	private const double NumberWidth = 0.07;
	private const double StringWidth = 0.265;
	private const int TableRowHeight = 30;

	private double _addValueOffset;
	private double _addValueSizeFactor = 1;
	private double _pointsSizeFactor = 1;
	private Dictionary<string, int> _clubRowOffset;
	private Dictionary<string, double> _clubRowSize;
	private Dictionary<string, int> _lastDrawnOrder;

	public PouleForm(List<Club> clubs, bool returns, int teamsAdvancing)
	{
		_teamsAdvancing = teamsAdvancing;
		DoubleBuffered = true;
		_poule = new Poule(clubs, returns, new Random());
		_clubRowOffset = new Dictionary<string, int>();
		_clubRowSize = new Dictionary<string, double>();
		_lastDrawnOrder = new Dictionary<string, int>();
		for (var i = 0; i < clubs.Count; i++)
		{
			_clubRowSize.Add(clubs[i].Name, 1);
			_clubRowOffset.Add(clubs[i].Name, 0);
			_lastDrawnOrder.Add(clubs[i].Name, i);
		}
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
		
		var font = new Font("Arial", 16);
		var brush = new SolidBrush(Color.Black);
		
		graphics.DrawString(matchString, font, brush, 12, startHeight + TableRowHeight * index);
		graphics.DrawString(scoreString, font, brush, 300, startHeight + TableRowHeight * index);
	}

	private void DrawStand(Graphics graphics)
	{
		//TODO: Create a table class to do all the drawing for the table
		var startHeight = 110;
		var font = new Font("Arial", 16);
		var brush = new SolidBrush(Color.Black);
		var pen = new Pen(Color.Black, 1);

		var format = new StringFormat
		{
			LineAlignment = StringAlignment.Center
		};
		
		var tableRect = new Rectangle(380, startHeight, Size.Width - 410, TableRowHeight);
		
		graphics.DrawRectangle(pen, tableRect.X, tableRect.Y, tableRect.Width, TableRowHeight);
        graphics.DrawString("#", font, brush, new Rectangle(tableRect.X, tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("Club", font, brush, new Rectangle(tableRect.X + (int)(NumberWidth * tableRect.Width), tableRect.Y, (int)(StringWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("Pts", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("P", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 2) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("W", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 3) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("D", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 4) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("L", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 5) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("+/-", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 6) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("+", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 7) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("-", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 8) * tableRect.Width), tableRect.Y, (int)(NumberWidth * tableRect.Width), TableRowHeight), format);
        graphics.DrawString("Rating", font, brush, new Rectangle(tableRect.X + (int)((StringWidth + NumberWidth * 9) * tableRect.Width), tableRect.Y, (int)(NumberWidth * 1.5 * tableRect.Width), TableRowHeight), format);
        
        if (_isOrdering)
        {
	        for (int i = 0; i < _poule.Stand.Count; i++)
	        {
		        var height = tableRect.Y + (i + 1) * TableRowHeight - _clubRowOffset[_poule.Stand[i].Club.Name];
		        var widthInc = tableRect.Width * (_clubRowSize[_poule.Stand[i].Club.Name] - 1);
		        var heightInc = TableRowHeight * (_clubRowSize[_poule.Stand[i].Club.Name] - 1);
		        var rect = new Rectangle((int)(tableRect.X - widthInc / 2), (int)(height - heightInc / 2), (int)(tableRect.Width + widthInc), (int)(TableRowHeight + heightInc));
		        var pos = i + 1;
		        DrawTableRow(graphics, _poule.Stand[i], rect, GetBackgroundColor(pos), pos, format);
	        }
        }
        else
        {
	        for (int i = 0; i < _poule.Clubs.Count; i++)
	        {
		        var height = tableRect.Y + (i + 1) * TableRowHeight;
		        var rect = new Rectangle(tableRect.X, height, tableRect.Width, TableRowHeight);
		        var pos = i + 1;
		        DrawTableRow(graphics, _poule.Stand[i], rect, GetBackgroundColor(pos), pos, format);
	        }
        }
  	}

	private void DrawTableRow(Graphics graphics, StandRow row, Rectangle rect, Color color, int pos, StringFormat format)
	{
		double baseFontSize = rect.Height / 2;
		var textHeight = rect.Y + rect.Height * .15;

		var addValueYAdjust = baseFontSize / 2 * (_addValueSizeFactor - 1);
		var pointsYAdjust = baseFontSize / 2 * (_pointsSizeFactor - 1);

		var font = new Font("Arial", (float)baseFontSize, FontStyle.Regular);
		var addPtsFont = new Font("Arial", (float)(baseFontSize * _addValueSizeFactor), FontStyle.Regular);

		var fontSize = baseFontSize;
		if (row.PointsAdded)
			fontSize *= _pointsSizeFactor;
		
		var adjustedFont = new Font("Arial", (float)fontSize, FontStyle.Regular);
		var brush = new SolidBrush(Color.Black);
		var pen = new Pen(Color.Black, 1);

		var heightInc = (int)(rect.Height * (_clubRowSize[row.Club.Name]));
		var startHeight = rect.Y;
		const double ptsPercentage = StringWidth + NumberWidth;
		const double gdPercentage = StringWidth + NumberWidth * 6;

		//TODO: Create a table class to do all the drawing for the table
		graphics.DrawRectangle(pen, rect.X, startHeight, rect.Width, heightInc);
		graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, color)), new Rectangle(rect.X, startHeight, rect.Width, heightInc));
        graphics.DrawString(pos.ToString(), font, brush, new Rectangle(rect.X, startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Club.Name, font, brush, new Rectangle(rect.X + (int)(NumberWidth * rect.Width), startHeight, (int)(StringWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GetPoints().ToString(), adjustedFont, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth) * rect.Width), startHeight - (int)(row.PointsAdded ? pointsYAdjust : 0), (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GetPlayed().ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 2) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Won.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 3) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Drawn.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 4) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Lost.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 5) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GetGoalDiff().ToString(), adjustedFont, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 6) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GoalsFor.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 7) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GoalsAgainst.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 8) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Club.GetRating().ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 9) * rect.Width), startHeight, (int)(NumberWidth * 1.5 * rect.Width), heightInc), format);

        if (row.PointsToAdd != null)
        {
	        graphics.DrawString(row.PointsToAdd >= 0 ? $"+{row.PointsToAdd}" : $"{row.PointsToAdd}", addPtsFont, brush, rect.X + (int)((ptsPercentage + _addValueOffset * NumberWidth) * rect.Width), (float)(textHeight - addValueYAdjust));
	        var goalDiff = row.GoalsForToAdd - row.GoalsAgainstToAdd;
	        graphics.DrawString(goalDiff >= 0 ? $"+{goalDiff}" : $"{goalDiff}", addPtsFont, brush, rect.X + (int)((gdPercentage + _addValueOffset * NumberWidth) * rect.Width), (float)(textHeight - addValueYAdjust));
        }
	}

	private void AddPoints()
	{
		var incSizeMs = 1000;
		var joinMs = 300;
		var increasePtsSizeMs = 50;
		var decreasePtsSizeMs = 150;
		var totalTime = incSizeMs + joinMs + increasePtsSizeMs + decreasePtsSizeMs;

		var incSizeFactor = 0.5;
		var addValueBaseOffset = 0.5;

		var startTime = DateTime.Now;

		double deltaTime = 0;

		var valuesAdded = false;

		while (deltaTime <= totalTime)
		{
			Refresh();
			deltaTime = (DateTime.Now - startTime).TotalMilliseconds;
			if(deltaTime <= incSizeMs)
			{
				_addValueSizeFactor = 1 + incSizeFactor * deltaTime / incSizeMs;
				_addValueOffset = addValueBaseOffset;
			}
			else if (deltaTime <= incSizeMs + joinMs)
			{
				var factor = (joinMs - (deltaTime - incSizeMs)) / joinMs;
				_addValueSizeFactor = (1 + incSizeFactor) * Math.Max(factor, 0.001);
				_addValueOffset = addValueBaseOffset * factor;
			}
			else if (deltaTime <= incSizeMs + joinMs + increasePtsSizeMs)
			{
				if (!valuesAdded)
				{
					foreach (var stand in _poule.Stand.Where(c => c.PointsToAdd != null))
					{
						stand.AddValues();
					}
					valuesAdded = true;
				}

				_pointsSizeFactor = 1 + incSizeFactor * (deltaTime - incSizeMs - joinMs) / increasePtsSizeMs;
			}
			else
			{
				var factor = (decreasePtsSizeMs - (deltaTime - incSizeMs - joinMs - increasePtsSizeMs)) / decreasePtsSizeMs;
				_pointsSizeFactor = 1 + incSizeFactor * Math.Max(factor, 0.001);
			}
		}

		ResetSizing();
		Refresh();
	}

	private void ResetSizing()
	{
		_pointsSizeFactor = 1;
		_addValueSizeFactor = 1;
		_addValueOffset = 0.5;
		_poule.Stand.ForEach(s => s.ResetSizing());
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
		AnimatePointsGained();
		Refresh();
	}

	private void BtnSimulateAll_Click(object sender, EventArgs e)
	{
		_poule.SimulateAllMatches();
		AnimatePointsGained();
		Refresh();
	}

	private void AnimatePointsGained()
	{
		AddPoints();
		OrderStand();
	}
	
	private void OrderStand()
	{
		var newStand = _poule.GetOrderedStand();

		var clubMoveY = new Dictionary<string, int>();

		for (int i = 0; i < newStand.Count; i++)
		{
			var oldIndex = _lastDrawnOrder[newStand[i].Club.Name];
			clubMoveY.Add(newStand[i].Club.Name, i - oldIndex);
		}

		if (clubMoveY.Values.Any(v => v != 0)) // Needs Order
		{
			Task.Delay(500);

			_clubRowSize = new Dictionary<string, double>();
			_clubRowOffset = new Dictionary<string, int>();

			_isOrdering = true;
			var changeSizeMs = 250;
			var orderTableMs = 500;
			var totalTime = 2 * changeSizeMs + orderTableMs;
				
			var changeSizeFactor = 0.1;
				
			var startTime = DateTime.Now;

			double deltaTime = 0;
				
			while (deltaTime <= totalTime)
			{
				deltaTime = (DateTime.Now - startTime).TotalMilliseconds;
				if(deltaTime <= changeSizeMs)
				{
					foreach (var stand in newStand)
					{
						if(clubMoveY[stand.Club.Name] < 0)
							_clubRowSize[stand.Club.Name] = 1 + changeSizeFactor * deltaTime / changeSizeMs;
						else if(clubMoveY[stand.Club.Name] > 0)
							_clubRowSize[stand.Club.Name] = 1 - changeSizeFactor * deltaTime / changeSizeMs;
						else
							_clubRowSize[stand.Club.Name] = 1;
						_clubRowOffset[stand.Club.Name] = clubMoveY[stand.Club.Name] * TableRowHeight;
					}
				}
				else if (deltaTime <= changeSizeMs + orderTableMs)
				{
					foreach (var stand in newStand)
					{
						if(clubMoveY[stand.Club.Name] < 0)
							_clubRowSize[stand.Club.Name] = 1 + changeSizeFactor;
						else if(clubMoveY[stand.Club.Name] > 0)
							_clubRowSize[stand.Club.Name] = 1 - changeSizeFactor;
						else
							_clubRowSize[stand.Club.Name] = 1;
						var percentageToMove = 1 - (deltaTime - changeSizeMs) / orderTableMs;
						_clubRowOffset[stand.Club.Name] = (int)(clubMoveY[stand.Club.Name] * TableRowHeight * percentageToMove);
					}					
				}
				else
				{
					foreach (var stand in newStand)
					{
						var percentageSizeLeft = 1 - (deltaTime - changeSizeMs - orderTableMs) / changeSizeMs;
						if(clubMoveY[stand.Club.Name] < 0)
							_clubRowSize[stand.Club.Name] = 1 + changeSizeFactor * percentageSizeLeft;
						else if(clubMoveY[stand.Club.Name] > 0)
							_clubRowSize[stand.Club.Name] = 1 - changeSizeFactor * percentageSizeLeft;
						else
							_clubRowSize[stand.Club.Name] = 1;
						_clubRowOffset[stand.Club.Name] = 0;
					}
				}
					
				Refresh();
			}
			_isOrdering = false;
				
			Refresh();
			_lastDrawnOrder = newStand.ToDictionary(s => s.Club.Name, s => newStand.IndexOf(s));
		}

		foreach (var club in _poule.Clubs)
		{
			_clubRowOffset[club.Name] = 0;
			_clubRowSize[club.Name] = 1;
		}
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