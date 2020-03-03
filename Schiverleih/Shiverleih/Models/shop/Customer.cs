using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        [Display(Name = "Vorname")]
        [MaxLength(90)]
        public string FName { get; set; }
        [Required]
        [Display(Name = "Nachname")]
        [MaxLength(90)]
        public string LName { get; set; }
        //aus zeitgründen nicht in eigene Tabelle ausgelagert
        [Required]
        [Display(Name = "Addresse")]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Tel. Nr.")]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Geburtstag")]
        public DateTime Birthday { get; set; }

        public virtual IEnumerable<CustomerProduct> CustomerProducts { get; set; }
    }
}