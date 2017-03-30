using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class Sale
    {
        public Sale(String item, String customer, double price, int quantity, String add, bool exp)
        {
            this.Item = item;
            this.Customer = customer;
            this.PricePerItem = price;
            this.Quantity = quantity;
            this.Address = add;
            this.ExpeditedShipping = exp;
        }
        public String Item { get; set; }
        public String Customer { get; set; }
        public double PricePerItem { get; set; }
        public int Quantity { get; set; }
        public String Address { get; set; }
        public bool ExpeditedShipping { get; set; }

        public override string ToString()
        {
            return "Item: " + Item + " Customer: " + Customer
                + " Price: " + PricePerItem + " Quantity: " + Quantity
                + " Address: " + Address + " Expedited Shipping: " + ExpeditedShipping.ToString();
        }
    }


}
