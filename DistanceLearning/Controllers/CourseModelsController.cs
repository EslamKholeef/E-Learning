using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DistanceLearning.Models;
using Microsoft.AspNet.Identity;

namespace DistanceLearning.Controllers
{
    [Authorize]
    public class CourseModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: CourseModels
        public ActionResult Index()
        {
            return View(db.CourseModels.ToList());
        }

        // GET: CourseModels/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = db.CourseModels.Find(id);
            ViewBag.CurrentCourse = courseModel.CourseName;

            ViewBag.courseID = id;
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        // GET: CourseModels/Create
        

        public ActionResult Create()
        {
            
            ViewBag.Categories = new SelectList(db.Categories, "Name", "Name");
            return View();
        }

        // POST: CourseModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(CourseModel courseModel, HttpPostedFileBase CourseLogo, HttpPostedFileBase CourseDemo)
        {
            string userId = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
            
            if (ModelState.IsValid)
            {
                foreach (var item in db.CourseModels.ToList())
                {
                    if(courseModel.Code == item.Code)
                    {
                        throw new InvalidOperationException("Course Code is already exists! try another one");

                    }
                }
                
                Directory.CreateDirectory(Server.MapPath("~/Content/Videos/"+courseModel.CourseName));
                // If directory does not exist, create it
                //if (!Directory.Exists(dir))
                //{
                //    Directory.CreateDirectory(dir);
                //}

                string path = Path.Combine(Server.MapPath("~/Content/CourseLogos"), CourseLogo.FileName);
                CourseLogo.SaveAs(path);
                courseModel.CourseLogo = CourseLogo.FileName;


                string path2 = Path.Combine(Server.MapPath("~/Content/Videos/"+courseModel.CourseName),CourseDemo.FileName);
                CourseDemo.SaveAs(path2);
                courseModel.DemoAboutTheCourse = CourseDemo.FileName;


                courseModel.MainPublisher = user;
                courseModel.RateOfLikes = 0;
                courseModel.RateOfUnLikes = 0;
                courseModel.FinalRatingDegree = 0;

                user.Courses.Add(courseModel);
                db.CourseModels.Add(courseModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(db.Categories, "Name", "Name", courseModel.CategoryId);
            return View(courseModel);
        }





        // GET: CourseModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = db.CourseModels.Find(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        // POST: CourseModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseModel courseModel, HttpPostedFileBase CourseLogo)
        {
            if (ModelState.IsValid)
            {
                
                if(CourseLogo != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/CourseLogos"), CourseLogo.FileName);
                    CourseLogo.SaveAs(path);
                    courseModel.CourseLogo = CourseLogo.FileName;

                }
                

                db.Entry(courseModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseModel);
        }

        // GET: CourseModels/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CourseModel courseModel = db.CourseModels.Find(id);
        //    if (courseModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(courseModel);
        //}

        // POST: CourseModels/Delete/5
        [HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ICollection<CoursesAndUsers> relatedDBTables = db.CoursesAndUsers.Where(x => x.TheCourse_Id == id).ToList();
            db.CoursesAndUsers.RemoveRange(relatedDBTables);

            ICollection<Note> Notes = db.Notes.Where(Note => Note.Course.Id == id).ToList();
            db.Notes.RemoveRange(Notes);


            CourseModel courseModel = db.CourseModels.Find(id);
            
            // to delete directory of the course .. this way
            var dir = Directory.CreateDirectory(Server.MapPath("~/Content/Videos/" + courseModel.CourseName));
            if (dir.Exists)
            {
                dir.Delete(true);
            }
            // to delete directory of the course  .. or this way by the specific path
            //Directory.Delete(@"H:\Eslam2020\2021\asp.net\Learning\DistanceLearning\DistanceLearning\Content\Videos\"+courseModel.CourseName, true);


            db.CourseModels.Remove(courseModel);

            db.SaveChanges();
            return RedirectToAction("Index","Manage");
        }


        [HttpGet]
        public ActionResult AddVideo( )
        {
            
            return View();
        }

        
       
        //[HttpPost]
        //public ActionResult AddVideo(string s)
        //{

        //    int? id = ViewBag.courseID;
        //    var course = db.CourseModels.Find(id);
        //    if (ModelState.IsValid)
        //    {

        //        course.Paths.Add(s);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index");

        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Register(int id)
        {

            CourseModel courseModel = db.CourseModels.Where(x => x.Id == id).FirstOrDefault();

            string userId = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();

            CoursesAndUsers c = new CoursesAndUsers();
            c.Course = courseModel;
            c.User = user;
            c.TheCourse_Id = id;
            c.TheUser_Id = userId;
            user.MyCourses.Add(c);
            courseModel.RegisteredUsers.Add(c);
            db.CoursesAndUsers.Add(c);
            db.SaveChanges();

            return RedirectToAction("index", "Manage");
        }


        [HttpGet]
        public ActionResult UnRegister(int id)
        {


            string userId = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();

            CoursesAndUsers c = db.CoursesAndUsers.Where(y => y.Id == id).FirstOrDefault();
            CourseModel courseModel = db.CourseModels.Where(x => x.Id == c.TheCourse_Id).FirstOrDefault();
            user.MyCourses.Remove(c);
            courseModel.RegisteredUsers.Remove(c);
            db.CoursesAndUsers.Remove(c);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }






        [HttpPost]
        public PartialViewResult Liked(int id)
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Where(a => a.Id == Id).SingleOrDefault();

            CourseModel courseModel = db.CourseModels.Where(x => x.Id == id).FirstOrDefault();
            

            if (courseModel.RateOfLikes == 0)
            {
                if (courseModel.RateOfUnLikes == 0)
                {
                    courseModel.RateOfLikes = courseModel.RateOfLikes + 1;
                    courseModel.FinalRatingDegree = courseModel.RateOfLikes * 100;
                }
                else
                {
                    courseModel.RateOfLikes = courseModel.RateOfLikes + 1;
                    courseModel.FinalRatingDegree = (((courseModel.RateOfLikes ) / ((courseModel.RateOfLikes ) + courseModel.RateOfUnLikes)) * 100);

                }

            }
            else
            {
                courseModel.RateOfLikes++;
                courseModel.FinalRatingDegree = (((courseModel.RateOfLikes) / ((courseModel.RateOfLikes) + courseModel.RateOfUnLikes)) * 100);

            }

            currentUser.LikedCourses.Add(courseModel);
            db.SaveChanges();
            return PartialView("_PartialCourseRate", courseModel);

        }


        [HttpPost]
        public PartialViewResult UnLiked(int id)
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Where(a => a.Id == Id).SingleOrDefault();

            CourseModel courseModel = db.CourseModels.Where(x => x.Id == id).FirstOrDefault();


            if (courseModel.RateOfUnLikes == 0)
            {
                if (courseModel.RateOfLikes == 0)
                {
                    courseModel.RateOfUnLikes = courseModel.RateOfUnLikes + 1;
                    courseModel.FinalRatingDegree = 0;
                    //courseModel.FinalRatingDegree = (courseModel.RateOfLikes / (courseModel.RateOfLikes + courseModel.RateOfUnLikes)) * 100;

                }
                else
                {
                    courseModel.RateOfUnLikes = courseModel.RateOfUnLikes + 1;
                    courseModel.FinalRatingDegree = (courseModel.RateOfLikes / (courseModel.RateOfLikes + courseModel.RateOfUnLikes)) * 100;
                }

            }


            else
            {
                if (courseModel.RateOfLikes == 0)
                {
                    courseModel.RateOfUnLikes = courseModel.RateOfUnLikes + 1;
                    courseModel.FinalRatingDegree = 0;
                    //courseModel.FinalRatingDegree = (courseModel.RateOfLikes / (courseModel.RateOfLikes + courseModel.RateOfUnLikes)) * 100;

                }
                else
                {
                    courseModel.RateOfUnLikes = courseModel.RateOfUnLikes + 1;
                    courseModel.FinalRatingDegree = (courseModel.RateOfLikes / (courseModel.RateOfLikes + courseModel.RateOfUnLikes)) * 100;

                }

            }


            currentUser.LikedCourses.Add(courseModel);

            db.SaveChanges();
            return PartialView("_PartialCourseRate", courseModel);
        }





        /*******************
         *    Comments
         *******************/
         [HttpPost]
        public ActionResult AddComment(int id, string text, string userId)
        {
            //var Id = User.Identity.GetUserId();
            ApplicationUser User = db.Users.Where(a => a.Id == userId).FirstOrDefault();

            CourseModel courseModel = db.CourseModels.Where(x => x.Id == id).FirstOrDefault();

            Comment comment = new Comment();
            comment.TheComment = text;
            comment.CourseId = id;
            comment.UserId = userId;
            comment.Course = courseModel;
            comment.User = User;
            comment.StudentName = User.UserName;
            comment.StudentLogo = User.ProfileImg;
            comment.CommentDate = DateTime.Now;

            User.Comments.Add(comment);
            courseModel.Comments.Add(comment);
            db.Comments.Add(comment);


            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }


        [HttpPost]
        public PartialViewResult PartialAddComment(int id, string text, string userId)
        {
            //var Id = User.Identity.GetUserId();
            ApplicationUser User = db.Users.Where(a => a.Id == userId).FirstOrDefault();

            CourseModel courseModel = db.CourseModels.Where(x => x.Id == id).FirstOrDefault();

            Comment comment = new Comment();
            comment.TheComment = text;
            comment.CourseId = id;
            comment.UserId = userId;
            comment.Course = courseModel;
            comment.User = User;
            comment.StudentName = User.UserName;
            comment.StudentLogo = User.ProfileImg;
            comment.CommentDate = DateTime.Now;

            User.Comments.Add(comment);
            courseModel.Comments.Add(comment);
            db.Comments.Add(comment);


            db.SaveChanges();

            return PartialView("_Comments",courseModel);
        }


        [HttpPost]
        public ActionResult DeleteComment(int id, int courseId, string UserId)
        {

            var comment = db.Comments.Where(x => x.Id == id).FirstOrDefault();

            var currentUser = db.Users.Where(a => a.Id == UserId).SingleOrDefault();

            var courseModel = db.CourseModels.Where(course => course.Id == courseId).FirstOrDefault();

            
            currentUser.Comments.Remove(comment);
            courseModel.Comments.Remove(comment);
            db.Comments.Remove(comment);
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }



        [HttpPost]
        public ActionResult PartialDeleteComment(int id, int courseId, string UserId)
        {

            var comment = db.Comments.Where(x => x.Id == id).FirstOrDefault();

            var currentUser = db.Users.Where(a => a.Id == UserId).SingleOrDefault();

            var courseModel = db.CourseModels.Where(course => course.Id == courseId).FirstOrDefault();


            currentUser.Comments.Remove(comment);
            courseModel.Comments.Remove(comment);
            db.Comments.Remove(comment);
            db.SaveChanges();

            return PartialView("_Comments", courseModel);
        }


        [HttpGet]
        public ActionResult ViewComments(int id)
        {
            var courseModel = db.CourseModels.Where(course => course.Id == id).FirstOrDefault();

            return PartialView("_Comments",courseModel.Comments.ToList());
        }



        [HttpPost]

        public PartialViewResult PartialCreate(Videos videos, HttpPostedFileBase UploadVideo)
        {
            CourseModel course = db.CourseModels.Where(x => x.Code == videos.CourseCode).First();
            ViewBag.CurrentCourse = course.CourseName;
            string path = Path.Combine(Server.MapPath("~/Content/Videos/" + course.CourseName), UploadVideo.FileName);
            UploadVideo.SaveAs(path);
            videos.Path = UploadVideo.FileName;

            course.Videos.Add(videos);
            videos.CourseModelId = course.Id;
            db.Videos.Add(videos);


            db.SaveChanges();
            //return RedirectToAction("Index");
            return PartialView("_CourseList", course);



        }

        [HttpPost]
        public PartialViewResult PartialDeleteVideo (int? id, int? courseId)
        {
            var course = db.CourseModels.Find(courseId);
            ViewBag.CurrentCourse = course.CourseName;
            Videos v = db.Videos.First(vi => vi.Id == id);
            course.Videos.Remove(v);
            db.Videos.Remove(v);

            return PartialView("_CourseList", course);

        }
    }
}
