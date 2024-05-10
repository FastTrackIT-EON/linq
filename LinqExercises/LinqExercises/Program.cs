using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Example_1_MyFirstQuery();

            PersonsDatabase.ReadFromXmlUsingXmlReader("Persons.xml");

            foreach (Person p in PersonsDatabase.Persons)
            {
                p.Print();
            }

            PersonsDatabase.SaveToXmlUsingXmlWriter("PersonsXmlWriter.xml");

            Console.WriteLine();
            Console.WriteLine("-------------");
            Console.WriteLine();

            ProductsDatabase.ReadFromXml("CategoriesWithProducts.xml");

            foreach (Category category in ProductsDatabase.Categories)
            {
                category.Print();
            }

            foreach (Product product in ProductsDatabase.Products)
            {
                product.Print();
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static void Example_1_MyFirstQuery()
        {
            // SQL-like syntax
            /*
            IEnumerable<int> query = from nr in Generators.GenerateAllNumbers()
                        where nr % 2 == 0
                        select nr;
            */

            var query = Generators.GenerateAllNumbers()
                                  .Where(nr => nr % 2 == 0)
                                  .Select(nr => nr);

            int count = 0;
            int maxCount = 5;
            foreach (int nr in query)
            {
                if (count < maxCount)
                {
                    Console.WriteLine(nr);
                    count++;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
