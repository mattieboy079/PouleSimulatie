using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;

namespace PouleSimulatie.Services;

public class AnimationService : IAnimationService
{
	private readonly Form _form;
	
	private const double NumberWidth = 0.07;
	private const double StringWidth = 0.265;
	private bool _isOrdering;
	private double _addValueOffset;
	private double _addValueSizeFactor = 1;
	private double _pointsSizeFactor = 1;
	private readonly Dictionary<string, int> _clubRowOffset;
	private readonly Dictionary<string, double> _clubRowSize;
	private Dictionary<string, int> _lastDrawnOrder;
	private int _rowHeight = 30;
	private Poule _poule { get; }
	
	public AnimationService(Form form, Poule poule)
	{
		_form = form;
		_poule = poule;
		_clubRowOffset = new Dictionary<string, int>();
		_clubRowSize = new Dictionary<string, double>();
		_lastDrawnOrder = new Dictionary<string, int>();
		for (var i = 0; i < _poule.Clubs.Count; i++)
		{
			_clubRowSize.Add(_poule.Clubs[i].Name, 1);
			_clubRowOffset.Add(_poule.Clubs[i].Name, 0);
			_lastDrawnOrder.Add(_poule.Clubs[i].Name, i);
		}
	}
	
	/// <summary>
	/// Animate the points gained by the clubs
	/// </summary>
	public void AnimatePointsGained()
	{
		AddPoints();
		OrderStand();
	}
	
	public void DrawPlayRound(Graphics graphics, IRenderer<DataTable> renderer, Rectangle rect, DataTable matches)
	{
		renderer.Draw(graphics, rect, matches);
	}
	
	public void DrawStand(Graphics graphics, IRenderer<DataTable> renderer, Rectangle rect, DataTable stand)
	{
		renderer.Draw(graphics, rect, stand);
		
		//Todo: Reimplement Animation
		// _rowHeight = Math.Min(30, rect.Height / (_poule.Clubs.Count + 1));
		//
		// double baseFontSize = Math.Max(1, _rowHeight / 2);
		// var font = new Font("Arial", (float)baseFontSize, FontStyle.Regular);
		// var brush = new SolidBrush(Color.Black);
		// var pen = new Pen(Color.Black, 1);
		//
		// var format = new StringFormat
		// {
		// 	LineAlignment = StringAlignment.Center
		// };
		//
		// var headerRect = rect with { Height = _rowHeight };
		//
		// graphics.DrawRectangle(pen, headerRect);
  //       graphics.DrawString("#", font, brush, headerRect with { Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Club", font, brush, headerRect with { X = headerRect.X + (int)(NumberWidth * headerRect.Width), Width = (int)(StringWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Points", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Played", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 2) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Win", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 3) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Draw", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 4) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Loss", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 5) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("+/-", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 6) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("+", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 7) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("-", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 8) * headerRect.Width), Width = (int)(NumberWidth * headerRect.Width) }, format);
  //       graphics.DrawString("Rating", font, brush, headerRect with { X = headerRect.X + (int)((StringWidth + NumberWidth * 9) * headerRect.Width), Width = (int)(NumberWidth * 1.5 * headerRect.Width) }, format);
  //       
        // if (_isOrdering)
        // {
	       //  for (int i = 0; i < _poule.Stand.Count; i++)
	       //  {
		      //   var height = rect.Y + (i + 1) * _rowHeight - _clubRowOffset[_poule.Stand[i].Club.Name];
		      //   var widthInc = rect.Width * (_clubRowSize[_poule.Stand[i].Club.Name] - 1);
		      //   var heightInc = _rowHeight * (_clubRowSize[_poule.Stand[i].Club.Name] - 1);
		      //   var rowRect = new Rectangle((int)(rect.X - widthInc / 2), (int)(height - heightInc / 2), (int)(rect.Width + widthInc), (int)(_rowHeight + heightInc));
		      //   var pos = i + 1;
		      //   DrawTableRow(graphics, _poule.Stand[i], rowRect, GetBackgroundColor(pos), pos, format);
	       //  }
        // }
        // else
        // {
	       //  for (int i = 0; i < _poule.Clubs.Count; i++)
	       //  {
		      //   var height = rect.Y + (i + 1) * _rowHeight;
		      //   var rowRect = rect with { Y = height, Height = _rowHeight };
		      //   var pos = i + 1;
		      //   DrawTableRow(graphics, _poule.Stand[i], rowRect, GetBackgroundColor(pos), pos, format);
	       //  }
        // }
  	}
	
