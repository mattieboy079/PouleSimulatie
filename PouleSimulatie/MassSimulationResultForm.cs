using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;

namespace PouleSimulatie;

public partial class MassSimulationResultForm : Form
{
	private readonly MassSimulationResult _result;
	private readonly IRenderer<DataTable> _tableRenderer;

	public MassSimulationResultForm(MassSimulationResult result, IRenderer<DataTable> tableRenderer)
	{
		_result = result;
		_tableRenderer = tableRenderer;
		InitializeComponent();
	}

	private void BtnSluiten_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void MassSimulationResultForm_Paint(object? sender, PaintEventArgs e)
	{
		var table = new MassSimulationResultTable(_result.GetClubAmount());
		_result.FillMassSimulationTable(ref table);
		_tableRenderer.Draw(e.Graphics, new Rectangle(12, 50, Size.Width - 50, Size.Height - 110), table);
	}
}