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

namespace DistanceLearning.Controllers
{
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index()
        {
            return View(db.Videos.ToList());
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }
            return View(videos);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Videos videos,HttpPostedFileBase UploadVideo)
        {
            CourseModel course = db.CourseModels.Where(x => x.Code == videos.CourseCode).First();
            if (course == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Content/Videos/"+course.CourseName), UploadVideo.FileName);
                UploadVideo.SaveAs(path);
                videos.Path = UploadVideo.FileName;
                
                course.Videos.Add(videos);
                videos.CourseModelId = course.Id;
                db.Videos.Add(videos);
                

                db.SaveChanges();
                //return RedirectToAction("Index");
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(videos);
        }


        [HttpPost]
        
        public PartialViewResult PartialCreate(Videos videos, HttpPostedFileBase UploadVideo)
        {
            CourseModel course = db.CourseModels.Where(x => x.Code == videos.CourseCode).First();
            
            
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

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }
            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videos);
        }

        // GET: Videos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Videos videos = db.Videos.Find(id);
        //    if (videos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(videos);
        //}

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int courseId)
        {
            var course = db.CourseModels.Find(courseId);
            Videos videos = db.Videos.Find(id);
            course.Videos.Remove(videos);

            db.Videos.Remove(videos);
            db.SaveChanges();
            //return RedirectToAction("Index");
            //return Redirect(Request.UrlReferrer.ToString());
            return PartialView("_CourseList", course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public PartialViewResult AddSrcToBigVideo(string SrcName)
        {
           
            ViewBag.BigVideoSrc = SrcName;
            
            return PartialView("_BigVideo");
        }
    }
}
