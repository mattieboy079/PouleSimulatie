using PouleSimulatie.Interfaces;
using PouleSimulatie.Objects;

namespace PouleSimulatie.Services;

public class AnimatedDataTableRendererService : ITableAnimatorService
{
	private bool _isOrdering;
	private int _rowHeight = 30;
	private readonly Dictionary<string, int> _clubRowOffset;
	private readonly Dictionary<string, double> _clubRowSize;
	private Dictionary<string, int> _lastDrawnOrder;

	public AnimatedDataTableRendererService(IReadOnlyList<Club> clubs)
	{
		_clubRowOffset = new Dictionary<string, int>();
		_clubRowSize = new Dictionary<string, double>();
		_lastDrawnOrder = new Dictionary<string, int>();
		for(var i = 0; i < clubs.Count; i++)
		{
			_clubRowOffset.Add(clubs[i].Name, 0);
			_clubRowSize.Add(clubs[i].Name, 1);
			_lastDrawnOrder.Add(clubs[i].Name, i);
		}
	}
	
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
        if(Math.Abs(widthPercentage - 1) > 0.01)
		{
			if (widthPercentage < 1)
			{
				baseFontSize = Math.Max(1, Math.Floor(baseFontSize * widthPercentage));
				font = new Font("Arial", (float)baseFontSize, FontStyle.Regular);
			}
			
			foreach (var column in dataTable.Columns)
			{
				column.Width = (int)(column.Width * widthPercentage);
			}
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
        if (_isOrdering)
        {
	        for (var i = 0; i < dataTable.Rows.Count; i++)
	        {
		        var clubName = dataTable.Rows[i].Cells["Club"];
		        var height = rect.Y + (i + 1) * _rowHeight - _clubRowOffset[clubName];
		        var widthInc = rect.Width * (_clubRowSize[clubName] - 1);
		        var heightInc = _rowHeight * (_clubRowSize[clubName] - 1);
		        var rowRect = new Rectangle((int)(rect.X - widthInc / 2), (int)(height - heightInc / 2), (int)(rect.Width + widthInc), (int)(_rowHeight + heightInc));
		        DrawRow(graphics, rowRect, dataTable.Columns, dataTable.Rows[i], font);
	        }
        }
        else
        {
	        for (var i = 0; i < dataTable.Rows.Count; i++)
	        {
		        var rowRect = rect with { Y = rect.Y + (i + 1) * _rowHeight, Height = _rowHeight };
		        DrawRow(graphics, rowRect, dataTable.Columns, dataTable.Rows[i], font);
	        }
        }
    }

    private void DrawRow(Graphics graphics, Rectangle rect, List<DataColumn> columns, DataRow row, Font font)
    {
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
	    return size.Width * 1.08f;
    }

    public void OrderTable(DataTable newStand, Action refreshAction)
	{
		var clubMoveY = new Dictionary<string, int>();

		for (int i = 0; i < newStand.Rows.Count; i++)
		{
			var oldIndex = _lastDrawnOrder[newStand.Rows[i].Cells["Club"]];
			clubMoveY.Add(newStand.Rows[i].Cells["Club"], i - oldIndex);
		}

		if (clubMoveY.Values.Any(v => v != 0)) // Needs Order
		{
			Task.Delay(500);
			
			_isOrdering = true;
			var changeSizeMs = 250;
			var orderTableMs = 500;
			var totalTime = 2 * changeSizeMs + orderTableMs;
				
			var changeSizeFactor = 0.1;
				
			var startTime = DateTime.Now;

			double deltaTime = 0;
				
			while (deltaTime <= totalTime)
			{
				deltaTime = (DateTime.Now - startTime).TotalMilliseconds;
				if(deltaTime <= changeSizeMs)
				{
					foreach (var stand in newStand.Rows)
					{
						if(clubMoveY[stand.Cells["Club"]] < 0)
							_clubRowSize[stand.Cells["Club"]] = 1 + changeSizeFactor * deltaTime / changeSizeMs;
						else if(clubMoveY[stand.Cells["Club"]] > 0)
							_clubRowSize[stand.Cells["Club"]] = 1 - changeSizeFactor * deltaTime / changeSizeMs;
						else
							_clubRowSize[stand.Cells["Club"]] = 1;
						_clubRowOffset[stand.Cells["Club"]] = clubMoveY[stand.Cells["Club"]] * _rowHeight;
					}
				}
				else if (deltaTime <= changeSizeMs + orderTableMs)
				{
					foreach (var stand in newStand.Rows)
					{
						if(clubMoveY[stand.Cells["Club"]] < 0)
							_clubRowSize[stand.Cells["Club"]] = 1 + changeSizeFactor;
						else if(clubMoveY[stand.Cells["Club"]] > 0)
							_clubRowSize[stand.Cells["Club"]] = 1 - changeSizeFactor;
						else
							_clubRowSize[stand.Cells["Club"]] = 1;
						var percentageToMove = 1 - (deltaTime - changeSizeMs) / orderTableMs;
						_clubRowOffset[stand.Cells["Club"]] = (int)(clubMoveY[stand.Cells["Club"]] * _rowHeight * percentageToMove);
					}					
				}
				else
				{
					foreach (var stand in newStand.Rows)
					{
						var percentageSizeLeft = 1 - (deltaTime - changeSizeMs - orderTableMs) / changeSizeMs;
						if(clubMoveY[stand.Cells["Club"]] < 0)
							_clubRowSize[stand.Cells["Club"]] = 1 + changeSizeFactor * percentageSizeLeft;
						else if(clubMoveY[stand.Cells["Club"]] > 0)
							_clubRowSize[stand.Cells["Club"]] = 1 - changeSizeFactor * percentageSizeLeft;
						else
							_clubRowSize[stand.Cells["Club"]] = 1;
						_clubRowOffset[stand.Cells["Club"]] = 0;
					}
				}
					
				refreshAction();
			}
			_isOrdering = false;
				
			refreshAction();
			_lastDrawnOrder = newStand.Rows.ToDictionary(s => s.Cells["Club"], s => newStand.Rows.IndexOf(s));
		}

		foreach (var row in newStand.Rows)
		{
			_clubRowOffset[row.Cells["Club"]] = 0;
			_clubRowSize[row.Cells["Club"]] = 1;
		}
	}
}