using MaritimaDominicana.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using static System.Net.Mime.MediaTypeNames;
using MaritimaDominicana.ViewModels;

namespace MaritimaDominicana.Controllers
{
    
    public class ProblemDetailsController : Controller
    {

        private SupportContext db = new SupportContext();

        // GET: ProblemDetails
        public ActionResult Index()
        {
            if(Session["userId"] == null)
            {
                HttpCookie myCookie = new HttpCookie("userId");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            IQueryable<ProblemDetail> problemDetails;
                problemDetails = db.ProblemDetails.Where(p => p.state != 3).OrderBy(p => p.Date)
                    .Include(p => p.Department).Include(p => p.Problem).Include(p => p.User).Include(p => p.Place).Include(p => p.Client).OrderByDescending(p => p.ProblemDetailId);

            return View(problemDetails);
        }

        // GET: ProblemDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userId"] == null)
            {
                HttpCookie myCookie = new HttpCookie("userId");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProblemDetail problemDetail = db.ProblemDetails.Find(id);
            if (problemDetail == null)
            {
                return HttpNotFound();
            }
            return View(problemDetail);
        }

        // GET: ProblemDetails/Create
        [LoginFilter]
        public ActionResult Create()
        {
            ViewBag.PlaceId = new SelectList(db.Places, "PlaceId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.ProblemId = new SelectList(db.Problems, "ProblemId", "Name");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");

            return View();
        }

        // POST: ProblemDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilter]
        public ActionResult Create([Bind(Include = "ProblemDetailId,ProblemId,ClientId,Title,DepartmentId,Description,PlaceId")] ProblemDetail problemDetail)
        {
            if (ModelState.IsValid)
            {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                    try
                    {
                        problemDetail.CreatedBy = (int)Session["UserId"];
                        problemDetail.Date = DateTime.Now;
                        problemDetail.state = 1;
                        db.ProblemDetails.Add(problemDetail);
                        db.SaveChanges();

                        if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                        {
                            int id = db.ProblemDetails.Max(p => p.ProblemDetailId);
                            string path = System.AppDomain.CurrentDomain.BaseDirectory;
                            Directory.CreateDirectory(path + "\\Uploads\\Images\\" + id);
                            Directory.CreateDirectory(path + "\\Uploads\\PDFs\\" + id);

                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                string fileName = Request.Files[i].FileName;
                                string ext = fileName.Substring(fileName.LastIndexOf("."));

                                if (ext == ".pdf")
                                {
                                    //Request.Files[i].SaveAs(path + "\\Uploads\\PDFs\\" + id + "\\" + i + ext);
                                     Request.Files[i].SaveAs(path + "\\Uploads\\PDFs\\"+ id+ "\\" +fileName);

                                }
                                else
                                {
                                    //Request.Files[i].SaveAs(path + "\\Uploads\\Images\\" + id + "\\" + i + ext);
                                    Request.Files[i].SaveAs(path + "\\Uploads\\Images\\" +id+"\\"+ fileName);
                                }
                            }

                        }

                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        string exc = ex.Message;
                        transaction.Rollback();
                        ViewBag.PlaceId = new SelectList(db.Places, "PlaceId", "Name");
                        ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", problemDetail.DepartmentId);
                        ViewBag.ProblemId = new SelectList(db.Problems, "ProblemId", "Name", problemDetail.ProblemId);
                        ViewBag.CreatedBy = new SelectList(db.Users, "UserId", "FirstName", problemDetail.CreatedBy);
                        ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", problemDetail.ClientId);
                        return View(problemDetail);

                    }
                    }

            }