	/// <summary>
	/// Animate the points to added to the stand
	/// </summary>
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
			_form.Refresh();
			deltaTime = (DateTime.Now - startTime).TotalMilliseconds;
			if (deltaTime <= incSizeMs)
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
		_form.Refresh();
	}
	
	/// <summary>
	/// Animate the ordering of the standtable
	/// </summary>
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
						_clubRowOffset[stand.Club.Name] = clubMoveY[stand.Club.Name] * _rowHeight;
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
						_clubRowOffset[stand.Club.Name] = (int)(clubMoveY[stand.Club.Name] * _rowHeight * percentageToMove);
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
					
				_form.Refresh();
			}
			_isOrdering = false;
				
			_form.Refresh();
			_lastDrawnOrder = newStand.ToDictionary(s => s.Club.Name, s => newStand.IndexOf(s));
		}

		foreach (var club in _poule.Clubs)
		{
			_clubRowOffset[club.Name] = 0;
			_clubRowSize[club.Name] = 1;
		}
	}

	/// <summary>
	/// Reset all sizing to default values after animation
	/// </summary>
	private void ResetSizing()
	{
		_pointsSizeFactor = 1;
		_addValueSizeFactor = 1;
		_addValueOffset = 0.5;
		_poule.Stand.ForEach(s => s.ResetSizing());
	}	
	
	/// <summary>
	/// Draw a row in the table
	/// </summary>
	/// <param name="graphics">The graphics to draw with</param>
	/// <param name="row">The rowInformation</param>
	/// <param name="rect">The rectangle defined for the row</param>
	/// <param name="color">The background color of the row</param>
	/// <param name="pos">The position of the row</param>
	/// <param name="format">The stringformat for drawing strings</param>
	private void DrawTableRow(Graphics graphics, StandRow row, Rectangle rect, Color color, int pos, StringFormat format)
	{
		double baseFontSize = Math.Max(1, rect.Height / 2);
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

		graphics.DrawRectangle(pen, rect.X, startHeight, rect.Width, heightInc);
		graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, color)), rect with { Y = startHeight, Height = heightInc });
        graphics.DrawString(pos.ToString(), font, brush, new Rectangle(rect.X, startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Club.Name, font, brush, new Rectangle(rect.X + (int)(NumberWidth * rect.Width), startHeight, (int)(StringWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GetPoints().ToString(), adjustedFont, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth) * rect.Width), startHeight - (int)(row.PointsAdded ? pointsYAdjust : 0), (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.GetPlayed().ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 2) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Wins.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 3) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Draws.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 4) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
        graphics.DrawString(row.Losses.ToString(), font, brush, new Rectangle(rect.X + (int)((StringWidth + NumberWidth * 5) * rect.Width), startHeight, (int)(NumberWidth * rect.Width), heightInc), format);
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

	/// <summary>
	/// Get the background color for a row in the stand
	/// </summary>
	/// <param name="pos">The position of the row</param>
	/// <returns>The color to use as background</returns>
	private Color GetBackgroundColor(int pos)
	{
		return pos <= _poule.TeamsAdvancing 
			? Color.LimeGreen 
			: Color.White;
	}
	
	/// <summary>
	/// Draw a match on the screen
	/// </summary>
	/// <param name="graphics">Graphics to draw with</param>
	/// <param name="index">Amount of matches drawn above</param>
	/// <param name="matchString">The clubs battling in this match</param>
	/// <param name="scoreString">The score result of the match</param>
	private void DrawMatch(Graphics graphics, int index, string matchString, string scoreString)
	{
		var startHeight = 110;
		
		var font = new Font("Arial", 16);
		var brush = new SolidBrush(Color.Black);
		
		graphics.DrawString(matchString, font, brush, 12, startHeight + _rowHeight * index);
		graphics.DrawString(scoreString, font, brush, 300, startHeight + _rowHeight * index);
	}
}