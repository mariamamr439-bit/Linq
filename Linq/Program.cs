using LINQ.DataSources;
using LINQ.Models;
using System.Linq.Expressions;
namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Linq1
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
            var productsInRange = Source.ProductList.Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);
            foreach (var product in productsInRange)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q5
            var productsCond = Source.ProductList.Where(p => p.UnitsInStock > 0 && p.Category == "Spices & Condiments");
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

            string[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr","cHeRry" };
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
            #endregion
            #region Linq2
            //Q1
            var expensiveProducts = Source.ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q2
            var page2 = Source.ProductList.Skip(5).Take(5);
            foreach (var product in page2)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q3
            var productLessThan25 = Source.ProductList.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
            foreach (var product in productLessThan25)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }
            //Q4
            var seafoodInStock = Source.ProductList.Where(p => p.Category == "Seafood").All(p => p.UnitsInStock > 0);
            Console.WriteLine(seafoodInStock);
            //Q5
            int[] ids = { 3, 9, 13, 18 };
            var containNine = ids.Contains(9);
            Console.WriteLine(containNine);
            //Q6
            var groupProductsByCategory1 = Source.ProductList.GroupBy(p => p.Category).Select(g => new
            {
                Category = g.Key,
                Count = g.Count()
            });
            foreach (var group in groupProductsByCategory1)
            {
                Console.WriteLine($"{group.Category} - {group.Count}");
            }
            //Q7
            var groupProductsByCategory2 = Source.ProductList.GroupBy(p => p.Category).Select(g => new
            {
                Category = g.Key,
                Products = g.Select(p => p.ProductName)
            });
            foreach (var group in groupProductsByCategory2)
            {
                Console.WriteLine($"{group.Category} - {string.Join(", ", group.Products)}");
            }
            //Q8
            var CatHasMoreThan3Products = Source.ProductList.GroupBy(p => p.Category)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key);
            foreach (var category in CatHasMoreThan3Products)
            {
                Console.WriteLine(category);
            }
            //Q9
            var cusByCountry = from c in Source.CustomerList
                               group c by c.Country into countryGroup
                               select new
                               {
                                   Country = countryGroup.Key,
                                   Count = countryGroup.Count(),
                                   TotalOrderValue = countryGroup.Sum(c => c.Orders.Sum(o => o.Total))
                               };
            foreach (var category in cusByCountry)
            {
                Console.WriteLine($"{category.Country} : {category.Count} : {category.TotalOrderValue}");
            }
            //Q10
            var totalUnits = Source.ProductList.Sum(P => P.UnitsInStock);
            Console.WriteLine(totalUnits);
            //Q11
            var minProduct = Source.ProductList.Min(p => p.UnitPrice);
            var maxProduct = Source.ProductList.Max(p => p.UnitPrice);
            Console.WriteLine($"Minimum Price + ${minProduct}: Maximum Price+ ${maxProduct}");
            //Q12
            var listOfCategories = Source.ProductList.Select(p => p.Category).Distinct();
            foreach (var category in listOfCategories)
            {
                Console.WriteLine(category);
            }
            //Q13
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };
            var aExceptB = setA.Except(setB);
            foreach (var element in aExceptB)
            {
                Console.WriteLine(element);
            }
            //Q14
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var countries = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }
            //Q15
            var dictionary = Source.ProductList.ToDictionary(p => p.ProductID);
            var product18 = dictionary[18];
            Console.WriteLine(product18);
            //Q16
            var firstPrice = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 50);
            Console.WriteLine(firstPrice);
            //Q17
            var firstpro = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 500);
            Console.WriteLine(firstpro);
            //Q18
            var multiplyOf7 = Enumerable.Range(1, 12).Select(i => i * 7);
            foreach (var item in multiplyOf7)
            {
                Console.WriteLine(item);
            }
            //Q19
            var evenNum = Enumerable.Range(1, 30).Where(n => n % 2 == 0);
            //Q20
            //var con = Source.ProductList.Take(3).Select(p => p.ProductName)
            //    .Concat(Source.CustomerList.Take(3).Select(c => c.CompanyName));
            //foreach (var item in con)
            //{
            //    Console.WriteLine(item);
            //}
            //Q21
            //var resulttt = Source.ProductList.Zip(Source.CustomerList, (p, c) => $"{p.ProductName} Sold To {c.CompanyName}");
            //foreach (var item in resulttt)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion
        }
    }
}
