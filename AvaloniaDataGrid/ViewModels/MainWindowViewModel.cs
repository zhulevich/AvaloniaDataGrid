using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace AvaloniaDataGrid.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

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
    }
}
