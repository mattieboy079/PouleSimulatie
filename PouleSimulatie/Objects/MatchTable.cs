namespace PouleSimulatie.Objects;

public class MatchTable : DataTable
{
    public MatchTable()
    {
        AddColumn("Home");
        AddColumn("Away");
        AddColumn("Result");
    }
}