namespace PouleSimulatie.Services;

public interface IAnimationService
{
    void DrawPlayRound(Graphics graphics, List<Match> matches);
    void DrawStand(Graphics graphics);
    void AnimatePointsGained();
}