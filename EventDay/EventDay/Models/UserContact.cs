using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class UserContact
    {
        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public int UserContactId { get; set; }

        //[ForeignKey("UserProfile")]
        public int UserOwnerId { get; set; }

        //[ForeignKey("UserProfileId")]
        public int UserMemberId { get; set; }

        //[ForeignKey("UserId")]
        public virtual UserProfile UserOwner { get; set; }
        public virtual UserProfile UserMember { get; set; }
    }
}