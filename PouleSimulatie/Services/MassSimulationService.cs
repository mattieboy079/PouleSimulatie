using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;

namespace PouleSimulatie.Services;

public class MassSimulationService : ISimulationService
{
    private int _simulationsFinished;
    
    public async Task<MassSimulationResult> Simulate(int simulations, IReadOnlyList<Club> clubs, bool returns, int teamsAdvancing)
    {
        _simulationsFinished = 0;
        
        if (clubs.Count < 2)
        {
            throw new ArgumentException("Voeg minimaal 2 teams toe.");
        }
        
        var random = new Random();
        
        var simulationResult = new MassSimulationResult(clubs);

        var startTime = DateTime.Now;
        var tasks = new List<Task>();
        for (int p = 0; p < simulations; p++)
        {
            tasks.Add(Task.Run(() =>
            {
                Poule poule = new(clubs, returns, teamsAdvancing, random);
                poule.Init();
                poule.SimulateAllMatches();
                simulationResult.AddResults(poule);
                _simulationsFinished++;
            }));
        }
		
        await Task.WhenAll(tasks);

        var timeTaken = Math.Round((DateTime.Now - startTime).TotalSeconds, 3);

        simulationResult.SetTimeAndSimulationAmount(timeTaken, _simulationsFinished);

        return simulationResult;
    }

    public int GetProgress()
    {
        return _simulationsFinished;
    }
}