namespace PouleSimulatie;

public class StandRow
{
    public Club Club { get; }
    public int Wins { get; private set; }
    public int Draws { get; private set; }
    public int Losses { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }

    public StandRow(Club club)
    {
        Club = club;
    }

    /// <summary>
    /// Add the results of a match to the club without an animation by directly assigning the results
    /// </summary>
    /// <param name="goalsFor">Goals scored in the match</param>
    /// <param name="goalsAgainst">Goalc received in the match</param>
    public void MatchPlayed(int goalsFor, int goalsAgainst)
    {
        GoalsFor += goalsFor;
        GoalsAgainst += goalsAgainst;
        
        if(goalsFor > goalsAgainst)
            Wins++;
        else if(goalsFor < goalsAgainst)
            Losses++;
        else
            Draws++;
    }
    
    /// <summary>
    /// Get the amount of matches played
    /// </summary>
    /// <returns>Amount of matches</returns>
    public int GetPlayed()
    {
        return Wins + Draws + Losses;
    }

    /// <summary>
    /// Get the amount of points
    /// </summary>
    /// <returns>Amount of points</returns>
    public int GetPoints()
    {
        return Wins * 3 + Draws;
    }

    /// <summary>
    /// Get the goal difference
    /// </summary>
    /// <returns>The goal difference</returns>
    public int GetGoalDiff()
    {
        return GoalsFor - GoalsAgainst;
    }
}