namespace PouleSimulatie;

public class Match
{
    private readonly int _ronde;
    public Club HomeClub { get; }
    public Club AwayClub { get; }
    public int HomeGoals { get; private set; }
    public int AwayGoals { get; private set; }
    
    public Match(Club homeClub, Club awayClub, int ronde)
    {
        _ronde = ronde;
        HomeClub = homeClub;
        AwayClub = awayClub;
    }
    
    public void PlayMatch()
    {
        var homeRating = HomeClub.Attack + HomeClub.Midfield + HomeClub.Defence;
        var awayRating = AwayClub.Attack + AwayClub.Midfield + AwayClub.Defence;
        
        var homeGoals = new Random().Next(0, homeRating);
        var awayGoals = new Random().Next(0, awayRating);
        
        HomeGoals = homeGoals;
        AwayGoals = awayGoals;
    }
}