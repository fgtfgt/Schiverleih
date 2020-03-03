using Shiverleih.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Shiverleih.Models.shop;
using Shiverleih.Models.shop.ViewModels;

namespace Shiverleih.Repositories
{
    /* Dieses Repo deckt die funktionen ab die im RentalController gebraucht werden
     * Desweiteren enthält es auch einiger Helper Funktionen um die hauptfunktionen zu ermöglichen/Lesbarer zu machen
     */
    public class RentalRepo
    {
        protected readonly RentalContext db = new RentalContext();

        //RentalID: gibt die id des verleih eintrags ein, das angezeigt werden soll
        // gibt ein Viemodel zurück das die Essentiellen Daten eines Verleihs enthält
        public RentalView getRental(int RentalID)
        {
            return (from rental in db.CustomerProducts.Include(r => r.Customer).Include(r => r.Product)
                    where rental.CustomerProductID == RentalID
                    select new RentalView {
                        RentalID = RentalID,
                        CustomerTitle = rental.Customer.FName + " " + rental.Customer.LName,
                        ProductTitle = rental.Product.Title,
                        CustomerID = rental.CustomerID,
                        ProductID = rental.ProductID
                    }).FirstOrDefault();
        }

        //Gibt alle Verleihungen zurück in form des RentlView Viewmodels
        public IEnumerable<RentalView> getRentals()
        {
            return (from rental in db.CustomerProducts.Include(r => r.Customer).Include(r => r.Product)
                    select new RentalView
                    {
                        RentalID = rental.CustomerProductID,
                        CustomerTitle = rental.Customer.FName + " " + rental.Customer.LName,
                        ProductTitle = rental.Product.Title,
                        CustomerID = rental.CustomerID,
                        ProductID = rental.ProductID
                    }).ToList();
        }

        public RentalDetail getRentalDetail(int RentalID)
        {
            return (from rental in db.CustomerProducts.Include(r => r.Customer).Include(r => r.Product).Include(r => r.Product.Category)
                    where rental.CustomerProductID == RentalID
                    select new RentalDetail
                    {
                        RentalID = RentalID,
                        CustomerTitle = rental.Customer.FName + " " + rental.Customer.LName,
                        ProductTitle = rental.Product.Title,
                        CategoryTitle = rental.Product.Category.Title,
                        RentalPricePerDay = rental.Product.RentalPricePerDay
                    }).FirstOrDefault();
        }

        //gibt eine liste aller Angelegten Kunden zurück
        public IEnumerable<Customer> getCustomers()
        {
            return db.Customers.ToList();
        }

        //gibt eine liste aller angelegten Produkte zurück
        public IEnumerable<Product> getProducts()
        {
            return db.Products.ToList();
        }

        /*ProductID: Die ID des Produkts das ausgeliehen werden soll
         *CustomerID: Die ID des Kunden Der das Produkt ausleiehn möchte
         * 
         * Prüft ob ein produkt noch auf lager is und erstellt dann den verleih eintrag, falls noch Produkte auflager sind
         * 
         * Rückgabewert: int, gibt an wieviel Reihen in der Datenbank angelegt/Editiert wurden, anhand davon kann geprüft werden ob die operation erfolgreich war
        */
        public int RentProduct(int ProductID, int CustomerID)
        {
            //wenn nix mehr da is, kann auch nichts verliehen werden
            if (getNumOfAvailableProducts(ProductID) == 0)
            {
                return 0;
            }

            var customer = db.Customers.Find(CustomerID);
            var product = (from p in db.Products.Include(p => p.Status)
                          where p.ProductID == ProductID
                          select p).FirstOrDefault();

            rent(ref product);

            var customerProduct = new CustomerProduct
                {
                    Customer = customer,
                    Product = product
                };

            db.Entry(product).State = EntityState.Modified;

            db.CustomerProducts.Add(customerProduct);
            return db.SaveChanges();
        }

        /* RentalID: Die ID des Verleih eintrags, der Editiert werden soll
         * NewCustomerID: Die ID des Kunden, der fortan als Kunde in diesem Verleiheintrag gesetzt werden soll
         * NewProductID: Die ID des Produkts, das fortan als Produkt in diesem Verleiheintrag gesetzt werden soll
         * 
         * Setzt die neuen ID's des Kunden oder Produkt ODER beides für den Gewählten verleih eintrag
         * Wenn ein neues Produkt gewählt wurde, wird zeitgleich auch geprüft ob dieses auch noch auf lager ist
         * 
         * Rückgabewert: int, gibt an wieviel Reihen in der Datenbank angelegt/Editiert wurden, anhand davon kann geprüft werden ob die operation erfolgreich war
         */
        public int EditRental(int RentalID ,int NewCustomerID, int NewProductID)
        {
            var rental = db.CustomerProducts.Find(RentalID);
            //wenn nix mehr da is, kann auch nichts verliehen werden
            if (rental.ProductID != NewProductID && getNumOfAvailableProducts(NewProductID) == 0)
            {
                return 0;
            }

            if (rental.ProductID != NewProductID)
            {
                var oldProduct = getProductByID(rental.ProductID);
                var newProduct = getProductByID(NewProductID);
                revertRent(ref oldProduct);
                rent(ref newProduct);
                db.Entry(oldProduct).State = EntityState.Modified;
                db.Entry(newProduct).State = EntityState.Modified;

                rental.ProductID = NewProductID;
            }
           
            rental.CustomerID = NewCustomerID;

            db.Entry(rental).State = EntityState.Modified;
            return db.SaveChanges();
        }

        /* RentalID: Die ID, des Verleih Eintrags dergelöscht werden soll 
         */
        public int DeleteRental(int RentalID)
        {
            var rental = db.CustomerProducts.Find(RentalID);
            var product = getProductByID(rental.ProductID);
            revertRent(ref product);

            db.CustomerProducts.Remove(rental);
            return db.SaveChanges();
        }

        /* RentalID: Die Verleih ID, von dem das Produkt wieder zuückgebracht wurde
         * Löscht den Verleiheintrag und setzt die verfügbar/verliehen variabeln wieder auf die entsprechenden werte
         */
        public int ReturnProduct(int RentalID)
        {
            var rental = db.CustomerProducts.Find(RentalID);
            var product = getProductByID(rental.ProductID);
            returnProduct(ref product);

            db.Entry(product).State = EntityState.Modified;
            db.CustomerProducts.Remove(rental);
            return db.SaveChanges();
        }

        //helper Funktionen die verschiedene kleine aufgaben lösen um die Lesbarkeit des Codes zu verbessern
        private int getNumOfAvailableProducts(int productID)
        {
            return (from p in db.Products.Include(p => p.Status)
                    where p.ProductID == productID
                    select p.Status.Available).FirstOrDefault();
        }

        private Product getProductByID(int productID)
        {
           return (from p in db.Products.Include(p => p.Status)
             where p.ProductID == productID
             select p).FirstOrDefault();
        }

        private void rent(ref Product product)
        {
            product.RentCount += 1;
            product.Status.Available -= 1;
            product.Status.Rented += 1;
        }

        private void revertRent(ref Product product)
        {
            product.RentCount -= 1;
            product.Status.Available += 1;
            product.Status.Rented -= 1;
        }

        private void returnProduct(ref Product product)
        {
            product.Status.Available += 1;
            product.Status.Rented -= 1;
        }
    }
}
