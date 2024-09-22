using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;

namespace PouleSimulatie.Services;

public class DataTableRendererService : IRenderer<DataTable>
{
    private int _rowHeight = 30;
    
    public void Draw(Graphics graphics, Rectangle rect, DataTable dataTable)
    {
        _rowHeight = Math.Min(30, rect.Height / (dataTable.Rows.Count + 1));
        double baseFontSize = Math.Max(1, _rowHeight / 2);
        var font = new Font("Arial", (float)baseFontSize, FontStyle.Regular);
        var brush = new SolidBrush(Color.Black);
        var pen = new Pen(Color.Black, 1);
        var format = new StringFormat { LineAlignment = StringAlignment.Center };

        foreach (var column in dataTable.Columns)
        {
            var headerTextWidth = GetTextWidth(graphics, column.Name, font);
            var maxCellTextWidth = dataTable.Rows.Max(row => GetTextWidth(graphics, row.Cells[column.Name], font));
            column.Width = (int)Math.Max(headerTextWidth, maxCellTextWidth);
        }
        
        var totalWidth = dataTable.Columns.Sum(c => c.Width);
        var widthPercentage = (double)rect.Width / totalWidth;
        foreach (var column in dataTable.Columns)
        {
            baseFontSize = Math.Max(1, baseFontSize * widthPercentage);
            column.Width = (int)(column.Width * widthPercentage);
        }
        
        // Draw header
        var headerRect = rect with { Height = _rowHeight };
        graphics.DrawRectangle(pen, headerRect);
        var xOffset = 0;
        foreach (var column in dataTable.Columns)
        {
            graphics.DrawString(column.Name, font, brush, new Rectangle(headerRect.X + xOffset, headerRect.Y, column.Width, headerRect.Height), format);
            xOffset += column.Width;
        }

        // Draw rows
        for (var i = 0; i < dataTable.Rows.Count; i++)
        {
            var rowRect = rect with { Y = rect.Y + (i + 1) * _rowHeight, Height = _rowHeight };
            DrawRow(graphics, rowRect, dataTable.Columns, dataTable.Rows[i]);
        }
    }

    public void DrawRow(Graphics graphics, Rectangle rect, List<DataColumn> columns, DataRow row)
    {
        double baseFontSize = Math.Max(1, rect.Height / 2);
        var font = new Font("Arial", (float)baseFontSize, FontStyle.Regular);
        var brush = new SolidBrush(Color.Black);
        var pen = new Pen(Color.Black, 1);
        var format = new StringFormat { LineAlignment = StringAlignment.Center };

        graphics.DrawRectangle(pen, rect);
        var xOffset = 0;
        foreach (var column in columns)
        {
            graphics.DrawString(row.Cells[column.Name], font, brush, new Rectangle(rect.X + xOffset, rect.Y, column.Width, rect.Height), format);
            xOffset += column.Width;
        }
    }

    private float GetTextWidth(Graphics graphics, string text, Font font)
    {
        var size = graphics.MeasureString(text, font);
        return size.Width;
    }
}