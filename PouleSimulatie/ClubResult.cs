namespace PouleSimulatie;

public class ClubResult
{
    public readonly string ClubName;
    private readonly Dictionary<int, int> _results;
    private int _totalPoints;
	
    public ClubResult(Club club, int clubsCount)
    {
        ClubName = club.Name;
        _results = new();
        for (int i = 1; i <= clubsCount; i++)
        {
            _results.Add(i, 0);
        }
    }
	
    /// <summary>
    /// Add the results of a club to the simulation results
    /// </summary>
    /// <param name="position">The position the club ended</param>
    /// <param name="points">The amount of points gained during the poule</param>
    public void AddResult(int position, int points)
    {
        _results[position]++;
        _totalPoints += points;
    }

    /// <summary>
    /// Get the result of a club
    /// </summary>
    /// <param name="simulations">amount of simulations ran</param>
    /// <returns>The clubresult</returns>
    public string GetResult(int simulations)
    {
        return $"{ClubName} - {string.Join(", ", _results.Select(r => $"{r.Key}: {r.Value}"))} - {Math.Round((double)_totalPoints / simulations, 2)} pts avg";
    }
}