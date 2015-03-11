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
        public string AccessId { get; set; }

        [DisplayName("Otwarte/Zamknięte")]
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

        [DisplayName("Bilety")]
        //[StringLength(100)]
        public decimal Price { get; set; }

        [DisplayName("Numer kontaktowy")]
        public string ContactNumber { get; set; }

        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [DisplayName("Www")]
        public string Website { get; set; }

        //DATY***********************
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateCreated { get; set; }

        [DisplayName("Data rozpoczęcia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateBegin { get; set; }

        [DisplayName("Data zakończenia wydarzenia")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Godzina rozpoczęcia wydarzenia")]
        public string HourBegin { get; set; }

        [DisplayName("Godzina zakończenia wydarzenia")]
        public string HourEnd { get; set; }

        [DisplayName("Data rozpoczęcia rejestracji")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateBeginRegistation { get; set; }

        [DisplayName("Godzina rozpoczęcia rejestracji")]
        public string HourBeginRegistration { get; set; }

        [DisplayName("Data zakończenia rejestracji")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateEndRegistation { get; set; }

        [DisplayName("Godzina zakończenia wydarzenia")]
        public string HourEndRegistration { get; set; }

        //ADDRESS*************************

        [DisplayName("Miasto")]
        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string City { get; set; }

        [DisplayName("Lokalizacja")]
        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        public string Locality { get; set; }

        [DisplayName("Województwo")]
        public string Voivoweship { get; set; }

        [DisplayName("Ulica")]
        public string Street { get; set; }

        [DisplayName("Numer domu")]
        public string HouseNumber { get; set; }

        [DisplayName("Numer lokalu")]
        public string ApartmentNumber { get; set; }

        [DisplayName("Kod pocztowy")]
        public string ZipCode { get; set; }

        /*moze zamienic na mape google?*/
        [DisplayName("Wskazówki dojazdu")]
        public string Directions { get; set; }

        //VIRTUAL**********************

        [DisplayName("Kategoria")]
        public virtual Category Category { get; set; } /*wirtualna sprawia ze dodaje sie kategoria*/

        public virtual IList<JoinEvent> JoinedUsers { get; set; }

        public virtual ICollection<Image> Image { get; set; }
        //public virtual IList<Image> Image { get; set; }
    }
}