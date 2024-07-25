using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using Spreadalonia;
using ReactiveUI;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;
using Avalonia.Controls.Shapes;



namespace AvaloniaDataGrid.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public TableData TableDataSpread { get; set; } = new TableData();
        public ObservableCollection<Person> People { get; }
        public int MaxColumnCount { get; set; }
        public int MaxRowCount { get; set; }

        public MainWindowViewModel()
        {
            var uniqueParts = new HashSet<string>();
            var people = new List<Person>();

            StringReader readerCsv = new StringReader(Properties.Resources.BIP);
            string line;

            while ((line = readerCsv.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                string[] parts = words[3].Split('/');

                foreach (string part in parts)
                {
                    if (uniqueParts.Add(part))  
                    {
                        people.Add(new Person(part, false));
                    }
                }
            }

            People = new ObservableCollection<Person>(people);
        }
        public void ButtonClick()
        {
            int i = 0;
            TableDataSpread.HorizontalHeaders.Add("Substance");
            this.RaisePropertyChanged(nameof(TableDataSpread));

            var substanceList = new List<string>();
            foreach (var people in this.People)
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

            

            StringReader reader = new StringReader(Properties.Resources.BIP);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                for (int a = 0; a < substanceList.Count; a++)
                {

                    for (int b = 0; b < substanceList.Count; b++)
                    {
                        if ((substanceList[a] + '/' + substanceList[b] == words[3]) ||
                            (substanceList[b] + '/' + substanceList[a] == words[3]))
                        {
                            TableDataSpread.Values[(a + 1, b)] = words[2];
                        }
                    }

                }
                
            }
            MaxColumnCount = substanceList.Count;
            MaxRowCount = substanceList.Count;
            this.RaisePropertyChanged(nameof(MaxColumnCount));
            this.RaisePropertyChanged(nameof(MaxRowCount));
        }
    }
}
