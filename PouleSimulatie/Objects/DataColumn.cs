namespace PouleSimulatie.Objects;

public class DataColumn
{
    public string Name { get; set; }
    public int Width { get; set; }

    public DataColumn(string name)
    {
        Name = name;
    }
}