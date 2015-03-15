using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentCategory> CommentCategory { get; set; }
        public DbSet<JoinEvent> JoinEvent { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<UserContact> UserContact { get; set; }
    }
}