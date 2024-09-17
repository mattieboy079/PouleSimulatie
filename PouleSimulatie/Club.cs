namespace PouleSimulatie;

public class Club
{
    public string Name { get; }
    public int Attack { get; }
    public int Midfield { get; }
    public int Defence { get; }

    public Club(string name, int attack, int midfield, int defence)
    {
        Name = name;
        Attack = attack;
        Midfield = midfield;
        Defence = defence;
    }
}