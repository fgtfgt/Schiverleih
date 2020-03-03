using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shiverleih.Models.shop
{
    //hier wird festgehalten wer derzeit einen Artikel ausgeliehen hat
    public class CustomerProduct
    {
        [Key]
        public int CustomerProductID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}