namespace PouleSimulatie.Objects;

public class DataRow
{
    public Dictionary<string, string> Cells { get; set; }

    public DataRow()
    {
        Cells = new Dictionary<string, string>();
    }
}