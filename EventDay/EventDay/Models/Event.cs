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

/*
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

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [DisplayName("Widoczne/Prywatne")]
        public string AccessId { get; set; }

        [DisplayName("Otwarte/Zamknięte")]
        public string RecruitmentId { get; set; }

        [DisplayName("Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(100)]
        public string Title { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [DisplayName("ilosc miejsc")]
        public int AccessId { get; set; }

        [DisplayName("Opis")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        //[StringLength(100)]
        public string Description { get; set; }

        [DisplayName("Regulamin")]
        //[StringLength(100)]
        //scierzka do pdf
        public string Regulations { get; set; }

        [DisplayName("Strój")]
        //[StringLength(100)]
        public string DressCode { get; set; }

        [DisplayName("Bilety")]
        //[StringLength(100)]
        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [DisplayName("Numer kontaktowy")]
        public string ContactNumber { get; set; }

        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateCreated { get; set; }

        //DATY 

        [DisplayName("Data rozpoczęcia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateBeginEvent { get; set; }

        [DisplayName("Godzina rozpoczęcia wydarzenia")]
        public DateTime HourBeginEvent { get; set; }

        [DisplayName("Przybliżona długość wydarzenia")]
        public string TimeEvent { get; set; }

        [DisplayName("Data rozpoczęcia rejestracji")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateBeginRegistation { get; set; }

        [DisplayName("Godzina rozpoczęcia wydarzenia")]
        public DateTime HourBeginRegistration { get; set; }

        [DisplayName("Data zakończenia rejestracji")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateEndRegistation { get; set; }

        [DisplayName("Godzina zakończenia wydarzenia")]
        public DateTime HourEndRegistration { get; set; }

        //ADDRESS
 
        [DisplayName("Miasto")]
        public string City { get; set; }
 
        [DisplayName("Województwo")]
        public string Voivoweship { get; set; }

        [DisplayName("Numer domu")]
        public string HouseNumber { get; set; }

        [DisplayName("Numer lokalu")]
        public string ApartmentNumber { get; set; }

        [DisplayName("Kod pocztowy")]
        public string ZipCode { get; set; }

        [DisplayName("Wskazówki dojazdu")]
        public string Directions { get; set; }
 
        [DisplayName("Kategoria")]
        public virtual Category Category { get; set; } //wirtualna sprawia ze dodaje sie kategoria

        public List<Image> Images { get; set; }
       //public List<Users> Users { get; set; }
    }
}
 
*/