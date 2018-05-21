using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Manipulation
{
    public class FilterViewModel
    {
        public FilterViewModel(string name, int age)
        {
           
            SelectedName = name;
            SelectedAge = age;
        }
        
        public string SelectedName { get; private set; }
        public int SelectedAge { get; private set; }
    }
}
