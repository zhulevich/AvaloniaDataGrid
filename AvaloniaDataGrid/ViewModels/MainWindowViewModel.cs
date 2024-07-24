using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using Spreadalonia;
using ReactiveUI;


namespace AvaloniaDataGrid.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TableData TableDataSpread { get; set; } = new TableData();
        public ObservableCollection<Person> People { get; }
        public MainWindowViewModel()
        {
            string filePath = "C:\\Users\\vlas\\Desktop\\BIP.csv";
            ReaderCsv readerCsv = new ReaderCsv(filePath);

            string[] uniqueParts = readerCsv.GetUniquePartsFromCsv();
            var people = new List<Person> { };

            foreach (var part in uniqueParts)
            {
                people.Add(new Person(part, false));
            }
            People = new ObservableCollection<Person>(people);
            
            
        }
        public void ButtonClick() 
        {
            int i = 0;
            var viewModel = new MainWindowViewModel();
            var substanceList = new List<string>();
            foreach (var people in viewModel.People)
            {
                if (people.IsChoosen == true)
                {
                    substanceList.Add(people.Substance);
                    TableDataSpread.HorizontalHeaders.Add(people.Substance);

                    TableDataSpread.Values[(0, i)] = people.Substance;
                    TableDataSpread.CellStates[(0, i)] = CellState.ReadOnly;
                    this.RaisePropertyChanged(nameof(TableDataSpread));
                    i++;
                }
            }
        }    
    }
}
