namespace PouleSimulatie;

public class Poule
{
    private readonly List<Club> _clubs;
    private List<Match> _matches;
    private Random _random;
    private bool _returns;
    public int TotalRondes { get; private set; }
    
    public Poule(List<Club> clubs, bool returns)
    {
        _clubs = clubs;
        _returns = returns;
        _random = new Random();
        _matches = new List<Match>();
    }

    private void CreateMatches()
    {
        var clubs = _clubs.OrderBy(c => _random.Next()).ToArray();
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
    }

    public void Init()
    {
        CreateMatches();
    }

    public List<Match> GetMatches(int round)
    {
        return _matches.Where(m => m.Round == round).ToList();
    }
}