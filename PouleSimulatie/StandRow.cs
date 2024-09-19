namespace PouleSimulatie;

public class StandRow
{
    public Club Club { get; }
    public int Won { get; set; }
    public int Drawn { get; set; }
    public int Lost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    
    // Properties for animation
    public int? PointsToAdd { get; set; }
    public int? GoalsForToAdd { get; set; }
    public int? GoalsAgainstToAdd { get; set; }
    public bool PointsAdded { get; set; }

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
        GoalsFor += GoalsForToAdd.Value;
        GoalsAgainst += GoalsAgainstToAdd.Value;
        PointsToAdd = null;
        GoalsForToAdd = null;
        GoalsAgainstToAdd = null;
        PointsAdded = true;
    }
}