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


    public class NotesController : Controller
    {
    public ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notes
        public ActionResult Index()
        {
            return View(db.Notes.ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note, HttpPostedFileBase Note_Logo_File)
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Where(a => a.Id == Id).SingleOrDefault();

            if (ModelState.IsValid)
            {

                //var dir = Directory.CreateDirectory(Server.MapPath("~/Content/NotesImages/" + currentUser.UserName));
                //string path = Path.Combine(Server.MapPath("~/Content/NotesImages/" + currentUser.UserName), Note_Logo_File.FileName);
                //Note_Logo_File.SaveAs(path);
                //note.NoteLogo = Note_Logo_File.FileName;

                var course = db.CourseModels.Where(cn => cn.CourseName == note.Course_Name).FirstOrDefault();
                var coursePublisher = db.Users.Where(a => a.Courses.Contains(course));

                note.NoteLogo = course.CourseLogo;
                note.Course = course;
                db.Notes.Add(note);
                currentUser.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index", "Manage", new { area = "" });
            }

            return View(note);
        }

        // GET: Notes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Note note = db.Notes.Find(id);
        //    if (note == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(note);
        //}

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id,string Note_Text)
        {
            Note note = db.Notes.Find(id);
            if (ModelState.IsValid)
            {
                //db.Entry(note).State = EntityState.Modified;
                note.Note_Text = HttpUtility.HtmlEncode(Note_Text);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Note note = db.Notes.Find(id);
        //    if (note == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(note);
        //}

        // POST: Notes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Note note = db.Notes.Find(id);
        //    var Id = User.Identity.GetUserId();
        //    var currentUser = db.Users.Where(a => a.Id == Id).SingleOrDefault();
        //    currentUser.Notes.Remove(note);
        //    db.Notes.Remove(note);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        [HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Where(a => a.Id == Id).SingleOrDefault();
            currentUser.Notes.Remove(note);
            db.Notes.Remove(note);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
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
        public PartialViewResult PartialNote(string name)
        {
            var note = db.Notes.Where(a => a.Course_Name == name).FirstOrDefault();
            return PartialView("_PartialNoteDetails", note);
        }
    }
}
