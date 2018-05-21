using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Manipulation
{
    public class IndexViewModel
    {
        public IEnumerable<Candidate> Candidates { get; set; }        
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
