namespace PouleSimulatie;

public class MassSimulationResult
{
    private List<ClubResult> ClubResults { get; set; }

    public MassSimulationResult(List<Club> clubs)
    {
        ClubResults = clubs.Select(c => new ClubResult(c, clubs.Count)).ToList();
    }

    public void AddResults(Poule poule)
    {
        var orderedStand = poule.GetOrderedStand();
        for(int i = 0; i < orderedStand.Count; i++)
            ClubResults.First(c => c.ClubName == orderedStand[i].Club.Name).AddResult(i + 1, orderedStand[i].GetPoints());
    }

    public string GetResults(int simulations)
    {
        return string.Join("\n", ClubResults.Select(c => c.GetResult(simulations)));
    }
}