using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DistanceLearning.Models;
using Microsoft.AspNet.Identity;

namespace DistanceLearning.Controllers
{
    public class ChatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chats
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var Chats = db.Chats.Where(f => f.FirstUserId == id || f.SecondUserId == id);
            return View(Chats.ToList());
        }

        // GET: Chats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // GET: Chats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Chats.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chat);
        }

        // GET: Chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: Chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chat);
        }

        // GET: Chats/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Chat chat = db.Chats.Find(id);
        //    if (chat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chat);
        //}

        // POST: Chats/Delete/5

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {

            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            Chat chat = db.Chats.Find(id);
            ICollection<Message> Msgs = db.Messages.Where(Ms => Ms.Chat.Id == id).ToList();

            db.Messages.RemoveRange(Msgs);

            db.Chats.Remove(chat);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        public ActionResult CreateChat(string id)
        {
            var Id = User.Identity.GetUserId();
            var currentUser = db.Users.Find(Id);
            var SecondUser = db.Users.Find(id);



            Chat chat = new Chat();
            chat.FirstUserId = User.Identity.GetUserId();
            chat.SecondUserId = id;
            chat.First_Sender_Pic = db.Users.Where(u => u.Id == chat.FirstUserId).FirstOrDefault().ProfileImg;
            chat.Second_Sender_Pic = db.Users.Where(u => u.Id == chat.SecondUserId).FirstOrDefault().ProfileImg;


            foreach (var item in db.Chats)
            {
                if((item.FirstUserId==id && item.SecondUserId==Id)||(item.SecondUserId == id && item.FirstUserId == Id))
                {

                    return Redirect(Request.UrlReferrer.ToString());
                }
                //else if (SecondUser.UserChats.Contains(chat))
                //{
                //    currentUser.UserChats.Add(chat);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }

            db.Chats.Add(chat);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult SendMsg (string content, int id)
        {
            var chat = db.Chats.Where(i => i.Id == id).FirstOrDefault();
            var SenderId = User.Identity.GetUserId();
            var Sender = db.Users.Find(SenderId);


            Message msg = new Message();
            msg.Chat = chat;
            msg.Message_Content = content;
            msg.Sender = Sender.UserName;
            msg.Send_Time = DateTime.Now;

            chat.Messages.Add(msg);

            db.SaveChanges();
            return PartialView("_ChatWindow", chat);

        }

        [HttpPost]
        public ActionResult ChatWindow (int id)
        {
            var chat = db.Chats.Find(id);
            return PartialView("_ChatWindow",chat);
        }





    }
}
