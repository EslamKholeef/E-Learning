using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistanceLearning.Models
{
    public class Note
    {
        public int Id { get; set; }

        [AllowHtml]
        public string Note_Text { get; set; }

        public string Course_Name { get; set; }

        public string NoteLogo { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public CourseModel Course { get; set; }
    }
}