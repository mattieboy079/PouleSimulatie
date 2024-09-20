using PouleSimulatie.MassSimulation;

namespace PouleSimulatie.Services;

public interface ISimulationService
{
    /// <summary>
    /// Perform a mass simulation of a poule with the given clubs
    /// </summary>
    /// <param name="simulations">the amount of simulations to perform</param>
    /// <param name="clubs">The clubs to create poules with</param>
    /// <param name="returns">Whether the poules should create return matches</param>
    /// <param name="teamsAdvancing">The amount of teams advancing to the next round</param>
    /// <returns>The result of the simulations</returns>
    Task<MassSimulationResult> Simulate(int simulations, IReadOnlyList<Club> clubs, bool returns, int teamsAdvancing);
    
    /// <summary>
    /// Get the amount of simulations that are finished
    /// </summary>
    /// <returns>Finished simulations</returns>
    int GetProgress();
}