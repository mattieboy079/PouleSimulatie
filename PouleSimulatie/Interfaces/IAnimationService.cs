using PouleSimulatie.Objects;

namespace PouleSimulatie.Interfaces;

public interface IAnimationService
{
    /// <summary>
    /// Draw the matches for the selected playround
    /// </summary>
    /// <param name="graphics">graphics to draw with</param>
    /// <param name="renderer">The renderer to use to draw the table</param>
    /// <param name="rect">The rectangle to be used by the match table</param>
    /// <param name="matches">The match table to draw</param>
    void DrawPlayRound(Graphics graphics, IRenderer<DataTable> renderer, Rectangle rect, DataTable matches);

    /// <summary>
    /// Draw the stand of the poule
    /// </summary>
    /// <param name="graphics">The graphics to draw with</param>
    /// <param name="renderer">The renderer to use to draw the table</param>
    /// <param name="rect">The rectangle to be used by the stand</param>
    /// <param name="stand">The stand to draw</param>
    void DrawStand(Graphics graphics, IRenderer<DataTable> renderer, Rectangle rect, DataTable stand);
    
    /// <summary>
    /// Animate the points gained by the clubs
    /// </summary>
    void AnimatePointsGained();
}