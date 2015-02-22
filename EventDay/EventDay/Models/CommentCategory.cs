using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class CommentCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CommentCategoryId { get; set; }

        public string Name { get; set; }
    }
}