using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistanceLearning.Models
{
    public class CoursesAndUsers
    {
        public int Id { get; set; }

        public int TheCourse_Id { get; set; }
        public virtual CourseModel Course { get; set; }

        public string TheUser_Id { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}