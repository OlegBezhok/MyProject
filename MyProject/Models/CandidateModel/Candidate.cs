using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyProject.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public int Experience { get; set; }
        public DateTime StartDate { get; set; }
        public string Resume { get; set; }
    }
}
