namespace PouleSimulatie.Objects;

public class MassSimulationResultTable : DataTable
{
    public MassSimulationResultTable(int clubAmount)
    {
        AddColumn("Club");
        AddColumn("Rating");
        AddColumn("Average Points");
        for(int i = 1; i <= clubAmount; i++)
        {
            AddColumn($"Placed {i}");
        }
    }
}