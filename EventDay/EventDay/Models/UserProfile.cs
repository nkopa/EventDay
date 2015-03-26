using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventDay.Models
{
    public class UserProfile
    {
        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        //[Key]
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        /*osobiste/organizacja*/
        [Required]
        [Display(Name = "Typ konta")]
        public string AccountTypeId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Płeć")]
        public string SexId { get; set; }

        [Display(Name = "Urodziny")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM'/'dd'/'yyyy}")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Zainteresowania")]
        public string Interest { get; set; }

        [Display(Name = "Województwo")]
        public string Voivodeship { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Powiadamiaj o eventach smsem")]
        public string SmsNotification { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [ScaffoldColumn(false)]
        public string ProfileImage { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreateTime { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdateTime { get; set; }

        /*
        [ScaffoldColumn(false)]
        public string Password { get; set; }
        */

        [ScaffoldColumn(false)]
        public string StatusId { get; set; }

        /*
        [ScaffoldColumn(false)]
        public string PrivilageId { get; set; }
        */
    }
}