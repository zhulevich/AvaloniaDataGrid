using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

public class ReaderCsv
{
    private readonly string _filePath;

    public ReaderCsv(string filePath)
    {
        _filePath = filePath;
    }

    public string[] GetUniquePartsFromCsv()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";", 
            HasHeaderRecord = true,
        };

        var uniqueParts = new HashSet<string>();

        using (var reader = new StreamReader(_filePath))
        using (var csv = new CsvReader(reader, config))
        {
            while (csv.Read())
            {
                
               
            var fourthColumn = csv.GetField(3); 
            var parts = fourthColumn.Split('/');

            foreach (var part in parts)
            {
                uniqueParts.Add(part);
            }
            }
        }

        var uniquePartsArray = new string[uniqueParts.Count];
        uniqueParts.CopyTo(uniquePartsArray);
        return uniquePartsArray;
    }
}