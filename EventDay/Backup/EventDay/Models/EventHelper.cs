using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class EventHelper
    {
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Event> Events { get; set; }
        public List<Category> Categories { get; set; }
    }
}