﻿using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;
using PouleSimulatie.Services;

namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly Poule _poule;
	private int _currentRound = 1;
	private readonly IAnimationService _animationService;
	private IRenderer<DataTable> _tableRenderer;
	public PouleForm(IReadOnlyList<Club> clubs, bool returns, int teamsAdvancing, IRenderer<DataTable> tableRenderer)
	{
		_tableRenderer = tableRenderer;
		DoubleBuffered = true;
		_poule = new Poule(clubs, returns, teamsAdvancing, new Random());
		_animationService = new AnimationService(this, _poule);
		InitializeComponent();
		Init();
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
		_poule.FillMatchTable(ref matchTable, _currentRound);
		_animationService.DrawPlayRound(graphics, _tableRenderer, new Rectangle(12, 110, 356, Size.Height - 170), matchTable);
		var standTable = new StandTable();
		_poule.FillStandTable(ref standTable);
		_animationService.DrawStand(graphics, _tableRenderer, new Rectangle(380, 110, Size.Width - 410, Size.Height - 170), standTable);
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
		//_animationService.AnimatePointsGained();
		Refresh();
	}

	/// <summary>
	/// Event handler for the simulate all matches button
	/// </summary>
	private void BtnSimulateAll_Click(object sender, EventArgs e)
	{
		_poule.SimulateAllMatches();
		//_animationService.AnimatePointsGained();
		Refresh();
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