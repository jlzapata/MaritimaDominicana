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
    public class UsersController : Controller
    {
        private SupportContext db = new SupportContext();

        // GET: Users
        [LoginFilter, TypeFilter]
        public ActionResult Index()
        {
            var users = db.Users.Where(u => u.Active == true).Include(u => u.Type);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        [LoginFilter,TypeFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [LoginFilter,TypeFilter]
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilter,TypeFilter]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Pasword,Email,TypeId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Pasword = Models.User.EncyptPassword(user.Pasword);
                user.Active = true;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", user.TypeId);
            return View(user);
        }

        // GET: Users/Edit/5
        [LoginFilter,TypeFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", user.TypeId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilter,TypeFilter]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Pasword,Email,TypeId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", user.TypeId);
            return View(user);
        }

        // GET: Users/Delete/5
        [LoginFilter,TypeFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [LoginFilter, TypeFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            user.Active = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            if(Session["UserId"] != null)
            {
                return RedirectToAction("Index", "ProblemDetails");
            }

            HttpCookie myCookie = new HttpCookie("userId");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email, Pasword")] User user)
        {
            User usuario = db.Users.Where(u => u.Email == user.Email && u.Active == true).FirstOrDefault();
            if(usuario != null)
            {
                if (user.VerifyPassword(usuario.Pasword))
                {
                    usuario.Connected = true;
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["user"] = usuario.FullName;
                    Session["UserId"] = usuario.UserId;
                    Session["type"] = usuario.Type.Name;
                    HttpCookie myCookie = new HttpCookie("userId", usuario.UserId.ToString());
                    myCookie.Expires = DateTime.Now.AddDays(1d);
                    Response.Cookies.Add(myCookie);

                    return RedirectToAction("index","ProblemDetails");
                }
            }

            ViewBag.error = "Usuario o contraseña incorrecta.";
            return View();
            
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Logout()
        {

            User user = db.Users.Find(Session["UserId"]);
            if (user != null)
            {
                user.Connected = false;
                db.SaveChanges();
            }
            if(Request.Cookies["userId"] != null)
            {
                HttpCookie myCookie = new HttpCookie("userId");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Session.Clear();
            Session.Abandon();
            return View("Login");

        }

        [LoginFilter]
        public JsonResult GetUsers(int userId, string exp)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            User user = db.Users.Find(userId);
            if(user != null)
            {
                List<User> usersGlobalList = new List<Models.User>();
                List<User> followersList = new List<Models.User>();
                var myFollowers = user.Followeds.ToList().Select(u => new { UserId = u.UserId, Name = u.FirstName + " " + u.LastName, Email = u.Email }); //Me siguen

                if (exp == null || exp.Trim().Length == 0) {
                    usersGlobalList = db.Users.Where(u => u.UserId != userId && u.Active == true).ToList();
                    followersList = user.Followers.ToList(); //Yo sigo
                }
                else
                {
                    usersGlobalList = db.Users.Where( u => u.UserId != userId && u.Active == true &&
                                                          (u.FirstName.ToLower().Contains(exp.ToLower()) || 
                                                           u.LastName.ToLower().Contains(exp.ToLower()) || 
                                                           u.Email.ToLower().Contains(exp.ToLower()))
                                                    ).ToList();

                    followersList = user.Followers.Where(u =>u.Active == true && u.FirstName.ToLower().Contains(exp.ToLower()) ||
                                                           u.LastName.ToLower().Contains(exp.ToLower()) ||
                                                           u.Email.ToLower().Contains(exp.ToLower())
                                                     ).ToList();

                    myFollowers = user.Followeds.Where(u => u.Active == true && u.FirstName.ToLower().Contains(exp.ToLower()) ||
                                                    u.LastName.ToLower().Contains(exp.ToLower()) ||
                                                    u.Email.ToLower().Contains(exp.ToLower())
                    ).ToList().Select(u => new { UserId = u.UserId, Name = u.FirstName + " " + u.LastName, Email = u.Email });
                } 

                var followers = followersList.Select(f => new { UserId = f.UserId, Name = f.FirstName + " " + f.LastName, Email = f.Email });


                var noFollowers = usersGlobalList.Except(followersList, new UserComparer()).Select(u => new
                {
                    UserId = u.UserId,
                    Name = u.FirstName + " " + u.LastName,
                    Email = u.Email
                });

                var data = new
                {
                  noFollowers,
                    followers,
                    myFollowers
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Follow(int? userId, int? followerId)
        {
            if (userId == null || followerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(userId);
            User follower = db.Users.Find(followerId);

            if (user == null || follower == null)
            {
                return HttpNotFound();
            }
            try
            {
                follower.Followers.Add(user);
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
        public ActionResult UnFollow(int? userId, int? followerId)
        {
            if (userId == null || followerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(userId);
            User follower = db.Users.Find(followerId);

            if (user == null || follower == null)
            {
                return HttpNotFound();
            }
            try
            {
                follower.Followers.Remove(user);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

    class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (x.UserId == y.UserId)
                return true;

            return false;
        }

        public int GetHashCode(User obj)
        {
            return obj.UserId.GetHashCode();

        }
    }
}
