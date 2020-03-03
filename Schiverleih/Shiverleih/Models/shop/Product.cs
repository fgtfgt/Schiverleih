using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } //is die Artikel nummer
        [Required]
        [Display(Name = "Artikel Bezeichnung")]
        [MaxLength(90)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Verleihpreis pro Tag")]
        public decimal RentalPricePerDay { get; set; }
        [Required]
        [Display(Name = "Verleih zähler")]
        public int RentCount { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int StatusID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Status Status { get; set; }
        public virtual IEnumerable<CustomerProduct> CustomerProducts { get; set; }
    }
}