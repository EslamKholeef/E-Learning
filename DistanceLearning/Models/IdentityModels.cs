using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DistanceLearning.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string UserType { get; set; }
        public string ProfileImg { get; set; }
        public string AboutUser { get; set; }

        public virtual ICollection<CourseModel> Courses { get; set; }
        public virtual ICollection<CoursesAndUsers> MyCourses { get; set; }
        public virtual ICollection<CourseModel> LikedCourses { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }



        //public virtual ICollection<ChatsAndUsers> Chats { get; set; }
        //public virtual ICollection<Chat> MyChats { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DistanceLearning.Models.CourseModel> CourseModels { get; set; }

        public System.Data.Entity.DbSet<DistanceLearning.Models.CoursesAndUsers> CoursesAndUsers { get; set; }

        public System.Data.Entity.DbSet<DistanceLearning.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<DistanceLearning.Models.Videos> Videos { get; set; }

        public System.Data.Entity.DbSet<DistanceLearning.Models.Note> Notes { get; set; }

        public System.Data.Entity.DbSet<DistanceLearning.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<DistanceLearning.Models.Chat> Chats { get; set; }
        public System.Data.Entity.DbSet<DistanceLearning.Models.Message> Messages { get; set; }
        
    }
}