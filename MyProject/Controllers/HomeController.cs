using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Manipulation;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        IHostingEnvironment _appEnvironment;
        public HomeController(ApplicationContext context, IHostingEnvironment appEnvironment)
        {
            this.db = context;
            _appEnvironment = appEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> Index(string name,int age, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Candidate> candidate = db.Candidates;
            if (!String.IsNullOrEmpty(name))
            {
                candidate = candidate.Where(p => p.Name.Contains(name));
            }
            if (age !=0 )
            {
                candidate = candidate.Where(p => p.Age == age);
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    candidate = candidate.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    candidate = candidate.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    candidate = candidate.OrderByDescending(s => s.Age);
                    break;
                case SortState.ExpAsc:
                    candidate = candidate.OrderBy(s => s.Experience);
                    break;
                case SortState.ExpDesc:
                    candidate = candidate.OrderByDescending(s => s.Experience);
                    break;
                case SortState.SalaryAsc:
                    candidate = candidate.OrderBy(s => s.Salary);
                    break;
                case SortState.SalaryDesc:
                    candidate = candidate.OrderByDescending(s => s.Salary);
                    break;
                default:
                    candidate = candidate.OrderBy(s => s.Name);
                    break;
            }
            var item = await candidate.ToListAsync();
            IndexViewModel viewModel = new IndexViewModel
            {
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(name,age),
                Candidates = item
            };
            bool auth = HttpContext.User.IsInRole("manager");
            if (auth)
            {
                return View(viewModel);
            }
            else
            {
                foreach (var candid in candidate.ToList())
                {
                    candid.Salary = 0;
                }
                return View(viewModel);
            }
           
        }
        [Authorize]
        public async Task<IActionResult> FullInfo(int? item_id)
        {
            bool auth = HttpContext.User.IsInRole("manager");
            if (auth)
            {
                if (item_id != null)
                {
                    Candidate candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == item_id);
                    if (candidate != null)
                        return View(candidate);
                }
            }
            else
            {
                if (item_id != null)
                {
                    Candidate candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == item_id);
                    if (candidate != null)
                        candidate.Salary = 0;
                        return View(candidate);
                }
            }
            return RedirectToAction("Exeption");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Editing(int? item_id, IFormFile uploadedFile)
        {

            bool auth = HttpContext.User.IsInRole("manager");
            if (auth)
            {
                if (item_id != null)
                {
                    Candidate candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == item_id);
                    if (candidate != null)
                        return View(candidate);
                }
                return RedirectToAction("Exeption");
            }
            return RedirectToAction("Exeption");
            
        }

        [HttpPost]
        public async Task<IActionResult> Editing(Candidate candidate, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Content/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                candidate.Resume = uploadedFile.FileName;
            }
           
                db.Candidates.Update(candidate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Create()
        {
            bool auth = HttpContext.User.IsInRole("manager");
            if (auth)
            return View();
            return RedirectToAction("Exeption");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Candidate candidate, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Content/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                candidate.Resume = uploadedFile.FileName;
            }
            db.Candidates.Add(candidate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? item_id)
        {
            bool auth = HttpContext.User.IsInRole("manager");
            if (auth)
            {
                if (item_id != null)
                {
                    Candidate candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == item_id);
                    if (candidate != null)
                        return View(candidate);
                }
                return RedirectToAction("Exeption");
            }
            return RedirectToAction("Exeption");
        }
        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Candidate candidate = new Candidate { Id = id.Value };
                db.Entry(candidate).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
                
            }
            return NotFound();
        }
        public IActionResult Exeption()
        {
            return View();
        }
    }
}
