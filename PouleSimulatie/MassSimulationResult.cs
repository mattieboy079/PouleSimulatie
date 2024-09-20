namespace PouleSimulatie;

public class MassSimulationResult
{
    private List<ClubResult> ClubResults { get; set; }

    public MassSimulationResult(IReadOnlyList<Club> clubs)
    {
        ClubResults = clubs.Select(c => new ClubResult(c, clubs.Count)).ToList();
    }

    /// <summary>
    /// Add the results of a poule to the simulation results
    /// </summary>
    /// <param name="poule">The simulated poule</param>
    public void AddResults(Poule poule)
    {
        var orderedStand = poule.GetOrderedStand();
        for(int i = 0; i < orderedStand.Count; i++)
            ClubResults.First(c => c.ClubName == orderedStand[i].Club.Name).AddResult(i + 1, orderedStand[i].GetPoints());
    }

    /// <summary>
    /// Get the result of the simulations
    /// </summary>
    /// <param name="simulations">amount of simulations ran</param>
    /// <returns>The result string to show</returns>
    public string GetResults(int simulations)
    {
        return string.Join("\n", ClubResults.Select(c => c.GetResult(simulations)));
    }
}