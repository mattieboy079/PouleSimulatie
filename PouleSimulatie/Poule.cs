namespace PouleSimulatie;

public class Poule
{
    public int TeamsAdvancing { get; }
    public IReadOnlyList<Club> Clubs { get; }
    private List<Match> _matches { get; set; }
    public List<StandRow> Stand { get; private set; }
    private Random _random { get; }
    private bool _returns { get; }
    public int TotalRondes { get; private set; }

    /// <summary>
    /// Create a new poule
    /// </summary>
    /// <param name="clubs">The clubs or teams that </param>
    /// <param name="returns">Whether the teams will have home and away matches against eachother</param>
    /// <param name="teamsAdvancing"></param>
    /// <param name="random">A randomizer object to avoid all simulations have the same seeded randomizer object</param>
    public Poule(IReadOnlyList<Club> clubs, bool returns, int teamsAdvancing, Random random)
    {
        TeamsAdvancing = teamsAdvancing;
        Clubs = clubs;
        Stand = Clubs.Select(c => new StandRow(c)).ToList();
        _returns = returns;
        _random = random;
        _matches = new List<Match>();
    }

    /// <summary>
    /// Initialize the poule
    /// </summary>
    public void Init()
    {
        CreateMatches();
    }

    /// <summary>
    /// Create the matches for this poule
    /// </summary>
    private void CreateMatches()
    {
        var clubs = Clubs.OrderBy(_ => _random.Next()).ToArray();

        var rondes = clubs.Length % 2 == 1
            ? clubs.Length
            : clubs.Length - 1;

        TotalRondes = rondes;
        if (_returns)
            TotalRondes *= 2;

        for (var r = 0; r < rondes; r++)
        {
            Club homeClub;
            Club awayClub;
            Club[] newClubs;
            int kans;
            if (clubs.Length % 2 == 1)
            {
                for (var c = 0; c < clubs.Length - 2; c += 2)
                {
                    kans = _random.Next(2);
                    
                    if (kans == 0)
                    {
                        homeClub = clubs[c];
                        awayClub = clubs[c + 1];
                    }
                    else
                    {
                        homeClub = clubs[c + 1];
                        awayClub = clubs[c];
                    }

                    var ronde = r + 1;
                    _matches.Add(new Match(homeClub, awayClub, ronde));
                    if (_returns)
                        _matches.Add(new Match(awayClub, homeClub, rondes + ronde));
                }

                newClubs = new Club[clubs.Length];
                for (var ii = 0; ii < clubs.Length; ii++)
                {
                    if (ii == 1)
                        newClubs[ii] = clubs[ii - 1];
                    else if (ii % 2 == 1)
                        newClubs[ii] = clubs[ii - 2];
                    else if (ii == clubs.Length - 1)
                        newClubs[ii] = clubs[ii - 1];
                    else if (ii != clubs.Length - 1)
                        newClubs[ii] = clubs[ii + 2];
                }
            }
            else
            {
                for (var c = 0; c < clubs.Length - 1; c += 2)
                {
                    kans = _random.Next(0, 2);
                    if (kans == 0)
                    {
                        homeClub = clubs[c];
                        awayClub = clubs[c + 1];
                    }
                    else
                    {
                        homeClub = clubs[c + 1];
                        awayClub = clubs[c];
                    }

                    var ronde = r + 1;
                    _matches.Add(new Match(homeClub, awayClub, ronde));
                    if (_returns)
                        _matches.Add(new Match(awayClub, homeClub, ronde + rondes));
                }

                newClubs = new Club[clubs.Length];
                for (var ii = 0; ii < clubs.Length; ii++)
                {
                    if (ii == 0)
                        newClubs[ii] = clubs[ii];
                    else if (ii == 1)
                        newClubs[ii] = clubs[ii + 1];
                    else if (ii == clubs.Length - 2)
                        newClubs[ii] = clubs[ii + 1];
                    else if (ii % 2 == 0)
                        newClubs[ii] = clubs[ii + 2];
                    else if (ii % 2 == 1)
                        newClubs[ii] = clubs[ii - 2];
                }
            }

            clubs = newClubs;
        }

        _matches = _matches.OrderBy(m => m.Round).ToList();
    }

    /// <summary>
    /// Get all matches for a specific round
    /// </summary>
    /// <param name="round">the round to get the matches for</param>
    /// <returns>List of matches in the given round</returns>
    public List<Match> GetMatches(int round)
    {
        return _matches.Where(m => m.Round == round).ToList();
    }

