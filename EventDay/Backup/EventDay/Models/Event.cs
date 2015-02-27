using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class Event
    {
        [Key]
        [ScaffoldColumn(false)]
        public int EventId { get; set; }

       
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateCreated { get; set; }

        [DisplayName("Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(100)]
        public string Title { get; set; }

        [DisplayName("Data rozpoczęcia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateBegin { get; set; }

        [DisplayName("Data zakończenia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Opis")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        //[StringLength(100)]
        public string Description { get; set; }

        [DisplayName("Miasto")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [DisplayName("Lokalizacja")]
        [Required(ErrorMessage = "Locality is required")]
        public string Locality { get; set; }

         [DisplayName("Kategoria")]
        public virtual Category Category { get; set; } /*wirtualna sprawia ze dodaje sie kategoria*/

    }
}