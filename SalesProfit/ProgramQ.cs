using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static Sale[] sales = new Sale[5];
        static void Main(string[] args)
        {
            Sale s1 = new Sale("Matzah", "Pesach LLC", 10.99, 1, "1369 E 9th St", false);
            Sale s2 = new Sale("Milk", "Me", 1.99, 7, "1369 E 9th St", true);
            Sale s3 = new Sale("Cookies", "The Company llc", 3.00, 2, "1369 E 9th St", false);
            Sale s4 = new Sale("Tea Biscuits", "Sam", 60.00, 1, "1369 E 9th St", true);
            Sale s5 = new Sale("Tea", "Yoily Yachevitch", 24.99, 10, "123 Main St", false);

            sales[0] = s1;
            sales[1] = s2;
            sales[2] = s3;
            sales[3] = s4;
            sales[4] = s5;

            //1.
            Console.WriteLine("Items over $10.00:\n" + Query1());

            //2.
            Console.WriteLine("Sales where quantity is 1 in descending order of price:\n" + Query2());

            //3.
            Console.WriteLine("Tea items with regular shipping:\n" + Query3());

            //4.
            Console.WriteLine("Addresses of sales over $100.00:\n" + Query4());

            //5.
            Console.WriteLine("Sales of LLCs ordered by total price:\n" + Query5());
            Console.ReadKey();
        }

        public static String Query1()
        {
            var filtered =
             from s in sales
             where s.PricePerItem > 10.0
             select s;
            var filt2 = sales.Where(s => s.PricePerItem > 10.0);
            StringBuilder print = new StringBuilder();
            foreach (var val in filtered)
            {
                print.Append(val.ToString() + "\n");
            }
            print.Append("Extension method:\n");
            foreach (var val in filt2)
            {
                print.Append(val.ToString() + "\n");
            }
            return print.ToString();
        }

        public static String Query2()
        {
            var filtered =
                from s in sales
                where s.Quantity == 1
                orderby s.PricePerItem descending
                select s;
            var filt2 = sales.Where(s => s.Quantity == 1).OrderByDescending(s => s.PricePerItem);
            StringBuilder print = new StringBuilder();
            foreach (var val in filtered)
            {
                print.Append(val.ToString() + "\n");
            }
            print.Append("Extension method:\n");
            foreach (var val in filt2)
            {
                print.Append(val.ToString() + "\n");
            }
            return print.ToString();
        }

        public static String Query3()
        {
            var filtered =
                from s in sales
                where s.Item == "Tea" && s.ExpeditedShipping == false
                select s;
            var filt2 = sales.Where(s => s.Item == "Tea" && s.ExpeditedShipping == false);
            StringBuilder print = new StringBuilder();
            foreach (var val in filtered)
            {
                print.Append(val.ToString() + "\n");
            }
            print.Append("Extension method:\n");
            foreach (var val in filt2)
            {
                print.Append(val.ToString() + "\n");
            }
            return print.ToString();
        }

        public static String Query4()
        {
            var filtered =
                from s in sales
                where (s.PricePerItem * s.Quantity) > 100.00
                select s.Address;
            var filt2 = sales.Where(s => (s.PricePerItem * s.Quantity) > 100.00).Select(s => s.Address);
            StringBuilder print = new StringBuilder();
            foreach (var val in filtered)
            {
                print.Append(val.ToString() + "\n");
            }
            print.Append("Extension method:\n");
            foreach (var val in filt2)
            {
                print.Append(val.ToString() + "\n");
            }
            return print.ToString();
        }

        public static string Query5()
        {
            var filtered =
                from s in sales
                where s.Customer.Contains("llc") || s.Customer.Contains("LLC")
                orderby (s.PricePerItem * s.Quantity)
                select new
                {
                    s.Item,
                    TotalPrice = (s.PricePerItem * s.Quantity),
                    Shipping = s.Address + (s.ExpeditedShipping ? "Expedite" : "")

                };
            var filt2 = sales.Where(s => s.Customer.Contains("llc") ||
            s.Customer.Contains("LLC")).Select(s => new
            {
                s.Item,
                totalPrice = (s.PricePerItem * s.Quantity),
                shipping = s.Address + (s.ExpeditedShipping ? "Expedite" : "")
            }).OrderBy(s => s.totalPrice);
            StringBuilder print = new StringBuilder();
            foreach (var val in filtered)
            {
                print.Append(val.ToString() + "\n");
            }
            print.Append("Extension method:\n");
            foreach (var val in filt2)
            {
                print.Append(val.ToString() + "\n");
            }
            return print.ToString();
        }

    }
}
