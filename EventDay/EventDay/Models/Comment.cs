using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class Comment
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CommentId { get; set; }

        [DisplayName("Treść")]
        [Required(ErrorMessage = "Content is required")]
        [StringLength(100)]
        public string Content { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateCreated { get; set; }

        public int EventId { get; set; }

        //public int CommentCategoryId { get; set; }

        public virtual Event Event { get; set; }

        //public virtual CommentCategory CommentCategory { get; set; }
    }
}