namespace PouleSimulatie.Interfaces;

public interface IRenderer<in T> where T : IDrawable
{
    void Draw(Graphics graphics, Rectangle rect, T drawable);
}