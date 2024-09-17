namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly Poule _poule;

	public PouleForm(List<Club> clubs)
	{
		DoubleBuffered = true;
		_poule = new Poule(clubs, false);
		Init();
		InitializeComponent();
	}

	private void Init()
	{
		_poule.Init();
		label1.Text = $"1/{_poule.TotalRondes}"; 
		Draw(1);
	}

	private void Draw(int round)
	{
		DrawStand();
		DrawPlayRound(round);
	}

	private void DrawPlayRound(int round)
	{
		throw new NotImplementedException();
	}

	private void DrawStand()
	{
		throw new NotImplementedException();
	}
}