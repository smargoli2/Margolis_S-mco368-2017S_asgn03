using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queries;

namespace SalesProfit
{
    class Program
    {
        static void Main(string[] args)
        {
            Sale[] sales = new Sale[5];

            Sale s1 = new Sale("Matzah", "Pesach LLC", 10.99, 1, "1369 E 9th St Lakewood", false);
            Sale s2 = new Sale("Coffee", "Margolis Pesach", 1.99, 7, "1369 E 9th St", true);
            Sale s3 = new Sale("Cookies", "The Company llc", 3.00, 2, "1369 E 9th St Lakewood", false);
            Sale s4 = new Sale("Tea Biscuits", "chometz and Pesach", 60.00, 1, "1369 E 9th St Lakewood", true);
            Sale s5 = new Sale("Coffee", "Yoily Yachevitch", 24.99, 10, "123 Main St", false);

            sales[0] = s1;
            sales[1] = s2;
            sales[2] = s3;
            sales[3] = s4;
            sales[4] = s5;

            Console.WriteLine("\nTotal Profit for coffee items: $" + TotalProfit(sales,
                (Sale sale) => sale.Item == "Coffee",
                (Sale sale) => (sale.PricePerItem * sale.Quantity) * .80,
                (Sale sale, double d) => Console.WriteLine("Coffee item for " + sale.Customer + ", total profit $" + d),
                (Sale sale) => Console.WriteLine("Non-coffee item, skipping.")) + "\n");//last action should do nothing in this case


            Console.WriteLine("\nTotal profit for expedited sales where quantity is greater than 1: $" +
                TotalProfit(sales, (Sale sale) => sale.Quantity > 1,
                (Sale sale) => ((sale.PricePerItem * sale.Quantity) + (sale.ExpeditedShipping == true ? 20.00 : 0.0)),
                (Sale sale, double d) => Console.WriteLine("Expedited shipping sale of " + sale.Item + " - Extra $20.00 profit $" + d),
                (Sale sale) => System.IO.File.WriteAllText(@"SingleOrder" + sale.Customer + ".txt", "Single order item " + sale.Item)) + "\n");

            Console.WriteLine("\nTotal profits for all Pesach orders delivered to Lakewood: $"
                + TotalProfit(sales, (Sale sale) => sale.Customer.Contains("Pesach") && sale.Address.Contains("Lakewood"),
                (Sale sale) => (sale.PricePerItem * sale.Quantity),
                (Sale sale, double d) => Console.WriteLine("Pesach order: " + sale.ToString()),
                (Sale sale) => System.IO.File.WriteAllText(@"Non Pesach order " + sale.Customer + ".txt", "Non pesach item: " + sale.Item)));

            Console.ReadKey();
        }
        public static double TotalProfit(Sale[] sales, Func<Sale, bool> isIn, Func<Sale, double> totalSale,
            Action<Sale, double> doToSale, Action<Sale> doToNotIncluded)
        {
            double total = 0.0;
            foreach (Sale s in sales)
            {
                if (isIn(s))//it's included
                {
                    double profit = totalSale(s);
                    doToSale(s, profit);
                    total += profit;
                }
                else
                {
                    // += totalSale(s);
                    doToNotIncluded(s);
                }

            }
            return total;
        }
    }
}
