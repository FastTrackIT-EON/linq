using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LinqExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonsDatabase.ReadFromXmlUsingXmlReader("Persons.xml");
            ProductsDatabase.ReadFromXml("CategoriesWithProducts.xml");

            // Example_1_MyFirstQuery();
            // Example_2_Where_WithPersons();
            // Example_3_OfType_WithPersons();
            // Example_3_OfType_WithObjects();

            Example_5_Select_ProjectDateOfBirths();

            //foreach (Person p in PersonsDatabase.Persons)
            //{
            //    p.Print();
            //}

            //PersonsDatabase.SaveToXmlUsingXmlWriter("PersonsXmlWriter.xml");

            Console.WriteLine();
            Console.WriteLine("-------------");
            Console.WriteLine();

            

            //foreach (Category category in ProductsDatabase.Categories)
            //{
            //    category.Print();
            //}

            //foreach (Product product in ProductsDatabase.Products)
            //{
            //    product.Print();
            //}

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

        private static void Example_2_Where_WithPersons()
        {
            //var query = from p in PersonsDatabase.Persons
            //            where p.Age > 14 && p.FirstName.StartsWith("D", StringComparison.OrdinalIgnoreCase)
            //            select p;

            var query = PersonsDatabase
                .Persons
                .Where((p, index) => p.Age > 14 &&
                                     p.FirstName.StartsWith("D", StringComparison.OrdinalIgnoreCase));

            foreach (Person p in query)
            {
                p.Print();
            }
        }

        private static void Example_3_OfType_WithPersons()
        {
            // need to have elements of different types
            // so we use inheritance 
            PersonsDatabase.Persons.Add(new Student(
                "StudentFirstName",
                "StudentLastName",
                new DateTime(1990, 5, 10),
                Gender.Female,
                "University of Cluj"));

            var query = PersonsDatabase.Persons.OfType<Student>();

            foreach (var student in query)
            {
                student.Print();
            }
        }

        private static void Example_3_OfType_WithObjects()
        {
            List<object> elements = new List<object>
            {
                1,
                2,
                "Test",
                "Test123",
                new Person("FirstName", "LastName", new DateTime(1991, 5, 10), Gender.Male),
                true
            };

            // var query = elements.OfType<int>().Where(nr => nr % 2 == 0);
            var query = elements.OfType<DateTime>()
                                .Where(d => d.Year == 2024)
                                .Select(d => d.Year.ToString());

            foreach (var result in query)
            {
                Console.WriteLine(result);
                // result.Print();
            }
        }

        private static void Example_4_OfType_WithDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("ProductName", typeof(string)));
            table.Columns.Add(new DataColumn("Price", typeof(decimal)));

            table.Rows.Add(1, "Product 1", 3.5M);
            table.Rows.Add(2, "Product 2", 5M);
            table.Rows.Add(3, "Product 3", 5.5M);

            var query = table.AsEnumerable()
                .Select(row => row["ProductName"])
                .OfType<string>()
                .Where(p => p.EndsWith("2", StringComparison.OrdinalIgnoreCase));

            foreach (var result in query)
            {
                Console.WriteLine(result);
            }
        }


        private static void Example_5_Select_ProjectDateOfBirths()
        {
         
            int nrDeOrdine = 0;
            var query = from p in PersonsDatabase.Persons
                        where p.Age > 18
                        select new
                        {
                            p.DateOfBirth,
                            NrDeOrdine = ++nrDeOrdine
                        };


            /*
            int nrDeOrdine = 0;
            var query = PersonsDatabase.Persons
                .Where(p => p.Age > 18)
                .Select((p, index) => new
                {
                    DateOfBirth = p.DateOfBirth,
                    NrDeOrdine = ++nrDeOrdine
                });
            */
            foreach (var result in query)
            {
                Console.WriteLine($"{result.NrDeOrdine} - {result.DateOfBirth:yyyy-MM-dd}");
            }
        }

    }
}
