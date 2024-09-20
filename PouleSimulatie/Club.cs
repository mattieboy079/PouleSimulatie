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

    /// <summary>
    /// Get the average rating of the club
    /// </summary>
    /// <returns>The average rating</returns>
    public int GetRating()
    {
        return (Attack + Midfield + Defence) / 3;
    }

    /// <summary>
    /// Get the attacking rating of the club based on the 2 times attack and midfield
    /// </summary>
    /// <returns>The attacking rating</returns>
    public double GetAttackRating()
    {
        return (double)(2 * Attack + Midfield) / 3;
    }
    
    /// <summary>
    /// Get the defending rating of the club based on the 2 times defence and midfield
    /// </summary>
    /// <returns>The defending rating</returns>
    public double GetDefendRating()
    {
        return (double)(2 * Defence + Midfield) / 3;
    }
}