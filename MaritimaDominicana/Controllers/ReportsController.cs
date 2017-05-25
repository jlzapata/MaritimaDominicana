using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaritimaDominicana.Models;
using System.Data.Entity.SqlServer;

namespace MaritimaDominicana.Controllers
{
    [LoginFilter]
    public class ReportsController : Controller
    {
        private SupportContext db = new SupportContext();

        public ActionResult Index() {
            
            return View(db.ProblemDetails.OrderByDescending(p=>p.ProblemDetailId));
        }



        public JsonResult HistoricoSolicitudes(DateTime? startDate, DateTime? endDate, int categoria)
        {
            if (endDate.HasValue)
            {
                endDate = endDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            IQueryable<ProblemDetail> problemDetails;

            if (startDate != null && endDate != null)
            {
                problemDetails = db.ProblemDetails.Where(p => p.Date >= startDate && p.Date <= endDate);
            }
            else if (startDate != null || endDate != null)
            {
                if (startDate != null)
                {
                    problemDetails = db.ProblemDetails.Where(p => p.Date >= startDate).OrderByDescending(p => p.ProblemDetailId);
                }
                else
                {
                    problemDetails = db.ProblemDetails.Where(p => p.Date <= endDate).OrderByDescending(p => p.ProblemDetailId);
                }
            }
            else
            {
                problemDetails = db.ProblemDetails.OrderByDescending(p => p.ProblemDetailId);
            }

            switch (categoria)
            { 
                case 0:
                    var data = from p in problemDetails
                               group p by p.Client into
                               problemDetailsGroup
                               where problemDetailsGroup.Count() > 0
                               select new
                               {
                                   categoria = problemDetailsGroup.Key.Name,
                                   quantity = problemDetailsGroup.Count()
                               }

                       ;


                    return Json(data, JsonRequestBehavior.AllowGet);

                case 1:
                    var data1 = from p in problemDetails
                                group p by p.Problem
                               into problemDetailsGroup
                                where problemDetailsGroup.Count() > 0
                                select new
                                {
                                    categoria = problemDetailsGroup.Key.Name,
                                    quantity = problemDetailsGroup.Count()
                                };

                    return Json(data1, JsonRequestBehavior.AllowGet);

                case 2:
                    var data2 = from p in problemDetails
                                group p by p.Department
                               into problemDetailsGroup
                                where problemDetailsGroup.Count() > 0
                                select new
                                {
                                    categoria = problemDetailsGroup.Key.Name,
                                    quantity = problemDetailsGroup.Count()
                                };

                    return Json(data2, JsonRequestBehavior.AllowGet);

                case 3:
                    var data3 = from p in problemDetails
                                where p.state != 3
                                group p by p.Assigned
                           into problemDetailsGroup
                                where problemDetailsGroup.Count() > 0 
                                select new
                                {
                                    categoria = problemDetailsGroup.Key.LastName != null? problemDetailsGroup.Key.FirstName + " " + problemDetailsGroup.Key.LastName : "Sin asignar",
                                    quantity = problemDetailsGroup.Count()
                                };

                    return Json(data3, JsonRequestBehavior.AllowGet);

                default:
                    var data4 = problemDetails.Select(p => new
                    {
                        problemDetailId = p.ProblemDetailId,
                        problem = p.Problem.Name,
                        client = p.Client.Name,
                        department = p.Department.Name,
                        createdBy = p.User.FirstName + " " + p.User.LastName,
                        description = p.Description,
                        date = p.Date.ToString(),
                        place = p.Place.Name
                    });

                    return Json(data4, JsonRequestBehavior.AllowGet);

            }

            
        }

        public ActionResult StaticsReport()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem {Value = "0", Text = "Cliente", Selected = true },
                new SelectListItem {Value = "1", Text = "Tipo de Solicitud"},
                new SelectListItem {Value = "2", Text = "Departamento"},
                new SelectListItem {Value = "3", Text = "Asignacion"}
            };
            ViewBag.categorias = new SelectList(items, "Value", "Text");
            return View();
        }



