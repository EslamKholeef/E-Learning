using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistanceLearning.Models
{
    public class Message
    {

        public string Sender { get; set; }

        public Chat Chat { get; set; }

        public int Id { get; set; }

        [AllowHtml]
        public string Message_Content { get; set; }

        public DateTime Send_Time { get; set; }
    }
}