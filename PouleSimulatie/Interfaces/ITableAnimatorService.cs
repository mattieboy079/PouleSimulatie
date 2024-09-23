using PouleSimulatie.Objects;

namespace PouleSimulatie.Interfaces;

public interface ITableAnimatorService : IRenderer<DataTable>
{
    /// <summary>
    /// Animate the datatable ordering
    /// </summary>
    /// <param name="newOrderedStand">The stand to update to</param>
    /// <param name="refreshAction">The action to refresh the screen</param>
    void OrderTable(DataTable newOrderedStand, Action refreshAction);
}