        public ActionResult ManagementTime()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem {Value = "4", Text = "Todas", Selected = true },
                new SelectListItem {Value = "0", Text = "Cliente"},
                new SelectListItem {Value = "1", Text = "Tipo de Solicitud"},
                new SelectListItem {Value = "2", Text = "Departamento"},
                new SelectListItem {Value = "3", Text = "Tecnico"}
            };

            ViewBag.categorias = new SelectList(items, "Value", "Text");
            return View();
        }



        public JsonResult ManagementTimeReport(DateTime? startDate, DateTime? endDate, int categoria)
        {
            
            IQueryable<Solution> solutions;

            if (startDate != null && endDate != null)
            {
                solutions = db.Solutions.Where(p => p.Date >= startDate && p.Date <= endDate);
            }
            else if (startDate != null || endDate != null)
            {
                if (startDate != null)
                {
                    solutions = db.Solutions.Where(p => p.Date >= startDate);
                }
                else
                {
                    solutions = db.Solutions.Where(p => p.Date <= endDate);
                }
            }
            else
            {
                solutions = db.Solutions;
            }
            

            if(solutions.Count() > 0)
            {
                switch (categoria)
                {
                    case 0:
                        var data = from s in solutions
                                   group s by s.ProblemDetail.Client into
                                   SolutionsGroup
                                   where SolutionsGroup.Count() > 0
                                   select new
                                   {
                                       category = SolutionsGroup.Key.Name,
                                       hora1 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 3600).Count(), //Entre 1 hora
                                       hora4 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 3600 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 14400).Count(), //Entre 4 horas
                                       hora8 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 14400 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 28800).Count(), //Entre 8 hora
                                       hora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 28800 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 86400).Count(), //Entre 24 horas
                                       mhora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 86400).Count(), //Mayor a 24 horas
                                      total = SolutionsGroup.Count()

                                   };

                        return Json(data, JsonRequestBehavior.AllowGet);

                    case 1:
                        var data1 = from s in solutions
                                    group s by s.ProblemDetail.Problem
                                   into SolutionsGroup
                                    where SolutionsGroup.Count() > 0
                                    select new
                                    {
                                        category = SolutionsGroup.Key.Name,
                                        hora1 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 3600).Count(), //Entre 1 hora
                                        hora4 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 3600 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 14400).Count(), //Entre 4 horas
                                        hora8 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 14400 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 28800).Count(), //Entre 8 hora
                                        hora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 28800 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 86400).Count(), //Entre 24 horas
                                        mhora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 86400).Count(), //Mayor a 24 horas
                                        total = SolutionsGroup.Count()
                                    };

                        return Json(data1, JsonRequestBehavior.AllowGet);

                    case 2:
                        var data2 = from s in solutions
                                    group s by s.ProblemDetail.Department
                                   into SolutionsGroup
                                    where SolutionsGroup.Count() > 0
                                    select new
                                    {
                                        category = SolutionsGroup.Key.Name,
                                        hora1 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 3600).Count(), //Entre 1 hora
                                        hora4 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 3600 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 14400).Count(), //Entre 4 horas
                                        hora8 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 14400 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 28800).Count(), //Entre 8 hora
                                        hora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 28800 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 86400).Count(), //Entre 24 horas
                                        mhora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 86400).Count(), //Mayor a 24 horas
                                        total = SolutionsGroup.Count()
                                    };

                        return Json(data2, JsonRequestBehavior.AllowGet);

                    case 3:
                        var data3 = from s in solutions
                                    group s by s.User
                                    into SolutionsGroup
                                    where SolutionsGroup.Count() > 0
                                    select new
                                    {
                                        category = SolutionsGroup.Key.FirstName + " " + SolutionsGroup.Key.LastName,
                                        hora1 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 3600).Count(), //Entre 1 hora
                                        hora4 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 3600 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 14400).Count(), //Entre 4 horas
                                        hora8 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 14400 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 28800).Count(), //Entre 8 hora
                                        hora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 28800 && SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) <= 86400).Count(), //Entre 24 horas
                                        mhora24 = SolutionsGroup.Where(sg => SqlFunctions.DateDiff("SS", sg.ProblemDetail.Date, sg.Date) > 86400).Count(), //Mayor a 24 horas
                                        total = SolutionsGroup.Count()

                                    };

                        return Json(data3, JsonRequestBehavior.AllowGet);

                    default:
                        var data4 = new
                        {
                            hora1 = solutions.Where(s => SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) <= 3600).Count(),
                            hora4 = solutions.Where(s => SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) > 3600 && SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) <= 14400).Count(), //Entre 4 horas
                            hora8 = solutions.Where(s => SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) > 14400 && SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) <= 28800).Count(), //Entre 8 hora
                            hora24 = solutions.Where(s => SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) > 28800 && SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) <= 86400).Count(), //Entre 24 horas
                            mhora24 = solutions.Where(s => SqlFunctions.DateDiff("SS", s.ProblemDetail.Date, s.Date) > 86400).Count(), //Mayor a 24 horas
                            total = solutions.Count()
                        };


                        return Json(data4, JsonRequestBehavior.AllowGet);

                }
            }else
            {
                string data = "";
                return Json(data, JsonRequestBehavior.AllowGet);
            }

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