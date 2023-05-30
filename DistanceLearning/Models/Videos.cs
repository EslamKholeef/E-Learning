using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistanceLearning.Models
{
    public class Videos
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string VideoTitle { get; set; }
        public string CourseCode { get; set; }

        public int CourseModelId { get; set; }
        public virtual CourseModel CourseModel { get; set; }
    }
}