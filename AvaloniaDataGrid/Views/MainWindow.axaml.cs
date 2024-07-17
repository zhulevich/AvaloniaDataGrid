using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaDataGrid.ViewModels;
using System;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using Avalonia.Data;
using System.Linq;

namespace AvaloniaDataGrid.Views
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                var substanceList = new List<string>();
                foreach (var people in viewModel.People)
                {
                    if (people.IsChoosen == true)
                    {
                        substanceList.Add(people.Substance);
                    }
                }
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                };
                using (var reader = new StreamReader("C:\\Users\\vlas\\Desktop\\BIP.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    string[,] OutputArray = new string[substanceList.Count + 1, substanceList.Count + 1];
                    
                    for (int a = 1; a < substanceList.Count + 1; a++)
                    {
                        OutputArray[a, 0] = substanceList[a - 1];
                        OutputArray[0, a] = substanceList[a - 1];
                        

                        for (int b = 1; b < substanceList.Count + 1; b++)
                        {
                            while (csv.Read())
                            {
                                if ((substanceList[a - 1] + '/' + substanceList[b - 1] == csv.GetField(3)) ||
                                    (substanceList[b - 1] + '/' + substanceList[a - 1] == csv.GetField(3)))
                                {
                                    OutputArray[a, b] = csv.GetField(2);
                                    break;
                                }
                                else
                                {
                                    OutputArray[a, b] = "-";
                                }
                            }
                        }
                    }

                    var tableViewModel = new TableViewModel(OutputArray);

                    var tableDataGrid = this.FindControl<DataGrid>("TableDataGrid");
                    tableDataGrid.ItemsSource = tableViewModel.TableData;

                    tableDataGrid.Columns.Clear();
                    foreach (var header in tableViewModel.ColumnHeaders)
                    {
                        tableDataGrid.Columns.Add(new DataGridTextColumn
                        {
                            Header = header,
                            Binding = new Binding($"Columns[{tableViewModel.ColumnHeaders.IndexOf(header)}]")
                        });
                    }

                    var tabControl = this.FindControl<TabControl>("tabControl");
                    var tableTabItem = tabControl.Items[1] as TabItem;

                    if (tableTabItem != null)
                    {
                        tableTabItem.DataContext = tableViewModel;
                    }
                }
            }
        }
    }
}