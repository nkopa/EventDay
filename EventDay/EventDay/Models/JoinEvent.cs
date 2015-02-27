using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class JoinEvent
    {
        [Key]
        public int JoinEventId { get; set; }

        [Required(ErrorMessage = "Użytkownik jest wymagany.")]
        public string Username { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime JoinDate { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Status jest wymagany.")]
        public int Status { get; set; }

        public virtual Event Event { get; set; }
    }
}