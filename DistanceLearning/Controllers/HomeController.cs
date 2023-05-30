using DistanceLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistanceLearning.Controllers
{
    public class HomeController : Controller
    {
        
        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Search(string Title)
        {
            var Result = db.CourseModels.Where(x => x.CourseName.Contains(Title)||
                                                x.CategoryName.Contains(Title)||
                                                x.MainPublisher.UserName.Contains(Title)
                                                ).ToList();
            return View(Result);
        }
    }
}