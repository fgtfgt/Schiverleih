using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Display(Name = "Kategorie")]
        [MaxLength(90)]
        public string Title { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}