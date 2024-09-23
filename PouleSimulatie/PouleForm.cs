using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;

namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly ITableAnimatorService _tableAnimator;
	private readonly IRenderer<DataTable> _tableRenderer;
	private readonly Poule _poule;
	private int _currentRound = 1;
	
	public PouleForm(IReadOnlyList<Club> clubs, bool returns, int teamsAdvancing, IRenderer<DataTable> tableRenderer, ITableAnimatorService tableAnimator)
	{
		_tableAnimator = tableAnimator;
		_tableRenderer = tableRenderer;
		
		DoubleBuffered = true;
		_poule = new Poule(clubs, returns, teamsAdvancing, new Random());
		InitializeComponent();
		Init();
		LblTeamsAdvancing.Text = $"{teamsAdvancing} {(teamsAdvancing == 1 ? "team gaat" : "teams gaan")} door naar de volgende ronde.";
	}

	/// <summary>
	/// Initialize the poule form
	/// </summary>
	private void Init()
	{
		_poule.Init();
		LblPlayround.Text = $"1/{_poule.TotalRondes}";
		Refresh();
	}
	
	/// <summary>
	/// Change the round to show of the poule
	/// </summary>
	/// <param name="round">The round to show</param>
	private void ChangeRound(int round)
	{
		_currentRound = round;
		LblPlayround.Text = $"{_currentRound}/{_poule.TotalRondes}";
		BtnPrevious.Enabled = _currentRound > 1;
		BtnNext.Enabled = _currentRound < _poule.TotalRondes;
		Refresh();
	}
	
	#region EventHandlers

	/// <summary>
	/// Paint event handler
	/// </summary>
	private void OnPaintHandler(object? sender, PaintEventArgs e)
	{
		var graphics = e.Graphics;

		var matchTable = new MatchTable();
		var startHeight = BtnPrevious.Location.Y + BtnPrevious.Size.Height + 10;
		_poule.FillMatchTable(ref matchTable, _currentRound);
		_tableRenderer.Draw(graphics, new Rectangle(12, startHeight, 356, Size.Height - 170), matchTable);
		var standTable = new StandTable();
		_poule.FillStandTable(ref standTable);
		_tableAnimator.Draw(graphics, new Rectangle(380, startHeight, Size.Width - 410, Size.Height - 170), standTable);
	}
	
	/// <summary>
	/// Refresh the screen after resize event
	/// </summary>
	private void SizeChangedHandler(object? sender, EventArgs e)
	{
		BtnExit.Location = new Point(Size.Width - 162, 12);
		Refresh();
	}

	/// <summary>
	/// Event handler for the play one match button
	/// </summary>
	private void BtnPlayOne_Click(object sender, EventArgs e)
	{
		var nextRound = _poule.GetNextMatchRound();
		if (nextRound == null)
		{
			MessageBox.Show("Alle wedstrijden zijn al gespeeld");
			return;
		}

		ChangeRound(nextRound.Value);
		_poule.SimulateNextMatch();
		var standTable = new StandTable();
		_poule.FillStandTable(ref standTable);
		_tableAnimator.OrderTable(standTable, Refresh);
		Refresh();

		if (_poule.GetNextMatchRound() == null)
			ShowAdvancingTeams();
	}

	/// <summary>
	/// Event handler for the simulate all matches button
	/// </summary>
	private void BtnSimulateAll_Click(object sender, EventArgs e)
	{
		_poule.SimulateAllMatches();
		var standTable = new StandTable();
		_poule.FillStandTable(ref standTable);
		_tableAnimator.OrderTable(standTable, Refresh);
		Refresh();
		ShowAdvancingTeams();
	}

	/// <summary>
	/// Show a message box with the advancing teams
	/// </summary>
	private void ShowAdvancingTeams()
	{
		MessageBox.Show("Alle wedstrijden zijn gespeeld! De teams die door gaan naar de volgende ronde zijn: " + string.Join(", ", _poule.GetAdvancingTeams().Select(c => c.Name)));
	}
	
	/// <summary>
	/// Eventhandler for the Previous round button
	/// </summary>
	private void BtnPrevious_Click(object sender, EventArgs e)
	{
		if(_currentRound > 1)
			ChangeRound(_currentRound - 1);
		
		Refresh();
	}

	/// <summary>
	/// Eventhandler for the Next round button
	/// </summary>
	private void BtnNext_Click(object sender, EventArgs e)
	{
		if(_currentRound < _poule.TotalRondes)
			ChangeRound(_currentRound + 1);
		
		Refresh();
	}

	/// <summary>
	/// Eventhandler for the Exit button
	/// </summary>
	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}
	
	#endregion
}