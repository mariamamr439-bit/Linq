using LINQ.DataSources;
using LINQ.Models;
namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1
            var seafoodProducts = Source.ProductList.Where(p => p.Category == "Seafood");
            foreach (var product in seafoodProducts)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q2
            var productNames = Source.ProductList.Select(p => p.ProductName);
            foreach (var name in productNames)
            {
                Console.WriteLine(name);
            }
            //Q3
            var sortedProductsUnit = Source.ProductList.OrderBy(p => p.UnitPrice);
            foreach (var product in sortedProductsUnit)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q4
            var productsInRange = Source.ProductList.Where(p => p.UnitPrice > 10 && p.UnitPrice < 30);
            foreach (var product in productsInRange)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q5
            var productsCond = Source.ProductList.Where(p => p.UnitsInStock > 0 && p.Category == "Condiments");
            foreach (var product in productsCond)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q6
            var productstype = Source.ProductList.Select(p => new
            {
                Name = p.ProductName,
                Price = p.UnitPrice,
                StockStatus = p.UnitsInStock > 0 ? "Available" : "Out of Stock"
            });
            foreach(var product in productstype)
            {
                Console.WriteLine($"{product.Name} - {product.Price} - {product.StockStatus}");
            }
            //Q7
            var productsPos = Source.ProductList.Select((p, index) => new
            {
                Position = index + 1,
                Name = p.ProductName,
            });
            foreach (var product in productsPos)
            {
                Console.WriteLine($"{product.Position} - {product.Name}");
            }
            //Q8
            var sortedProductsCat = Source.ProductList.OrderBy(p => p.Category).ThenByDescending(pc => pc.UnitPrice);
            foreach (var product in sortedProductsCat)
            {
                Console.WriteLine($"{product.Category} - {product.ProductName} - {product.UnitPrice}");
            }
            //Q9
            var sortedProductsUntDesc = Source.ProductList.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitsInStock);
            foreach (var product in sortedProductsUntDesc)
            {
                Console.WriteLine($"{product.ProductName} : {product.UnitsInStock}");
            }
            //Q10
            var result = from c in Source.CustomerList
                         from o in c.Orders
                         where o.OrderDate.Year >= 1997
                         select new
                         {
                             c.CustomerID,
                             o.OrderDate
                         };
            foreach (var order in result)
            {
                Console.WriteLine($"{order.CustomerID} - {order.OrderDate}");
            }
            //Q11 same as Q7
            //Q12

            string[] Arr = { "aPPLE", "bAnaNa", "Cherry", "daTe", "eldeRberry" };
            var sortedArray = Arr.OrderBy(word => word.Length)
                .ThenBy(word => word, StringComparer.OrdinalIgnoreCase);
            foreach (var word in sortedArray)
            {
                Console.WriteLine(word);
            }
            //Q13
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var resultDigits = digits.Where(d => d.Length>1 && d[1] =='i').Reverse();
            foreach (var digit in resultDigits)
            {
                Console.WriteLine(digit);
            }
        }
    }
}
