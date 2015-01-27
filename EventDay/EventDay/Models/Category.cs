using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class Category
    {

        [Key]
        [ScaffoldColumn(false)]
        public int CategoryId { get; set; }

        [DisplayName("Kategoria")]
        public string Name { get; set; }

        public List<Event> Events { get; set; }
    }
}