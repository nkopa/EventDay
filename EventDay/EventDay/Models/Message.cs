using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class Message
    {
        [Key]
        [ScaffoldColumn(false)]
        public int MessageId { get; set; }

        [ScaffoldColumn(false)]
        public string FromUser { get; set; }

        [DisplayName("Odbiorca")]
        [Required(ErrorMessage = "Odbiorca jest wymagany.")]
        public string ToUser { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SendDate { get; set; }

        [DisplayName("Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(100)]
        public string Title { get; set; }

        [DisplayName("Wiadomość")]
        [Required(ErrorMessage = "Wiadomość jest wymagana.")]
        public string MessageText { get; set; }

        [ScaffoldColumn(false)]
        public bool IsRead { get; set; }
    }
}