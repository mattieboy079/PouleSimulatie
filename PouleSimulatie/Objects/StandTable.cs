namespace PouleSimulatie.Objects;

public class StandTable : DataTable
{
    public StandTable()
    {
        AddColumn("#");
        AddColumn("Club");
        AddColumn("Points");
        AddColumn("Played");
        AddColumn("Win");
        AddColumn("Draw");
        AddColumn("Loss");
        AddColumn("+/-");
        AddColumn("+");
        AddColumn("-");
        AddColumn("Rating");
    }
}