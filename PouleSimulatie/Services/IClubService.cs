namespace PouleSimulatie.Services;

public interface IClubService
{
    IEnumerable<Club> CreateRandomClubs(int amount);
    Club TryCreatingClub(string clubName, decimal attack, decimal midfield, decimal defence);
    bool RemoveClub(string clubName);
    IReadOnlyList<Club> Clubs { get; }
}