    /// <summary>
    /// Order the stand based on the points, goal difference, goals for and head to head result
    /// </summary>
    /// <returns>The ordered stand</returns>
    public List<StandRow> GetOrderedStand()
    {
        Stand = Stand
            .OrderByDescending(s => s.GetPoints())
            .ThenByDescending(s => s.GetGoalDiff())
            .ThenByDescending(s => s.GoalsFor)
            .ThenByDescending(s => s.Club, Comparer<Club>.Create(GetHeadToHeadResult))
            .ToList();
        return Stand;
    }

    /// <summary>
    /// Get the head to head result between two clubs
    /// </summary>
    /// <param name="club1">The first club</param>
    /// <param name="club2">The second club</param>
    /// <returns>value < 0 -> Club1 wins, value = 0 -> Equal, value > 0 Club2 wins</returns>
    private int GetHeadToHeadResult(Club club1, Club club2)
    {
        var matchesBetweenClubs = _matches.Where(m => 
            (m.HomeClub == club1 && m.AwayClub == club2) || 
            (m.HomeClub == club2 && m.AwayClub == club1)).ToList();

        var club1Points = 0;
        var club2Points = 0;
        var club1Goals = 0;
        var club2Goals = 0;

        foreach (var match in matchesBetweenClubs)
        {
            if (match.HomeClub == club1)
            {
                club1Goals += match.HomeGoals;
                club2Goals += match.AwayGoals;
                if (match.HomeGoals > match.AwayGoals)
                    club1Points += 3;
                else if (match.HomeGoals < match.AwayGoals)
                    club2Points += 3;
                else
                {
                    club1Points += 1;
                    club2Points += 1;
                }
            }
            else
            {
                club1Goals += match.AwayGoals;
                club2Goals += match.HomeGoals;
                if (match.HomeGoals > match.AwayGoals)
                    club2Points += 3;
                else if (match.HomeGoals < match.AwayGoals)
                    club1Points += 3;
                else
                {
                    club1Points += 1;
                    club2Points += 1;
                }
            }
        }

        if (club1Points == club2Points)
            return club1Goals - club2Goals;
        
        return club1Points - club2Points;
    }
    
    /// <summary>
    /// Simulate the next match in the poule
    /// </summary>
    public void SimulateNextMatch()
    {
        var nextMatch = _matches.FirstOrDefault(m => !m.IsPlayed);
        if (nextMatch == null)
            return;
        
        SimulateMatch(nextMatch, true);
    }

    /// <summary>
    /// Simulate all the remaining matches in the poule
    /// </summary>
    public void SimulateAllMatches()
    {
        Parallel.ForEach(_matches.Where(m => !m.IsPlayed), m => SimulateMatch(m, false));
        foreach (var standRow in Stand)
            standRow.AddValues();
    }

    /// <summary>
    /// Simulate a given match and update the stand
    /// </summary>
    /// <param name="match">The match to simulate</param>
    /// <param name="animatedScore">whether the results should be animated</param>
    private void SimulateMatch(Match match, bool animatedScore)
    {
        match.Simulate(_random);
        UpdateStand(match, animatedScore);
    }

    /// <summary>
    /// Update the stand based on the result of a playedMatch
    /// </summary>
    /// <param name="playedMatch">The played playedMatch</param>
    /// <param name="animatedScore">Whether the results should be prepared for animation</param>
    private void UpdateStand(Match playedMatch, bool animatedScore)
    {
        var homeStand = Stand.First(s => s.Club == playedMatch.HomeClub);
        var awayStand = Stand.First(s => s.Club == playedMatch.AwayClub);

        if (animatedScore)
        {
            homeStand.MatchPlayedAnimated(playedMatch.HomeGoals, playedMatch.AwayGoals);
            awayStand.MatchPlayedAnimated(playedMatch.AwayGoals, playedMatch.HomeGoals);
        }
        else
        {
            homeStand.MatchPlayed(playedMatch.HomeGoals, playedMatch.AwayGoals);
            awayStand.MatchPlayed(playedMatch.AwayGoals, playedMatch.HomeGoals);
        }
    }

    /// <summary>
    /// Get the first round with matches that are not played yet
    /// </summary>
    /// <returns>The roundnumber, null means all matches have been played</returns>
    public int? GetNextMatchRound()
    {
        return _matches.FirstOrDefault(m => !m.IsPlayed)?.Round;
    }
}