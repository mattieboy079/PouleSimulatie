namespace PouleSimulatie;

public class MassSimulationService
{
    private readonly IReadOnlyList<Club> _clubs;
    private readonly bool _returns;
    private int _simulationsFinished;

    public MassSimulationService(IReadOnlyList<Club> clubs, bool returns)
    {
        _clubs = clubs;
        _returns = returns;
    }

    public async Task<MassSimulationResult?> Simulate(int simulations)
    {
        _simulationsFinished = 0;
        
        if (_clubs.Count < 2)
        {
            MessageBox.Show("Voeg minimaal 2 teams toe.");
            return null;
        }
        
        var random = new Random();
        
        var simulationResult = new MassSimulationResult(_clubs);

        var startTime = DateTime.Now;
        var tasks = new List<Task>();
        for (int p = 0; p < simulations; p++)
        {
            tasks.Add(Task.Run(() =>
            {
                Poule poule = new(_clubs, _returns, random);
                poule.Init();
                poule.SimulateAllMatches();
                simulationResult.AddResults(poule);
                _simulationsFinished++;
            }));
        }
		
        await Task.WhenAll(tasks);

        var timeTaken = Math.Round((DateTime.Now - startTime).TotalSeconds, 3);

        simulationResult.SetTime(timeTaken);

        return simulationResult;
    }

    public int GetProgress()
    {
        return _simulationsFinished;
    }
}