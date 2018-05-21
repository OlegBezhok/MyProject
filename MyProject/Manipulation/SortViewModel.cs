using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Manipulation
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } 
        public SortState AgeSort { get; private set; }    
        public SortState ExpSort { get; private set; }   
        public SortState SalarySort { get; private set; }     

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ExpSort = sortOrder == SortState.ExpAsc ? SortState.ExpDesc : SortState.ExpAsc;
            SalarySort = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;
        }
    }
}
