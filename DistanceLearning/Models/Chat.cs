using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistanceLearning.Models
{
    public class Chat
    {

        public int Id { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        //public virtual ICollection<ChatsAndUsers> Users { get; set; }

        
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }

        
        public string First_Sender_Pic { get; set; }
        public string Second_Sender_Pic { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        
    }
}