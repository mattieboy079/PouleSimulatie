using System.Globalization;

namespace PouleSimulatie.Objects;

public class ClubResult
{
    public readonly string ClubName;
    private readonly int _rating;
    private readonly Dictionary<int, int> _results;
    public int TotalPoints { get; private set; }
	
    public ClubResult(Club club, int clubsCount)
    {
        ClubName = club.Name;
        _rating = club.GetRating();
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
        TotalPoints += points;
    }

    private double GetAveragePoints()
    {
        return Math.Round((double)TotalPoints / _results.Values.Sum(v => v), 2);
    }

    public void FillRow(MassSimulationResultTable dataTable, DataRow row)
    {
        dataTable.AddValue(row, "Club", ClubName);
        dataTable.AddValue(row, "Rating", _rating.ToString());
        dataTable.AddValue(row, "Average Points", GetAveragePoints().ToString(CultureInfo.CurrentCulture));
        foreach (var kvp in _results)
        {
            dataTable.AddValue(row, $"Placed {kvp.Key}", kvp.Value.ToString());
        }
    }
}