using PouleSimulatie.Interfaces;

namespace PouleSimulatie.Objects;

public class DataTable : IDrawable
{
    public List<DataColumn> Columns { get; }
    public List<DataRow> Rows { get; }

    public DataTable()
    {
        Columns = new List<DataColumn>();
        Rows = new List<DataRow>();
    }

    /// <summary>
    /// Add a value to the given row
    /// </summary>
    /// <param name="row">The row to insert the cell into</param>
    /// <param name="columnName">The name of the column</param>
    /// <param name="value">Value to insert into the cell</param>
    /// <exception cref="ArgumentException">The columnName does not exist</exception>
    public void AddValue(DataRow row, string columnName, string value)
    {
        if (Columns.All(c => c.Name != columnName))
            throw new ArgumentException($"Column '{columnName}' does not exist.");
        
        row.Cells[columnName] = value;
    }

    /// <summary>
    /// Add a column to the table
    /// </summary>
    /// <param name="name">The headerName of the column</param>
    public void AddColumn(string name)
    {
        Columns.Add(new DataColumn(name));
    }
}