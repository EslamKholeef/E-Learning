using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistanceLearning.Models
{
    public class Comment
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string StudentName { get; set; }
        public string StudentLogo { get; set; }

        public int CourseId { get; set; }
        public CourseModel Course { get; set; }

        public int Id { get; set; }

        public string TheComment { get; set; }

        public DateTime CommentDate { get; set; }
    }
}