namespace PouleSimulatie;

public class StandRow
{
    public Club Club { get; }
    public int Wins { get; private set; }
    public int Draws { get; private set; }
    public int Losses { get; private set; }
    public int GoalsFor { get; private set; }
    public int GoalsAgainst { get; private set; }
    
    /// <summary>
    /// Used in animation to add the points to the total points
    /// And determine if the club gets a win, draw or loss
    /// </summary>
    public int? PointsToAdd { get; private set; }
    
    /// <summary>
    /// Used in animation to create the goal difference animation and add the goals to the total goalsFor
    /// </summary>
    public int? GoalsForToAdd { get; private set; }
    
    /// <summary>
    /// Used in animation to create the goal difference animation and add the goals to the total goalsAgainst
    /// </summary>
    public int? GoalsAgainstToAdd { get; private set; }
    
    /// <summary>
    /// Used to create the bouncing effect when the new results are added
    /// </summary>
    public bool PointsAdded { get; private set; }

    public StandRow(Club club)
    {
        Club = club;
    }

    /// <summary>
    /// Add the results of a match to the club with an animation using the ToAdd parameters
    /// </summary>
    /// <param name="goalsFor">Goals scored in the match</param>
    /// <param name="goalsAgainst">Goalc received in the match</param>
    public void MatchPlayedAnimated(int goalsFor, int goalsAgainst)
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
    
    /// <summary>
    /// Add the values to add to the real values
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">If pointsToAdd has any number other than 3, 1 or 0 since this cannot determine a win loss or draw</exception>
    public void AddValues()
    {
        if (PointsToAdd == null)
            return;
        
        var pts = PointsToAdd.Value;
        switch (pts)
        {
            case 3:
                Wins++;
                break;
            case 1:
                Draws++;
                break;
            case 0:
                Losses++;
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
    /// Reset the animation values to default after animation
    /// </summary>
    public void ResetSizing()
    {
        PointsAdded = false;
    }
}