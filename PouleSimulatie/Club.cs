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

    public int GetRating()
    {
        return (Attack + Midfield + Defence) / 3;
    }

    public double GetAttackRating()
    {
        return (double)(2 * Attack + Midfield) / 3;
    }

    public double GetDefendRating()
    {
        return (double)(2 * Defence + Midfield) / 3;
    }
}