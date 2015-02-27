using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class Image
    {
        [Key]
        [ScaffoldColumn(false)]
        public int GalleryId { get; set; }

        [DisplayName("Tytuł")]
        public string Name { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; }

        [ForeignKey("Event")]
        [ScaffoldColumn(false)]
        public string EventId { get; set; }

        [ForeignKey("User")]
        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime AddDate { get; set; }

        [ScaffoldColumn(false)]
        public string Url { get; set; }
    }
}