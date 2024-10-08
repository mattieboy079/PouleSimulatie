using PouleSimulatie.Interfaces;
using PouleSimulatie.Services;

namespace PouleSimulatie;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		IClubService clubService = new ClubService();
		ISimulationService simulationService = new MassSimulationService();
		Application.Run(new MainForm(clubService, simulationService));
	}
}