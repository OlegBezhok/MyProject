
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyProject.Models.CandidateModel
{
    public class CandidateInitializer
    {
        public static void Initializer(ApplicationContext context)
        {
            if (context.Candidates.Count() == 0)
            {
                context.AddRange(
                    new Candidate
                    {
                        Name = "Oleg Bezhok",
                        Age = 19,
                        Experience = 1,
                        Position = "Junior",
                        Salary = 500,
                        StartDate = DateTime.Now,
                        Resume = "/Content/Бежок Олег Александрович.doc"
                    },
                    new Candidate
                    {

                        Name = "Sasha Lvova",
                        Age = 18,
                        Experience = 1,
                        Position = "Junior",
                        Salary = 300,
                        StartDate = DateTime.Now,
                        Resume = "/Content/Бежок Олег Александрович.doc"
                    }
                    );
                context.SaveChanges();
            }

                
        }
    }
}

