namespace PouleSimulatie.Services;

public interface IAnimationService
{
    /// <summary>
    /// Draw the matches for the selected playround
    /// </summary>
    /// <param name="graphics">graphics to draw with</param>
    /// <param name="matches">The matches to draw</param>
    void DrawPlayRound(Graphics graphics, List<Match> matches);

    /// <summary>
    /// Draw the stand of the poule
    /// </summary>
    /// <param name="graphics">The graphics to draw with</param>
    /// <param name="rect">The rectangle to be assigned by the stand</param>
    void DrawStand(Graphics graphics, Rectangle rect);
    
    /// <summary>
    /// Animate the points gained by the clubs
    /// </summary>
    void AnimatePointsGained();
}