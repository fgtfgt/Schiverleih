using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop
{
    public class Status
    {   
        [Key]
        public int StatusID { get; set; }
        [Required]
        public int Rented { get; set; }
        [Required]
        public int Available { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}