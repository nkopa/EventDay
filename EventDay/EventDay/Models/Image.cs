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
        public int ImageId { get; set; }

        [DisplayName("Tytuł")]
        public string Name { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; }

        [ForeignKey("Event")]
        [ScaffoldColumn(false)]
        public int EventId { get; set; }

        //pozostalosc po koncepcji wielu moderatorow jednego eventu
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime AddDate { get; set; }

        [ScaffoldColumn(false)]
        public string Url { get; set; }

        public virtual Event Event { get; set; }
    }
}