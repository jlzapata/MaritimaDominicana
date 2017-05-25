using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaritimaDominicana.Models;

namespace MaritimaDominicana.Controllers
{
    public class SolutionsController : Controller
    {
        private SupportContext db = new SupportContext();

        // GET: Solutions
        public ActionResult Index()
        {
            var solutions = db.Solutions.Include(s => s.ProblemDetail).Include(s => s.User);
            return View(solutions.ToList());
        }

        // GET: Solutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solution solution = db.Solutions.Find(id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }

        public JsonResult SearchSolution(string exp)
        {
            IQueryable<Solution> result;
            if (exp.Trim() != null && exp.Trim().Count() > 0)
            {
                result = db.Solutions.Where(s => s.ProblemDetail.Problem.Name.ToString().ToLower().Contains(exp.ToLower()) ||
                                                 s.SolutionDescription.ToString().ToLower().Contains(exp.ToLower()) ||
                                                 s.ProblemDetail.Title.ToString().ToLower().Contains(exp.ToLower()) ||
                                                 s.User.FirstName.ToString().ToLower().Contains(exp.ToLower()) ||
                                                 s.User.LastName.ToString().ToLower().Contains(exp.ToLower()));
            }else
            {
                result = db.Solutions;
            }
            
            
            var data = result.Select(s => new {
                solutionId = s.SolutionId,
                problem = s.ProblemDetail.Problem.Name,
                title = s.ProblemDetail.Title,
                name = s.User.FirstName + " " + s.User.LastName,
                solution = s.SolutionDescription,
                date = s.Date.ToString()
            });

            return Json(data, JsonRequestBehavior.AllowGet);

        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
