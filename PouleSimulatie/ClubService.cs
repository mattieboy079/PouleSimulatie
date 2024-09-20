using System.Data;

namespace PouleSimulatie;

public class ClubService
{
    private readonly List<Club> _clubs = new();
    public IReadOnlyList<Club> Clubs => _clubs.AsReadOnly();

    /// <summary>
    /// Try to add a club to the list of clubs
    /// </summary>
    /// <param name="club">The club to add</param>
    /// <returns>true if addition was succesful</returns>
    public bool AddClub(Club club)
    {
        if (_clubs.Any(c => c.Name == club.Name))
        {
            MessageBox.Show("Deze club bestaat al.");
            return false;
        }
     
        _clubs.Add(club);
        return true;
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

    public Club TryCreatingClub(string clubName, decimal attack, decimal midfield, decimal defence)
    {
        if (Clubs.Any(c => c.Name == clubName))
            throw new DuplicateNameException("Deze club bestaat al.");
		
        var club = new Club(clubName, (int)attack, (int)midfield, (int)defence);

        _clubs.Add(club);
        return club;
    }
}