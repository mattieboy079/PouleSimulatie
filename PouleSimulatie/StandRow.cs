namespace PouleSimulatie;

public class StandRow
{
    public Club Club { get; }
    public int Won { get; set; }
    public int Drawn { get; set; }
    public int Lost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }

    public StandRow(Club club)
    {
        Club = club;
    }

    public void MatchPlayed(int goalsFor, int goalsAgainst)
    {
        GoalsFor += goalsFor;
        GoalsAgainst += goalsAgainst;

        if (goalsFor > goalsAgainst)
            Won++;
        else if (goalsFor < goalsAgainst)
            Lost++;
        else
            Drawn++;
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
}