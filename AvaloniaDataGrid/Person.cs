using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaDataGrid
{
    public class Person
    {
        public string Substance  { get; set; }
        public bool IsChoosen { get; set; }


        public Person(string substance, bool isChoosen)
        {
            Substance = substance;
            IsChoosen = isChoosen;
        }
    }
}
