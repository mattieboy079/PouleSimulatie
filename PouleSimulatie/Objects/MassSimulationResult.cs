namespace PouleSimulatie.Objects;

public class MassSimulationResult
{
    private double _timeTaken;
    private int _simulations;
    private readonly List<ClubResult> _clubResults;

    public MassSimulationResult(IReadOnlyList<Club> clubs)
    {
        _clubResults = clubs.Select(c => new ClubResult(c, clubs.Count)).ToList();
    }

    /// <summary>
    /// Add the results of a poule to the simulation results
    /// </summary>
    /// <param name="poule">The simulated poule</param>
    public void AddResults(Poule poule)
    {
        var orderedStand = poule.GetOrderedStand();
        for(int i = 0; i < orderedStand.Count; i++)
            _clubResults.First(c => c.ClubName == orderedStand[i].Club.Name).AddResult(i + 1, orderedStand[i].GetPoints());
    }

    /// <summary>
    /// Set the time taken and the amount of simulations finished
    /// </summary>
    /// <param name="timeTaken">The time the simulation took</param>
    /// <param name="simulationsFinished">The amount of simulations finished</param>
    public void SetTimeAndSimulationAmount(double timeTaken, int simulationsFinished)
    {
        _timeTaken = timeTaken;
        _simulations = simulationsFinished;
    }

    /// <summary>
    /// Get the amount of clubs in the simulations
    /// </summary>
    /// <returns>The amount of clubs in the simulations</returns>
    public int GetClubAmount()
    {
        return _clubResults.Count;
    }
    
    /// <summary>
    /// Get the time taken for the simulation
    /// </summary>
    /// <returns>The time in seconds</returns>
    public double GetTimeTaken()
    {
        return _timeTaken;
    }
    
    /// <summary>
    /// Fill a Datatable with the simulation results
    /// </summary>
    /// <param name="dataTable">The table to fill with the results</param>
    public void FillMassSimulationTable(ref MassSimulationResultTable dataTable)
    {
        var orderedResult = _clubResults.OrderByDescending(c => c.TotalPoints).ToList();
        foreach (var result in orderedResult)
        {
            var row = new DataRow();
            dataTable.Rows.Add(row);
            result.FillRow(dataTable, row);
        }
    }
}