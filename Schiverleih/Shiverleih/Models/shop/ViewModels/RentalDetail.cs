using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop.ViewModels
{
    public class RentalDetail
    {
        [Key]
        [Display(Name = "Verleih Nr.")]
        public int RentalID { get; set; }
        [Display(Name = "Kunde")]
        public string CustomerTitle { get; set; }
        [Display(Name = "Produkt")]
        public string ProductTitle { get; set; }
        [Display(Name = "Kategorie")]
        public string CategoryTitle { get; set; }
        [Display(Name = "Verleihpreis pro Tag")]
        public decimal RentalPricePerDay { get; set; }
    }
}