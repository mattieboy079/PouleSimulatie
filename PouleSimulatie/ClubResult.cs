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
	
    public void AddResult(int position, int points)
    {
        _results[position]++;
        _totalPoints += points;
    }

    public string GetResult(int simulations)
    {
        return $"{ClubName} - {string.Join(", ", _results.Select(r => $"{r.Key}: {r.Value}"))} - {Math.Round((double)_totalPoints / simulations, 2)} pts avg";
    }
}