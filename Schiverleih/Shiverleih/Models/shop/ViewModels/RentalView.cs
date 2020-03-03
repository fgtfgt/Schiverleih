using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop.ViewModels
{
    public class RentalView
    {
        [Key]
        [Display(Name = "Verleih Nr.")]
        public int RentalID { get; set; }
        [Display(Name = "Kunde")]
        public string CustomerTitle { get; set; }
        [Display(Name ="Produkt")]
        public string ProductTitle { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
    }
}