            ViewBag.PlaceId = new SelectList(db.Places, "PlaceId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", problemDetail.DepartmentId);
            ViewBag.ProblemId = new SelectList(db.Problems, "ProblemId", "Name", problemDetail.ProblemId);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserId", "FirstName", problemDetail.CreatedBy);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", problemDetail.ClientId);
            return View(problemDetail);
        }

        // GET: ProblemDetails/Edit/5
        [LoginFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProblemDetail problemDetail = db.ProblemDetails.Find(id);
            if (problemDetail == null)
            {
                return HttpNotFound();
            }
            List<state> states = new List<state>()
            {
                new state {StateId =1, Name="Abierto" },
                new state {StateId =2, Name="Asignado" },
                new state {StateId =3, Name="Cerrado" }
            };

            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            ViewBag.Images = null;
            ViewBag.PDFs = null;
            try
            {
                var images = Directory.GetFiles(path + "Uploads\\Images\\" + id);

                if (images.Length < 1)
                {
                    throw new Exception();
                }

                string[] imagenes = new string[images.Length];
                for (int i = 0; i < images.Length; i++)
                {
                    int index = images[i].LastIndexOf("\\");
                    imagenes[i] = images[i].Substring(index);
                }

                ViewBag.Images = imagenes;
            }
            catch (Exception)
            {
                ViewBag.Images = null;
            }
            try
            {
                var files = Directory.GetFiles(path + "Uploads\\PDFs\\" + id);

                if (files.Length < 1)
                {
                    throw new Exception();
                }
                string[] PDFs = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    int index = files[i].LastIndexOf("\\");
                    PDFs[i] = files[i].Substring(index);
                }

                ViewBag.PDFs = PDFs;
            }
            catch (Exception)
            {
                ViewBag.PDFs = null;
            }

            ViewBag.PlaceId = new SelectList(db.Places, "PlaceId", "Name", problemDetail.PlaceId);
            ViewBag.StateId = new SelectList(states, "StateId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", problemDetail.DepartmentId);
            ViewBag.ProblemId = new SelectList(db.Problems, "ProblemId", "Name", problemDetail.ProblemId);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserId", "FirstName", problemDetail.CreatedBy);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", problemDetail.ClientId);

            return View(problemDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilter]
        public ActionResult Edit([Bind(Include = "ProblemDetailId,ProblemId,Title,DepartmentId,Description,Date,CreatedBy,ClientId,PlaceId,AssignedTo")] ProblemDetail problemDetail)
        {
            problemDetail.Modified_by = (int)Session["UserId"];
            problemDetail.Update_at = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(problemDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceId = new SelectList(db.Places, "PlaceId", "Name", problemDetail.PlaceId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", problemDetail.DepartmentId);
            ViewBag.ProblemId = new SelectList(db.Problems, "ProblemId", "Name", problemDetail.ProblemId);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserId", "FirstName", problemDetail.CreatedBy);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", problemDetail.ClientId);

            return View(problemDetail);
        }

        // GET: ProblemDetails/Delete/5
        [LoginFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProblemDetail problemDetail = db.ProblemDetails.Find(id);
            if (problemDetail == null)
            {
                return HttpNotFound();
            }
            return View(problemDetail);
        }

        // POST: ProblemDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [LoginFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            ProblemDetail problemDetail = db.ProblemDetails.Find(id);
            db.ProblemDetails.Remove(problemDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [LoginFilter]
        public ActionResult Asignar(int problemDetailId, int userId)
        {
            try
            {
                ProblemDetail problemDetail = db.ProblemDetails.Find(problemDetailId);
                Models.User user = db.Users.Find(userId);

                if (problemDetail == null || user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                problemDetail.Assigned = user;
                problemDetail.state = 2;
                problemDetail.AssignedAt = DateTime.Now;
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Close([Bind(Include = "SolutionDescription, SolutionId")] Solution solution, int ProblemDetailId)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                ProblemDetail problemDetail = db.ProblemDetails.Find(ProblemDetailId);
                if(problemDetail != null)
                {
                    try
                    {
                        solution.UserId = (int)Session["UserId"];
                        solution.SolutionId = ProblemDetailId;
                        solution.Date = DateTime.Now;
                        db.Entry(solution).State = EntityState.Added;
                        problemDetail.state = 3;
                        problemDetail.AssignedTo = null;

                        db.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        string excepcion = ex.Message;
                        transaction.Rollback();
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

    }

        [LoginFilter]
        public JsonResult getFollowedEntries(int id)
        {
            var problemDetails = db.Database.SqlQuery<NotificationProblemDetail>("Select p.Name as Problema, pd.Title as Titulo, d.Name as Departamento, u.FirstName +' ' + u.LastName as [CreadoPor], pd.Description as Descripcion from ProblemDetails pd inner join Problems p " +
                                                                                     "on pd.ProblemId = p.ProblemId inner join Departments d " +
                                                                                     "on pd.DepartmentId = d.DepartmentId inner join Users u " +
                                                                                     "on pd.CreatedBy = u.UserId inner join Places pl " +
                                                                                     "on pd.PlaceId = pl.PlaceId " +
                                                                                     "where DATEDIFF(SS,Date, GETDATE()) <= 20 AND CreatedBy = SOME( " +
                                                                                     "SELECT UserId FROM Followers " +
                                                                                     "where FollowerId = {0})", id).ToList();
        
            return Json(problemDetails, JsonRequestBehavior.AllowGet);
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

    public class state
    {
        public int StateId { get; set; }
        public string Name { get; set; }  
    } 
}
