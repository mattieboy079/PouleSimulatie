using System.Data;

namespace PouleSimulatie.Services;

public class ClubService : IClubService
{
    private readonly List<Club> _clubs = new();
    public IReadOnlyList<Club> Clubs => _clubs.AsReadOnly();

    /// <summary>
    /// Try to create a club and return it
    /// </summary>
    /// <param name="clubName">The name for the club</param>
    /// <param name="attack">The attack rating</param>
    /// <param name="midfield">The midfield rating</param>
    /// <param name="defence">The defence rating</param>
    /// <returns>The club when creation was succesful</returns>
    /// <exception cref="DuplicateNameException">When the clubname already exists</exception>
    public Club TryCreatingClub(string clubName, decimal attack, decimal midfield, decimal defence)
    {
        if (Clubs.Any(c => c.Name == clubName))
            throw new DuplicateNameException("Deze club bestaat al.");
		
        var club = new Club(clubName, (int)attack, (int)midfield, (int)defence);

        _clubs.Add(club);
        return club;
    }


    /// <summary>
    /// Try to remove a club from the list of clubs
    /// </summary>
    /// <param name="clubName">The name of the club to remove</param>
    /// <returns>true if the removal was succesful</returns>
    public bool RemoveClub(string clubName)
    {
        var club = _clubs.FirstOrDefault(c => c.Name == clubName);
        if (club == null) return false;
        
        _clubs.Remove(club);
        return true;
    }

    /// <summary>
    /// Create a list of random clubs
    /// </summary>
    /// <param name="amount">Amount of clubs to create</param>
    /// <returns>The created clubs</returns>
    public IEnumerable<Club> CreateRandomClubs(int amount)
    {
        var random = new Random();
        for (int i = 0; amount > 0; i++)
        {
            if(Clubs.Any(c => c.Name == $"Club {i + 1}"))
                continue;
            
            var min = 1;
            var max = 99;
            var club = new Club($"Club {i + 1}", random.Next(min, max), random.Next(min, max), random.Next(min, max));
            _clubs.Add(club);
            yield return club;
            amount--;
        }
    }
}