namespace PouleSimulatie;

public class Club
{
    public string Name { get; }
    private readonly int _attack;
    private readonly int _midfield;
    private readonly int _defence;

    public Club(string name, int attack, int midfield, int defence)
    {
        Name = name;
        _attack = attack;
        _midfield = midfield;
        _defence = defence;
    }

    /// <summary>
    /// Get the average rating of the club
    /// </summary>
    /// <returns>The average rating</returns>
    public int GetRating()
    {
        return (_attack + _midfield + _defence) / 3;
    }

    /// <summary>
    /// Get the attacking rating of the club based on the 2 times attack and midfield
    /// </summary>
    /// <returns>The attacking rating</returns>
    public double GetAttackRating()
    {
        return (double)(2 * _attack + _midfield) / 3;
    }
    
    /// <summary>
    /// Get the defending rating of the club based on the 2 times defence and midfield
    /// </summary>
    /// <returns>The defending rating</returns>
    public double GetDefendRating()
    {
        return (double)(2 * _defence + _midfield) / 3;
    }

    /// <summary>
    /// Get the information string of the club
    /// </summary>
    /// <returns>The information string</returns>
    public override string ToString()
    {
        return $"{Name} - A:{_attack} M:{_midfield} D:{_defence}";
    }
}