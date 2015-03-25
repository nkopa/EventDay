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

        [DisplayName("Widoczne/Prywatne")]
        [Required(ErrorMessage = "Wymagane")]
        public string AccessId { get; set; }

        [DisplayName("Otwarte/Zamknięte")]
        [Required(ErrorMessage = "Wymagane")]
        public string RecruitmentId { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [DisplayName("Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(100)]
        public string Title { get; set; }

        [DisplayName("Opis")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(1000)]
        public string Description { get; set; }

        [DisplayName("ilosc miejsc")]
        public int Capacity { get; set; }

        [DisplayName("Regulamin")]
        //[StringLength(100)]
        //scierzka do pdf
        public string Regulations { get; set; }

        [DisplayName("Baner")]
        //[StringLength(100)]
        //scierzka do pliku
        public string ProfileImage { get; set; }

        //[DisplayName("Strój")]
        //[StringLength(100)]
        //public string DressCode { get; set; }

        [DisplayName("Cena biletu")]
        //[StringLength(100)]
        [RegularExpression("[0-9]*,{0,1}[0-9]{1,2}$", ErrorMessage = "Cena nieprawidłowy format")]
        public decimal Price { get; set; }

        [DisplayName("Numer kontaktowy")]
        [StringLength(7)]
        public string ContactNumber { get; set; }

        [DisplayName("Email")]
        [StringLength(25)]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail nieprawidłowy format")]
        public string ContactEmail { get; set; }

        [DisplayName("Www")]
        [StringLength(30)]
        public string Website { get; set; }

        //DATY***********************
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateCreated { get; set; }

        [DisplayName("Data rozpoczęcia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateBegin { get; set; }

        [DisplayName("Data zakończenia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Godzina rozpoczęcia wydarzenia")]
        [Required(ErrorMessage = "Godzina jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public string HourBegin { get; set; }

        [DisplayName("Godzina zakończenia wydarzenia")]
        [Required(ErrorMessage = "Godzina jest wymagana")]
        public string HourEnd { get; set; }

        //litorwka DateBeginRegist(r)ation
        [DisplayName("Data rozpoczęcia rejestracji")]
        [Required(ErrorMessage = "Data jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateBeginRegistation { get; set; }

        [DisplayName("Godzina rozpoczęcia rejestracji")]
        [Required(ErrorMessage = "Godzina jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public string HourBeginRegistration { get; set; }

        //litorwka DateBeginRegist(r)ation
        [DisplayName("Data zakończenia rejestracji")]
        [Required(ErrorMessage = "Data jest wymagana")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEndRegistation { get; set; }

        [DisplayName("Godzina zakończenia rejestracji")]
        [Required(ErrorMessage = "Godzina jest wymagana")]
        public string HourEndRegistration { get; set; }

        //ADDRESS*************************

        [DisplayName("Miasto")]
        [Required(ErrorMessage = "Miejscowosc jest wymagana")]
        public string City { get; set; }

       /* [DisplayName("Lokalizacja")]
        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        public string Locality { get; set; }*/

        [DisplayName("Województwo")]
        [StringLength(20)]
        public string Voivoweship { get; set; }

        [DisplayName("Ulica")]
        [StringLength(20)]
        public string Street { get; set; }

        [DisplayName("Numer domu")]
        [StringLength(20)]
        public string HouseNumber { get; set; }

        [DisplayName("Numer lokalu")]
        [StringLength(20)]
        public string ApartmentNumber { get; set; }

        [DisplayName("Kod pocztowy")]
        [StringLength(6)]
        [RegularExpression("[0-9][0-9]-[0-9][0-9][0-9]$", ErrorMessage = "Kod pocztowy nieprawidłowy format")]
        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        public string ZipCode { get; set; }

        /*moze zamienic na mape google?*/
        [DisplayName("Wskazówki dojazdu")]
        [StringLength(300)]
        public string Directions { get; set; }

        //VIRTUAL**********************

        [DisplayName("Kategoria")]
        public virtual Category Category { get; set; } /*wirtualna sprawia ze dodaje sie kategoria*/

        public virtual IList<JoinEvent> JoinedUsers { get; set; }

        public virtual ICollection<Image> Image { get; set; }
        //public virtual IList<Image> Image { get; set; }
    }
}