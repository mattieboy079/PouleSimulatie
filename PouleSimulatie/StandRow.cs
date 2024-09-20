namespace PouleSimulatie;

public class StandRow
{
    public Club Club { get; }
    public int Won { get; private set; }
    public int Drawn { get; private set; }
    public int Lost { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }
    
    // Properties for animation
    public int? PointsToAdd { get; private set; }
    public int? GoalsForToAdd { get; private set; }
    public int? GoalsAgainstToAdd { get; private set; }
    public bool PointsAdded { get; private set; }

    public StandRow(Club club)
    {
        Club = club;
    }

    /// <summary>
    /// Add the results of a match to the club
    /// </summary>
    /// <param name="goalsFor">Goals scored in the match</param>
    /// <param name="goalsAgainst">Goalc received in the match</param>
    public void MatchPlayed(int goalsFor, int goalsAgainst)
    {
        if(PointsToAdd != null)
            AddValues();
        
        GoalsForToAdd = goalsFor;
        GoalsAgainstToAdd = goalsAgainst;

        if (goalsFor > goalsAgainst)
            PointsToAdd = 3;
        else if (goalsFor < goalsAgainst)
            PointsToAdd = 0;
        else
            PointsToAdd = 1;
    }
    
    /// <summary>
    /// Get the amount of matches played
    /// </summary>
    /// <returns>Amount of matches</returns>
    public int GetPlayed()
    {
        return Won + Drawn + Lost;
    }

    /// <summary>
    /// Get the amount of points
    /// </summary>
    /// <returns>Amount of points</returns>
    public int GetPoints()
    {
        return Won * 3 + Drawn;
    }

    /// <summary>
    /// Get the goal difference
    /// </summary>
    /// <returns>The goal difference</returns>
    public int GetGoalDiff()
    {
        return GoalsFor - GoalsAgainst;
    }
    
    /// <summary>
    /// Add the values to add to the real values
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Any number other than 3, 1 or 0 since this cannot determine a win loss or draw</exception>
    public void AddValues()
    {
        if (PointsToAdd == null)
            return;
        
        var pts = PointsToAdd.Value;
        switch (pts)
        {
            case 3:
                Won++;
                break;
            case 1:
                Drawn++;
                break;
            case 0:
                Lost++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        GoalsFor += GoalsForToAdd!.Value;
        GoalsAgainst += GoalsAgainstToAdd!.Value;
        PointsToAdd = null;
        GoalsForToAdd = null;
        GoalsAgainstToAdd = null;
        PointsAdded = true;
    }

    /// <summary>
    /// Reset the values to default after animation
    /// </summary>
    public void ResetSizing()
    {
        PointsAdded = false;
    }
}