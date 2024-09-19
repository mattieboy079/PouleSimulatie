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

    public void MatchPlayed(int goalsFor, int goalsAgainst)
    {
        GoalsForToAdd = goalsFor;
        GoalsAgainstToAdd = goalsAgainst;

        if (goalsFor > goalsAgainst)
            PointsToAdd = 3;
        else if (goalsFor < goalsAgainst)
            PointsToAdd = 0;
        else
            PointsToAdd = 1;
    }
    
    public int GetPlayed()
    {
        return Won + Drawn + Lost;
    }

    public int GetPoints()
    {
        return Won * 3 + Drawn;
    }

    public int GetGoalDiff()
    {
        return GoalsFor - GoalsAgainst;
    }
    
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

    public void ResetSizing()
    {
        PointsAdded = false;
    }
}