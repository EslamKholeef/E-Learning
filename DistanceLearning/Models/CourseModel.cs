using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistanceLearning.Models
{
    public class CourseModel
    {
        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }
        [AllowHtml]
        public string AboutTheCourse { get; set; }
        public string DemoAboutTheCourse { get; set; }
        public string CourseLogo { get; set; }
        public string CategoryName { get; set; }
        public double RateOfLikes { get; set; }
        public double RateOfUnLikes { get; set; }
        public double FinalRatingDegree { get; set; }


        public virtual ApplicationUser MainPublisher { get; set; }
        public virtual ICollection<CoursesAndUsers> RegisteredUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }



        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Code { get; set; }
        public virtual ICollection<Videos> Videos { get; set; }

        
    }
}