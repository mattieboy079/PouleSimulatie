namespace PouleSimulatie;

public class Poule
{
    public List<Club> Clubs { get; }
    private List<Match> _matches { get; set; }
    public List<StandRow> Stand { get; private set; }
    private Random _random { get; }
    private bool _returns { get; }
    public int TotalRondes { get; private set; }
    
    public Poule(List<Club> clubs, bool returns, Random random)
    {
        Clubs = clubs;
        Stand = Clubs.Select(c => new StandRow(c)).ToList();
        _returns = returns;
        _random = random;
        _matches = new List<Match>();
    }

    private void CreateMatches()
    {
        var clubs = Clubs.OrderBy(_ => _random.Next()).ToArray();
        int rondes;

        if (clubs.Length % 2 == 1)
            rondes = clubs.Length;
        else
            rondes = clubs.Length - 1;

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

    public void Init()
    {
        CreateMatches();
    }

    public List<Match> GetMatches(int round)
    {
        return _matches.Where(m => m.Round == round).ToList();
    }

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
    
    public void SimulateNextMatch()
    {
        var nextMatch = _matches.FirstOrDefault(m => !m.IsPlayed);
        if (nextMatch == null)
            return;
        
        SimulateMatch(nextMatch);
    }

    public void SimulateAllMatches()
    {
        Parallel.ForEach(_matches.Where(m => !m.IsPlayed), SimulateMatch);
    }

    private void SimulateMatch(Match match)
    {
        match.Simulate(_random);
        UpdateStand(match);
    }
    
    private void UpdateStand(Match match)
    {
        var homeStand = Stand.First(s => s.Club == match.HomeClub);
        var awayStand = Stand.First(s => s.Club == match.AwayClub);
        
        homeStand.MatchPlayed(match.HomeGoals, match.AwayGoals);
        awayStand.MatchPlayed(match.AwayGoals, match.HomeGoals);
    }

    public int? GetNextMatchRound()
    {
        return _matches.FirstOrDefault(m => !m.IsPlayed)?.Round;
    }
}