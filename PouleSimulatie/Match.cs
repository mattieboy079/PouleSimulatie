namespace PouleSimulatie;

public class Match
{
    public int Round { get; }
    public Club HomeClub { get; }
    public Club AwayClub { get; }
    public int HomeGoals { get; private set; }
    public int AwayGoals { get; private set; }
    public bool IsPlayed { get; private set; }

    public Match(Club homeClub, Club awayClub, int ronde)
    {
        Round = ronde;
        HomeClub = homeClub;
        AwayClub = awayClub;
    }

    public void Simulate(Random random)
    {
        var baseScoreChance = 0.5;
        var homeChanceModifier = HomeClub.GetAttackRating() / AwayClub.GetDefendRating();
        var homeScoreChance = GetScoreChance(baseScoreChance, homeChanceModifier);
        var awayChanceModifier = AwayClub.GetAttackRating() / HomeClub.GetDefendRating();
        var awayScoreChance = GetScoreChance(baseScoreChance, awayChanceModifier);
        
        while(random.NextDouble() < homeScoreChance / Math.Sqrt(HomeGoals + 1))
            HomeGoals++;
        
        while(random.NextDouble() < awayScoreChance / Math.Sqrt(AwayGoals + 1))
            AwayGoals++;

        IsPlayed = true;
    }

    private double GetScoreChance(double baseChance, double modifier)
    {
        double chance;
        
        if (modifier > 1)
            chance = 1 - (1 - baseChance) / modifier;
        else
            chance = baseChance * modifier;

        var minimumChance = 0.4;
        var maximumChance = 1; 
        return chance * (maximumChance - minimumChance) + minimumChance;
    }
}