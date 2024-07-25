using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaDataGrid.ViewModels
{
    public class TableViewModel : ReactiveObject
    {
        public ObservableCollection<TableRow> TableData { get; } = new ObservableCollection<TableRow>();
        public List<string> ColumnHeaders { get; } = new List<string>();

        public TableViewModel(string[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < cols; i++)
            {
                ColumnHeaders.Add(array[0, i]);
            }

            for (int i = 1; i < rows; i++)
            {
                var row = new TableRow();
                for (int j = 0; j < cols; j++)
                {
                    row.Columns.Add(array[i, j]);
                }
                TableData.Add(row);
            }
        }
    }

    public class TableRow : ReactiveObject
    {
        public List<string> Columns { get; } = new List<string>();
    }
}