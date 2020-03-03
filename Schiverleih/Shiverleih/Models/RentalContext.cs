using Shiverleih.Models.shop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shiverleih.Models
{
    public class RentalContext : ApplicationDbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CustomerProduct> CustomerProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
    }
}