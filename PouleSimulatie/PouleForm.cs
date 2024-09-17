namespace PouleSimulatie;

public partial class PouleForm : Form
{
	private readonly Poule _poule;

	public PouleForm(List<Club> clubs)
	{
		_poule = new Poule(clubs, false);
		_poule.Init();
		InitializeComponent();
	}

	private void PouleForm_Load(object sender, EventArgs e)
	{

	}
}